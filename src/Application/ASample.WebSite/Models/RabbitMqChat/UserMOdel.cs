using System;

namespace ASample.WebSite.Models.RabbitMqChat
{
    public class UserModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}