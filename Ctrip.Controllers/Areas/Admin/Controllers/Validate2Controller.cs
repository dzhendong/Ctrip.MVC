using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Ctrip.Model;
using Ctrip.Model._Model._03Validate3;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// 为了能够将验证特性应用于Action 方法的参数
    /// </summary>
    public class Validate2Controller : Controller
    {
        protected override IActionInvoker CreateActionInvoker()
        {
            IActionInvoker actionlnvoker = base.CreateActionInvoker();

            if (actionlnvoker is ControllerActionInvoker)
            {
                return new ParameterValidationActionlnvoker();
            }
            else
            {
                return new ParameterValidationAsyncActionlnvoker();
            }
        }
        
        public ActionResult Add(
            [Display(Name = "第1个操作数")]
            [Range(10, 20, ErrorMessage = "{O} 必须在{ 1 }和{2 }之间! ")]
            [ModelBinder(typeof(ParameterValidationModelBinder))]
            double operandl,
            [Display(Name = "第2 个操作数")]
            [Range(10, 20, ErrorMessage = "{O} 必须在{1}和{2 }之间! ")]
            [ModelBinder(typeof(ParameterValidationModelBinder))]
            double operand2)
        {
            double result = 0.00;
            if (ModelState.IsValid)
            {
                result = operandl + operand2;
            }
            return View(new OperationData
            {
                Operandl = operandl,
                Operand2 = operand2,
                Operator = "Add",
                Result = result
            });
        }


        public ActionResult Index()
        {
            return View(new Employee5());
        }

        [HttpPost]
        public ActionResult Index(Employee5 employee)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "验证成功");
                return View(new Employee5());
            }
            else
            {
                return View(new Employee5());
            }
        }
    }
}
