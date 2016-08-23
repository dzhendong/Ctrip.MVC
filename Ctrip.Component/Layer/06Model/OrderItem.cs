
namespace Ctrip.Component.Layer
{
    public class OrderItem
    {
        public long OitemId { get; set; }

        public long Pid { get; set; }

        public float Price { get; set; }

        public int Number { get; set; }

        public Product Product { get; set; }
    }
}