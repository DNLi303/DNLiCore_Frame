using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNLiCore.Utility
{
    public class Rsp
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static baseReturnModel Success(string msg="success",object result=null)
        {
            baseReturnModel model = new baseReturnModel()
            {
                code=1,
                msg=msg,
                result=result,
                time=DateTime.Now
            };
            return model;
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static baseReturnModel Fail( string msg = "fail", int code = -1, object result = null)
        {
            baseReturnModel model = new baseReturnModel()
            {
                code = code,
                msg = msg,
                result = result,
                time = DateTime.Now
            };
            return model;
        }


        public class baseReturnModel
        {
            public int code { get; set; } = 0;
            public string msg { get; set; } = "";
            public Object result { get; set; }
            public DateTime time { get; set; } = DateTime.Now;
        }

    }
}
