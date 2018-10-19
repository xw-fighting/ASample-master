
using ASample.Unity;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace ASample.WebSite
{
    public partial class Startup
    {
        public static void UnityConfig(IAppBuilder app)
        {
            var container = BuildUnityContainer();
            app.Use(container);
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            UnityService.Init();
            var container = UnityService.Current;
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            
            return container;
        }
    }
}