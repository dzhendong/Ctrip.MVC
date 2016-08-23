using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ctrip.Component;
using Ctrip.Model;

namespace Ctrip.MVC.Areas.Test.Controllers
{
    public class Grid1Controller : Controller
    {
       public ActionResult Index(int? pageNo)
        {
            if (pageNo == null || pageNo <= 0)
            {
                pageNo = 1;
            }

            var list = new OrderListModelV2();
            //配置
            List<TableColumn> columns = new List<TableColumn>();
            TextColumn col1 = new TextColumn();
            col1.Caption = "订单编号";
            col1.IsSort = false;
            col1.IsVisible = true;
            col1.Order = 2;
            col1.ColumnDataMember = "Code";
            col1.ThClassName = "";
            col1.ThInlineStyle = "";
            col1.TextLen = 15;            
            columns.Add(col1);

            TextColumn col3 = new TextColumn();
            col3.Caption = "订单ID";
            col3.IsSort = false;
            col3.IsVisible = true;
            col3.Order = 1;
            col3.ColumnDataMember = "{查看}";
            col3.ThClassName = "";
            col3.ThInlineStyle = "";

            columns.Add(col3);

            TextColumn col4 = new TextColumn();
            col4.Caption = "商户返卷";
            col4.IsSort = false;
            col4.IsVisible = true;
            col4.Order = 3;
            col4.ColumnDataMember = "CustomerInvoice";
            col4.ThClassName = "";
            col4.ThInlineStyle = "";
            col4.TextLen = 15;            

            columns.Add(col4);

            TextColumn col8 = new TextColumn();
            col8.Caption = "图片";
            col8.IsSort = false;
            col8.IsVisible = true;
            col8.Order = 8;
            col8.ColumnDataMember = "CommissionRate";
            col8.ThClassName = "";
            columns.Add(col8);

            TextColumn col5 = new TextColumn();
            col5.Caption = "用户返卷";
            col5.IsSort = false;
            col5.IsVisible = true;
            col5.Order = 4;
            col5.ColumnDataMember = "CommissionRate";
            col5.ThClassName = "";
            col5.ThInlineStyle = "";
            col5.TextLen = 15;
            
            columns.Add(col5);

            TextColumn col6 = new TextColumn();
            col6.Caption = "订单时间";
            col6.IsSort = false;
            col6.IsVisible = true;
            col6.Order = 5;
            col6.ColumnDataMember = "OrderTime";
            col6.ThClassName = "";
            col6.ThInlineStyle = "";

            columns.Add(col6);


            list.columns = columns;
            //设置样式
            list.options = new Options();
            list.options.CurrenTableStyle.IsHighLight = true;
            list.options.CurrenTableStyle.IsHeadVisible = true;

            //填充数据
            List<OrderModelV2> orders = new List<OrderModelV2>();
            Int32 index = 2;
            for (int i = 0; i < 11; i++)
            {
                OrderModelV2 order = new OrderModelV2();
                order.OrderId = "11111fd" + (index == 2 ? "b" : i.ToString());
                order.CommissionRate = "1000" + (index == 2 ? "b" : i.ToString());
                order.Code = pageNo + " - 10254617" + (index == 2 ? "b" : i.ToString());
                order.CtripInvoice = 200;
                order.CommissionRate = "100";
                order.CtripInvoice = 45.00;
                order.ShopInvoice = -56;
                order.CustomerInvoice = 145.00m;
                order.OrderAmount = 998;
                order.OType = "商户";
                order.EnableAdd = 1;
                order.EnableDel = 1;
                order.EnableEdit = 1;
                order.OrderInfo = "这只是测试，这只是测试" + (index == 2 ? "b" : i.ToString());
                order.OrderTime = Convert.ToDateTime("2014-05-06");
                order.LinkAction = "http://www.cnblogs.com";

                orders.Add(order);

            }
            OrderModelV2 order3 = new OrderModelV2();
            order3.OrderId = "3111fd";
            order3.CommissionRate = "130";
            order3.Code = "10rf617";
            order3.CtripInvoice = 200;
            order3.CommissionRate = "400";
            order3.CtripInvoice = 43.00;
            order3.ShopInvoice = -56;
            order3.CustomerInvoice = 145.00m;
            order3.OrderAmount = 998;
            order3.EnableAdd = 1;
            order3.EnableDel = 1;
            order3.EnableEdit = 0;
            order3.OType = "商户3";
            order3.OrderInfo = "这只是dd试，另外一条数据";
            order3.OrderTime = Convert.ToDateTime("2014-05-06");
            order3.LinkAction = "http://www.baidu.com";

            orders.Add(order3);

            OrderModelV2 order4 = new OrderModelV2();
            order4.OrderId = "2222";
            order4.CommissionRate = "1220";
            order4.Code = "10r227";
            order4.CtripInvoice = 200;
            order4.CommissionRate = "200";
            order4.CtripInvoice = 2.00;
            order4.ShopInvoice = -26;
            order4.CustomerInvoice = 125.00m;
            order4.OrderAmount = 28;
            order4.OType = "商户4";
            order4.EnableAdd = 0;
            order4.EnableDel = 1;
            order4.EnableEdit = 1;
            order4.OrderInfo = "这只是dd试，另外2数据";
            order4.OrderTime = Convert.ToDateTime("2014-05-06");
            order4.LinkAction = "http://www.baidu.com";
            order4.ImgSrc =
                "http://images.cnitblog.com/blog/327384/201309/29183952-02ad05eee45d444b8ad8206fb0ad6281.png";

            orders.Add(order4);
            list.orders = orders;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_GridDemo1", list);
            }
            return View(list);
        }

