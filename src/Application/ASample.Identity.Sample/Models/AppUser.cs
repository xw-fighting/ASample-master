﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASample.Identity.Sample.Models
{
    public class AppUser : IdentityUser
    {
        public string Country { get; set; }
    }
}