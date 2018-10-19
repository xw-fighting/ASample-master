using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASample.WebSite.Models.RabbitMqChat
{
    public class DataLayer
    {
        private static List<UserModel> userList = new List<UserModel>();
        static  DataLayer()
        {
            userList.Add(new UserModel()
            {
                Id = Guid.NewGuid(),
                Email = "1627901679@qq.com",
                Password = "123"
            });
            userList.Add(new UserModel()
            {
                Id = Guid.NewGuid(),
                Email = "542235197@qq.com",
                Password = "123"
            });
            userList.Add(new UserModel()
            {
                Id = Guid.NewGuid(),
                Email = "396985437@qq.com",
                Password = "123"
            });

        }

        public void Register(UserModel userModel)
        {
            userList.Add(userModel);
        }

        public UserModel Login(string email,string password)
        {
            var result = userList.FirstOrDefault(c => c.Email == email && c.Password == password);
            return result;
        }

        public List<UserModel> GetUserList()
        {
            var result = userList.ToList();
            return result;
        }

    }
}