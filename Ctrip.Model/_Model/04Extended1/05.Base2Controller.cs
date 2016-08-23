using System.Web.Mvc;

namespace Ctrip.Model
{
   public abstract class Base2Controller: Controller
    {
        public Base2Controller()
        {
            this.ActionInvoker = new ExtendedControllerActionInvoker();
        }
    }
}
