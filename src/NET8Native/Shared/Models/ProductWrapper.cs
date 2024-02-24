/*--****************************************************************************
  --* Project Name    : Shared
  --* Reference       : System.Diagnostics.CodeAnalysis
  --* Description     : Product wrapper
  --* Configuration Record
  --* Review            Ver  Author           Date      Cr       Comments
  --* 001               001  A HATKAR         09/11/24  CR-XXXXX Original
  --****************************************************************************/
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