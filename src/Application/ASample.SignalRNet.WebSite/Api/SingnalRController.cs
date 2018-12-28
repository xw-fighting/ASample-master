using ASample.SignalRNet.WebSite.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ASample.SignalRNet.WebSite.Api
{
    public class SingnalRController : ApiController
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<UserModel> GetUserList()
        {
            var list = new List<UserModel>()
            {
                new UserModel()
                {
                    Name = "xiaowei",
                    Birthday = "1992-02-24",
                    Sex = "男",
                },
                new UserModel()
                {
                    Name = "test1",
                    Birthday = "1992-02-24",
                    Sex = "男",
                },
                new UserModel()
                {
                    Name = "test2",
                    Birthday = "1992-02-24",
                    Sex = "男",
                },
                new UserModel()
                {
                    Name = "test3",
                    Birthday = "1992-02-24",
                    Sex = "男",
                },
            };
            return list;
        }

        
    }
}
