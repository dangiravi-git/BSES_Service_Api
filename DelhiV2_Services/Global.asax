        <%@ Application Language="C#" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
    //public void Init(HttpApplication context)
    //{
    //    context.PreSendRequestHeaders += OnPreSendRequestHeaders;
    //}

    public void Dispose()
    {
    }
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "*");
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "*");
    }
    //sir please deploy it 
    protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
    {
        //HttpContext context = HttpContext.Current;
        //context.Response.Headers.Remove("X-SourceFiles");
        //context.Response.Headers.Remove("X-Powered-By");
        //context.Response.Headers.Remove("Server");

        //context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';");
        //context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        //context.Response.Headers.Add("X-Frame-Options", "DENY");
        //context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
        //context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
        //context.Response.Headers.Add("Server", "NA");
    }
    private void OnPreSendRequestHeaders(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        context.Response.Headers.Remove("X-SourceFiles");
    }
    //public class RemoveHeadersModule : IHttpModule
    //{
    //    public void Init(HttpApplication context)
    //    {
    //        context.PreSendRequestHeaders += OnPreSendRequestHeaders;
    //    }

    //    public void Dispose()
    //    {
    //    }

    //    private void OnPreSendRequestHeaders(object sender, EventArgs e)
    //    {
    //        HttpContext context = HttpContext.Current;
    //        context.Response.Headers.Remove("X-SourceFiles");
    //        context.Response.Headers.Remove("X-Powered-By");
    //        context.Response.Headers.Remove("Server");

    //        context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';");
    //        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    //        context.Response.Headers.Add("X-Frame-Options", "DENY");
    //        context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    //        context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
    //        context.Response.Headers.Add("Server", "NA");
    //    }
    //}

</script>
