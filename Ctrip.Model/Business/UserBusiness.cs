using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Ctrip.Model
{
    public class UserBusiness : IUserBusiness
    {
        /// <summary>
        /// 作为模拟的数据集
        /// </summary>
        private static User[] UserList = new[]
        {
            new User{ ID = 1, UserName = "张三", Password = "111111", Roles = new RolesM{ID = 101,Name = "employee"}},
            new User{ ID = 2, UserName = "李四", Password = "111111", Roles = new RolesM{ID = 102,Name = "manager"}},
            new User{ ID = 3, UserName = "admin", Password = "admin", Roles = new RolesM{ID = 103,Name = "admin"}}
        };

        /// <summary>
        /// 根据用户名获取角色
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public RolesM GetRoles(string userName)
        {
            return UserList
                .Where(o => o.UserName == userName)
                .Select(o => o.Roles)
                .FirstOrDefault();
        }

        /// <summary>
        /// 根据用户名和密码获取用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User GetByNameAndPassword(string name, string password)
        {
            return UserList
                .FirstOrDefault(u => u.UserName == name && u.Password == password);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="createPersistentCookie"></param>
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("值不能为 null 或为空。", "userName");

            //FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);

            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
            //       1,
            //       userName,
            //       DateTime.Now,
            //       DateTime.Now.Add(FormsAuthentication.Timeout),
            //       createPersistentCookie,
            //       userName
            //       );

            //HttpCookie cookie = new HttpCookie(
            //    FormsAuthentication.FormsCookieName,
            //    FormsAuthentication.Encrypt(ticket));

            //HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void SignOut()
        {
            //FormsAuthentication.SignOut();
        }
    }
}