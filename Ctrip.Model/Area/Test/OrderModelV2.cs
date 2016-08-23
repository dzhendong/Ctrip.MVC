using System;
using System.Collections.Generic;
using Ctrip.Component;

namespace Ctrip.Model
{
    public class OrderModelV2
    {
        public string OrderId { get; set; }
        public string Code { get; set; }
        public string OType { get; set; }
        public string OrderInfo { get; set; }
        public string CommissionRate { get; set; }
        public string LinkAction { get; set; }
        public string ImgSrc { get; set; }
        public Int32 EnableAdd { get; set; }
        public Int32 EnableEdit { get; set; }
        public Int32 EnableDel { get; set; }
        public Int32 OrderAmount { get; set; }
        public Int32 PaidAmount { get; set; }
        public Double CtripInvoice { get; set; }
        public float ShopInvoice { get; set; }
        public decimal CustomerInvoice { get; set; }
        public DateTime? OrderTime { get; set; }

        public object GetValue(string propertyName)
        {
            return this.GetType().GetProperty(propertyName).GetValue(this, null);
        }
    }

    public class OrderListModelV2
    {
        public List<OrderModelV2> orders { get; set; }
        public List<TableColumn> columns { get; set; }
        public Options options { get; set; }

        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
    }
}