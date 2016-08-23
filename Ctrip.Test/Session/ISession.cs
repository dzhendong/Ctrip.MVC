using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ctrip.Test.Session
{
    public interface ISession
    {
        #region Write-1
        /// <summary>
        /// 登录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        void KSet<T>(T obj) where T : new();

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        object KGet<T>() where T : new();

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        bool KIsExist();

        void KRemove();


        void RemoveAll();

        void Clear();

        /// <summary>
        /// 续期
        /// </summary>
        void Postpone();
        #endregion

        #region Read-1

        /// <summary>
        /// 用户列表数量
        /// </summary>
        /// <returns></returns>
        int KSearchKeys();

        /// <summary>
        /// 注销某个用户
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        bool KRemove<T>(string sessionId) where T : new();

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        T KGet<T>(string sessionId) where T : new();

        /// <summary>
        /// 在线用户对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> KSearchValues<T>() where T : new();

        /// <summary>
        /// 在线用户SessionId列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<string> KSearchKeys<T>() where T : new();
        #endregion
    }
}