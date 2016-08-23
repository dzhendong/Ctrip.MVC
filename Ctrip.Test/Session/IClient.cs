using System;
using System.Collections.Generic;

namespace Ctrip.Test.Session
{
    public interface IClient<T>
    {
        #region Write
        /// <summary>
        /// 移除指定的记录
        /// </summary>
        /// <param name="key">需要移除的主键,一般为对象ID值</param>
        /// <returns></returns>
        bool HRemove(string key);

        /// <summary>
        /// 存储对象到缓存中
        /// </summary>
        /// <param name="key">需要写入的主键,一般为对象ID值,必须是文本/数字等对象</param>
        /// <param name="value">对象</param>
        bool HSet(string key, T value);

        /// <summary>
        /// 延期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiresTime"></param>
        void KSetEntryIn(string key, TimeSpan expiresTime);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireIn"></param>
        /// <returns></returns>
        bool KSet(string key, T value, TimeSpan expireIn);

        /// <summary>
        /// 写入key/value值
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireIn"></param>
        void KSet(string key, string value, TimeSpan expireIn);

        /// <summary>
        /// 以Key/Value的形式存储对象到缓存中
        /// </summary>
        /// <typeparam name="T">对象类别</typeparam>
        /// <param name="value">要写入的集合</param>
        void KSet(Dictionary<string, T> value);

        /// <summary>
        /// 移除Key/Value的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KRemove(string key);
        #endregion

        #region Read

        /// <summary>
        /// 根据ID获取指定对象
        /// </summary>
        /// <param name="key">需要获取的主键,一般为对象ID值</param>
        /// <returns></returns>
        T HGet(string key);

        /// <summary>
        /// 读取Key/Value值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        bool KIsExist(string key);

        /// <summary>
        /// 读取Key/Value值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T KGet(string key);

        /// <summary>
        /// 读取Key/Value值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        Dictionary<string, T> KGet(IList<string> keys);

        /// <summary>
        /// 读取Key/Value值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        IList<T> KGetList(IList<string> keys);

        /// <summary>
        /// 返回根据条件查找到的KEY对象列表        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pattern"></param>
        /// <returns></returns>
        IList<string> KSearchKeys(string pattern);

        /// <summary>
        /// 返回根据条件查找到的value对象列表        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pattern"></param>
        /// <returns></returns>
        IList<T> KSearchValues(string pattern);

        #endregion
    }
}
