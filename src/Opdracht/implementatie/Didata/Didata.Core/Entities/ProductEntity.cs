using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Didata.Core.Entities
{
  public class ProductEntity
  {
    public ProductEntity()
    {

    }

    public ProductEntity(string productId, string description, double amount, double price)
    {
      ProductId = productId;
      Description = description;
      Amount = amount;
      Price = price;
    }

    [Required]
    [RegularExpression("[A-Za-Z0-9]+")]
    [MaxLength(50)]
    public string ProductId { get; private set; }

    [MaxLength(200)]
    public string Description { get; private set; }

    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    public double Amount { get; private set; }

    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    public double Price { get; private set; }

    public override string ToString()
    {
      return $"ProductId: {ProductId} + Description: {Description} + Amount {Amount} + Price {Price}";
    }
  }
}

