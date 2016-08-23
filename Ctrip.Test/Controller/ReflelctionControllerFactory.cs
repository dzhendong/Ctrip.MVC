﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Ctrip.Test
{
    /// <summary>
    /// 创建一个自定义ControllerFactory模拟Controller默认激活机制
    /// </summary>
    public class ReflelctionControllerFactory : IControllerFactory
    {
        //其他成员
        private static List<Type> controllerTypes;
        static ReflelctionControllerFactory()
        {
            controllerTypes = new List<Type>();
            foreach (Assembly assembly in BuildManager.GetReferencedAssemblies())
            {
                controllerTypes.AddRange(assembly.GetTypes().Where(type => typeof(IController).IsAssignableFrom(type)));
            }
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            Type controllerType = this.GetControllerType(requestContext.RouteData, controllerName);
            if (null == controllerType)
            {
                throw new HttpException(404, "No controller found");
            }
            return (IController)Activator.CreateInstance(controllerType);
        }

        private static bool IsNamespaceMatch(string requestedNamespace, string targetNamespace)
        {
            if (!requestedNamespace.EndsWith(".*", StringComparison.OrdinalIgnoreCase))
            {
                return string.Equals(requestedNamespace, targetNamespace, StringComparison.OrdinalIgnoreCase);
            }
            requestedNamespace = requestedNamespace.Substring(0, requestedNamespace.Length - ".*".Length);
            if (!targetNamespace.StartsWith(requestedNamespace, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return ((requestedNamespace.Length == targetNamespace.Length) || (targetNamespace[requestedNamespace.Length] == '.'));
        }

        private Type GetControllerType(IEnumerable<string> namespaces, Type[] controllerTypes)
        {
            var types = (from type in controllerTypes
                         where namespaces.Any(ns => IsNamespaceMatch(ns, type.Namespace))
                         select type).ToArray();
            switch (types.Length)
            {
                case 0: return null;
                case 1: return types[0];
                default: throw new InvalidOperationException("Multiple types were found that match the requested controller name.");
            }
        }

        //其他成员
        protected virtual Type GetControllerType(RouteData routeData, string controllerName)
        {
            //根据类型名称筛选
            var types = controllerTypes.Where(type => string.Compare(controllerName + "Controller", type.Name, true) == 0).ToArray();
            if (types.Length == 0)
            {
                return null;
            }

            //通过路由对象的命名空间进行匹配
            var namespaces = routeData.DataTokens["Namespaces"] as IEnumerable<string>;
            namespaces = namespaces ?? new string[0];
            Type contrllerType = this.GetControllerType(namespaces, types);
            if (null != contrllerType)
            {
                return contrllerType;
            }

            //是否允许采用后备命名空间
            bool useNamespaceFallback = true;
            if (null != routeData.DataTokens["UseNamespaceFallback"])
            {
                useNamespaceFallback = (bool)(routeData.DataTokens["UseNamespaceFallback"]);
            }

            //如果不允许采用后备命名空间，返回Null
            if (!useNamespaceFallback)
            {
                return null;
            }

            //通过当前ControllerBuilder的默认命名空间进行匹配
            contrllerType = this.GetControllerType(ControllerBuilder.Current.DefaultNamespaces, types);
            if (null != contrllerType)
            {
                return contrllerType;
            }

            //如果只存在一个类型名称匹配的Controller，则返回之
            if (types.Length == 1)
            {
                return types[0];
            }

            //如果具有多个类型名称匹配的Controller，则抛出异常
            throw new InvalidOperationException("Multiple types were found that match the requested controller name.");
        }

        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (null != disposable)
            {
                disposable.Dispose();
            }
        }

        //其他成员
        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            Type controllerType = this.GetControllerType(requestContext.RouteData, controllerName);
            if (null == controllerType)
            {
                return SessionStateBehavior.Default;
            }
            SessionStateAttribute attribute = controllerType.GetCustomAttributes(true).OfType<SessionStateAttribute>()
               .FirstOrDefault();
            attribute = attribute ?? new SessionStateAttribute(SessionStateBehavior.Default);
            return attribute.Behavior;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class SessionStateAttribute : Attribute
    {
        private SessionStateBehavior behavior = SessionStateBehavior.Default;

        public SessionStateAttribute(SessionStateBehavior behavior) { }

        public SessionStateBehavior Behavior 
        {
            get 
            {
                return behavior;
            }
        }
    }
}