using System.Diagnostics.CodeAnalysis;

namespace Shared.Models;

/// <summary>
/// Represents product
/// </summary>
public class Product
{
    #region Ctor

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Product))]
    public Product()
    {
        Id = string.Empty;
        Name = string.Empty;
    }

    public Product(string id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    #endregion

    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public void SetPrice(decimal newPrice)
    {
        Price = Math.Round(newPrice, 2);
    }

    public override string ToString()
    {
        return "Product{" +
                "id='" + Id + '\'' +
                ", name='" + Name + '\'' +
                ", price='" + Price +
                '}';
    }
}