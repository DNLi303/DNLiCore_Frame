using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNLiCore.Model
{
    [Table(Name = "t_user")]
    public class t_user
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int ID { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string userAccount { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string userPwd { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updateTime { get; set; }



    }
}
