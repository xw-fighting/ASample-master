using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using ASample.Identity.EntityFramework.Models.AggregateRoots;
using System.Security.Claims;
using ASample.Identity.EntityFramework;

namespace ASample.Web.Identity.Manager
{
    public class ASampleUserManager : UserManager<IdentityUser,Guid>
    {
        public ASampleUserManager(ASampleUserStore store) : base(store)
        {

        }

        public static ASampleUserManager Create(
               IdentityFactoryOptions<ASampleUserManager> options,
               IOwinContext context)
        {

            ASampleIdentityDbContext db = context.Get<ASampleIdentityDbContext>();
            //UserStore<T> 是 包含在 Microsoft.AspNet.Identity.EntityFramework 中，它实现了 UserManger 类中与用户操作相关的方法。 
            //也就是说UserStore<T>类中的方法（诸如：FindById、FindByNameAsync...）通过EntityFramework检索和持久化UserInfo到数据库中
            ASampleUserManager manager = new ASampleUserManager(new ASampleUserStore());

            //自定义的User Validator
            //manager.UserValidator = new CustomUserValidator(manager)
            //{
            //    AllowOnlyAlphanumericUserNames = false,
            //    RequireUniqueEmail = true
            //};

            //自定义的Password Validator
            //manager.PasswordValidator = new CustomPasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = false,
            //    RequireDigit = false,
            //    RequireLowercase = true,
            //    RequireUppercase = true
            //};
            return manager;
        }
    }
}
