using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASample.Identity.Web.Test.Models
{
    // 可以通过将更多属性添加到 ApplicationUser 类来为用户添加配置文件数据，请访问 https://go.microsoft.com/fwlink/?LinkID=317594 了解详细信息。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
            return userIdentity;
        }

        public string Address { get;set; }

        public string City { get; set; }
    }

    public class ASampleWebIdentityTestContext : IdentityDbContext<ApplicationUser>
    {
        public ASampleWebIdentityTestContext()
            : base("ASampleWebIdentityTestContext", throwIfV1Schema: false)
        {
        }

        public static ASampleWebIdentityTestContext Create()
        {
            return new ASampleWebIdentityTestContext();
        }
    }
}