        public ActionResult Order2()
        {
            return View();
        }

        public ActionResult demo1(int? pageNo)
        {
            if (pageNo == null || pageNo <= 0)
            {
                pageNo = 1;
            }

            var list = new OrderListModelV2();
            //配置
            List<TableColumn> columns = new List<TableColumn>();
            TextColumn col1 = new TextColumn();
            col1.Caption = "订单编号";
            col1.IsSort = false;
            col1.IsVisible = true;
            col1.Order = 2;
            col1.ColumnDataMember = "Code";
            col1.ThClassName = "";
            col1.ThInlineStyle = "";
            col1.TextLen = 15;          
            columns.Add(col1);

            TextColumn col3 = new TextColumn();
            col3.Caption = "订单ID";
            col3.IsSort = false;
            col3.IsVisible = true;
            col3.Order = 1;
            col3.ColumnDataMember = "{查看}";
            col3.ThClassName = "";
            col3.ThInlineStyle = "";

            columns.Add(col3);

            TextColumn col4 = new TextColumn();
            col4.Caption = "商户返卷";
            col4.IsSort = false;
            col4.IsVisible = true;
            col4.Order = 3;
            col4.ColumnDataMember = "CustomerInvoice";
            col4.ThClassName = "";
            col4.ThInlineStyle = "";
            col4.TextLen = 15;
           
            columns.Add(col4);

            TextColumn col8 = new TextColumn();
            col8.Caption = "图片";
            col8.IsSort = false;
            col8.IsVisible = true;
            col8.Order = 8;
            col8.ColumnDataMember = "CommissionRate";
            col8.ThClassName = "";
            columns.Add(col8);

            TextColumn col5 = new TextColumn();
            col5.Caption = "用户返卷";
            col5.IsSort = false;
            col5.IsVisible = true;
            col5.Order = 4;
            col5.ColumnDataMember = "CommissionRate";
            col5.ThClassName = "";
            col5.ThInlineStyle = "";
            col5.TextLen = 15;
           
            columns.Add(col5);

            TextColumn col6 = new TextColumn();
            col6.Caption = "订单时间";
            col6.IsSort = false;
            col6.IsVisible = true;
            col6.Order = 5;
            col6.ColumnDataMember = "OrderTime";
            col6.ThClassName = "";
            col6.ThInlineStyle = "";

            columns.Add(col6);


            list.columns = columns;
            //设置样式
            list.options = new Options();
            list.options.CurrenTableStyle.IsHighLight = true;
            list.options.CurrenTableStyle.IsHeadVisible = true;

            //填充数据
            List<OrderModelV2> orders = new List<OrderModelV2>();
            Int32 index = 2;
            for (int i = 0; i < 11; i++)
            {
                OrderModelV2 order = new OrderModelV2();
                order.OrderId = "11111fd" + (index == 2 ? "b" : i.ToString());
                order.CommissionRate = "1000" + (index == 2 ? "b" : i.ToString());
                order.Code = pageNo + " - 10254617" + (index == 2 ? "b" : i.ToString());
                order.CtripInvoice = 200;
                order.CommissionRate = "100";
                order.CtripInvoice = 45.00;
                order.ShopInvoice = -56;
                order.CustomerInvoice = 145.00m;
                order.OrderAmount = 998;
                order.OType = "商户";
                order.EnableAdd = 1;
                order.EnableDel = 1;
                order.EnableEdit = 1;
                order.OrderInfo = "这只是测试，这只是测试" + (index == 2 ? "b" : i.ToString());
                order.OrderTime = Convert.ToDateTime("2014-05-06");
                order.LinkAction = "http://www.cnblogs.com";

                orders.Add(order);

            }
            OrderModelV2 order3 = new OrderModelV2();
            order3.OrderId = "3111fd";
            order3.CommissionRate = "130";
            order3.Code = "10rf617";
            order3.CtripInvoice = 200;
            order3.CommissionRate = "400";
            order3.CtripInvoice = 43.00;
            order3.ShopInvoice = -56;
            order3.CustomerInvoice = 145.00m;
            order3.OrderAmount = 998;
            order3.EnableAdd = 1;
            order3.EnableDel = 1;
            order3.EnableEdit = 0;
            order3.OType = "商户3";
            order3.OrderInfo = "这只是dd试，另外一条数据";
            order3.OrderTime = Convert.ToDateTime("2014-05-06");
            order3.LinkAction = "http://www.baidu.com";

            orders.Add(order3);

            OrderModelV2 order4 = new OrderModelV2();
            order4.OrderId = "2222";
            order4.CommissionRate = "1220";
            order4.Code = "10r227";
            order4.CtripInvoice = 200;
            order4.CommissionRate = "200";
            order4.CtripInvoice = 2.00;
            order4.ShopInvoice = -26;
            order4.CustomerInvoice = 125.00m;
            order4.OrderAmount = 28;
            order4.OType = "商户4";
            order4.EnableAdd = 0;
            order4.EnableDel = 1;
            order4.EnableEdit = 1;
            order4.OrderInfo = "这只是dd试，另外2数据";
            order4.OrderTime = Convert.ToDateTime("2014-05-06");
            order4.LinkAction = "http://www.baidu.com";
            order4.ImgSrc =
                "http://images.cnitblog.com/blog/327384/201309/29183952-02ad05eee45d444b8ad8206fb0ad6281.png";

            orders.Add(order4);
            list.orders = orders;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_GridDemo1", list);
            }
            return View(list);
        }

        public ActionResult demo2(int? pageNo)
        {
            if (pageNo == null || pageNo <= 0)
            {
                pageNo = 1;
            }

            var list = new OrderListModelV2();
            //配置
            List<TableColumn> columns = new List<TableColumn>();
            TextColumn col1 = new TextColumn();
            col1.Caption = "订单编号";
            col1.IsSort = false;
            col1.IsVisible = true;
            col1.Order = 2;
            col1.ColumnDataMember = "Code";
            col1.ThClassName = "";
            col1.ThInlineStyle = "";
            col1.TextLen = 15;           
            columns.Add(col1);

            LinkColumn col3 = new LinkColumn();
            col3.Caption = "订单ID";
            col3.IsSort = false;
            col3.IsVisible = true;
            col3.Order = 1;
            col3.ColumnDataMember = "{查看}";
            col3.ThClassName = "";
            col3.ThInlineStyle = "";
            col3.EventParameter = new string[] { "Code", "CommissionRate" };
            col3.HrefValue = "LinkAction";
            col3.LinkInlineStyle = "";
            col3.OnClick = "thisClickTest";

            columns.Add(col3);

            TextColumn col4 = new TextColumn();
            col4.Caption = "商户返卷";
            col4.IsSort = false;
            col4.IsVisible = true;
            col4.Order = 3;
            col4.ColumnDataMember = "CustomerInvoice";
            col4.ThClassName = "";
            col4.ThInlineStyle = "";
            col4.TextLen = 15;
           
            columns.Add(col4);

            ImgColumn col8 = new ImgColumn();
            col8.Caption = "图片";
            col8.IsSort = false;
            col8.IsVisible = true;
            col8.Order = 8;
            col8.ColumnDataMember = "CommissionRate";
            col8.ThClassName = "";
            col8.ThInlineStyle = "width:50px;height:40px;";
            col8.SrcValue = "ImgSrc";
            columns.Add(col8);

            TextColumn col5 = new TextColumn();
            col5.Caption = "用户返卷";
            col5.IsSort = false;
            col5.IsVisible = true;
            col5.Order = 4;
            col5.ColumnDataMember = "CommissionRate";
            col5.ThClassName = "";
            col5.ThInlineStyle = "";
            col5.TextLen = 15;
           
            columns.Add(col5);

            TextColumn col6 = new TextColumn();
            col6.Caption = "订单时间";
            col6.IsSort = false;
            col6.IsVisible = true;
            col6.Order = 5;
            col6.ColumnDataMember = "OrderTime";
            col6.ThClassName = "";
            col6.ThInlineStyle = "";

            columns.Add(col6);

            list.columns = columns;
            //设置样式
            list.options = new Options();
            list.options.CurrenTableStyle.IsHighLight = true;
            list.options.CurrenTableStyle.IsHeadVisible = true;


            //填充数据
            List<OrderModelV2> orders = new List<OrderModelV2>();
            Int32 index = 2;
            for (int i = 0; i < 11; i++)
            {
                OrderModelV2 order = new OrderModelV2();
                order.OrderId = "11111fd" + (index == 2 ? "b" : i.ToString());
                order.CommissionRate = "1000" + (index == 2 ? "b" : i.ToString());
                order.Code = pageNo + " - 10254617" + (index == 2 ? "b" : i.ToString());
                order.CtripInvoice = 200;
                order.CommissionRate = "100";
                order.CtripInvoice = 45.00;
                order.ShopInvoice = -56;
                order.CustomerInvoice = 145.00m;
                order.OrderAmount = 998;
                order.OType = "商户";
                order.EnableAdd = 1;
                order.EnableDel = 1;
                order.EnableEdit = 1;
                order.OrderInfo = "这只是测试，这只是测试" + (index == 2 ? "b" : i.ToString());
                order.OrderTime = Convert.ToDateTime("2014-05-06");
                order.LinkAction = "http://www.cnblogs.com";

                orders.Add(order);

            }
            OrderModelV2 order3 = new OrderModelV2();
            order3.OrderId = "3111fd";
            order3.CommissionRate = "130";
            order3.Code = "10rf617";
            order3.CtripInvoice = 200;
            order3.CommissionRate = "400";
            order3.CtripInvoice = 43.00;
            order3.ShopInvoice = -56;
            order3.CustomerInvoice = 145.00m;
            order3.OrderAmount = 998;
            order3.EnableAdd = 1;
            order3.EnableDel = 1;
            order3.EnableEdit = 0;
            order3.OType = "商户3";
            order3.OrderInfo = "这只是dd试，另外一条数据";
            order3.OrderTime = Convert.ToDateTime("2014-05-06");
            order3.LinkAction = "http://www.baidu.com";

            orders.Add(order3);

            OrderModelV2 order4 = new OrderModelV2();
            order4.OrderId = "2222";
            order4.CommissionRate = "1220";
            order4.Code = "10r227";
            order4.CtripInvoice = 200;
            order4.CommissionRate = "200";
            order4.CtripInvoice = 2.00;
            order4.ShopInvoice = -26;
            order4.CustomerInvoice = 125.00m;
            order4.OrderAmount = 28;
            order4.OType = "商户4";
            order4.EnableAdd = 0;
            order4.EnableDel = 1;
            order4.EnableEdit = 1;
            order4.OrderInfo = "这只是dd试，另外2数据";
            order4.OrderTime = Convert.ToDateTime("2014-05-06");
            order4.LinkAction = "http://www.baidu.com";
            order4.ImgSrc =
                "http://images.cnitblog.com/blog/327384/201309/29183952-02ad05eee45d444b8ad8206fb0ad6281.png";

            orders.Add(order4);
            list.orders = orders;

            if (Request.IsAjaxRequest())
            {
                //Thread.Sleep(2000);
                return PartialView("_GridDemo2", list);
            }
            return View(list);
        }

        public ActionResult demo3(int? pageNo)
        {
            if (pageNo == null || pageNo <= 0)
            {
                pageNo = 1;
            }

            var list = new OrderListModelV2();
            //配置
            List<TableColumn> columns = new List<TableColumn>();
            TextColumn col1 = new TextColumn();
            col1.Caption = "订单编号";
            col1.IsSort = false;
            col1.IsVisible = true;
            col1.Order = 2;
            col1.ColumnDataMember = "Code";
            col1.ThClassName = "";
            col1.ThInlineStyle = "";
            col1.TextLen = 15;            
            columns.Add(col1);

            LinkColumn col3 = new LinkColumn();
            col3.Caption = "订单ID";
            col3.IsSort = false;
            col3.IsVisible = true;
            col3.Order = 1;
            col3.ColumnDataMember = "{查看}";
            col3.ThClassName = "";
            col3.ThInlineStyle = "";
            col3.EventParameter = new string[] { "Code", "CommissionRate" };
            col3.HrefValue = "LinkAction";
            col3.LinkInlineStyle = "font-size:14;color:red;";
            col3.OnClick = "thisClickTest";

            columns.Add(col3);

            TextColumn col4 = new TextColumn();
            col4.Caption = "商户返卷";
            col4.IsSort = false;
            col4.IsVisible = true;
            col4.Order = 3;
            col4.ColumnDataMember = "CustomerInvoice";
            col4.ThClassName = "";
            col4.ThInlineStyle = "";
            col4.TextLen = 15;
           
            columns.Add(col4);

            ImgColumn col8 = new ImgColumn();
            col8.Caption = "图片";
            col8.IsSort = false;
            col8.IsVisible = true;
            col8.Order = 8;
            col8.ColumnDataMember = "CommissionRate";
            col8.ThClassName = "";
            col8.ThInlineStyle = "width:50px;height:40px;";
            col8.SrcValue = "ImgSrc";
            columns.Add(col8);

            TextColumn col5 = new TextColumn();
            col5.Caption = "用户返卷";
            col5.IsSort = false;
            col5.IsVisible = true;
            col5.Order = 4;
            col5.ColumnDataMember = "CommissionRate";
            col5.ThClassName = "";
            col5.ThInlineStyle = "";
            col5.TextLen = 15;
           
            columns.Add(col5);

            TextColumn col6 = new TextColumn();
            col6.Caption = "订单时间";
            col6.IsSort = false;
            col6.IsVisible = true;
            col6.Order = 5;
            col6.ColumnDataMember = "OrderTime";
            col6.ThClassName = "";
            col6.ThInlineStyle = "";

            columns.Add(col6);

            ButtonColumn col7 = new ButtonColumn();
            col7.Caption = "操作";
            col7.IsSort = false;
            col7.IsVisible = true;
            col7.Order = 5;
            col7.ColumnDataMember = "{新增}";
            col7.ThClassName = "";
            col7.ThInlineStyle = "";            
            col7.ButtonEvents = new string[] { "click", "focus" };
            col7.EventParameter = new string[] { "Code", "CommissionRate" };
            col7.OnClick = "testClick";
            columns.Add(col7);

            ButtonColumn col17 = new ButtonColumn();
            col17.Caption = "操作";
            col17.IsSort = false;
            col17.IsVisible = true;
            col17.Order = 5;
            col17.ColumnDataMember = "{编辑}";
            col17.ThClassName = "";
            col17.ThInlineStyle = "";            
            col17.ButtonEvents = new string[] { "click", "focus" };
            col17.EventParameter = new string[] { "Code", "CommissionRate" };
            col17.OnClick = "testClick7";
            columns.Add(col17);

            ButtonColumn col18 = new ButtonColumn();
            col18.Caption = "操作";
            col18.IsSort = false;
            col18.IsVisible = true;
            col18.Order = 5;
            col18.ColumnDataMember = "{删除}";
            col18.ThClassName = "";
            col18.ThInlineStyle = "";            
            col18.ButtonEvents = new string[] { "click", "focus" };
            col18.EventParameter = new string[] { "Code", "CommissionRate" };
            col18.OnClick = "testClick8";
            columns.Add(col18);

            list.columns = columns;
            //设置样式
            list.options = new Options();
            list.options.CurrenTableStyle.IsHighLight = true;
            list.options.CurrenTableStyle.IsHeadVisible = true;


            //填充数据
            List<OrderModelV2> orders = new List<OrderModelV2>();
            Int32 index = 2;
            for (int i = 0; i < 11; i++)
            {
                OrderModelV2 order = new OrderModelV2();
                order.OrderId = "11111fd" + (index == 2 ? "b" : i.ToString());
                order.CommissionRate = "1000" + (index == 2 ? "b" : i.ToString());
                order.Code = pageNo + " - 10254617" + (index == 2 ? "b" : i.ToString());
                order.CtripInvoice = 200;
                order.CommissionRate = "100";
                order.CtripInvoice = 45.00;
                order.ShopInvoice = -56;
                order.CustomerInvoice = 145.00m;
                order.OrderAmount = 998;
                order.OType = "商户";
                order.EnableAdd = 1;
                order.EnableDel = 1;
                order.EnableEdit = 1;
                order.OrderInfo = "这只是测试，这只是测试" + (index == 2 ? "b" : i.ToString());
                order.OrderTime = Convert.ToDateTime("2014-05-06");
                order.LinkAction = "http://www.cnblogs.com";

                orders.Add(order);

            }
            OrderModelV2 order3 = new OrderModelV2();
            order3.OrderId = "3111fd";
            order3.CommissionRate = "130";
            order3.Code = "10rf617";
            order3.CtripInvoice = 200;
            order3.CommissionRate = "400";
            order3.CtripInvoice = 43.00;
            order3.ShopInvoice = -56;
            order3.CustomerInvoice = 145.00m;
            order3.OrderAmount = 998;
            order3.EnableAdd = 1;
            order3.EnableDel = 1;
            order3.EnableEdit = 0;
            order3.OType = "商户3";
            order3.OrderInfo = "这只是dd试，另外一条数据";
            order3.OrderTime = Convert.ToDateTime("2014-05-06");
            order3.LinkAction = "http://www.baidu.com";

            orders.Add(order3);

            OrderModelV2 order4 = new OrderModelV2();
            order4.OrderId = "2222";
            order4.CommissionRate = "1220";
            order4.Code = "10r227";
            order4.CtripInvoice = 200;
            order4.CommissionRate = "200";
            order4.CtripInvoice = 2.00;
            order4.ShopInvoice = -26;
            order4.CustomerInvoice = 125.00m;
            order4.OrderAmount = 28;
            order4.OType = "商户4";
            order4.EnableAdd = 0;
            order4.EnableDel = 1;
            order4.EnableEdit = 1;
            order4.OrderInfo = "这只是dd试，另外2数据";
            order4.OrderTime = Convert.ToDateTime("2014-05-06");
            order4.LinkAction = "http://www.baidu.com";
            order4.ImgSrc =
                "http://images.cnitblog.com/blog/327384/201309/29183952-02ad05eee45d444b8ad8206fb0ad6281.png";

            orders.Add(order4);
            list.orders = orders;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_GridDemo3", list);
            }
            return View(list);
        }

        public ActionResult demo4(int? pageNo)
        {
            if (pageNo == null || pageNo <= 0)
            {
                pageNo = 1;
            }

            var list = new OrderListModelV2();
            //配置
            List<TableColumn> columns = new List<TableColumn>();
            TextColumn col1 = new TextColumn();
            col1.Caption = "订单编号";
            col1.IsSort = false;
            col1.IsVisible = true;
            col1.Order = 2;
            col1.ColumnDataMember = "Code";
            col1.ThClassName = "";
            col1.ThInlineStyle = "";
            col1.TextLen = 15;            
            columns.Add(col1);

            LinkColumn col3 = new LinkColumn();
            col3.Caption = "订单ID";
            col3.IsSort = false;
            col3.IsVisible = true;
            col3.Order = 1;
            col3.ColumnDataMember = "{查看}";
            col3.ThClassName = "";
            col3.ThInlineStyle = "";
            col3.EventParameter = new string[] { "Code", "CommissionRate" };
            col3.HrefValue = "LinkAction";
            col3.LinkInlineStyle = "font-size:14;color:red;";
            col3.OnClick = "thisClickTest";

            columns.Add(col3);

            TextColumn col4 = new TextColumn();
            col4.Caption = "商户返卷";
            col4.IsSort = false;
            col4.IsVisible = true;
            col4.Order = 3;
            col4.ColumnDataMember = "CustomerInvoice";
            col4.ThClassName = "";
            col4.ThInlineStyle = "";
            col4.TextLen = 15;
            
            columns.Add(col4);

            ImgColumn col8 = new ImgColumn();
            col8.Caption = "图片";
            col8.IsSort = false;
            col8.IsVisible = true;
            col8.Order = 8;
            col8.ColumnDataMember = "CommissionRate";
            col8.ThClassName = "";
            col8.ThInlineStyle = "width:50px;height:40px;";
            col8.SrcValue = "ImgSrc";
            columns.Add(col8);

            TextColumn col5 = new TextColumn();
            col5.Caption = "用户返卷";
            col5.IsSort = false;
            col5.IsVisible = true;
            col5.Order = 4;
            col5.ColumnDataMember = "CommissionRate";
            col5.ThClassName = "";
            col5.ThInlineStyle = "";
            col5.TextLen = 15;
            
            columns.Add(col5);

            TextColumn col6 = new TextColumn();
            col6.Caption = "订单时间";
            col6.IsSort = false;
            col6.IsVisible = true;
            col6.Order = 5;
            col6.ColumnDataMember = "OrderTime";
            col6.ThClassName = "";
            col6.ThInlineStyle = "";

            columns.Add(col6);

            ButtonColumn col7 = new ButtonColumn();
            col7.Caption = "操作";
            col7.IsSort = false;
            col7.IsVisible = true;
            col7.Order = 5;
            col7.ColumnDataMember = "{新增}";
            col7.ThClassName = "";
            col7.ThInlineStyle = "";
            col7.ButtonEvents = new string[] { "click", "focus" };
            col7.EventParameter = new string[] { "Code", "CommissionRate", "11" };
            col7.OnClick = "testClick";
            columns.Add(col7);

            ButtonColumn col17 = new ButtonColumn();
            col17.Caption = "操作";
            col17.IsSort = false;
            col17.IsVisible = true;
            col17.Order = 5;
            col17.ColumnDataMember = "{编辑}";
            col17.ThClassName = "";
            col17.ThInlineStyle = "";
            col17.ButtonEvents = new string[] { "click", "focus" };
            col17.EventParameter = new string[] { "Code", "CommissionRate" };
            col17.OnClick = "testClick7";
            columns.Add(col17);

            ButtonColumn col18 = new ButtonColumn();
            col18.Caption = "操作";
            col18.IsSort = false;
            col18.IsVisible = true;
            col18.Order = 5;
            col18.ColumnDataMember = "{删除}";
            col18.ThClassName = "";
            col18.ThInlineStyle = "";
            col18.ButtonEvents = new string[] { "click", "focus" };
            col18.EventParameter = new string[] { "Code", "CommissionRate" };
            col18.OnClick = "testClick8";
            columns.Add(col18);

            list.columns = columns;
            //设置样式
            list.options = new Options();
            list.options.CurrenTableStyle.IsHighLight = true;
            list.options.CurrenTableStyle.IsHeadVisible = true;
            //自定义样式
            Dictionary<string, string> thDic = new Dictionary<string, string>();
            thDic.Add("Code", "font-size:24px;color:blue;");
            list.options.SetStyle(EnumSetSytleType.Style, thDic);

            Dictionary<string, string> tbD6ic = new Dictionary<string, string>();
            tbD6ic.Add("OrderTime", "font-size:14px;color:#06c;font-weight:bold;");
            list.options.SetStyle(EnumSetSytleType.Style, tbD6ic);
            tbD6ic.Add("CommissionRate", "class2");
            list.options.SetStyle(EnumSetSytleType.Class, tbD6ic);

            Dictionary<string, string> tbdDic = new Dictionary<string, string>();
            tbdDic.Add("CustomerInvoice", "class3");
            list.options.SetStyle(EnumSetSytleType.Class, tbdDic);

            //填充数据
            List<OrderModelV2> orders = new List<OrderModelV2>();
            Int32 index = 2;
            for (int i = 0; i < 11; i++)
            {
                OrderModelV2 order = new OrderModelV2();
                order.OrderId = "11111fd" + (index == 2 ? "b" : i.ToString());
                order.CommissionRate = "1000" + (index == 2 ? "b" : i.ToString());
                order.Code = pageNo + " - 10254617" + (index == 2 ? "b" : i.ToString());
                order.CtripInvoice = 200;
                order.CommissionRate = "100";
                order.CtripInvoice = 45.00;
                order.ShopInvoice = -56;
                order.CustomerInvoice = 145.00m;
                order.OrderAmount = 998;
                order.OType = "商户";
                order.EnableAdd = 1;
                order.EnableDel = 1;
                order.EnableEdit = 1;
                order.OrderInfo = "这只是测试，这只是测试" + (index == 2 ? "b" : i.ToString());
                order.OrderTime = Convert.ToDateTime("2014-05-06");
                order.LinkAction = "http://www.cnblogs.com";

                orders.Add(order);

            }
            OrderModelV2 order3 = new OrderModelV2();
            order3.OrderId = "3111fd";
            order3.CommissionRate = "130";
            order3.Code = "10rf617";
            order3.CtripInvoice = 200;
            order3.CommissionRate = "400";
            order3.CtripInvoice = 43.00;
            order3.ShopInvoice = -56;
            order3.CustomerInvoice = 145.00m;
            order3.OrderAmount = 998;
            order3.EnableAdd = 1;
            order3.EnableDel = 1;
            order3.EnableEdit = 0;
            order3.OType = "商户3";
            order3.OrderInfo = "这只是dd试，另外一条数据";
            order3.OrderTime = Convert.ToDateTime("2014-05-06");
            order3.LinkAction = "http://www.baidu.com";

            orders.Add(order3);

            OrderModelV2 order4 = new OrderModelV2();
            order4.OrderId = "2222";
            order4.CommissionRate = "1220";
            order4.Code = "10r227";
            order4.CtripInvoice = 200;
            order4.CommissionRate = "200";
            order4.CtripInvoice = 2.00;
            order4.ShopInvoice = -26;
            order4.CustomerInvoice = 125.00m;
            order4.OrderAmount = 28;
            order4.OType = "商户4";
            order4.EnableAdd = 0;
            order4.EnableDel = 1;
            order4.EnableEdit = 1;
            order4.OrderInfo = "这只是dd试，另外2数据";
            order4.OrderTime = Convert.ToDateTime("2014-05-06");
            order4.LinkAction = "http://www.baidu.com";
            order4.ImgSrc =
                "http://images.cnitblog.com/blog/327384/201309/29183952-02ad05eee45d444b8ad8206fb0ad6281.png";

            orders.Add(order4);
            list.orders = orders;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_GridDemo4", list);
            }
            return View(list);
        }    
    }
}
