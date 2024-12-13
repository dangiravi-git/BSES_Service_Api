using System;
using System.Web;

public class Global : HttpApplication
{
    // This is where you can handle the Application start event
    protected void Application_Start(object sender, EventArgs e)
    {
        // Code to run on application start
        // For example, setting up logging, initialization, etc.
        System.Diagnostics.Debug.WriteLine("Application Started");

        // Example: Initialize logging service, dependency injection, etc.
    }

    // This is where you can handle errors globally
    protected void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        // Handle the error (e.g., log it, or show a friendly message)
        System.Diagnostics.Debug.WriteLine("An error occurred: " + ex.Message);

        // You can also clear the error if handled
        Server.ClearError();
    }

    // This event occurs when the session starts
    protected void Session_Start(object sender, EventArgs e)
    {
        // Code to run on session start
    }

    // This event occurs when the session ends
    protected void Session_End(object sender, EventArgs e)
    {
        // Code to run on session end
    }

    // This event occurs when the application ends
    protected void Application_End(object sender, EventArgs e)
    {
        // Code to run on application end
        System.Diagnostics.Debug.WriteLine("Application Ended");
    }
}