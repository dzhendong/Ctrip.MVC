using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// MVC中用ajax提交json对象数组
    /// MVC 已经自定义了 JsonValueProviderFactory
    /// 可以满足Json的需要
    /// Application_Start
    /// ValueProviderFactories.Factories.Add（new JsonArrayValueProviderFactory（））;
    /// </summary>
    public class JsonArrayValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            var formValues = controllerContext.HttpContext.Request.Form;

            var values = new Dictionary<string, object>();
            Regex regex = new Regex(@"\[[A-Za-z]*\]");
            for (int i = 0; i < formValues.Count; i++)
            {
                string key = formValues.Keys[i];
                var matchs = regex.Matches(key);
                if (matchs.Count > 0)
                {
                    foreach (Match match in matchs)
                    {
                        if (match.Value != "[]")
                            key = key.Replace(match.Value, "." + match.Value.Trim('[', ']'));
                    }
                    if (key.EndsWith("[]"))
                        values.Add(key.Replace("[]", ""), formValues.GetValues(i));
                    else
                        values.Add(key, formValues[i]);
                }
            }

            return new DictionaryValueProvider<object>(values, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}