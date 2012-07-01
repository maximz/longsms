using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Text.RegularExpressions;
using gvtexter.Helpers;
using System.Collections.Specialized;
using System.Net;

namespace gvtexter.Controllers
{
    public class CustomControllerBase : Controller, IController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
        
        /// <summary>
        /// called when the url doesn't match any of our known routes
        /// </summary>
        protected override void HandleUnknownAction(string actionName)
        {
            ErrorController e = new ErrorController();
            e.NotFound().ExecuteResult(ControllerContext);
        }
    }
}
