using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Configuration.Internal;
using System.Web.Http;

namespace SWAT.Client.WebClient
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
