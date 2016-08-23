namespace Ctrip.Component
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TableBuilder
    {
        // Methods
        public static string CreateTableHtml<T>(Options tableConfig, IEnumerable<TableColumn> columns, IEnumerable<T> dataSrc)
        {
            StringBuilder builder = new StringBuilder();
            new Dictionary<string, string>();
            if (((tableConfig == null) || (dataSrc == null)) || (dataSrc.Count<T>() == 0))
            {
                return string.Empty;
            }
            if (tableConfig.CurrenTableStyle == null)
            {
                tableConfig.CurrenTableStyle = new TableStyle();
            }
            Options currentStyle = tableConfig.CurrenTableStyle.GetCurrentStyle(tableConfig, columns.Count<TableColumn>());
            builder.Append("<table id=\"" + currentStyle.TableID + "\" cellpadding=\"" + currentStyle.CurrenTableStyle.Cellpadding.ToString() + "\" cellspacing=\"" + currentStyle.CurrenTableStyle.Cellspacing.ToString() + "\" width=\"" + currentStyle.CurrenTableStyle.GetWidth() + "\" height=\"" + currentStyle.CurrenTableStyle.GetHeight() + "\" class=\"" + currentStyle.CurrenTableStyle.TableClassName + "\" style=\"" + currentStyle.CurrenTableStyle.TableInlineStyle + "border:1px solid #ddd;border-right: 0px;border-top:0px;border-collapse: separate;-webkit-border-radius: 4px;-moz-border-radius: 4px;border-radius: 4px;\">");
            if ((columns != null) && (columns.Count<TableColumn>() > 0))
            {
                Dictionary<string, List<TableColumn>> targetColunms = GetTargetColunms(columns);
                builder.Append(CreateTHeadHtml(currentStyle, targetColunms));
                builder.Append(CreateTBodyHtml<T>(currentStyle, dataSrc, targetColunms));
            }
            builder.Append("</table>");
            return builder.ToString();
        }

        public static string CreateTBodyHtml<T>(Options curConfig, IEnumerable<T> dataSrc, Dictionary<string, List<TableColumn>> targetCols)
        {
            IEnumerable<T> enumerable;
            StringBuilder builder = new StringBuilder();
            builder.Append("<tbody id=\"" + curConfig.CurrenTableStyle.TBodyID + "\" style=\"" + curConfig.CurrenTableStyle.TbodyInlineStyleStr + "\" class=\"" + curConfig.CurrenTableStyle.TbodyClassName + "\">");
            if ((dataSrc.Count<T>() > curConfig.RowNum) && (curConfig.RowNum > 0))
            {
                enumerable = dataSrc.Take<T>(curConfig.RowNum);
            }
            else
            {
                enumerable = dataSrc;
            }
            int num = 0;
            foreach (T local in enumerable)
            {
                num++;
                if (curConfig.CurrenTableStyle.IsHighLight && ((num % 2) == 0))
                {
                    builder.Append("<tr style=\"background-color:#ccebf8;\">");
                }
                else
                {
                    builder.Append("<tr>");
                }
                foreach (KeyValuePair<string, List<TableColumn>> pair in targetCols)
                {
                    string srcStr = string.Empty;
                    if ((pair.Value.Count > 0) && (pair.Value.Count < 2))
                    {
                        TableColumn columnItem = pair.Value[0];
                        if (CommonHelper.IsConstValue(columnItem.ColumnDataMember))
                        {
                            srcStr = CommonHelper.GetValueByName(columnItem.ColumnDataMember);
                        }
                        else if ((local.GetType() != null) && (local.GetType().GetProperty(columnItem.ColumnDataMember) != null))
                        {
                            srcStr = (local.GetType().GetProperty(columnItem.ColumnDataMember).GetValue(local, null) == null) ? string.Empty : local.GetType().GetProperty(columnItem.ColumnDataMember).GetValue(local, null).ToString();
                        }
                        if (columnItem.IsVisible)
                        {
                            builder.Append("<td  id=\"td_" + curConfig.TableID + "__" + (CommonHelper.IsConstValue(columnItem.ColumnDataMember) ? "" : columnItem.ColumnDataMember) + "\" title=\"" + srcStr + "\" ");
                            string str2 = GenerateColuumStyle(curConfig, "td_" + curConfig.TableID + "_" + columnItem.ColumnDataMember);
                            builder.Append("style=\"border-right:solid 1px #CCCCCC;border-top:solid 1px #CCCCCC;text-align: center;vertical-align: middle;");
                            if (!string.IsNullOrEmpty(str2))
                            {
                                if (str2.Contains("style") && (str2.Length > 6))
                                {
                                    builder.Append(str2.Split(new char[] { '=' })[1]);
                                    builder.Append("\"");
                                }
                                else
                                {
                                    builder.Append("\"");
                                    builder.Append(str2);
                                }
                            }
                            else
                            {
                                builder.Append("\"");
                            }
                            builder.Append(">");
                            builder.Append(Formater.Format(curConfig.TableID, columnItem, srcStr, local));
                            builder.Append("</td>");
                        }
                        continue;
                    }
                    string str3 = string.Empty;
                    foreach (TableColumn column2 in pair.Value)
                    {
                        if (CommonHelper.IsConstValue(column2.ColumnDataMember))
                        {
                            srcStr = CommonHelper.GetValueByName(column2.ColumnDataMember);
                        }
                        else if ((local.GetType() != null) && (local.GetType().GetProperty(column2.ColumnDataMember) != null))
                        {
                            srcStr = (local.GetType().GetProperty(column2.ColumnDataMember).GetValue(local, null) == null) ? string.Empty : local.GetType().GetProperty(column2.ColumnDataMember).GetValue(local, null).ToString();
                        }
                        str3 = str3 + Formater.Format(curConfig.TableID, column2, srcStr, local);
                    }
                    TableColumn column3 = pair.Value[0];
                    if (column3.IsVisible)
                    {
                        builder.Append("<td id=\"td_" + curConfig.TableID + "__" + (CommonHelper.IsConstValue(column3.ColumnDataMember) ? "" : column3.ColumnDataMember) + "\" title=\"" + srcStr + "\" ");
                        string str4 = GenerateColuumStyle(curConfig, "td_" + curConfig.TableID + "_" + column3.ColumnDataMember);
                        builder.Append("style=\"border-right:solid 1px #CCCCCC;border-top:solid 1px #CCCCCC;text-align: left;vertical-align: middle;");
                        if (!string.IsNullOrEmpty(str4))
                        {
                            if (str4.Contains("style") && (str4.Length > 6))
                            {
                                builder.Append(str4.Split(new char[] { '=' })[1]);
                                builder.Append("\"");
                            }
                            else
                            {
                                builder.Append("\"");
                                builder.Append(str4);
                            }
                        }
                        else
                        {
                            builder.Append("\"");
                        }
                        builder.Append(">");
                        builder.Append(str3);
                        builder.Append("</td>");
                    }
                }
                builder.Append("</tr>");
            }
            builder.Append("</tbody>");
            return builder.ToString();
        }

        public static string CreateTHeadHtml(Options curConfig, Dictionary<string, List<TableColumn>> targetCols)
        {
            StringBuilder builder = new StringBuilder();
            TableStyle currenTableStyle = curConfig.CurrenTableStyle;
            currenTableStyle.TheadInlineStyle = currenTableStyle.TheadInlineStyle + (curConfig.CurrenTableStyle.IsHeadVisible ? "" : "display: none;");
            builder.Append("<thead id=\"" + curConfig.CurrenTableStyle.TheadID + "\" style=\"" + curConfig.CurrenTableStyle.TheadInlineStyle + "\" class=\"" + curConfig.CurrenTableStyle.TheadClassName + "\">");
            foreach (KeyValuePair<string, List<TableColumn>> pair in targetCols)
            {
                if (!string.IsNullOrEmpty(pair.Key))
                {
                    TableColumn column = pair.Value[0];
                    if (column.IsVisible)
                    {
                        builder.Append("<th ");
                        builder.Append("id=\"th_" + curConfig.TableID + "_" + (CommonHelper.IsConstValue(column.ColumnDataMember) ? "" : column.ColumnDataMember) + "\" data-value=\"" + column.ColumnDataMember + "\" title=\"" + pair.Key + "\" ");
                        builder.Append(" style=\"" + column.ThInlineStyle + "border-right:solid 1px #CCCCCC;border-top:solid 1px #CCCCCC;\" class=\"" + column.ThClassName + "\"");
                        builder.Append(string.Concat(new object[] { " width=\"", column.THWidth, curConfig.CurrenTableStyle.GetStyleUnit(), "\" " }));
                        builder.Append(">");
                        builder.Append(pair.Key);
                        builder.Append("</th>");
                    }
                }
            }
            builder.Append("</thead>");
            return builder.ToString();
        }

        public static string GenerateColuumStyle(Options curConfig, string targetValue)
        {
            StringBuilder builder = new StringBuilder();
            if (curConfig.customizedStyleCount > 0)
            {
                foreach (StyleParameter parameter in curConfig.GetCustomizedStyle)
                {
                    if (parameter.Name.Equals(targetValue))
                    {
                        builder.Append("style=" + parameter.Value);
                    }
                }
            }
            if (curConfig.customizedClassCount > 0)
            {
                foreach (StyleParameter parameter2 in curConfig.GetCustomizedClass)
                {
                    if (parameter2.Name.Equals(targetValue))
                    {
                        builder.Append("class=" + parameter2.Value);
                    }
                }
            }
            return builder.ToString().Trim();
        }

        public static Dictionary<string, List<TableColumn>> GetTargetColunms(IEnumerable<TableColumn> columns)
        {
            Dictionary<string, List<TableColumn>> dictionary = new Dictionary<string, List<TableColumn>>();
            foreach (TableColumn column in columns)
            {
                if ((column != null) && !string.IsNullOrEmpty(column.Caption))
                {
                    if (!dictionary.ContainsKey(column.Caption))
                    {
                        if (!string.IsNullOrEmpty(column.ColumnDataMember))
                        {
                            dictionary.Add(column.Caption, new List<TableColumn> { column });
                        }
                    }
                    else if (column is ButtonColumn)
                    {
                        ButtonColumn column2 = (ButtonColumn)column;
                        if (!string.IsNullOrEmpty(column2.EnableButtonValue))
                        {
                            List<TableColumn> list2 = dictionary[column.Caption];
                            list2.Add(column);
                            dictionary[column.Caption] = list2;
                        }
                    }
                }
            }
            return dictionary;
        }
    }
}

