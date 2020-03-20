using DNLiCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNLiCore.Service
{

    public class UserService
    {
        public IFreeSql _freesql;

        public UserService(IFreeSql freesql)
        {
            _freesql = freesql;
        }

        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="t_User"></param>
        /// <returns></returns>
        public int AddUser(t_user t_User)
        {
            t_User.createTime = DateTime.Now;
            t_User.updateTime = DateTime.Now;
            return _freesql.Insert(t_User).ExecuteAffrows();
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="t_User"></param>
        /// <returns></returns>
        public int UpdateUser(t_user t_User)
        {            
            t_User.updateTime = DateTime.Now;
            return _freesql.Update<t_user>(t_User).ExecuteAffrows();
        }

        public t_user GetUserInfo(int ID)
        {
            return _freesql.Select<t_user>().Where(s => s.ID == ID).First();
        }

        public t_user GetUserInfo(string userAccount)
        {
            return _freesql.Select<t_user>().Where(s => s.userAccount==userAccount).First();
        }
       
    }
}
