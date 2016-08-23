using System.Collections.Generic;
using System;

namespace Ctrip.Component.Layer
{
    public class OrderViewModel : EventArgs 
    {
        public long OId { get; set; }

        public string OName { get; set; }

        public string Address { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}