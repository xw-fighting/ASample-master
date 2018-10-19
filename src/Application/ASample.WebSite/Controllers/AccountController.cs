using ASample.WebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASample.Identity.Domain.Models.AggregateRoots;
using ASample.Identity.Application.Manager;
using ASample.Identity.Application;

namespace ASample.WebSite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
        {
           
        }

        public AccountController(ASampleUserManager userManager, ASampleSignInManager signInManager)
        {
            ASampleUserManager = userManager;
            ASampleSignInManager = signInManager;
        }

        private ASampleSignInManager _signInManager;
        private ASampleUserManager _userManager;

        public ASampleSignInManager ASampleSignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ASampleSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ASampleUserManager ASampleUserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ASampleUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = await ASampleUserManager.FindAsync(model.UserName, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "无效的用户名或密码");
                }
                else
                {
                    var claimsIdentity = await ASampleUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    //claimsIdentity.AddClaims(LocationClaimsProvider.GetClaims(claimsIdentity));
                    //claimsIdentity.AddClaims(ClaimsRoles.CreateRolesFromClaims(claimsIdentity));
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claimsIdentity);
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            //var user2 = HttpContext.User;
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        //POST: /Account/Register
       [HttpPost]
       [AllowAnonymous]
       [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Name, PasswordHash = model.Password };
                var result = await ASampleUserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await ASampleSignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // 有关如何启用帐户确认和密码重置的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=320771
                    // 发送包含此链接的电子邮件
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "确认你的帐户", "请通过单击 <a href=\"" + callbackUrl + "\">這裏</a>来确认你的帐户");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        private ASampleUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ASampleUserManager>(); }
        }

        private IAuthenticationManager AuthManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }
}