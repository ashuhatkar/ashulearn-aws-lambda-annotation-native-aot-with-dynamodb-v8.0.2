/*--****************************************************************************
  --* Project Name    : DotnetServerlessDemo
  --* Reference       : Amazon.Lambda.Annotations
  --*                   Amazon.Lambda.Annotations.APIGateway
  --*                   Amazon.Lambda.APIGatewayEvents
  --*                   System.Diagnostics.CodeAnalysis
  --*                   System.Threading.Tasks
  --*                   Shared.DataAccess
  --*                   Shared.Models
  --* Description     : Function class
  --* Configuration Record
  --* Review            Ver  Author           Date      Cr       Comments
  --* 001               001  A HATKAR         09/11/24  CR-XXXXX Original
  --****************************************************************************/
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Shared.DataAccess;
using Shared.Models;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DotNetLambdaAot;

/// <summary>
/// A collection of sample Lambda functions that provides a REST api
/// </summary>
public partial class Function
{
    #region Fields

    private readonly IProductsDAO _dataAccess;

    #endregion

    #region Ctor

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Function))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(APIGatewayHttpApiV2ProxyRequest))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(APIGatewayHttpApiV2ProxyResponse))]
    public Function(IProductsDAO dataAccess)
    {
        _dataAccess = dataAccess;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Get products
    /// </summary>
    /// <param name="context">Object that allows you to access useful information available within the lambda execution env.</param>
    /// <returns>A task that represents the async operation</returns>
    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, template: "/")]
    public virtual async Task<ProductWrapper> GetProductsAsync(ILambdaContext context)
    {
        context.Logger.LogInformation($"");
        return await _dataAccess.GetAllProducts();
    }

    /// <summary>
    /// Get product
    /// </summary>
    /// <param name="id">Identifier (Partition key)</param>
    /// <param name="barcode">Barcode (Sort key)</param>
    /// <returns>A task that represents the async operation</returns>
    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, template:"/{id}/{barcode}")]
    public virtual async Task<Product> GetProductAsync(string id, string barcode)
    {
        return await _dataAccess.GetProduct(id, barcode);
    }

    /// <summary>
    /// Delete product
    /// </summary>
    /// <param name="id">Identifier (Partition key)</param>
    /// <param name="barcode">Barcode (Sort key)</param>
    /// <returns>A task that represents the async operation</returns>
    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Delete, template: "/{id}/{barcode}")]
    public virtual async Task<string> DeleteProductAsync(string id, string barcode)
    {
        await _dataAccess.DeleteProduct(id, barcode);

        return "Deleted";
    }

    /// <summary>
    /// Create product
    /// </summary>
    /// <param name="product">Product</param>
    /// <returns>A task that represents the async operation</returns>
    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Post, template: "/create")]
    public virtual async Task<Product> CreateProductAsync([FromBody] Product product)
    {
        await _dataAccess.CreateProduct(product);

        return product;
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="product">Product</param>
    /// <returns>A task that represents the async operation</returns>
    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Put, template: "/update")]
    public virtual async Task<Product> UpdateProductAsync([FromBody] Product product)
    {
        await _dataAccess.PutProduct(product);

        return product;
    }

    #endregion
}