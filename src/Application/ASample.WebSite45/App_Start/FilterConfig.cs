﻿using System.Web;
using System.Web.Mvc;

namespace ASample.WebSite45
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
