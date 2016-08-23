
using System;
using System.Web.Routing;

namespace Ctrip.Component
{
    public class PagerConfig
    {
        // Fields
        private string _containerTagName;

        // Methods
        public PagerConfig()
        {
            this.AutoHide = true;
            this.CssClass = "mvcPager";
            this.PageIndexParameterName = "pageIndex";
            this.NumericPagerItemCount = 7;
            this.AlwaysShowFirstLastPageNumber = false;
            this.ShowPrevNext = true;
            this.ShowNumericPagerItems = true;
            this.ShowFirstLast = true;
            this.ShowMorePagerItems = true;
            this.ShowDisabledPagerItems = true;
            this.PrevPageText = "< 上一页";
            this.NextPageText = "下一页 >";
            this.FirstPageText = "<<第一页";
            this.LastPageText = "最末页>>";
            this.MorePageText = "...";
            this.SeparatorHtml = "";
            this.ContainerTagName = "div";
            this.PageIndexBoxType = PageIndexBoxType.TextBox;
            this.ShowPageIndexBox = true;
            this.MaximumPageIndexItems = 100;
            this.ShowGoButton = true;
            this.GoButtonText = "跳转";
            this.MaxPageIndex = 0;
            this.InvalidPageIndexErrorMessage = "无效的页面索引";
            this.PageIndexOutOfRangeErrorMessage = "页面索引超出范围";
            this.UseAjax = false;
            this.AjaxHttpMethod = "Get";
            this.CurrentPagerItemWrapperFormatString = "<span class=\"current_page_item\">{0}</span>";
            this.NumericPagerItemWrapperFormatString = "<span class=\"page_number_item\">{0}</span>";
            this.PagerItemWrapperFormatString = "<span class=\"page_item\">{0}</span>";
            this.NavigationPagerItemWrapperFormatString = "<span class=\"page_nav_item\">{0}</span>";
            this.PageIndexBoxWrapperFormatString = "<span class=\"page_input_item\"><span>第</span>{0}<span>页</span></span>";
            this.GoToPageSectionWrapperFormatString = "<span class=\"page_goto_item\">{0}</span>";
            this.RouteValues = new RouteValueDictionary();
        }

        // Properties
        public string ActionName { get; set; }

        public string AjaxHttpMethod { get; set; }

        public string AjaxOnBegin { get; set; }

        public string AjaxOnComplete { get; set; }

        public string AjaxOnFailure { get; set; }

        public string AjaxOnSuccess { get; set; }

        public string AjaxUpdateTargetId { get; set; }

        public bool AlwaysShowFirstLastPageNumber { get; set; }

        public bool AutoHide { get; set; }

        public string ContainerTagName
        {
            get
            {
                return this._containerTagName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("ContainerTagName不能为空", "ContainerTagName");
                }
                this._containerTagName = value;
            }
        }

        public string ControllerName { get; set; }

        public string CssClass { get; set; }

        public string CurrentPageNumberFormatString { get; set; }

        public string CurrentPagerItemWrapperFormatString { get; set; }

        public string FirstPageText { get; set; }

        public string GoButtonText { get; set; }

        public string GoToPageSectionWrapperFormatString { get; set; }

        public string HorizontalAlign { get; set; }

        public object HtmlAttributes { get; set; }

        public string Id { get; set; }

        public string InvalidPageIndexErrorMessage { get; set; }

        public string LastPageText { get; set; }

        public int MaximumPageIndexItems { get; set; }

        public int MaxPageIndex { get; set; }

        public string MorePagerItemWrapperFormatString { get; set; }

        public string MorePageText { get; set; }

        public string NavigationPagerItemWrapperFormatString { get; set; }

        public string NextPageText { get; set; }

        public int NumericPagerItemCount { get; set; }

        public string NumericPagerItemWrapperFormatString { get; set; }

        public PageIndexBoxType PageIndexBoxType { get; set; }

        public string PageIndexBoxWrapperFormatString { get; set; }

        public string PageIndexOutOfRangeErrorMessage { get; set; }

        public string PageIndexParameterName { get; set; }

        internal string PageNumberFormatString { get; set; }

        public string PagerItemWrapperFormatString { get; set; }

        public string PrevPageText { get; set; }

        public string RouteName { get; set; }

        public RouteValueDictionary RouteValues { get; set; }

        public string SeparatorHtml { get; set; }

        public bool ShowDisabledPagerItems { get; set; }

        public bool ShowFirstLast { get; set; }

        public bool ShowGoButton { get; set; }

        public bool ShowMorePagerItems { get; set; }

        public bool ShowNumericPagerItems { get; set; }

        public bool ShowPageIndexBox { get; set; }

        public bool ShowPrevNext { get; set; }

        internal bool UseAjax { get; set; }
    }
}