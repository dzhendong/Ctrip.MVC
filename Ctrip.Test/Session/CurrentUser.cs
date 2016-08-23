using System;
using System.Web;

namespace Ctrip.Test.Session
{
    #region Enum

    /// <summary>
    /// ������
    /// </summary>
    public enum AgentCategoryEnum
    {
        /// <summary>
        /// �ÿ�
        /// </summary>
        Guest = 0,

        /// <summary>
        /// ������
        /// </summary>
        Travel = 1,

        /// <summary>
        /// ��Ӧ��
        /// </summary>
        Supplier = 5,

        /// <summary>
        /// �ؽ�
        /// </summary>
        Grafting = 6,

        /// <summary>
        /// ��������
        /// </summary>
        Others = 99
    }

    #endregion

    /// <summary>
    /// ��ȡ��ǰ��¼�û�������
    /// </summary>
    public static class CurrentUser
    {
        #region Extension
       
        public static RedisSession SessionExt()
        {
            HttpContext context = HttpContext.Current;
            HttpContextWrapper contextWrapper = new HttpContextWrapper(context);
            return new RedisSession(contextWrapper);
        }

        #endregion

        #region Delegate

        public delegate void SetCurrentUserEvent();

        public static event SetCurrentUserEvent OnSetCurrentUser;

        #endregion

        #region Field

