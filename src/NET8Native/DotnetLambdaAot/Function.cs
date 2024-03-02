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
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.APIGatewayEvents;
using Shared.DataAccess;
using Shared.Models;

namespace DotNetLambdaAot;

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

    [LambdaFunction()]
    [HttpApi(LambdaHttpMethod.Get, template: "/")]
    public virtual async Task<ProductWrapper> GetProductsAsync()
    {
        return await _dataAccess.GetAllProducts();
    }
    
    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Get, template:"/{id}")]
    public virtual async Task<Product> GetProductAsync(string id)
    {
        return await _dataAccess.GetProduct(id);
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Delete, template: "/{id}")]
    public virtual async Task<string> DeleteProductAsync(string id)
    {
        await _dataAccess.DeleteProduct(id);

        return "Deleted";
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Post, template: "/create")]
    public virtual async Task<Product> CreateProductAsync([FromBody] Product product)
    {
        await _dataAccess.CreateProduct(product);

        return product;
    }

    [LambdaFunction]
    [HttpApi(LambdaHttpMethod.Put, template: "/update")]
    public virtual async Task<Product> UpdateProductAsync([FromBody] Product product)
    {
        await _dataAccess.PutProduct(product);

        return product;
    }

    #endregion
}