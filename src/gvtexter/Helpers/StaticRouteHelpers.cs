using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gvtexter.Helpers
{
    public static class StaticRouteHelpers
    {
        /// <summary>
        /// Scripts the SRC.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        public static MvcHtmlString ScriptSrc(this HtmlHelper h, string virtualPath)
        {
            var revNum = Current.RevNumber();
            var absolutePath = System.Web.VirtualPathUtility.ToAbsolute(virtualPath) + "?" + revNum;
            var scriptinclude = "<script src=\"" + absolutePath + "\"></script>";
            return new MvcHtmlString(scriptinclude);
        }

        /// <summary>
        /// Links the SRC.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        public static MvcHtmlString LinkSrc(this HtmlHelper h, string virtualPath)
        {
            var revNum = Current.RevNumber();
            var absolutePath = System.Web.VirtualPathUtility.ToAbsolute(virtualPath) + "?" + revNum;
            var linkinclude = "<link href=\"" + absolutePath + "\" rel=\"stylesheet\" />";
            return new MvcHtmlString(linkinclude);
        }

        /// <summary>
        /// Resolves the static URL.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        public static MvcHtmlString ResolveStaticUrl(this HtmlHelper h, string virtualPath)
        {
            var revNum = Current.RevNumber();
            var absolutePath = ResolveDynamicUrl(h, virtualPath).ToString() + "?" + revNum;
            return new MvcHtmlString(absolutePath);
        }
        /// <summary>
        /// Resolves the dynamic URL.
        /// </summary>
        /// <param name="h">The h.</param>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        public static MvcHtmlString ResolveDynamicUrl(this HtmlHelper h, string virtualPath)
        {
            var absolutePath = System.Web.VirtualPathUtility.ToAbsolute(virtualPath);
            return new MvcHtmlString(absolutePath);
        }
    }
}