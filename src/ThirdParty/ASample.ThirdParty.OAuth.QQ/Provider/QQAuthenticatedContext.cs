using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Microsoft.Owin.Security;
using System.Globalization;

namespace ASample.ThirdParty.OAuth.QQ
{
    public class QQAuthenticatedContext : BaseContext
    {
        /// <summary>
        /// Gets the JSON-serialized user
        /// </summary>
        public JObject User { get; set; }

        public string AccessToken { get; set; }

        public TimeSpan? ExpiresIn { get; set; }

        public ClaimsIdentity Identity { get; set; }

        public AuthenticationProperties Properties { get; set; }

        public string OpenId { get; set; }

        public string NickName { get; set; }

        public string FigureUrl { get; set; }

        public string FigureUrl_1 { get; set; }

        public string FigureUrl_2 { get; set; }

        public string FigureUrl_QQ_1 { get; set; }

        public string FigureUrl_QQ_2 { get; set; }

        public string Gender { get; set; }

        public string Is_Yellow_Vip { get; set; }

        public string IsVip { get; set; }

        public string Yellow_Vip_Level { get; set; }

        public string Level { get; set; }

        public string Is_Yellow_Year_Vip { get; set; }



        public QQAuthenticatedContext(IOwinContext context,string openId,JObject userInfo,string accessToken,string refreshToken,string expires) : base(context)
        {
            if (string.IsNullOrWhiteSpace(openId))
            {
                throw new ArgumentNullException("openId 为空");
            }

            this.User = userInfo ?? throw new ArgumentNullException("user 获取失败");
            this.AccessToken = accessToken;

            int num ;
            if(int.TryParse(expires,NumberStyles.Integer,CultureInfo.InvariantCulture,out num))
            {
                this.ExpiresIn = new TimeSpan?(TimeSpan.FromSeconds((double)num));
            }

            this.OpenId = openId;
            this.NickName = GetSafeValue("nickname", userInfo);
            this.FigureUrl = GetSafeValue("figureurl", userInfo);
            this.FigureUrl_1 = GetSafeValue("figureurl_1", userInfo);
            this.FigureUrl_2 = GetSafeValue("figureurl_2", userInfo);
            this.FigureUrl_QQ_1 = GetSafeValue("figureurl_qq_1", userInfo);
            this.FigureUrl_QQ_2 = GetSafeValue("figureurl_qq_2", userInfo);
            this.Gender = GetSafeValue("gender", userInfo);
            this.Is_Yellow_Vip = GetSafeValue("is_yellow_vip", userInfo);
            this.IsVip = GetSafeValue("vip", userInfo);
            this.Yellow_Vip_Level = GetSafeValue("yellow_vip_level", userInfo);
            this.Level = GetSafeValue("level", userInfo);
            this.Is_Yellow_Year_Vip = GetSafeValue("is_yellow_year_vip", userInfo);
        }

        private string GetSafeValue(string name, IDictionary<string, JToken> dictionary)
        {
            if(!dictionary.ContainsKey(name))
            {
                return null;
            }
            return dictionary[name].ToString();
        }
    }
}
