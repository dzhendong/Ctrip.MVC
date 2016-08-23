using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 继承 ValidationAttribute 抽象类，重写 IsValid() 方法，以实现服务端验证
    /// 实现 IClientValidatable 接口的 GetClientValidationRules() 方法，以实现客户端验证
    /// 浅谈在ASP.NET MVC3中使用IClientValidatable接口实现客户端和服务器端同时验证
    /// 例如：Range、RegularExpression、Required、StringLength等验证属性，这些属性极大的方便了服务器端的验证
    /// 同时我们还可以自定义验证属性来满足我们特殊的需求，MVC3的出现进一步提升了验证的便捷性，具体体现在
    /// 新增了IValidatableObject和IClientValidatable接口，
    /// 以及默认支持Range、RegularExpression、Required、StringLength等验证属性在客户端和服务器端进行验证。
    /// IValidatableObject接口，网上已经有很多的资料了，
    /// 今天我主要介绍IClientValidatable这个接口，
    /// IClientValidatable 接口允许 ASP.NET MVC 在运行时发现支持的客户端验证器，
    /// 这个接口被用来支持集成不同的验证框架——摘自：ASP.NET MVC3概述。
    /// 下面我们就来看看如何使用IClientValidatable接口来实现客户端和服务器端的验证。
    /// </summary>
    public class IntegerAttribute : ValidationAttribute, IClientValidatable
    {
        // 服务端验证逻辑，判断输入是否为整型
        public override bool IsValid(object value)
        {
            var number = Convert.ToString(value);
            return Regex.IsMatch(number, @"^[0-9]+$");
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessage,

                //ValidationType - 指定一个 key（字符串）
                //该 key 用于关联服务端验证逻辑与客户端验证逻辑。
                //注：这个 key 必须都是由小写字母组成
                ValidationType = "isinteger"
            };

            // 向客户端验证代码传递参数
            rule.ValidationParameters.Add("param1", "value1");
            rule.ValidationParameters.Add("param2", "value2");

            yield return rule;
        }
    }

    /// <summary>
    /// 为下拉列表框添加一个自定义验证规则
    /// [NotDefaultValue(InputString = "1")]/*如果用户选择为１，则验证不通过*/
    /// public IEnumerable<SelectListItem> Liker { get; set; }
    /// $.validator.addMethod('notdefaultvalue', function (value, element, param) {
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class NotDefaultValueAttribute : ValidationAttribute, IClientValidatable
    {
        public string InputString { get; set; }
        public NotDefaultValueAttribute()
        {
            ErrorMessage = "请选其中一项";
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                ValidationType = "notdefaultvalue",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            rule.ValidationParameters["inputstring"] = InputString;

            yield return rule;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string inputString = (string)value;

            if (inputString.Contains(InputString))
            {
                return false;
            }

            return true;
        }
    }
}