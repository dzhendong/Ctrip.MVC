using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 创建自定义View
    /// Application Start
    /// ViewEngines.Engines.Insert(O , new StaticFileViewEngine());
    /// </summary>
    public class StaticFileViewEngine : IViewEngine
    {
        private Dictionary<ViewEngineResultCacheKey, ViewEngineResult>
            viewEngineResults = new Dictionary<ViewEngineResultCacheKey , ViewEngineResult>();

        private object syncHelper = new object();

        public ViewEngineResult FindPartialView(ControllerContext controllerContext,
            string partialViewName, bool useCache)
        {
            return this.FindView(controllerContext, partialViewName, null, useCache);
        }

        public ViewEngineResult FindView(ControllerContext controllerContext,
            string viewName, string masterName, bool useCache)
        {
            string controllerName = controllerContext.RouteData.GetRequiredString("controller");

            ViewEngineResultCacheKey key = new ViewEngineResultCacheKey(controllerName, viewName);

            ViewEngineResult result;

            if (!useCache)
            {
                result = InternalFindView(controllerContext, viewName,controllerName);
                viewEngineResults[key] = result;
                return result;
            }

            if (viewEngineResults.TryGetValue(key, out result))
            {
                return result;
            }

            lock (syncHelper)
            {
                if (viewEngineResults.TryGetValue(key, out result))
                {
                    return result;
                }

                result = InternalFindView(controllerContext, viewName,controllerName);
                viewEngineResults[key] = result;
                return result;
            }
        }

        private ViewEngineResult InternalFindView(ControllerContext
            controllerContext, string viewName, string controllerName)
        {
            string[] searchLocations = new string[]
            {
                string.Format( "-/views/{O}/{l} .shtml" , controllerName , viewName) ,
                string.Format( "-/views/Shared/{O}.shtml" , viewName)
            };

            string fileName = controllerContext.HttpContext.Request.MapPath(searchLocations[0]);

            if (File.Exists(fileName))
            {
                return new ViewEngineResult(new StaticFileView(fileName), this);
            }

            fileName = string.Format(@"\views\Shared\{O}.shtml" , viewName);
            if (File.Exists(fileName))
            { 
                return new ViewEngineResult( new StaticFileView(fileName) , this);
            }
            return new ViewEngineResult(searchLocations);
        }

        public void ReleaseView(ControllerContext controllerContext, IView view)
        { }
    }
}
