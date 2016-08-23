using Ctrip.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// 元数据相关
    /// </summary>
    public class Validate1Controller : Controller
    {        
        public ActionResult Index()
        {
            ModelValidatorProvider validatorProvider = new DataErrorInfoModelValidatorProvider();
            return View(GetValidators(typeof(Contact), validatorProvider));
        }

        /// <summary>
        /// 探测CompositeModelValidator采用的验证行为
        /// </summary>        
        public ActionResult Index1()
        {
            Contact contact = GetContact();
            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => new Contact(), typeof(Contact));
            ModelValidator validator = ModelValidator.GetModelValidator(metadata, ControllerContext);
            return View(validator.Validate(contact));
        }

        public ActionResult Index2()
        {
            Contact contact = GetContact();
            return View(contact);
        }

        [HttpPost]
        public ActionResult Index2(Contact contact)
        {
            //一
            //return View(contact);

            //二
            return View("Message1",contact);
        }

        public ActionResult Index3(Contact contact)
        { 
            ModelState.AddModelError("Name" , "请输入姓名") ;
            ModelState.AddModelError("PhoneNo" , "请输入电话号码") ;
            ModelState.AddModelError("EmailAddress" , "请输入电子邮箱地址") ;
            ModelState.AddModelError("" , "系统发生异常，详细信息请与管理员联系") ;
            return View();
        }


        public ActionResult Index4()
        {
            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => new Employee5(), typeof(Employee5));
            ModelValidator validator = ModelValidator.GetModelValidator(metadata, ControllerContext);
            return View(validator);
        }

        /// <summary>
        /// 错误验证
        /// 生成的错误类
        /// </summary>
        private IEnumerable<ModelValidator> GetValidators(Type dataType,ModelValidatorProvider validatorProvider)
        {
            //ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(() => new Contact(), dataType);
            //ModelValidator validator = ModelValidator.GetModelValidator(metadata,ControllerContext);
            
            ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(null, dataType);

            foreach (var validator in validatorProvider.GetValidators(metadata,ControllerContext))
            {
                yield return validator;
            }

            foreach (var propertyMetadata in metadata.Properties)
            {
                foreach (var validator in validatorProvider.GetValidators(propertyMetadata, ControllerContext))
                { 
                    yield return validator;
                }
            }
        }

        private Contact GetContact()
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

            return contact;
        }
    }
}
