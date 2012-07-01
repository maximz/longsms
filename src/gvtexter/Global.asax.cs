using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RiaLibrary.Web;
using MvcMiniProfiler;
using System.IO;

namespace gvtexter
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoutes(); // Register Attribute Based Routes which the current assembly contains (RiaLibrary.Web = http://maproutes.codeplex.com)

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start()
		{
			GlobalFilters.Filters.Add(new HandleErrorAttribute());

			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);

			// Route Debugger: to use, uncomment this line:
			//RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
		}


		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			Current.Context.Response.BufferOutput = true;
			MiniProfiler profiler = null;

			#if DEBUG
				profiler = MvcMiniProfiler.MiniProfiler.Start();
			#endif

			using (profiler.Step("Application_BeginRequest"))
			{
				// you can start profiling your code immediately
			}
		}

		protected void Application_EndRequest(object sender, EventArgs e)
		{
			MvcMiniProfiler.MiniProfiler.Stop();
		}

	}
}