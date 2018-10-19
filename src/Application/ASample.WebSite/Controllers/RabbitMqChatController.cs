using ASample.ThirdParty.RabbitMq;
using ASample.WebSite.Models.RabbitMqChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ASample.WebSite.Controllers
{
    public class RabbitMqChatController : Controller
    {
        //静态数据
        DataLayer dl = new DataLayer();
        // GET: RabbitMqChat

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["userid"] == null)
            {
                return RedirectToAction("login");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// 登录展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {
            string email = userModel.Email;
            string password = userModel.Password;
            UserModel user = dl.Login(email, password);
            if (user!=null)
            {
                ViewData["status"] = 1;
                ViewData["msg"] = "login Successful...";
                Session["username"] = user.Email;
                Session["userid"] = user.Id.ToString();
                return RedirectToAction("Index");
            }
            else
            {

                ViewData["status"] = 2;
                ViewData["msg"] = "invalid Email or Password...";
                return View();
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="friend"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SendMsg(string message, string friend)
        {
            RabbitMqService obj = new RabbitMqService();
            bool flag = obj.SendMessage(message, friend);
            return Json(null);
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Receive()
        {
            try
            {
                RabbitMqService obj = new RabbitMqService();
                string userqueue = Session["username"].ToString();
                string message = obj.ReceiveMessage(userqueue);
                return Json(message);
            }
            catch (Exception)
            {
                return null;
            }


        }

        /// <summary>
        /// 获取朋友列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFriendList()
        {
            var id = Session["userid"].ToString();
            List<UserModel> users = dl.GetUserList();
            List<ListItem> userlist = new List<ListItem>();
            foreach (var item in users)
            {
                userlist.Add(new ListItem
                {
                    Value = item.Email,
                    Text = item.Email

                });
            }
            return Json(userlist);
        }
    }
}