using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASample.Identity.Sample
{
    public class FilterConfig
    {
        //It is good practice to make your appliation secure by default and then whitelist the controllers/actions that allow anonymous access. To do this we'll add the built in  AuthorizeAttribute as a global filter.
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}