using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.APIGatewayEvents;
using Shared.DataAccess;
using Shared.Models;

namespace DotNetLambdaAot;

public class Function
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

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, template: "/")]
    public async Task<ProductWrapper> GetProductsAsync()
    {
        return await _dataAccess.GetAllProducts();
    }
    
    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, template:"/{id}")]
    public async Task<Product> GetProductAsync(string id)
    {
        return await _dataAccess.GetProduct(id);
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Delete, template: "/{id}")]
    public async Task<string> DeleteProductAsync(string id)
    {
        await _dataAccess.DeleteProduct(id);

        return "Deleted";
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Post, template: "/")]
    public async Task<Product> CreateProductAsync([FromBody] Product product)
    {
        await _dataAccess.CreateProduct(product);
        //await _dataAccess.PutProduct(product);

        return product;
    }

    #endregion
}