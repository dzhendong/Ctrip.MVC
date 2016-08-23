using Ctrip.Model;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Mvc;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    public class Bind2Controller : BaseController
    {
        protected override IValueProvider CreateValueProvider()
        {
            NameValueCollection dataSource = new NameValueCollection();

            //简单类型
            //dataSource.Add("Foo" , "ABC");
            //dataSource.Add("Bar" , "123");
            //dataSource.Add("Baz" , "456.01");
            //dataSource.Add("Qux" , "789.01");

            //复杂类型
            dataSource.Add("Name", "张三");
            dataSource.Add("PhoneNo", "123456789");
            dataSource.Add("EmailAddress", "zhangsan@gmail.com");
            dataSource.Add("Address. Province", "江苏");
            dataSource.Add("Address.City", "苏州'1");
            dataSource.Add("Address.District", "工业园区");
            dataSource.Add("Address.Street", "星湖街328 号");

            return new NameValueCollectionValueProvider(dataSource, CultureInfo.CurrentCulture);
        }

        public ActionResult Index()
        {
            return this.InvokeAction("DemoAction2");
        }

        /// <summary>
        /// 简单类型
        /// </summary>    
        public ActionResult DemoAction1(string foo, int bar, [Bind(Prefix = "qux")]double baz)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("foo", foo);
            parameters.Add("bar", bar);
            parameters.Add("baz", baz);

            return View("Index", parameters);
        }

        /// <summary>
        /// 复杂类型
        /// </summary>   
        public ActionResult DemoAction2(Contact foo, Contact bar)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("foo.Name", foo.Name);
            parameters.Add("foo.PhoneNo", foo.PhoneNo);
            parameters.Add("foo.EmailAddress", foo.EmailAddress);

            Address address = foo.Address;

            parameters.Add("foo.Address", string.Format("{O} 省{1 }市{2}{3}",
                address.Province,
                address.City,
                address.District, address.Street));

            parameters.Add("bar.Name", bar.Name);
            parameters.Add("bar.PhoneNo", bar.PhoneNo);
            parameters.Add("bar.EmailAddress", bar.EmailAddress);

            address = bar.Address;

            parameters.Add("bar.Address", string.Format("{O} 省{ 1 }市{2}{3}",
                address.Province,
                address.City,
                address.District,
                address.Street));

            return View("Index", parameters);
        }
    }
}
