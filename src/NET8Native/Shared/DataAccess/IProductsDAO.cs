using Shared.Models;

namespace Shared.DataAccess;

/// <summary>
/// Represents product repo interface
/// </summary>
public interface IProductsDAO
{
    /// <summary>
    /// Get product
    /// </summary>
    /// <param name="id">Product identitifer</param>
    /// <returns>A task that represents an asynchronous operation</returns>
    Task<Product> GetProduct(string id);

    /// <summary>
    /// Create/Update product
    /// </summary>
    /// <param name="product">Product</param>
    /// <returns>A task that represents an asynchronous operation</returns>
    Task PutProduct(Product product);

    /// <summary>
    /// Delete product
    /// </summary>
    /// <param name="id">Product identifier</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task DeleteProduct(string id);

    /// <summary>
    /// List of products
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    Task<ProductWrapper> GetAllProducts();
}