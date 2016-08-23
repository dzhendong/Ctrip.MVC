using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ctrip.Test.Session
{
    /// <summary>
    /// redis缓存客户端调用类
    /// </summary>
    internal class RedisClient<T> : IClient<T> where T : new()
    {
        #region Write

        private string GetEntryId()
        {
            return typeof(T).FullName;
        }

        /// <summary>
        /// 移除指定的记录
        /// </summary>
        /// <param name="key">需要移除的主键,一般为对象ID值</param>
        /// <returns></returns>
        public bool HRemove(string key)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                string hashId = GetEntryId();

                return client.RemoveEntryFromHash(hashId, key.ToString());
            };

            return TryRedisWrite<bool>(fun);
        }

        /// <summary>
        /// 存储对象到缓存中
        /// </summary>
        /// <param name="key">需要写入的主键,一般为对象ID值,必须是文本/数字等对象</param>
        /// <param name="value">对象</param>
        public bool HSet(string key, T value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                string hashId = GetEntryId();
                string ser = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                return client.SetEntryInHash(hashId, key.ToString(), ser);
            };

            return TryRedisWrite<bool>(fun);
        }

        /// <summary>
        /// 延期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="expiresTime"></param>
        public void KSetEntryIn(string key, TimeSpan expiresTime)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.ExpireEntryIn(key, expiresTime);
                return false;
            };

            TryRedisWrite(fun);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireIn"></param>
        /// <returns></returns>
        public bool KSet(string key, T value, TimeSpan expireIn)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.SetEntry(key, Newtonsoft.Json.JsonConvert.SerializeObject(value), expireIn);
                return true;
            };

            return TryRedisWrite(fun);
        }

        /// <summary>
        /// 写入key/value值
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireIn"></param>
        public void KSet(string key, string value, TimeSpan expireIn)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                if (expireIn == System.TimeSpan.MinValue)
                {
                    client.SetEntry(key, value);
                }
                else
                {
                    client.SetEntry(key, value, expireIn);
                }
                return false;
            };

            TryRedisWrite(fun);
        }

        /// <summary>
        /// 以Key/Value的形式存储对象到缓存中
        /// </summary>
        /// <typeparam name="T">对象类别</typeparam>
        /// <param name="value">要写入的集合</param>
        public void KSet(Dictionary<string, T> value)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                client.SetAll<T>(value);
                return true;
            };

            TryRedisWrite(fun);
        }

        /// <summary>
        /// 移除Key/Value的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KRemove(string key)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                return client.Remove(key);
            };

            return TryRedisWrite(fun);
        }

        #endregion

        #region Read

        /// <summary>
        /// 根据ID获取指定对象
        /// </summary>
        /// <param name="key">需要获取的主键,一般为对象ID值</param>
        /// <returns></returns>
        public T HGet(string key)
        {
            if (key == null)
            {
                return default(T);
            }

            Func<IRedisClient, T> fun = (IRedisClient client) =>
            {
                string ser = "";
                string hashId = GetEntryId();
                ser = client.GetValueFromHash(hashId, key.ToString());

                return ser == null ? new T() : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(ser);
            };

            return TryRedisRead(fun);
        }

        /// <summary>
        /// 读取Key/Value值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KIsExist(string key)
        {
            Func<IRedisClient, bool> fun = (IRedisClient client) =>
            {
                string ser = "";
                ser = client.GetValue(key.ToString());

                return string.IsNullOrEmpty(ser) == false;
            };

            return TryRedisRead(fun);
        }

        /// <summary>
        /// 读取Key/Value值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T KGet(string key)
        {
            Func<IRedisClient, T> fun = (IRedisClient client) =>
            {
                string ser = "";
                ser = client.GetValue(key.ToString());
                if (string.IsNullOrEmpty(ser) == false)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(ser);
                }
                else
                {
                    return default(T);
                }
            };
            return TryRedisRead(fun);
        }

        /// <summary>
        /// 读取Key/Value值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public Dictionary<string, T> KGet(IList<string> keys)
        {
            Func<IRedisClient, Dictionary<string, T>> fun = (IRedisClient client) =>
             {
                 return (Dictionary<string, T>)client.GetAll<T>(keys);
             };

            return TryRedisRead(fun);
        }

        /// <summary>
        /// 读取Key/Value值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IList<T> KGetList(IList<string> keys)
        {
            Dictionary<string, T> dics = KGet(keys);
            return dics.Values.ToList();
        }

        /// <summary>
        /// 返回根据条件查找到的KEY对象列表        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public IList<string> KSearchKeys(string pattern)
        {
            Func<IRedisClient, IList<string>> fun = (IRedisClient client) =>
            {
                return client.SearchKeys(pattern);
            };

            return TryRedisRead(fun);
        }

        /// <summary>
        /// 返回根据条件查找到的value对象列表        
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public IList<T> KSearchValues(string pattern)
        {
            Func<IRedisClient, IList<T>> fun = (IRedisClient client) =>
            {
                IList<string> keys = new List<string>();

                //先查找KEY
                keys = client.SearchKeys(pattern);

                if (keys != null && keys.Count > 0)
                {
                    //再直接根据key返回对象列表
                    Dictionary<string, T> dics = (Dictionary<string, T>)client.GetAll<T>(keys);

                    return dics.Values.ToList<T>();
                }
                else
                {
                    return new List<T>();
                }

            };

            return TryRedisRead(fun);
        }

        #endregion

        #region TryRedis
        /// <summary>
        /// 读取
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <param name="doRead"></param>
        /// <returns></returns>
        public F TryRedisRead<F>(Func<IRedisClient, F> doRead)
        {
            PooledRedisClientManager prcm = new RedisProvider().GetClientManagers();
            IRedisClient client = null;
            try
            {
                using (client = prcm.GetReadOnlyClient())
                {
                    return doRead(client);
                }
            }
            catch (RedisException ex)
            {
                return default(F);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <typeparam name="F"></typeparam>
        /// <param name="doWrite"></param>
        /// <returns></returns>
        public F TryRedisWrite<F>(Func<IRedisClient, F> doWrite)
        {
            PooledRedisClientManager prcm = new RedisProvider().GetClientManagers();
            IRedisClient client = null;
            try
            {
                using (client = prcm.GetClient())
                {
                    return doWrite(client);
                }
            }
            catch (RedisException)
            {
                throw new Exception("Redis写入异常.Host:" + client.Host + ",Port:" + client.Port);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
        }
        #endregion
    }
}