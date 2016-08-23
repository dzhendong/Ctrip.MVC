using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc.Async;

namespace ILvYou.Btrip.Order.Domain.Wfl
{
    [Serializable]
    public class ClientData
    {
        /// <summary>
        /// 新消息
        /// </summary>
        public const int MsgNewInformation = 1;

        /// <summary>
        /// 消息类型
        /// 传送的数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="data"></param>
        public ClientData(int type, object data)
        {
            this.t = type;
            this.data = data;
        }

        /// <summary>
        /// 消息类型
        /// t=>Type
        /// </summary>
        public int t
        {
            get;
            private set;
        }

        /// <summary>
        /// 传送数据
        /// data=>Data
        /// </summary>
        public object data
        {
            get;
            private set;
        }
    }

    /// <summary>
    /// 连接数据管理类
    /// </summary>
    public class PollingMannger
    {      
        /// <summary>
        /// 发送信息委托
        /// </summary>
        /// <param name="to"></param>
        /// <param name="data"></param>
        public delegate void SendMessage(ClientData data);

        /// <summary>
        /// 连接管理定时器
        /// </summary>
        static Timer ManngerTime;

        public static SendMessage Send = new SendMessage(SendTo);

        /// <summary>
        /// 在线用户集合
        /// Dictionary 多线程出现高CPU问题     
        /// </summary>
        static Hashtable Online { get; set; }

        /// <summary>
        /// 连接自动超时时间
        /// </summary>
        static TimeSpan TimeOut = TimeSpan.FromSeconds(60 * 2.5);

        /// <summary>
        /// 最多连接数
        /// </summary>
        static int MaxConnection = 1000000;

        /// <summary>
        /// 连接ID随机数
        /// </summary>
        static Random radm = new Random(1);

        /// <summary>
        /// 连接对象
        /// </summary>
        /// <param name="connection"></param>
        static void RemoveConnection(PollingMannger connection)
        {
            if (connection == null)
                return;
            Online.Remove(connection.Id);
            PollingMannger.SendTo(new ClientData(ClientData.MsgNewInformation, new { id = connection.Id }));
        }

        /// <summary>
        /// 将一个连接从集合中移除
        /// </summary>
        /// <param name="id">连接唯一标识ID</param>
        public static void RemoveConnection(int id, string userId)
        {
            try
            {
                if (Online.ContainsKey(id))
                {
                    var connection = Online[id] as PollingMannger;
                    RemoveConnection(connection);
                }
            }
            catch (Exception e)
            {
                ///多线程同时操作时有可能会不存在
            }
        }

        /// <summary>
        /// 添加或者激活一个新连接
        /// </summary>
        static public void AddConnection(int? id, AsyncManager asyncMannger)
        {
            if (id.HasValue && Online.ContainsKey(id.Value))
            {
                (Online[id.Value] as PollingMannger).Active(asyncMannger);
                return;
            }
            PollingMannger newConnection = new PollingMannger(asyncMannger);
            ///通知别人我上线了
            PollingMannger.SendTo(new ClientData(ClientData.MsgNewInformation, new { id = newConnection.Id, content = newConnection.Id + "上线了!" }));
        }

