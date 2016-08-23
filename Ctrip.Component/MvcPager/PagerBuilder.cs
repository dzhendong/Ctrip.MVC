
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace Ctrip.Component
{
    internal class PagerBuilder
    {
        private readonly string _actionName;
        private readonly AjaxHelper _ajax;
        private readonly AjaxOptions _ajaxOptions;
        private readonly string _controllerName;
        private readonly int _endPageIndex;
        private readonly HtmlHelper _html;
        private IDictionary<string, object> _htmlAttributes;
        private readonly int _pageIndex;
        private readonly PagerConfig _pagerCfg;
        private readonly string _routeName;
        private readonly RouteValueDictionary _routeValues;
        private readonly int _startPageIndex;
        private readonly int _totalPageCount;
        private const string ScriptPageIndexName = "*_MvcPager_PageIndex_*";

        internal PagerBuilder(HtmlHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerConfig pagerCfg, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
            : this(helper, null, actionName, controllerName, totalPageCount, pageIndex, pagerCfg, routeName, routeValues, null, htmlAttributes)
        {
        }

        internal PagerBuilder(HtmlHelper helper, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerConfig pagerCfg, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
            : this(helper, null, actionName, controllerName, totalPageCount, pageIndex, pagerCfg, routeName, routeValues, ajaxOptions, htmlAttributes)
        {
        }

        internal PagerBuilder(HtmlHelper html, AjaxHelper ajax, string actionName, string controllerName, int totalPageCount, int pageIndex, PagerConfig pagerCfg, string routeName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, IDictionary<string, object> htmlAttributes)
        {
            this._totalPageCount = 1;
            this._startPageIndex = 1;
            this._endPageIndex = 1;
            if (string.IsNullOrEmpty(actionName))
            {
                if (ajax != null)
                {
                    actionName = (string)ajax.ViewContext.RouteData.Values["action"];
                }
                else
                {
                    actionName = (string)html.ViewContext.RouteData.Values["action"];
                }
            }
            if (string.IsNullOrEmpty(controllerName))
            {
                if (ajax != null)
                {
                    controllerName = (string)ajax.ViewContext.RouteData.Values["controller"];
                }
                else
                {
                    controllerName = (string)html.ViewContext.RouteData.Values["controller"];
                }
            }
            if (pagerCfg == null)
            {
                pagerCfg = new PagerConfig();
            }
            this._html = html;
            this._ajax = ajax;
            this._actionName = actionName;
            this._controllerName = controllerName;
            if ((pagerCfg.MaxPageIndex == 0) || (pagerCfg.MaxPageIndex > totalPageCount))
            {
                this._totalPageCount = totalPageCount;
            }
            else
            {
                this._totalPageCount = pagerCfg.MaxPageIndex;
            }
            this._pageIndex = pageIndex;
            this._pagerCfg = pagerCfg;
            this._routeName = routeName;
            this._routeValues = routeValues;
            this._ajaxOptions = ajaxOptions;
            this._htmlAttributes = htmlAttributes;
            this._startPageIndex = pageIndex - (pagerCfg.NumericPagerItemCount / 2);
            if ((this._startPageIndex + pagerCfg.NumericPagerItemCount) > this._totalPageCount)
            {
                this._startPageIndex = (this._totalPageCount + 1) - pagerCfg.NumericPagerItemCount;
            }
            if (this._startPageIndex < 1)
            {
                this._startPageIndex = 1;
            }
            this._endPageIndex = (this._startPageIndex + this._pagerCfg.NumericPagerItemCount) - 1;
            if (this._endPageIndex > this._totalPageCount)
            {
                this._endPageIndex = this._totalPageCount;
            }
        }

        private void AddFirst(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerCfg.FirstPageText, 1, this._pageIndex == 1, PagerItemType.FirstPage);
            if (!item.Disabled || (item.Disabled && this._pagerCfg.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private void AddLast(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerCfg.LastPageText, this._totalPageCount, this._pageIndex >= this._totalPageCount, PagerItemType.LastPage);
            if (!item.Disabled || (item.Disabled && this._pagerCfg.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private void AddMoreAfter(ICollection<PagerItem> results)
        {
            if (this._endPageIndex < this._totalPageCount)
            {
                int pageIndex = this._startPageIndex + this._pagerCfg.NumericPagerItemCount;
                if (pageIndex > this._totalPageCount)
                {
                    pageIndex = this._totalPageCount;
                }
                PagerItem item = new PagerItem(this._pagerCfg.MorePageText, pageIndex, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddMoreBefore(ICollection<PagerItem> results)
        {
            if ((this._startPageIndex > 1) && this._pagerCfg.ShowMorePagerItems)
            {
                int pageIndex = this._startPageIndex - 1;
                if (pageIndex < 1)
                {
                    pageIndex = 1;
                }
                PagerItem item = new PagerItem(this._pagerCfg.MorePageText, pageIndex, false, PagerItemType.MorePage);
                results.Add(item);
            }
        }

        private void AddNext(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerCfg.NextPageText, this._pageIndex + 1, this._pageIndex >= this._totalPageCount, PagerItemType.NextPage);
            if (!item.Disabled || (item.Disabled && this._pagerCfg.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private void AddPageNumbers(ICollection<PagerItem> results)
        {
            for (int i = this._startPageIndex; i <= this._endPageIndex; i++)
            {
                string str = i.ToString();
                if ((i == this._pageIndex) && !string.IsNullOrEmpty(this._pagerCfg.CurrentPageNumberFormatString))
                {
                    str = string.Format(this._pagerCfg.CurrentPageNumberFormatString, str);
                }
                else if (!string.IsNullOrEmpty(this._pagerCfg.PageNumberFormatString))
                {
                    str = string.Format(this._pagerCfg.PageNumberFormatString, str);
                }
                PagerItem item = new PagerItem(str, i, false, PagerItemType.NumericPage);
                results.Add(item);
            }
        }

        private void AddPrevious(ICollection<PagerItem> results)
        {
            PagerItem item = new PagerItem(this._pagerCfg.PrevPageText, this._pageIndex - 1, this._pageIndex == 1, PagerItemType.PrevPage);
            if (!item.Disabled || (item.Disabled && this._pagerCfg.ShowDisabledPagerItems))
            {
                results.Add(item);
            }
        }

        private string BuildGoToPageSection(ref string pagerScript)
        {
            int num;
            ViewContext viewContext = this._html.ViewContext;
            if (int.TryParse((string)viewContext.HttpContext.Items["_MvcPager_ControlIndex"], out num))
            {
                num++;
            }
            viewContext.HttpContext.Items["_MvcPager_ControlIndex"] = num.ToString();
            string str = "_MvcPager_Ctrl" + num;
            string str2 = this.GenerateAnchor(new PagerItem("0", 0, false, PagerItemType.NumericPage));
            if (num == 0)
            {
                pagerScript = pagerScript + this.KeyDownScript().Replace("%TotalPageCount%", this._totalPageCount.ToString()) + this.GoToPageScript().Replace("%InvalidPageIndexErrorMessage%", this._pagerCfg.InvalidPageIndexErrorMessage).Replace("%PageIndexOutOfRangeErrorMessage%", this._pagerCfg.PageIndexOutOfRangeErrorMessage);
            }
            string str3 = null;
            if (!this._pagerCfg.ShowGoButton)
            {
                str3 = " onchange=\"_MvcPager_GoToPage(this," + this._totalPageCount + ")\"";
            }
            StringBuilder builder = new StringBuilder();
            if (this._pagerCfg.PageIndexBoxType == PageIndexBoxType.DropDownList)
            {
                int num2 = this._pageIndex - (this._pagerCfg.MaximumPageIndexItems / 2);
                if ((num2 + this._pagerCfg.MaximumPageIndexItems) > this._totalPageCount)
                {
                    num2 = (this._totalPageCount + 1) - this._pagerCfg.MaximumPageIndexItems;
                }
                if (num2 < 1)
                {
                    num2 = 1;
                }
                int num3 = (num2 + this._pagerCfg.MaximumPageIndexItems) - 1;
                if (num3 > this._totalPageCount)
                {
                    num3 = this._totalPageCount;
                }
                builder.AppendFormat("<select id=\"{0}\"{1}>", str + "_pib", str3);
                for (int i = num2; i <= num3; i++)
                {
                    builder.AppendFormat("<option value=\"{0}\"", i);
                    if (i == this._pageIndex)
                    {
                        builder.Append(" selected=\"selected\"");
                    }
                    builder.AppendFormat(">{0}</option>", i);
                }
                builder.Append("</select>");
            }
            else
            {
                builder.AppendFormat("<input type=\"text\" id=\"{0}\" value=\"{1}\" style=\"width:50px;\" onkeydown=\"_MvcPager_Keydown(event)\"{2}/>", str + "_pib", this._pageIndex, str3);
            }
            if (!string.IsNullOrEmpty(this._pagerCfg.PageIndexBoxWrapperFormatString))
            {
                builder = new StringBuilder(string.Format(this._pagerCfg.PageIndexBoxWrapperFormatString, builder));
            }
            if (this._pagerCfg.ShowGoButton)
            {
                builder.AppendFormat("<input type=\"button\" value=\"{0}\" onclick=\"_MvcPager_GoToPage(document.getElementById('{1}')," + this._totalPageCount + ")\"/>", this._pagerCfg.GoButtonText, str + "_pib");
            }
            builder.AppendFormat("<span id=\"{0}\" style=\"display:none;width:0px;height:0px\">{1}</span>", str + "_piblink", str2);
            if (!string.IsNullOrEmpty(this._pagerCfg.GoToPageSectionWrapperFormatString) || !string.IsNullOrEmpty(this._pagerCfg.PagerItemWrapperFormatString))
            {
                return string.Format(this._pagerCfg.GoToPageSectionWrapperFormatString ?? this._pagerCfg.PagerItemWrapperFormatString, builder);
            }
            return builder.ToString();
        }

        private MvcHtmlString CreateWrappedPagerElement(PagerItem item, string el)
        {
            string str = el;
            switch (item.Type)
            {
                case PagerItemType.FirstPage:
                case PagerItemType.NextPage:
                case PagerItemType.PrevPage:
                case PagerItemType.LastPage:
                    if (!string.IsNullOrEmpty(this._pagerCfg.NavigationPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerCfg.PagerItemWrapperFormatString))
                    {
                        str = string.Format(this._pagerCfg.NavigationPagerItemWrapperFormatString ?? this._pagerCfg.PagerItemWrapperFormatString, el);
                    }
                    break;

                case PagerItemType.MorePage:
                    if (!string.IsNullOrEmpty(this._pagerCfg.MorePagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerCfg.PagerItemWrapperFormatString))
                    {
                        str = string.Format(this._pagerCfg.MorePagerItemWrapperFormatString ?? this._pagerCfg.PagerItemWrapperFormatString, el);
                    }
                    break;

                case PagerItemType.NumericPage:
                    if ((item.PageIndex != this._pageIndex) || (string.IsNullOrEmpty(this._pagerCfg.CurrentPagerItemWrapperFormatString) && string.IsNullOrEmpty(this._pagerCfg.PagerItemWrapperFormatString)))
                    {
                        if (!string.IsNullOrEmpty(this._pagerCfg.NumericPagerItemWrapperFormatString) || !string.IsNullOrEmpty(this._pagerCfg.PagerItemWrapperFormatString))
                        {
                            str = string.Format(this._pagerCfg.NumericPagerItemWrapperFormatString ?? this._pagerCfg.PagerItemWrapperFormatString, el);
                        }
                        break;
                    }
                    str = string.Format(this._pagerCfg.CurrentPagerItemWrapperFormatString ?? this._pagerCfg.PagerItemWrapperFormatString, el);
                    break;
            }
            return MvcHtmlString.Create(str + this._pagerCfg.SeparatorHtml);
        }

        private string GenerateAnchor(PagerItem item)
        {
            string str = this.GenerateUrl(item.PageIndex);
            if (!this._pagerCfg.UseAjax)
            {
                return ("<a href=\"" + str + "\" onclick=\"window.open(this.attributes.getNamedItem('href').value,'_self')\"></a>");
            }
            StringBuilder builder = new StringBuilder();
            if ((!string.IsNullOrEmpty(this._ajaxOptions.OnFailure) || !string.IsNullOrEmpty(this._ajaxOptions.OnBegin)) || (!string.IsNullOrEmpty(this._ajaxOptions.OnComplete) && (this._ajaxOptions.HttpMethod.ToUpper() != "GET")))
            {
                builder.Append("$.ajax({type:'").Append((this._ajaxOptions.HttpMethod.ToUpper() == "GET") ? "get" : "post");
                builder.Append("',url:$(this).attr('href')");
                if (!string.IsNullOrEmpty(this._ajaxOptions.OnSuccess))
                {
                    builder.Append(",success:").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnSuccess));
                }
                else
                {
                    builder.Append(",success:function(data,status,xhr){$('#");
                    builder.Append(this._ajaxOptions.UpdateTargetId).Append("').html(data);}");
                }
                if (!string.IsNullOrEmpty(this._ajaxOptions.OnFailure))
                {
                    builder.Append(",error:").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnFailure));
                }
                if (!string.IsNullOrEmpty(this._ajaxOptions.OnBegin))
                {
                    builder.Append(",beforeSend:").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnBegin));
                }
                if (!string.IsNullOrEmpty(this._ajaxOptions.OnComplete))
                {
                    builder.Append(",complete:").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnComplete));
                }
                builder.Append("});return false;");
            }
            else if (this._ajaxOptions.HttpMethod.ToUpper() == "GET")
            {
                builder.Append("$('#").Append(this._ajaxOptions.UpdateTargetId);
                builder.Append("').load($(this).attr('href')");
                if (!string.IsNullOrEmpty(this._ajaxOptions.OnComplete))
                {
                    builder.Append(",").Append(HttpUtility.HtmlAttributeEncode(this._ajaxOptions.OnComplete));
                }
                builder.Append(");return false;");
            }
            else
            {
                builder.Append("$.post($(this).attr('href'), function(data) {$('#");
                builder.Append(this._ajaxOptions.UpdateTargetId);
                builder.Append("').html(data);});return false;");
            }
            if (!string.IsNullOrEmpty(str))
            {
                return string.Format(CultureInfo.InvariantCulture, "<a href=\"{0}\" onclick=\"{1}\">{2}</a>", new object[] { str, builder, item.Text });
            }
            return this._html.Encode(item.Text);
        }

        private MvcHtmlString GenerateJqAjaxPagerElement(PagerItem item)
        {
            if (item.Disabled)
            {
                return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
            }
            return this.CreateWrappedPagerElement(item, this.GenerateAnchor(item));
        }

        private MvcHtmlString GenerateMsAjaxPagerElement(PagerItem item)
        {
            if ((item.PageIndex == this._pageIndex) && !item.Disabled)
            {
                return this.CreateWrappedPagerElement(item, item.Text);
            }
            if (item.Disabled)
            {
                return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
            }
            if ((item.PageIndex >= 1) && (item.PageIndex <= this._totalPageCount))
            {
                return this.CreateWrappedPagerElement(item, this.GenerateAnchor(item));
            }
            return null;
        }

        private MvcHtmlString GeneratePagerElement(PagerItem item)
        {
            string str = this.GenerateUrl(item.PageIndex);
            if (item.Disabled)
            {
                return this.CreateWrappedPagerElement(item, string.Format("<a disabled=\"disabled\">{0}</a>", item.Text));
            }
            return this.CreateWrappedPagerElement(item, string.IsNullOrEmpty(str) ? this._html.Encode(item.Text) : string.Format("<a href='{0}'>{1}</a>", str, item.Text));
        }

        private string GenerateUrl(int pageIndex)
        {
            if ((pageIndex > this._totalPageCount) || (pageIndex == this._pageIndex))
            {
                return null;
            }
            RouteValueDictionary currentRouteValues = this.GetCurrentRouteValues(this._html.ViewContext);
            if (pageIndex == 0)
            {
                currentRouteValues[this._pagerCfg.PageIndexParameterName] = "*_MvcPager_PageIndex_*";
            }
            else
            {
                currentRouteValues[this._pagerCfg.PageIndexParameterName] = pageIndex;
            }
            UrlHelper helper = new UrlHelper(this._html.ViewContext.RequestContext);
            if (!string.IsNullOrEmpty(this._routeName))
            {
                return helper.RouteUrl(this._routeName, currentRouteValues);
            }
            return helper.RouteUrl(currentRouteValues);
        }

        private RouteValueDictionary GetCurrentRouteValues(ViewContext viewContext)
        {
            RouteValueDictionary dictionary = this._routeValues ?? new RouteValueDictionary();
            NameValueCollection queryString = viewContext.HttpContext.Request.QueryString;
            if ((queryString != null) && (queryString.Count > 0))
            {
                string[] array = new string[] { "x-requested-with", "xmlhttprequest", this._pagerCfg.PageIndexParameterName.ToLower() };
                foreach (string str in queryString.Keys)
                {
                    if (!string.IsNullOrEmpty(str) && (Array.IndexOf<string>(array, str.ToLower()) < 0))
                    {
                        dictionary[str] = queryString[str];
                    }
                }
            }
            dictionary["action"] = this._actionName;
            dictionary["controller"] = this._controllerName;
            return dictionary;
        }

        private string GoToPageScript()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("function _MvcPager_GoToPage(_pib,_mp) {");
            builder.Append(" var pageIndex;");
            builder.Append(" if(_pib.tagName==\"SELECT\") {");
            builder.Append("  pageIndex=_pib.options[_pib.selectedIndex].value;");
            builder.Append(" } else { ");
            builder.Append("  pageIndex=_pib.value;");
            builder.Append("  var r=new RegExp(\"^\\\\s*(\\\\d+)\\\\s*$\");");
            builder.Append("  if (!r.test(pageIndex)) {");
            builder.Append("   alert(\"%InvalidPageIndexErrorMessage%\");return;");
            builder.Append("  } else if (RegExp.$1<1||RegExp.$1>_mp) {");
            builder.Append("   alert(\"%PageIndexOutOfRangeErrorMessage%\");return;");
            builder.Append("  }");
            builder.Append(" }");
            builder.Append(" var _hl=document.getElementById(_pib.id+'link').childNodes[0];");
            builder.Append(" var _lh=_hl.href;");
            builder.Append(" _hl.href=_lh.replace('*_MvcPager_PageIndex_*',pageIndex);");
            builder.Append(" if (_hl.click) {");
            builder.Append("  _hl.click();");
            builder.Append(" } else {");
            builder.Append("  var evt=document.createEvent('MouseEvents');");
            builder.Append("  evt.initEvent('click',true,true);");
            builder.Append("  _hl.dispatchEvent(evt);");
            builder.Append(" }");
            builder.Append(" _hl.href=_lh;");
            builder.Append("}");
            return builder.ToString();
        }

        private string KeyDownScript()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("function _MvcPager_Keydown(e) {");
            builder.Append(" var _kc, _pib;");
            builder.Append(" if (window.event) {");
            builder.Append("  _kc=e.keyCode;");
            builder.Append("  _pib=e.srcElement;");
            builder.Append(" } else if (e.which) {");
            builder.Append("  _kc=e.which;");
            builder.Append("  _pib=e.target;");
            builder.Append(" }");
            builder.Append(" var validKey=(_kc==8||_kc==46||_kc==37||_kc==39||(_kc>=48&&_kc<=57)||(_kc>=96&&_kc<=105));");
            builder.Append(" if(!validKey) {");
            builder.Append("  if(_kc==13) {");
            builder.Append("   _MvcPager_GoToPage(_pib,%TotalPageCount%);");
            builder.Append("  }");
            builder.Append("  if(e.preventDefault){");
            builder.Append("   e.preventDefault();");
            builder.Append("  } else {");
            builder.Append("   event.returnValue=false;");
            builder.Append("  }");
            builder.Append(" }");
            builder.Append("}");
            return builder.ToString();
        }

        /// <summary>
        /// 重绘控件
        /// </summary>
        /// <returns></returns>
        internal MvcHtmlString RenderPager()
        {
            // 总页面数小于或等于1时，返回空
            if (_totalPageCount <= 1 && _pagerCfg.AutoHide)
            {
                return MvcHtmlString.Create(string.Empty);
            }
            // 超出页码范围时，返回错误提示消息
            if ((_pageIndex > _totalPageCount && _totalPageCount > 0) || _pageIndex < 1)
            {
                return MvcHtmlString.Create(string.Format("{0}<div style=\"color:red;font-weight:bold\">{1}</div>{0}",
                    string.Empty, _pagerCfg.PageIndexOutOfRangeErrorMessage));
            }

            var pagerItems = new List<PagerItem>();

            if (_pagerCfg.ShowFirstLast)
            {
                AddFirst(pagerItems);
            }

            if (_pagerCfg.ShowPrevNext)
            {
                AddPrevious(pagerItems);
            }

            if (_pagerCfg.ShowNumericPagerItems)
            {
                if (_pagerCfg.AlwaysShowFirstLastPageNumber && _startPageIndex > 1)
                {
                    pagerItems.Add(new PagerItem("1", 1, false, PagerItemType.NumericPage));
                }

                if (_pagerCfg.ShowMorePagerItems)
                {
                    AddMoreBefore(pagerItems);
                }

                // 数字页码
                AddPageNumbers(pagerItems);

                if (_pagerCfg.ShowMorePagerItems)
                {
                    AddMoreAfter(pagerItems);
                }

                if (_pagerCfg.AlwaysShowFirstLastPageNumber && _endPageIndex < _totalPageCount)
                {
                    pagerItems.Add(new PagerItem(_totalPageCount.ToString(), _totalPageCount, false,
                        PagerItemType.NumericPage));
                }
            }

            if (_pagerCfg.ShowPrevNext)
            {
                AddNext(pagerItems);
            }

            if (_pagerCfg.ShowFirstLast)
            {
                AddLast(pagerItems);
            }

            var sb = new StringBuilder();
            if (_pagerCfg.UseAjax)
            {
                foreach (PagerItem item in pagerItems)
                {
                    sb.Append(GenerateJqAjaxPagerElement(item));
                }
            }
            else
            {
                foreach (PagerItem item in pagerItems)
                {
                    sb.Append(GeneratePagerElement(item));
                }
            }
            var tb = new TagBuilder(_pagerCfg.ContainerTagName);
            if (!string.IsNullOrEmpty(_pagerCfg.Id))
            {
                tb.GenerateId(_pagerCfg.Id);
            }
            if (!string.IsNullOrEmpty(_pagerCfg.CssClass))
            {
                tb.AddCssClass(_pagerCfg.CssClass);
            }

            // 处理水平对齐
            if (!string.IsNullOrEmpty(_pagerCfg.HorizontalAlign))
            {
                string strAlign = "text-align:" + _pagerCfg.HorizontalAlign.ToLower();
                if (_htmlAttributes == null)
                {
                    _htmlAttributes = new RouteValueDictionary { { "style", strAlign } };
                }
                else
                {
                    if (_htmlAttributes.Keys.Contains("style"))
                    {
                        _htmlAttributes["style"] += ";" + strAlign;
                    }
                }
            }
            tb.MergeAttributes(_htmlAttributes, true);

            // 添加JS脚本
            string pagerScript = string.Empty;
            if (_pagerCfg.ShowPageIndexBox)
            {
                sb.Append(BuildGoToPageSection(ref pagerScript));
            }
            else
            {
                sb.Length -= _pagerCfg.SeparatorHtml.Length;
            }
            tb.InnerHtml = sb.ToString();
            if (!string.IsNullOrEmpty(pagerScript))
            {
                pagerScript = "<script type=\"text/javascript\">//<![CDATA[\r\n" + pagerScript + "\r\n//]]>\r\n</script>";
            }

            // 添加默认的样式
            StringBuilder pagerCssCfg = new StringBuilder();
            pagerCssCfg.Append(" \r\n + ' .mvcPager {  } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager a:link { color:#000; text-decoration:none; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager a:visited { color:#000; text-decoration:none; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager a:hover { color:orange; text-decoration:none; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager a[disabled=disabled] { color:#9A9A9A; text-decoration:none; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager input[type=text] { width:50px; height:24px; line-height:24px;margin-left:5px; margin-right:5px; text-align:center; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager select { width:50px; height:30px; line-height:30px; margin-left:5px; margin-right:5px; text-align:center; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager input[type=button] { width:50px; height:30px; margin-left:5px; margin-right:5px; text-align:center; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager .pages { padding:10px; background:#fff; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager .page_item { padding:8px; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager .current_page_item { padding:8px; color:#000; margin-left:10px; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager .page_number_item { padding:8px; margin-left:10px; border:1px solid #ccc;" +
                " border-top-left-radius: 5px; border-top-right-radius: 5px; border-bottom-right-radius: 5px; border-bottom-left-radius: 5px; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager .page_nav_item { padding:8px; background:#E4E4E4; margin-left:10px; " +
                "border-top-left-radius: 5px; border-top-right-radius: 5px;border-bottom-right-radius: 5px; border-bottom-left-radius: 5px; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager .page_input_item { padding:0px; margin-right:10px; } ' ");
            pagerCssCfg.Append(" \r\n + ' .mvcPager .page_goto_item { padding:0px; margin-left:10px; } ' ");

            var pagerCss = "<style type=\"text/css\" id=\"mvcPagerCss\">'" + pagerCssCfg + " + '</style>";

            StringBuilder pagerCssJs = new StringBuilder();
            pagerCssJs.Append("<script type=\"text/javascript\">//<![CDATA[\r\n $(document).ready(function() { ");
            pagerCssJs.Append(" if($('#mvcPagerCss').length == 0) { $('head').append('" + pagerCss + "'); } ");
            pagerCssJs.Append("}); \r\n//]]>\r\n</script> ");

            pagerScript += pagerCssJs.ToString();

            return MvcHtmlString.Create(pagerScript + tb.ToString(TagRenderMode.Normal));
        } 
    }
}
 