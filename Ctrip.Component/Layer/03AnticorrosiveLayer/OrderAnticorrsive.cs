using System.Collections.Generic;
using System.Linq;

namespace Ctrip.Component.Layer
{
    /// <summary>
    /// OrderViewModel 防腐对象
    /// 将服务的DTO与显示端的ViewModel之间的转换放入防腐层
    /// 1-转换逻辑过程化，直接写在防腐层的方法中
    /// 2-转换逻辑对象化，建立起封装、重用结构，防止进一步腐化
    /// </summary>
    public class OrderAnticorrsive : IOrderAnticorrsive
    {
        private readonly IOrderServiceClient orderServiceClient;

        private readonly IProductServiceClient productServiceClient;

        public OrderAnticorrsive(IOrderServiceClient orderServiceClient, IProductServiceClient productServiceClient)
        {
            this.orderServiceClient = orderServiceClient;
            this.productServiceClient = productServiceClient;
        }

        public OrderViewModel GetOrderViewModel(long oId)
        {
            var order = orderServiceClient.GetOrderByOid(oId);

            if (order == null && order.Items != null && order.Items.Count > 0) return null;

            var result = new OrderViewModel()
            {
                OId = order.OId,
                Address = order.Address,
                OName = order.OName,
                Items = new System.Collections.Generic.List<OrderItem>()
            };

            if (order.Items.Count == 1)
            {
                //调用单个获取商品接口
                var product = productServiceClient.GetProductByPid(order.Items[0].Pid);

                if (product != null)
                {
                    result.Items.Add(ConvertOrderItem(order.Items[0], product));
                }
            }
            else
            {
                List<long> pids = (from item in order.Items select item.Pid).ToList();

                //调用批量获取商品接口
                var products = productServiceClient.GetProductsByIds(pids);

                if (products != null)
                {
                    //批量转换OrderItem类型
                    result.Items = ConvertOrderItems(products, order.Items);
                }

            }

            return result;
        }

        private static OrderItem ConvertOrderItem(OrderItem orderItem, Product product)
        {
            if (product == null) return null;

            return new OrderItem()
            {
                Number = orderItem.Number,
                OitemId = orderItem.OitemId,
                Pid = orderItem.Pid,
                Price = orderItem.Price,

                Product = new Product()
                {
                    Pid = product.Pid,
                    PName = product.PName,
                    PGroup = product.PGroup,
                    Production = product.Production
                }
            };
        }

        private static List<OrderItem> ConvertOrderItems(List<Product> products, List<OrderItem> orderItems)
        {
            var result = new List<OrderItem>();

            orderItems.ForEach(item =>
            {
                var orderItem = ConvertOrderItem(item, products.Where(p => p.Pid == item.Pid).FirstOrDefault());
                if (orderItem != null)
                {
                    result.Add(orderItem);
                }
            });

            return result;
        }

        public void SetController(OrderController orderController)
        {
            //防腐层监听显示逻辑事件
            orderController.SubmitOrderEvent += orderController_SubmitOrderEvent;
        }

        private void orderController_SubmitOrderEvent(object sender, OrderViewModel e)
        {
            //提交订单的逻辑
        }
    }
}