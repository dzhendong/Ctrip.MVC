
using System.Collections.Generic;

namespace Ctrip.Component.Layer
{
    public class IProductServiceClient
    {
        public Product GetProductByPid(long pid)
        {
            return null;
        }

        public List<Product> GetProductsByIds(List<long> pids)
        {
            return null;
        }
    }
}