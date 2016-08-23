namespace Ctrip.Component
{
    using System;

    public static class Formater
    {
        // Methods
        private static string ArryToStr(string[] array, object item)
        {
            string str = string.Empty;
            if ((array != null) && (array.Length > 0))
            {
                foreach (string str2 in array)
                {
                    str = str + ",'" + CommonHelper.GetValue(item, str2) + "'";
                }
                if (!string.IsNullOrEmpty(str))
                {
                    str = str.Remove(0, 1);
                }
            }
            return str;
        }

        public static string Format(string tableID, TableColumn columnItem, object item)
        {
            if (columnItem != null)
            {
                return Format(tableID, columnItem, columnItem.Caption, item);
            }
            return string.Empty;
        }

        public static string Format(string tableID, TableColumn columnItem, string srcStr, object item)
        {
            if ((columnItem == null) || string.IsNullOrEmpty(srcStr))
            {
                return string.Empty;
            }
            string str = srcStr;
            string valueByName = string.Empty;
            if (CommonHelper.IsConstValue(columnItem.ColumnDataMember))
            {
                valueByName = CommonHelper.GetValueByName(columnItem.ColumnDataMember);
            }
            else
            {
                valueByName = CommonHelper.GetValue(item, columnItem.ColumnDataMember);
            }
            if (columnItem is TextColumn)
            {
                TextColumn column = (TextColumn)columnItem;
                if ((column.TextLen > 0) && !string.IsNullOrEmpty(srcStr))
                {
                    return CommonHelper.Interception(srcStr, column.TextLen);
                }
                return srcStr;
            }
            if (columnItem is NumberColumn)
            {
                NumberColumn column2 = (NumberColumn)columnItem;
                if (!string.IsNullOrEmpty(column2.TextType))
                {
                    double result = 0.0;
                    if (double.TryParse(srcStr, out result))
                    {
                        return string.Format(column2.TextType, Convert.ToDouble(srcStr));
                    }
                }
                return srcStr;
            }
            if (columnItem is DateTimeColumn)
            {
                DateTimeColumn column3 = (DateTimeColumn)columnItem;
                if (!string.IsNullOrEmpty(column3.TextType) && column3.TextType.Contains("{"))
                {
                    return string.Format(column3.TextType, Convert.ToDateTime(srcStr));
                }
                return Convert.ToDateTime(srcStr).ToString(column3.TextType);
            }
            if (columnItem is ImgColumn)
            {
                ImgColumn column4 = (ImgColumn)columnItem;
                return ((("<img id=\"" + tableID + "_img_" + (CommonHelper.IsConstValue(column4.ColumnDataMember) ? "" : columnItem.ColumnDataMember) + "_" + column4.Order.ToString() + "\" src=\"" + CommonHelper.GetValue(item, column4.SrcValue) + "\"  style=\"" + (string.IsNullOrEmpty(column4.ImgInlineStyle) ? column4.ThInlineStyle : column4.ImgInlineStyle) + "\" class=\"" + (string.IsNullOrEmpty(column4.ImgkClassName) ? column4.ThClassName : column4.ImgkClassName) + "\" title=\"" + valueByName + "\"") + "onclick=\"" + GenerateFunctionName(new string[] { column4.OnClick }, ArryToStr(column4.EventParameter, item)) + "\"") + " />");
            }
            if (columnItem is LinkColumn)
            {
                LinkColumn column5 = (LinkColumn)columnItem;
                return ((("<a id=\"" + tableID + "_a_" + (CommonHelper.IsConstValue(column5.ColumnDataMember) ? "" : columnItem.ColumnDataMember) + "_" + column5.Order.ToString() + "\" target=\"" + CommonHelper.GetTargetName(column5.TargetName) + "\" href=\"" + CommonHelper.GetValue(item, column5.HrefValue) + "\" style=\"" + column5.LinkInlineStyle + "\" class=\"" + column5.LinkClassName + "\" ") + "onclick=\"" + GenerateFunctionName(new string[] { column5.OnClick }, ArryToStr(column5.EventParameter, item)) + "\"") + " >" + valueByName + "</a>");
            }
            if (!(columnItem is ButtonColumn))
            {
                return str;
            }
            ButtonColumn column6 = (ButtonColumn)columnItem;
            if (!string.IsNullOrEmpty(column6.EnableButtonValue) && (CommonHelper.GetValue(item, column6.EnableButtonValue) == "1"))
            {
                return ((("<a href=\"javascript:void(0);\" name=\"" + tableID + "_button_" + (CommonHelper.IsConstValue(column6.ColumnDataMember) ? "" : columnItem.ColumnDataMember) + "_" + column6.Order.ToString() + "\" style=\"" + column6.ButtonInlineStyle + "display:inline-block;margin-right:6px;\" class=\"" + column6.ButtonClassName + "\" value=\"" + valueByName + "\"") + "onclick=\"" + GenerateFunctionName(new string[] { column6.OnClick }, ArryToStr(column6.EventParameter, item)) + "\"") + " >" + valueByName + "</a>");
            }
            return "";
        }

        private static string GenerateFunctionName(string[] eventNames, string paras)
        {
            string str = string.Empty;
            if ((eventNames != null) && (eventNames.Length > 0))
            {
                foreach (string str2 in eventNames)
                {
                    if (!string.IsNullOrEmpty(str2) && (str2.IndexOf(":") >= 0))
                    {
                        string str3 = str;
                        str = str3 + ";" + str2.Split(new char[] { ':' })[0] + "(" + str2.Split(new char[] { ':' })[1] + ")";
                    }
                    else if (!string.IsNullOrEmpty(str2))
                    {
                        string str4 = str;
                        str = str4 + ";" + str2 + "(" + paras + ")";
                    }
                }
                if (!string.IsNullOrEmpty(paras))
                {
                    str = str.Remove(0, 1) + ";";
                }
            }
            return str;
        }
    }

}

