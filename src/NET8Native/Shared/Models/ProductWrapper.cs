using System.Diagnostics.CodeAnalysis;

namespace Shared.Models;

/// <summary>
/// Represents product wrapper
/// </summary>
public class ProductWrapper
{
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(ProductWrapper))]
    public ProductWrapper()
    {
        Products = new List<Product>();
    }

    public ProductWrapper(List<Product> products)
    {
        Products = products;
    }

    public List<Product> Products { get; set; }
}