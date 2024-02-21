using System.Diagnostics.CodeAnalysis;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Shared.Models;

namespace Shared.DataAccess;

/// <summary>
/// Represents products repo
/// </summary>
public class DynamoDbProducts : IProductsDAO
{
    #region Fields

    private static readonly string PRODUCT_TABLE_NAME = Environment.GetEnvironmentVariable("PRODUCT_TABLE_NAME") ?? string.Empty;
    private readonly AmazonDynamoDBClient _dynamoDbClient;

    #endregion

    #region Ctor

    [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(DynamoDbProducts))]
    public DynamoDbProducts()
    {
        _dynamoDbClient = new AmazonDynamoDBClient();
        _dynamoDbClient.DescribeTableAsync(PRODUCT_TABLE_NAME).GetAwaiter().GetResult();
    }

    #endregion

    #region Methods

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
    /// Create/Update product
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

    #endregion
}