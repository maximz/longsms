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
using gvtexter.Controllers;
using System.Runtime.Remoting.Messaging;
using System.Web.Caching;
using MvcMiniProfiler;
using MvcMiniProfiler.Data;
using System.Reflection;
using System.Diagnostics;
using System.Web.Security;
using System.Text;

namespace gvtexter
{
    /// <summary>
    /// Helper class that provides quick access to common objects used across a single request.
    /// </summary>
    public static class Current
    {
        //const string DISPOSE_CONNECTION_KEY = "dispose_connections";

        /// <summary>
        /// Shortcut to HttpContext.Current.
        /// </summary>
        public static HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        /// <summary>
        /// Shortcut to HttpContext.Current.Request.
        /// </summary>
        public static HttpRequest Request
        {
            get { return Context.Request; }
        }

        /// <summary>
        /// Gets the controller for the current request; should be set during init of current request's controller.
        /// </summary>
        public static CustomControllerBase Controller
        {
            get { return Context.Items["Controller"] as CustomControllerBase; }
            set { Context.Items["Controller"] = value; }
        }

        /// <summary>
        /// Returns the current MiniProfiler. Shorthand for MiniProfiler.Current - just so that all our common things can be stored under one class (Current)
        /// </summary>
        public static MiniProfiler MiniProfiler
        {
            get { return MiniProfiler.Current; }
        }

        /// <summary>
        /// Gets the current "revision number" slug (for busting our caching when we update files).
        /// </summary>
        /// <returns></returns>
        public static string RevNumber()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.ProductVersion; // gets file version info

            var lastTwoPartsOfVersion = version.Substring(version.IndexOf(".", version.IndexOf(".") + 1) + 1); // gets substring of version, starting at index of second dot (first dot's index is used as startIndex in indexOf)
            return URLFriendly(lastTwoPartsOfVersion);
        }

        /// <summary>
        /// Produces optional, URL-friendly version of a title, "like-this-one". 
        /// From http://code.google.com/p/stack-exchange-data-explorer/source/browse/App/StackExchange.DataExplorer/Helpers/HtmlUtilities.cs (MIT License)
        /// </summary>
        private static string URLFriendly(string title)
        {
            if (title == null) return "";

            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            string s;
            char c;

            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' || c == '\\' || c == '-' || c == '_')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if (c >= 128)
                {
                    s = c.ToString().ToLowerInvariant();
                    if ("àåáâäãåą".Contains(s))
                    {
                        sb.Append("a");
                    }
                    else if ("èéêëę".Contains(s))
                    {
                        sb.Append("e");
                    }
                    else if ("ìíîïı".Contains(s))
                    {
                        sb.Append("i");
                    }
                    else if ("òóôõöø".Contains(s))
                    {
                        sb.Append("o");
                    }
                    else if ("ùúûü".Contains(s))
                    {
                        sb.Append("u");
                    }
                    else if ("çćč".Contains(s))
                    {
                        sb.Append("c");
                    }
                    else if ("żźž".Contains(s))
                    {
                        sb.Append("z");
                    }
                    else if ("śşš".Contains(s))
                    {
                        sb.Append("s");
                    }
                    else if ("ñń".Contains(s))
                    {
                        sb.Append("n");
                    }
                    else if ("ýŸ".Contains(s))
                    {
                        sb.Append("y");
                    }
                    else if (c == 'ł')
                    {
                        sb.Append("l");
                    }
                    else if (c == 'đ')
                    {
                        sb.Append("d");
                    }
                    else if (c == 'ß')
                    {
                        sb.Append("ss");
                    }
                    else if (c == 'ğ')
                    {
                        sb.Append("g");
                    }
                    prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }

        /// <summary>
        /// Gets the current Google Analytics key.
        /// </summary>
        /// <returns></returns>
        public static string GAnalyticsKey()
        {
            return System.Configuration.ConfigurationManager.AppSettings["GAnalyticsKey"];
        }

    }
}