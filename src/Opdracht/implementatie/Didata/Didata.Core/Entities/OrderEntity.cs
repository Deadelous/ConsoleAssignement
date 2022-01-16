using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Didata.Core.Entities
{
  public class OrderEntity
  {
    public OrderEntity()
    {

    }

    public OrderEntity(long orderId, string description, long customerId, IEnumerable<ProductEntity> products)
    {
      OrderId = orderId;
      Description = description;
      CustomerId = customerId;
      Products = products;
    }

    [Required]
    public long OrderId { get; private set; }

    [MaxLength(100)]
    public string Description { get; private set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "CustomerId should be greater than 0")]
    public long CustomerId { get; private set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Product should be greater than or equal to 1")]
    public IEnumerable<ProductEntity> Products { get; set; }

    public override string ToString()
    {
      StringBuilder strBuilder = new StringBuilder();
      foreach (ProductEntity product in Products)
      {
        strBuilder.AppendLine(product.ToString());
      }
      return $"OrderId: {OrderId} + Description: {Description} + CustomerId {CustomerId} + Products: {strBuilder}";
    }
  }
}

