using Didata.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Didata.Application.JsonMethods
{
  public class Order
  {
    // Create a public OrderEntity that can be used for multiple methods if needed.
    public OrderEntity OrderTestData()
    {
      ProductEntity productOne = new ProductEntity("PID1", "pid1 description", 1.5, 19.5);
      ProductEntity productTwo = new ProductEntity("PID2", "pid2 description", 2.5, 12.1);

      List<ProductEntity> products = new List<ProductEntity>
      {
        productOne,
        productTwo
      };

      OrderEntity order = new OrderEntity(1, "Order 123", 123, products);

      return order;
    }
  }
}

