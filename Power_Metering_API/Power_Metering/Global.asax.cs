using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Serilog;
namespace Power_Metering
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.File(Server.MapPath("~/Logs/PowerMeteringApi-.txt"), rollingInterval: RollingInterval.Day)
           .CreateLogger();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_End()
        {
            Log.CloseAndFlush();
        }
        protected void Application_BeginRequest()
        {
            var request = HttpContext.Current.Request;
            Log.Information("Received {Method} request to {Url} from {RemoteIpAddress}. Headers: {Headers}",
                request.HttpMethod, request.Url, request.UserHostAddress, request.Headers);
        }
        protected void Application_EndRequest()
        {
            var response = HttpContext.Current.Response;
            Log.Information("Sent response with status code {StatusCode}.", response.StatusCode);
        }
    }
}
