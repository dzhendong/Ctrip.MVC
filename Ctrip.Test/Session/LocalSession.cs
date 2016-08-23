
using System;
using System.Collections.Generic;
using System.Web;

namespace Ctrip.Test.Session
{
    /// <summary>
    /// 本地缓存
    /// </summary>
    public class LocalSession : SessionBase, ISession
    {
        #region Ctor
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_context"></param>
        public LocalSession(HttpContextBase _context)
            : base(_context)
        {
        }
        #endregion

        #region Write-1
        /// <summary>
        /// 登录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void KSet<T>(T obj) where T : new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public object KGet<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 用户是否在线
        /// </summary>
        /// <returns></returns>
        public bool KIsExist()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 退出
        /// </summary>
        public void KRemove()
        {
            throw new NotImplementedException();
        }

        public void RemoveAll()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 续期
        /// </summary>
        public void Postpone()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Read-1

        /// <summary>
        /// 用户列表数量
        /// </summary>
        /// <returns></returns>
        public int KSearchKeys()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 注销某个用户
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool KRemove<T>(string sessionId) where T : new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public T KGet<T>(string sessionId) where T : new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 在线用户对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> KSearchValues<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 在线用户SessionId列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<string> KSearchKeys<T>() where T : new()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region this
        public object this[string name]
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public object this[int index]
        {
            get
            {
                return null;
            }
            set
            {

            }
        }
        #endregion
    }
}
