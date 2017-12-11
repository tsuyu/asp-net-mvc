using System.IO;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using System.Web.Mvc;

namespace asp_net_mvc.Helpers
{
    public static class StaticFileHelpers
    {
        /// <summary>
        /// Renders a script tag for the provided virtual path.
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="path">Virtual path for which to generate the script tag.</param>
        /// <param name="appendVersion">Indicates whether to append a file version fingerprint to the url. Default is true.</param>
        /// <returns></returns>
        public static MvcHtmlString Script(this HtmlHelper helper, string path, bool appendVersion = true)
        {
            var url = appendVersion ? AppendVersion(path) : path;
            var html = $"<script src=\"{url}\"></script>";
            return new MvcHtmlString(html);
        }

        /// <summary>
        /// Renders a stylesheet link tag for the provided virtual path.
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="path">Virtual path for which to generate the script tag.</param>
        /// <param name="appendVersion">Indicates whether to append a file version fingerprint to the url. Default is true.</param>
        /// <returns></returns>
        public static MvcHtmlString Stylesheet(this HtmlHelper helper, string path, bool appendVersion = true)
        {
            var url = appendVersion ? AppendVersion(path) : path;
            var html = $"<link href=\"{url}\" rel=\"stylesheet\">";
            return new MvcHtmlString(html);
        }

        /// <summary>
        /// Appends a version fingerprint (based on file last-write-time) query string parameter to a root-relative file path.
        /// </summary>
        /// <param name="rootRelativePath">The root-relative path to the file that should be fingerprinted.</param>
        /// <returns>The path with the version fingerprint appended.</returns>
        public static string AppendVersion(string rootRelativePath)
        {
            if (HttpRuntime.Cache[rootRelativePath] == null)
            {
                var absolutePath = HostingEnvironment.MapPath(rootRelativePath);
                var lastWriteTime = File.GetLastWriteTime(absolutePath);

                if (rootRelativePath.StartsWith("~"))
                {
                    rootRelativePath = rootRelativePath.Substring(1);
                }

                var versionedUrl = rootRelativePath + "?v=" + lastWriteTime.Ticks;
                HttpRuntime.Cache.Insert(rootRelativePath, versionedUrl, new CacheDependency(absolutePath));
            }

            return HttpRuntime.Cache[rootRelativePath] as string;
        }

        public static MvcHtmlString IsActive(this HtmlHelper htmlHelper, string action, string controller,
            string activeClass="class=\"active\"", string inActiveClass = "")
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && action == routeAction);

            return new MvcHtmlString(returnActive ? activeClass : inActiveClass);
        }
    }
}