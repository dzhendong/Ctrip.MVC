using System;
using System.Collections.Generic;
using System.Web;

namespace Ctrip.Test.Session
{
    /// <summary>
    /// 用户状态管理
    /// </summary>
    public class RedisSession : SessionBase, ISession
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_context"></param>
        public RedisSession(HttpContextBase _context)
            : base(_context)
        {
        }

        #region Write-1
        /// <summary>
        /// 登录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void KSet<T>(T obj) where T : new()
        {
            new RedisClient<T>().KSet(SessionId, obj, new TimeSpan(0, RedisProvider.TimeOut, 0));
        }

        /// <summary>
        /// 用户是否在线
        /// </summary>
        /// <returns></returns>
        public bool KIsExist()
        {
            return new RedisClient<object>().KIsExist(SessionId);
        }

        /// <summary>
        /// 退出
        /// </summary>
        public void KRemove()
        {
            new RedisClient<object>().KRemove(SessionId);
        }

        public void RemoveAll()
        {
            new RedisClient<object>().KRemove(SessionId);
        }

        public void Clear()
        {
            new RedisClient<object>().KRemove(SessionId);
        }

        /// <summary>
        /// 续期
        /// </summary>
        public void Postpone()
        {
            new RedisClient<object>().KSetEntryIn(SessionId, new TimeSpan(0, RedisProvider.TimeOut, 0));
        }
        #endregion

        #region Read-1

        /// <summary>
        /// 用户列表数量
        /// </summary>
        /// <returns></returns>
        public int KSearchKeys()
        {
            return new RedisClient<object>().KSearchKeys(RedisSession.SessionName + "*").Count;
        }

        /// <summary>
        /// 注销某个用户
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public bool KRemove<T>(string sessionId) where T : new()
        {
            return new RedisClient<T>().KRemove(sessionId);
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public object KGet<T>() where T : new()
        {
            return new RedisClient<T>().KGet(SessionId);
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public T KGet<T>(string sessionId) where T : new()
        {
            return new RedisClient<T>().KGet(sessionId);
        }

        /// <summary>
        /// 在线用户对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> KSearchValues<T>() where T : new()
        {
            return new RedisClient<T>().KSearchValues(RedisSession.SessionName + "*");
        }

        /// <summary>
        /// 在线用户SessionId列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<string> KSearchKeys<T>() where T : new()
        {
            return new RedisClient<T>().KSearchKeys(RedisSession.SessionName + "*");
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

