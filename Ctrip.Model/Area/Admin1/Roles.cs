
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Ctrip.Model
{
    public class RolesM
    {
        public int ID { get; set; }

        [UIHint("Ternplate A")]
        [UIHint("Ternplate B", "Mvc")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 元数据
    /// </summary>
    public class DemoModel
    {
        [HiddenInput]        
        public string Foo { get; set; }

        [UIHint("Template A")]
        [UIHint("Template B", "Mvc")]
        [HiddenInput(DisplayValue = false)]
        [DisplayName("Bar")]
        public string Bar { get; set; }

        [UIHint("Template A")]
        [UIHint("Template B")]
        [DisplayName("baz")]
        [AllowHtml]
        [DisplayText]
        [Display(Name = "BAZ", Description = "Desc",ShortName = "B", Prompt = "Watermark...", Order = 999)]
        public string Baz { get; set; }

        [DisplayName("是否兼职")]
        public bool IsPartTime { get; set; }

        [UIHint("EmailAddress")]
        public string Foo1 { get; set; }

        [UIHint("Hiddenlnput")]
        public string Foo2 { get; set; }

        [UIHint("String")]
        public string Foo3 { get; set; }

        [UIHint ("Text") ]
        public string Bar3 { get; set; }

        [UIHint("Html")]
        public string Foo4 { get; set; }

        [UIHint("Url") ]
        public string Foo5 { get; set; }

        [UIHint("MultilineText") ]
        public string Foo6 { get; set; }

        [UIHint("Password")]
        public string Foo7 { get; set; }

        [UIHint("Decimal")]
        public string Foo8 { get; set; }

        [UIHint("Collection") ]
        public object[] Foo9 { get; set; }

        [DisplayName(" 省")]
        public string Province{ get; set; }

        [DisplayName(" 市")]
        public string City { get; set; }

        [DisplayName(" 区")]
        public string District { get; set; }

        [DisplayName(" 街道")]
        public string Street { get; set; }
    }

    /// <summary>
    /// 对应着7 个相应的Http报头。
    /// </summary>
    public class CommonHttpHeaders
    {
        public string Connection { get; set; }
        public string Accept { get; set; }
        public string AcceptCharset { get; set; }
        public string AcceptEncoding { get; set; }
        public string AcceptLanguage { get; set; }
        public string Host { get; set; }
        public string UserAgent { get; set; }
    }

    public class Foo { }

    [ModelBinder(typeof(BarModelBinder))]
    public class Bar { }

    public class Baz { }
}