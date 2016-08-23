using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Mvc;
using Ctrip.Model;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// http://localhost:52809/Admin/Home/Index
    /// </summary>
    public class Bind1Controller : Controller
    {
        public ActionResult Bind1([Bind(Prefix = "HomeAddress")]Address2 address)
        {
            return View(address);
        }	

        /// <summary>
        /// 不查找或只查找某个属性
        /// </summary>    
        public ActionResult Bind2([Bind(Prefix = "HomeAddress", Exclude = "Country")]Address2 address)
        {
		    return View(address);
	    }

        /// <summary>
        /// 返回指定前缀的Key
        /// </summary>
        public ActionResult Bind3()
        {
            NameValueCollection datasource = new NameValueCollection();
            datasource.Add("foo.Name", "Foo");
            datasource.Add("foo.PhoneNo", "123456789");
            datasource.Add("foo.EmailAddress", "Foo@gmail.com");

            datasource.Add("foo.Address.Province", "江苏");
            datasource.Add("foo.Address.City", "苏州");
            datasource.Add("foo.Address.District", "工业园区");
            datasource.Add("foo.Address.Street", "星湖街328 号");

            NameValueCollectionValueProvider valueProvider =
                new NameValueCollectionValueProvider(datasource, CultureInfo.InvariantCulture);          

            return View(valueProvider);
        }

        /// <summary>
        /// 返回指定前缀的Key
        /// </summary>
        public ActionResult Bind4()
        {          
            NameValueCollection datasource = new NameValueCollection();
            datasource.Add("first[0].Narne", "Foo");
            datasource.Add("first[0].PhoneNo", "123456789");
            datasource.Add("first[0].ErnailAddress", "Foo@grnail.com");
            datasource.Add("first[1].Narne", "Bar");
            datasource.Add("first[1].PhoneNo", "987654321");
            datasource.Add("first[1].ErnailAddress", "Bar@grnail.com");

            NameValueCollectionValueProvider valueProvider =
            new NameValueCollectionValueProvider(datasource,
            CultureInfo.InvariantCulture);

            return View(valueProvider);
        }

        public void Bind5()
         {
             NameValueCollection datasource = new NameValueCollection();
             datasource.Add("first[0].Name", "Foo");
             datasource.Add("first[0].PhoneNo", "123456789");
             datasource.Add("first[0].EmailAddress", "Foo@gmail.com");
  
             datasource.Add("first[1].Name", "Bar");
             datasource.Add("first[1].PhoneNo", "987654321");
             datasource.Add("first[1].EmailAddress", "Bar@gmail.com");
             NameValueCollectionValueProvider valueProvider = new NameValueCollectionValueProvider(datasource, CultureInfo.InvariantCulture);
  
             var keyDictionary = valueProvider.GetKeysFromPrefix("first");
             Response.Write("first<br/>");
             foreach (var item in keyDictionary)
             {
                 Response.Write(string.Format("{0}: {1}<br/>", item.Key, item.Value));
             }
  
             keyDictionary = valueProvider.GetKeysFromPrefix("first[0]");
             Response.Write("<br/>first[0]<br/>");
             foreach (var item in keyDictionary)
             {
                 Response.Write(string.Format("{0}: {1}<br/>", item.Key, item.Value));
             }
  
             keyDictionary = valueProvider.GetKeysFromPrefix("first[1]");
             Response.Write("<br/>first[1]<br/>");
             foreach (var item in keyDictionary)
             {
                 Response.Write(string.Format("{0}: {1}<br/>", item.Key, item.Value));
             }
         }

        public ActionResult Factory1(CommonHttpHeaders headers)
        {             
            return View(headers);            
        }

        public ActionResult ChildAction1()
        {
            ControllerContext.RouteData.Values["Foo"] = "abc";
            ControllerContext.RouteData.Values["Bar"] = "ijk";
            ControllerContext.RouteData.Values["Baz"] = "xyz";

            ChildActionValueProvider valueProvider = new ChildActionValueProvider(ControllerContext);

            return View(valueProvider);
        }

        /// <summary>
        /// FooModelBinder 和 BarModelBinder通过ModelBinderAttribute 特性分别应用到参数和参数类型上
        /// 参数b但则会采用默认的DefaultModelBinder 进行绑定
        /// 全局注册
        /// Application_Start
        /// ModelBinders.Binders.Add(typeof(Baz) , new BazModelBinder());
        /// </summary>
        /// <returns></returns>
        public ActionResult Binder1()
        {            
            ControllerDescriptor controllerDescriptor =
            new ReflectedControllerDescriptor(typeof(BaseController));
            ActionDescriptor actionDescriptor = controllerDescriptor. FindAction (ControllerContext, "DemoAction");            
            return View(actionDescriptor);
        }

        public void DemoAction([ModelBinder(typeof(FooModelBinder))]Foo foo,Bar bar,Baz baz)
        { 
        }        

        public ActionResult State1()
        {
            Address address = new Address
            {
                Province = "江苏",
                City = "苏州",
                District = "工业区",
                Street = "街道"
            };

            Contact contact = new Contact
            {
                Name = "张三",
                PhoneNo = "1111111111",
                EmailAddress = "111@11.com",
                Address = address
            };
            
            return View(contact);
        }

        [HttpPost]
        public ActionResult State1(Contact contact)
        {
            return View("ModelState", this.ViewData.ModelState);
        }

        /// <summary>
        /// 绑定到数组
        /// </summary>  
        public ActionResult Array1(string[] names)
        {
            names = names ?? new string[0];
            return View(names);
        }

        /// <summary>
        /// 绑定到集合
        /// </summary>
        public ActionResult List1(IList<Address> addresses)
        {
            addresses = addresses ?? new List<Address>();
            return View(addresses);
        }
    }
}