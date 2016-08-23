
namespace Ctrip.Component
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Routing;

    public static class PagerHelper
    {
        // Methods
        public static MvcHtmlString AjaxPager(this HtmlHelper html, int totalItemCount, int pageSize, int pageIndex, string pageIndexParameterName, string updateTargetId)
        {
            return html.AjaxPager(totalItemCount, pageSize, pageIndex, pageIndexParameterName, updateTargetId, null);
        }

        public static MvcHtmlString AjaxPager(this HtmlHelper html, int totalItemCount, int pageSize, int pageIndex, string pageIndexParameterName, string updateTargetId, PagerConfig cfg)
        {
            int totalPageCount = (int)Math.Ceiling((double)(((double)totalItemCount) / ((double)pageSize)));
            PagerConfig pagerCfg = ApplyConfig(cfg);
            pagerCfg.UseAjax = true;
            pagerCfg.PageIndexParameterName = pageIndexParameterName ?? pagerCfg.PageIndexParameterName;
            AjaxOptions ajaxOptions = new AjaxOptions
            {
                UpdateTargetId = updateTargetId,
                HttpMethod = pagerCfg.AjaxHttpMethod,
                OnBegin = pagerCfg.AjaxOnBegin,
                OnComplete = pagerCfg.AjaxOnComplete,
                OnFailure = pagerCfg.AjaxOnFailure,
                OnSuccess = pagerCfg.AjaxOnSuccess
            };
            PagerBuilder builder = new PagerBuilder(html, pagerCfg.ActionName, pagerCfg.ControllerName, totalPageCount, pageIndex, pagerCfg, pagerCfg.RouteName, new RouteValueDictionary(pagerCfg.RouteValues), ajaxOptions, new RouteValueDictionary(pagerCfg.HtmlAttributes));
            return builder.RenderPager();
        }

        private static PagerConfig ApplyConfig(PagerConfig cfg)
        {
            PagerConfig config = new PagerConfig();
            if (cfg != null)
            {
                config.Id = cfg.Id ?? cfg.Id;
                config.HorizontalAlign = cfg.HorizontalAlign ?? config.HorizontalAlign;
                config.AutoHide = cfg.AutoHide;
                config.AlwaysShowFirstLastPageNumber = cfg.AlwaysShowFirstLastPageNumber;
                config.ShowPrevNext = cfg.ShowPrevNext;
                config.ShowNumericPagerItems = cfg.ShowNumericPagerItems;
                config.ShowFirstLast = cfg.ShowFirstLast;
                config.ShowMorePagerItems = cfg.ShowMorePagerItems;
                config.ShowDisabledPagerItems = cfg.ShowDisabledPagerItems;
                config.CssClass = cfg.CssClass ?? config.CssClass;
                config.SeparatorHtml = cfg.SeparatorHtml ?? config.SeparatorHtml;
                config.NumericPagerItemCount = (cfg.NumericPagerItemCount > 0) ? cfg.NumericPagerItemCount : config.NumericPagerItemCount;
                config.PrevPageText = cfg.PrevPageText ?? config.PrevPageText;
                config.NextPageText = cfg.NextPageText ?? config.NextPageText;
                config.FirstPageText = cfg.FirstPageText ?? config.FirstPageText;
                config.LastPageText = cfg.LastPageText ?? config.LastPageText;
                config.MorePageText = cfg.MorePageText ?? config.MorePageText;
                config.ContainerTagName = cfg.ContainerTagName ?? config.ContainerTagName;
                config.PageIndexBoxType = cfg.PageIndexBoxType;
                config.ShowPageIndexBox = cfg.ShowPageIndexBox;
                config.MaximumPageIndexItems = config.MaximumPageIndexItems;
                config.ShowGoButton = cfg.ShowGoButton;
                config.GoButtonText = cfg.GoButtonText ?? config.GoButtonText;
                config.MaxPageIndex = cfg.MaxPageIndex;
                config.InvalidPageIndexErrorMessage = cfg.InvalidPageIndexErrorMessage ?? config.InvalidPageIndexErrorMessage;
                config.PageIndexOutOfRangeErrorMessage = cfg.PageIndexOutOfRangeErrorMessage ?? config.PageIndexOutOfRangeErrorMessage;
                config.CurrentPagerItemWrapperFormatString = cfg.CurrentPagerItemWrapperFormatString ?? config.CurrentPagerItemWrapperFormatString;
                config.NumericPagerItemWrapperFormatString = cfg.NumericPagerItemWrapperFormatString ?? config.NumericPagerItemWrapperFormatString;
                config.PagerItemWrapperFormatString = cfg.PagerItemWrapperFormatString ?? config.PagerItemWrapperFormatString;
                config.NavigationPagerItemWrapperFormatString = cfg.NavigationPagerItemWrapperFormatString ?? config.NavigationPagerItemWrapperFormatString;
                config.PageIndexBoxWrapperFormatString = cfg.PageIndexBoxWrapperFormatString ?? config.PageIndexBoxWrapperFormatString;
                config.GoToPageSectionWrapperFormatString = cfg.GoToPageSectionWrapperFormatString ?? config.GoToPageSectionWrapperFormatString;
                config.UseAjax = cfg.UseAjax;
                config.AjaxUpdateTargetId = cfg.AjaxUpdateTargetId ?? config.AjaxUpdateTargetId;
                config.AjaxOnBegin = cfg.AjaxOnBegin ?? config.AjaxOnBegin;
                config.AjaxOnComplete = cfg.AjaxOnComplete ?? config.AjaxOnComplete;
                config.AjaxOnFailure = cfg.AjaxOnFailure ?? config.AjaxOnFailure;
                config.AjaxOnSuccess = cfg.AjaxOnSuccess ?? config.AjaxOnSuccess;
                config.AjaxHttpMethod = cfg.AjaxHttpMethod ?? config.AjaxHttpMethod;
                config.ActionName = cfg.ActionName ?? config.ActionName;
                config.ControllerName = cfg.ControllerName ?? config.ControllerName;
                config.RouteName = cfg.RouteName ?? config.RouteName;
                config.RouteValues = cfg.RouteValues ?? config.RouteValues;
                config.HtmlAttributes = cfg.HtmlAttributes ?? config.HtmlAttributes;
            }
            return config;
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, string pageIndexParameterName)
        {
            return helper.Pager(totalItemCount, pageSize, pageIndex, pageIndexParameterName, null);
        }

        public static MvcHtmlString Pager(this HtmlHelper helper, int totalItemCount, int pageSize, int pageIndex, string pageIndexParameterName, PagerConfig cfg)
        {
            int totalPageCount = (int)Math.Ceiling((double)(((double)totalItemCount) / ((double)pageSize)));
            PagerConfig pagerCfg = ApplyConfig(cfg);
            pagerCfg.PageIndexParameterName = pageIndexParameterName ?? pagerCfg.PageIndexParameterName;
            PagerBuilder builder = new PagerBuilder(helper, pagerCfg.ActionName, pagerCfg.ControllerName, totalPageCount, pageIndex, pagerCfg, pagerCfg.RouteName, new RouteValueDictionary(pagerCfg.RouteValues), new RouteValueDictionary(pagerCfg.HtmlAttributes));
            return builder.RenderPager();
        }
    }
}