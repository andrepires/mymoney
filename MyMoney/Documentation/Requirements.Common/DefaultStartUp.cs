using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Newtonsoft.Json;
using Owin;

namespace MyMoney.Documentation.Requirements.Common
{
    public class DefaultStartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            var jSettings = new JsonSerializerSettings();
            config.Formatters.JsonFormatter.SerializerSettings = jSettings;

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{oid}",
            //    defaults: new { oid = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "AdminApi",
            //    routeTemplate: "api/{controller}/{id}"
            //);
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
            config.Routes.MapHttpRoute("admin-getRepositoryType", "admin/{configurationId}",
                new { controller = "Admin", action = "GetRepositoryType", logging = false },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("admin-updateRepositoryType", "admin/{configurationId}",
                new { controller = "Admin", action = "UpdateRepositoryType", logging = true },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });

            appBuilder.UseWebApi(config);
        }
    }
}