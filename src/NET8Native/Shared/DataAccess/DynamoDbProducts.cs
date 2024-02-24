/*--****************************************************************************
  --* Project Name    : Shared
  --* Reference       : Amazon.DynamoDBv2
  --*                   Amazon.DynamoDBv2.DataModel
  --*                   Amazon.DynamoDBv2.Model
  --*                   Amazon.Lambda.Annotations.APIGateway
  --*                   Shared.DataAccess
  --* Description     : Data access using DynamoDB
  --* Configuration Record
  --* Review            Ver  Author           Date      Cr       Comments
  --* 001               001  A HATKAR         09/11/24  CR-XXXXX Original
  --****************************************************************************/
using System.Diagnostics.CodeAnalysis;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Annotations.APIGateway;
using Shared.Models;

namespace Shared.DataAccess;

/// <summary>
/// Represents products repo
/// </summary>
public class DynamoDBProducts : IProductsDAO
{
    #region Fields

    private static readonly string PRODUCT_TABLE_NAME = Environment.GetEnvironmentVariable("PRODUCT_TABLE_NAME") ?? string.Empty;
    private readonly AmazonDynamoDBClient _dynamoDbClient;
    private readonly IDynamoDBContext _dynamoDbContext;

    #endregion

    #region Ctor

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(DynamoDBContext))]
    public DynamoDBProducts()
    {
        _dynamoDbClient = new AmazonDynamoDBClient();
        _dynamoDbContext = new DynamoDBContext(new AmazonDynamoDBClient());
        _dynamoDbClient.DescribeTableAsync(PRODUCT_TABLE_NAME).GetAwaiter().GetResult();
    }

    #endregion

    #region Methods

    /// <summary>
    /// List of products
    /// </summary>
    /// <returns>A task that represents the asynchronous operation</returns>
    public async Task<ProductWrapper> GetAllProducts()
    {
        var data = await _dynamoDbClient.ScanAsync(new ScanRequest()
        {
            TableName = PRODUCT_TABLE_NAME,
            Limit = 20
        });

        var products = new List<Product>();

        foreach (var item in data.Items)
        {
            products.Add(ProductMapper.ProductFromDynamoDB(item));
        }

        return new ProductWrapper(products);
    }

    /// <summary>
    /// Get product
    /// </summary>
    /// <param name="id">Product identitifer</param>
    /// <returns>A task that represents an asynchronous operation</returns>
    public async Task<Product> GetProduct(string id)
    {
        var getItemResponse = await _dynamoDbClient.GetItemAsync(new GetItemRequest(PRODUCT_TABLE_NAME,
            new Dictionary<string, AttributeValue>(1)
            {
                {ProductMapper.PK, new AttributeValue(id)}
            }));

        return getItemResponse.IsItemSet ? ProductMapper.ProductFromDynamoDB(getItemResponse.Item) : null;
    }

    /// <summary>
    /// Create product
    /// </summary>
    /// <param name="product">Product</param>
    /// <returns>A task that represents an asynchronous operation</returns>
    public async Task CreateProduct([FromBody] Product product)
    {
        await _dynamoDbContext.SaveAsync(product);
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="product">Product</param>
    /// <returns>A task that represents an asynchronous operation</returns>
    public async Task PutProduct(Product product)
    {
        await _dynamoDbClient.PutItemAsync(PRODUCT_TABLE_NAME, ProductMapper.ProductToDynamoDb(product));
    }

    /// <summary>
    /// Delete product
    /// </summary>
    /// <param name="id">Product identifier</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public async Task DeleteProduct(string id)
    {
        await _dynamoDbClient.DeleteItemAsync(PRODUCT_TABLE_NAME, new Dictionary<string, AttributeValue>(1)
        {
            {ProductMapper.PK, new AttributeValue(id)}
        });
    }

    #endregion
}