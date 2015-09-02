using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Newtonsoft.Json;

namespace MyMoney.Domain.Application.WebServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            // Web API configuration and services
            var jSettings = new JsonSerializerSettings();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{oid}",
            //    defaults: new { oid = RouteParameter.Optional }
            //);
            //config.Routes.MapHttpRoute(
            //               name: "AdminApi",
            //               routeTemplate: "api/{controller}/{id}"
            //           );

            //Account Routes
            config.Routes.MapHttpRoute("account-get", "accounts/{oid}",
                new { controller = "Accounts", action = "Get", logging = false },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("account-list", "accounts",
                new { controller = "Accounts", action = "Get", logging = false },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("account-create", "accounts",
                new { controller = "Accounts", action = "Post", logging = true },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("account-update", "accounts/{oid}",
                new { controller = "Accounts", action = "Put", logging = true },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });

            config.Routes.MapHttpRoute("account-delete", "accounts/{oid}",
                new { controller = "Accounts", action = "Delete", logging = true },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) });

            //Admin Routes
            config.Routes.MapHttpRoute("admin-getConfiguration", "admin/{configurationId}",
                new { controller = "Admin", action = "GetConfiguration", logging = false },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("admin-updateConfiguration", "admin/{configurationId}",
                new { controller = "Admin", action = "UpdateConfiguration", logging = true },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });
        }
    }
}