        /// <summary>
        /// 异步发送消息
        /// End
        /// </summary>
        /// <param name="asyncResult"></param>
        public static void EndSend(IAsyncResult asyncResult)
        {
            try
            {
                Send.EndInvoke(asyncResult);
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// 给所有人发送信息
        /// </summary>
        /// <param name="data">接收的数据</param>
        static void SendTo(ClientData data)
        {
            PollingMannger[] tempOnlines = new PollingMannger[Online.Values.Count + 10];
            Online.Values.CopyTo(tempOnlines, 0);
            var len = tempOnlines.Length;
            for (int i = 0; i < len; i++)
            {
                try
                {
                    PollingMannger polling = tempOnlines[i];
                    if (polling != null)
                        polling.AddStack(data);
                }
                catch (Exception e)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 异步发送消息
        /// Begin
        /// </summary>
        /// <param name="to"></param>
        /// <param name="data"></param>
        /// <param name="callBack"></param>
        /// <param name="object"></param>
        /// <returns></returns>
        public static IAsyncResult BeginSend(ClientData data, AsyncCallback callBack, object @object)
        {
            try
            {
                return Send.BeginInvoke(data, callBack, @object);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        ///分配一个连接ID
        /// </summary>
        /// <returns></returns>
        static int GetNewId()
        {
            var tempId = radm.Next(MaxConnection);
            while (Online.ContainsKey(tempId))
                tempId = radm.Next(MaxConnection);
            return tempId;
        }

        /// <summary>
        /// 用户是否在线
        /// </summary>
        /// <param name="uid">要判断的用户</param>
        /// <returns></returns>
        public static bool IsOline(string uid)
        {
            PollingMannger[] tempOnlines = new PollingMannger[Online.Values.Count + 10];
            Online.Values.CopyTo(tempOnlines, 0);
            for (int i = 0; i < tempOnlines.Length; i++)
            {
                try
                {
                    var tempOnline = tempOnlines[i];
                    if (tempOnline != null)
                        return true;
                }
                catch (Exception e)
                {
                    break;
                }
            }
            return false;
        }

        /// <summary>
        /// 静态变量初始化
        /// </summary>
        static PollingMannger()
        {
            //Online = new Dictionary<int, PollingMannger>();
            Online = new Hashtable();
            ///连接最大过期时间(也就是超过这个时间就会被清除)
            TimeSpan maxTimeOut = TimeSpan.FromMinutes(5);
            ///每隔5分钟进行一次连接清理
            ManngerTime = new Timer(o =>
            {
                PollingMannger[] pollings = new PollingMannger[Online.Values.Count + 10];
                Online.Values.CopyTo(pollings, 0);
                int len = pollings.Length;
                DateTime currentTime = DateTime.Now;
                for (int i = 0; i < len; i++)
                {
                    try
                    {
                        var tempPolling = pollings[i];
                        if (tempPolling == null)
                            continue;
                        ///移除长时没有用的连接
                        if ((currentTime - tempPolling.LastActiveTime).TotalMinutes > maxTimeOut.TotalMinutes)
                        {
                            RemoveConnection(tempPolling);
                            ///如果这个连接还没响应就先响应掉
                            if (!tempPolling.AsyncMannger.Parameters.ContainsKey("Finish"))
                                tempPolling.AsyncMannger.Finish();
                        }

                    }
                    catch (Exception e)
                    {

                    }
                }
            }, null, maxTimeOut, TimeSpan.FromMinutes(10));
        }

        public PollingMannger(AsyncManager asyncManager)
        {
            this.TaskQueue = new Queue<ClientData>();
            this.LastActiveTime = DateTime.Now;
            this.Id = GetNewId();
            this.AsyncMannger = asyncManager;
            asyncManager.Parameters["id"] = Id;
            ///将自己添加入连接集合
            Online.Add(Id, this);
        }

        /// <summary>
        /// 心跳激活
        /// </summary>
        /// <param name="asyncManager"></param>
        public void Active(AsyncManager asyncManager)
        {
            asyncManager.Parameters["id"] = this.Id;
            this.LastActiveTime = DateTime.Now;
            this.AsyncMannger = asyncManager;
            DequeueTask();
        }

        /// <summary>
        /// 任务队列
        /// </summary>
        Queue<ClientData> TaskQueue { get; set; }

        /// <summary>
        /// 最后激活时间
        /// </summary>
        public DateTime LastActiveTime { get; set; }

        /// <summary>
        /// 连接唯一编号 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 当前连接的上下文
        /// </summary>
        public AsyncManager AsyncMannger
        {
            get { return asyncMannger; }
            set
            {
                var mySession = this;
                asyncMannger = value;
                var tempMannger = value;
                Timer tempTime = null;
                tempTime = new Timer(o =>
                {
                    if (!tempMannger.Parameters.ContainsKey("Finish"))
                        tempMannger.Finish();
                }, null, TimeOut, TimeSpan.FromSeconds(0));
                tempMannger.Parameters["time"] = tempTime;
            }
        }

        AsyncManager asyncMannger;

        /// <summary>
        /// 添加一要运送的数据
        /// 注意:要可序列化
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="data">要传送的数据</param>
        void AddStack(ClientData clientData)
        {
            this.TaskQueue.Enqueue(clientData);
            if (!this.asyncMannger.Parameters.ContainsKey("Finish"))
                DequeueTask();
        }

        /// <summary>
        ///完成队列中的任务
        /// </summary>
        void DequeueTask()
        {
            if (this.TaskQueue.Count > 0)
            {
                List<ClientData> datas = new List<ClientData>();
                while (this.TaskQueue.Count > 0)
                    datas.Add(this.TaskQueue.Dequeue());
                this.asyncMannger.Parameters["Datas"] = datas;
                this.asyncMannger.Finish();
            }
        }
    }
}
