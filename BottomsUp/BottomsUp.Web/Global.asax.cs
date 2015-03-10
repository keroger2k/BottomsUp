using BottomsUp.Core.Data;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BottomsUp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var ignored = MiniProfiler.Settings.IgnoredPaths.ToList();

            ignored.Add("WebResource.axd");
            ignored.Add("ScriptResource.axd");
            ignored.Add("/Content/");
            ignored.Add("/__browserLink/");
            ignored.Add("/fonts/");
            ignored.Add("/Scripts/");
            ignored.Add(".js");

            MiniProfiler.Settings.IgnoredPaths = ignored.ToArray();


            MiniProfilerEF6.Initialize();

        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }
}
