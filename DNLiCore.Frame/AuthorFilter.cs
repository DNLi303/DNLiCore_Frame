using DNLiCore.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;


namespace DNLiCore.Frame
{
    public class AuthorFilter : IAuthorizationFilter
    {
        private IMemoryCache _cache;
        public AuthorFilter(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (IsHaveAllow(context.Filters))
            {
                return;
            }
            //需要登录权限的,先从header中拿
            string Authorization = context.HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(Authorization))
            {
                //验证token是否正常

                var cacheAuthor = _cache.Get(Authorization);
                if (cacheAuthor != null)
                {
                    //刷新当前token时间
                    _cache.Set(Authorization, cacheAuthor, new MemoryCacheEntryOptions
                    {
                        SlidingExpiration = TimeSpan.FromHours(3)
                    });
                }
                else
                {
                    //已经过期了
                    var jsonModel = Rsp.Fail("已过期", -999);
                    context.Result = new JsonResult(jsonModel);

                }
            }
            else
            {
                // 返回未登录的信息               
                var jsonModel = Rsp.Fail("未登录", -999);
                context.Result = new JsonResult(jsonModel);
            }

        }


        /// <summary>
        /// 判断是否不需要权限
        /// </summary>
        /// <param name="filers"></param>
        /// <returns></returns>
        public static bool IsHaveAllow(IList<IFilterMetadata> filers)
        {
            for (int i = 0; i < filers.Count; i++)
            {
                if (filers[i] is IAllowAnonymousFilter)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsAjax(HttpRequest req)
        {
            bool result = false;

            var xreq = req.Headers.ContainsKey("x-requested-with");
            if (xreq)
            {
                result = req.Headers["x-requested-with"] == "XMLHttpRequest";
            }

            return result;
        }

    }
}
