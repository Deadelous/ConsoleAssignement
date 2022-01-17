using System.ComponentModel.DataAnnotations;


namespace Didata.Core.Entities
{
  /// <summary>
  /// ProductId = Artikelnummer, alfanumeriek, max 50 karakters
  /// Description = Artikelomschrijving, tekst max 200 karakters
  /// Amount = Aantal, decimaal maximaal 2 cijfers achter het punt.
  /// Price = Prijs, decimaal maximaal 2 cijfers achter het punt.
  /// </summary>

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

