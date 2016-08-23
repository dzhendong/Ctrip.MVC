
namespace Ctrip.Model
{
    public interface IUserBusiness
    {
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        RolesM GetRoles(string userName);

        /// <summary>
        /// 根据用户名和密码获取用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User GetByNameAndPassword(string name, string password);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="createPersistentCookie"></param>
        void SignIn(string userName, bool createPersistentCookie);

        /// <summary>
        /// 注销
        /// </summary>
        void SignOut();
    }

}