using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MobilePhoneDistributor_Web.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(name:"API Default",routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional });
        }
    }
}