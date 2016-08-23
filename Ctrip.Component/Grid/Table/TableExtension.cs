namespace Ctrip.Component
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Ctrip.Component;

    public static class TableExtension
    {
        // Methods
        public static MvcHtmlString Table<T>(this HtmlHelper helper, List<TableColumn> columns, Options options, IEnumerable<T> dataSrc)
        {
            return MvcHtmlString.Create(TableBuilder.CreateTableHtml<T>(options, columns, dataSrc));
        }
    }
}

