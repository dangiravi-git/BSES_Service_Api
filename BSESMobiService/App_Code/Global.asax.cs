using System;
using System.Web;
using Serilog;

namespace USEPMS
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            // Configure Serilog for logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console() // Optional: Logs to console during debugging
                .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "Logs/BSESMOBAPI-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Application has started.");
        }

        protected void Application_End()
        {
            Log.Information("Application is shutting down.");
            Log.CloseAndFlush();
        }

        protected void Application_BeginRequest()
        {
            try
            {
                var request = HttpContext.Current.Request;
                Log.Information("Received {Method} request to {Url} from {RemoteIpAddress}. Headers: {Headers}",
                    request.HttpMethod, request.Url, request.UserHostAddress, request.Headers);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error logging request information.");
            }
        }

        protected void Application_EndRequest()
        {
            try
            {
                var response = HttpContext.Current.Response;
                Log.Information("Sent response with status code {StatusCode}.", response.StatusCode);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error logging response information.");
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Log.Error(ex, "An unhandled exception occurred.");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Log.Information("Session started.");
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Log.Information("Session ended.");
        }
    }
}
