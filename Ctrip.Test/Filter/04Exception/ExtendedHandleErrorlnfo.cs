using System;
using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// ExtendedHandleErrorInfo 继承自HandleErrorInfo 
    /// 它只额外定义了一个表示错误消息的ErrorMessage 属性
    /// </summary>
    public class ExtendedHandleErrorlnfo:HandleErrorInfo
    {
        public string ErrorMessage { get; private set; }

        public ExtendedHandleErrorlnfo(Exception exception,string controllerName , string actionName, string errorMessage)
        : base(exception,controllerName,actionName)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}