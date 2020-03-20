using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNLiCore.Model;
using DNLiCore.Service;
using DNLiCore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DNLiCore.Frame.Controllers
{
    public class APIController : Controller
    {
        public UserService _userService;
        public APIController(UserService userService)
        {
            _userService = userService;
        }

        #region 登录
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        public JsonResult Login()
        {
            return null;
        }
        #endregion

        #region 注册
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        public JsonResult Register([FromBody] t_user clientModel)
        {
            //参数过滤
            if (string.IsNullOrEmpty(clientModel.userAccount))
            {
                return Json(Rsp.Fail("账号不能为空", -1));
            }
            if (string.IsNullOrEmpty(clientModel.userPwd))
            {
                return Json(Rsp.Fail("密码不能为空", -2));
            }
            var userModel = _userService.GetUserInfo(clientModel.userAccount);
            if (userModel != null)
            {
                return Json(Rsp.Fail("账号已存在", -3));
            }
            else
            {
                //密码加密
                clientModel.userPwd = EncryptHelper.AESEncrypt(clientModel.userPwd, "DNLiCore");
                int resultInt = _userService.AddUser(clientModel);
                return Json(Rsp.Success(resultInt.ToString()));
            }
        }
        #endregion
    }
}