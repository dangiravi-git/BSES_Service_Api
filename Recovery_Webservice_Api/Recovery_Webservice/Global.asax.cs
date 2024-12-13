using System.Web;
using System.Web.Http;
using Serilog;

namespace Recovery_Webservice
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Server.MapPath("~/Logs/RecoveryApi-.txt"), rollingInterval: RollingInterval.Day) 
            .CreateLogger();

            GlobalConfiguration.Configure(WebApiConfig.Register);

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
