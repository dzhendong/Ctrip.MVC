using System;
using System.Web.Http;

namespace Ctrip.Component.Layer
{
    /// <summary>
    /// 显示逻辑
    /// </summary>
    public class OrderController : ApiController
    {
        private IOrderAnticorrsive orderAnticorrsive;

        public OrderController(IOrderAnticorrsive orderAnticorrsive)
        {
            this.orderAnticorrsive = orderAnticorrsive;

            //设置控制器到防腐对象中
            this.orderAnticorrsive.SetController(this);
        }

        public event EventHandler<OrderViewModel> SubmitOrderEvent;

        [HttpGet]
        public void SubmitOrder(OrderViewModel order)
        {
            this.SubmitOrderEvent(this, order);
        }
    }
}