        /// <summary>
        /// ��ǰ�û���ID
        /// �����ǰ�û�û�е�¼������0
        /// </summary>
        public static long UserId
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return 0;
                }
                if (SessionExt() == null)
                {
                    return 0;
                }
                return Convert.ToInt64(SessionExt()["CurrentUser_UserId"]);
            }
            set { SessionExt()["CurrentUser_UserId"] = value; }
        }

        /// <summary>
        /// �ʺ�����
        /// </summary>
        public static string UserCode
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }
                if (SessionExt() == null)
                {
                    return "";
                }
                return Convert.ToString(SessionExt()["CurrentUser_UserCode"]);
            }
            set { SessionExt()["CurrentUser_UserCode"] = value; }
        }

        /// <summary>
        /// ��ʵ����
        /// </summary>
        public static string LoginName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }
                if (SessionExt() == null)
                {
                    return "";
                }
                return Convert.ToString(SessionExt()["CurrentUser_LoginName"]);
            }
            set { SessionExt()["CurrentUser_LoginName"] = value; }
        }

        public static string Email
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }
                if (SessionExt() == null)
                {
                    return "";
                }
                return Convert.ToString(SessionExt()["CurrentUser_Email"]);
            }
            set { SessionExt()["CurrentUser_Email"] = value; }
        } 

        /// <summary>
        /// �ָ��ǳ�
        /// </summary>
        public static string UserName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }
                if (SessionExt() == null)
                {
                    return "";
                }
                return Convert.ToString(SessionExt()["CurrentUser_UserName"]);
            }
            set { SessionExt()["CurrentUser_UserName"] = value; }
        }

        /// <summary>
        /// �ֻ�����
        /// </summary>
        public static string Mobile
        {
            get { return Convert.ToString(SessionExt()["CurrentUser_Mobile"]); }
            set { SessionExt()["CurrentUser_Mobile"] = value; }
        }

        /// <summary>
        /// �Ƿ���ֻ���
        /// </summary>
        public static bool IsBindMobile
        {
            get { return Convert.ToBoolean(SessionExt()["CurrentUser_IsBindMobile"]); }
            set { SessionExt()["CurrentUser_IsBindMobile"] = value; }
        }

        /// <summary>
        /// ���û����ð�����
        /// </summary>
        public static bool BindReminder
        {
            get { return Convert.ToBoolean(SessionExt()["CurrentUser_BindReminder"]); }
            set { SessionExt()["CurrentUser_BindReminder"] = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public static AgentCategoryEnum AgentCategory
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return AgentCategoryEnum.Others;
                }
                if (SessionExt() == null)
                {
                    return AgentCategoryEnum.Others;
                }
                int a = Convert.ToInt32(SessionExt()["CurrentUser_Agent"]);
                AgentCategoryEnum agent = (AgentCategoryEnum) a;
                return agent;
            }
            set
            {
                int b = (int) value;
                SessionExt()["CurrentUser_Agent"] = b;
            }
        }

        public static int Category
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return 0;
                }
                if (SessionExt() == null)
                {
                    return 0;
                }
                return Convert.ToInt32(SessionExt()["CurrentUser_Agent"]);
            }
        }

        /// <summary>
        /// �û����ͣ�0���˺ţ�1���˻�
        /// </summary>
        public static int AccountType
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return 0;
                }
                if (SessionExt() == null)
                {
                    return 0;
                }
                return Convert.ToInt32(SessionExt()["CurrentUser_AccountType"]);
            }
            set { SessionExt()["CurrentUser_AccountType"] = value; }
        }

        /// <summary>
        /// �ŵ�ID
        /// </summary>
        public static long StoreId
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return 0;
                }
                if (SessionExt() == null)
                {
                    return 0;
                }
                return Convert.ToInt64(SessionExt()["CurrentUser_StoreId"]);
            }
            set { SessionExt()["CurrentUser_StoreId"] = value; }
        }

        /// <summary>
        /// �ŵ����
        /// </summary>
        public static string StoreCode
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }
                if (SessionExt() == null)
                {
                    return "";
                }
                return Convert.ToString(SessionExt()["CurrentUser_StoreCode"]);
            }
            set { SessionExt()["CurrentUser_StoreCode"] = value; }
        }

        /// <summary>
        /// �ŵ�����
        /// </summary>
        public static string StoreName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }
                if (SessionExt() == null)
                {
                    return "";
                }
                return Convert.ToString(SessionExt()["CurrentUser_StoreName"]);
            }
            set { SessionExt()["CurrentUser_StoreName"] = value; }
        }

        /// <summary>
        /// �ֻ㸶ID
        /// </summary>
        public static long LhfActId
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return 0;
                }
                if (SessionExt() == null)
                {
                    return 0;
                }
                return Convert.ToInt64(SessionExt()["CurrentUser_LhfActId"]);
            }
            set { SessionExt()["CurrentUser_LhfActId"] = value; }
        }

        /// <summary>
        /// ���ص�ǰ�û��Ƿ��¼.
        /// </summary>
        public static bool IsLogin
        {
            get { return (UserId > 0); }
        }

        /// <summary>
        /// BPP��˾Id
        /// </summary>
        public static long CompanyId
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return 0;
                }
                if (SessionExt() == null)
                {
                    return 0;
                }
                return Convert.ToInt64(SessionExt()["CurrentUser_CompanyId"]);
            }
            set { SessionExt()["CurrentUser_CompanyId"] = value; }
        }

        /// <summary>
        /// BPP��˾���
        /// </summary>
        public static String CompanyCode
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }
                if (SessionExt() == null)
                {
                    return "";
                }
                return Convert.ToString(SessionExt()["CurrentUser_CompanyCode"]);
            }
            set { SessionExt()["CurrentUser_CompanyCode"] = value; }
        }

        /// <summary>
        /// BPP��˾���
        /// </summary>
        public static String CompanyName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }
                if (SessionExt() == null)
                {
                    return "";
                }
                return Convert.ToString(SessionExt()["CurrentUser_CompanyName"]);
            }
            set { SessionExt()["CurrentUser_CompanyName"] = value; }
        }

        /// <summary>
        /// bpp��˾ȫ��
        /// </summary>
        public static String CompanyAllName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }
                if (SessionExt() == null)
                {
                    return "";
                }
                return Convert.ToString(SessionExt()["CurrentUser_CompanyAllName"]);
            }
            set { SessionExt()["CurrentUser_CompanyAllName"] = value; }
        }

        /// <summary>
        /// BPP��˾���
        /// </summary>
        public static String CompanyIntro
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return "";
                }
                if (SessionExt() == null)
                {
                    return "";
                }
                return Convert.ToString(SessionExt()["CurrentUser_CompanyIntro"]);
            }
            set { SessionExt()["CurrentUser_CompanyIntro"] = value; }
        }

        /// <summary>
        /// BPP��˾Logo
        /// </summary>
        public static String CompanyImg
        {
            get { return Convert.ToString(SessionExt()["CurrentUser_CompanyImg"]); }
            set { SessionExt()["CurrentUser_CompanyImg"] = value; }
        }

        #endregion

        #region APIs

        /// <summary>
        /// ���õ�ǰ�û���Ϣ.���ڵ�¼ʱ����.
        /// </summary>
        /// <param name="userId">�û���User ID</param>
        public static void SetCurrentUser(Int32 userId)
        {
            if (userId == 0)
            {
                RemoveCurrentUser();
                return;
            }

            if (OnSetCurrentUser != null) OnSetCurrentUser();
        }

        /// <summary>
        /// ��յ�ǰ�û���Ϣ
        /// </summary>
        public static void RemoveCurrentUser()
        {
            SessionExt()["CurrentUser_UserId"] = 0;
            SessionExt()["CurrentUser_UserCode"] = "";
            SessionExt()["CurrentUser_LoginName"] = "";
            SessionExt()["CurrentUser_Email"] = "";
            SessionExt()["CurrentUser_UserName"] = "";
            SessionExt()["CurrentUser_Mobile"] = "";
            SessionExt()["CurrentUser_Agent"] = "";
            SessionExt()["CurrentUser_CompanyId"] = 0;
            SessionExt()["CurrentUser_CompanyCode"] = "";
            SessionExt()["CurrentUser_CompanyName"] = "";
            SessionExt()["CurrentUser_CompanyAllName"] = "";
            SessionExt()["CurrentUser_IsBindMobile"] = "";
            SessionExt()["CurrentUser_BindReminder"] = "";
            SessionExt()["CurrentUser_AccountType"] = "";
            SessionExt()["CurrentUser_StoreId"] = 0;
            SessionExt()["CurrentUser_StoreCode"] = "";
            SessionExt()["CurrentUser_StoreName"] = "";
            SessionExt()["CurrentUser_LhfActId"] = 0;

            if (SessionExt()["account"] != null)
            {
                SessionExt().RemoveAll();
            }
        }

        /// <summary>
        /// ���session��Ϣ.����log outʱ����
        /// </summary>
        public static void ClearSession()
        {
            SessionExt().Clear();
        }

        #endregion
    }
}