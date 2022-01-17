using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;


namespace Didata.Core.Entities
{

  /// <summary>
  /// OrderId = Bestelling id, numeriek en groter dan 0
  /// Description = Omschrijving, tekst max 100 karakters en optioneel
  /// CustomerId = Klantnummer, numeriek en groter dan 0
  /// Products: Lijst van artikelen, minimaal 1 artikel
  /// </summary>
  public class OrderEntity 
  {
    public OrderEntity()
    {

    }

    public OrderEntity(long orderId, string description, long customerId, List<ProductEntity> products)
    {
      OrderId = orderId;
      Description = description;
      CustomerId = customerId;
      Products = products;
      ProductCount = products.Count;
      TotalPrice = products.Sum(x => x.Price);
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
  
    public int ProductCount { get; private set; }

    public double TotalPrice { get; private set; }
    
    public List<ProductEntity> Products { get; private set; }

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

