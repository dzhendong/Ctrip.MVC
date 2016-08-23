
namespace Ctrip.Component.Layer
{
    public interface IOrderAnticorrsive
    {
        //依赖注入接口是完全为了将控制器与防腐对象之间隔离用的
        void SetController(OrderController orderController);

        OrderViewModel GetOrderViewModel(long oId);
    }
}