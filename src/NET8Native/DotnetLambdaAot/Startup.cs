using Amazon.DynamoDBv2;
using Amazon.Lambda.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Shared.DataAccess;

namespace DotNetLambdaAot;

/// <summary>
/// Represents startup class of application
/// </summary>
[LambdaStartup]
public class Startup
{
    #region Ctor

    public Startup()
    {
    }

    #endregion

    /// <summary>
    /// Add services to the application and configure service provider
    /// </summary>
    /// <param name="services">Collection of service descriptors</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.BuildServiceProvider(validateScopes: false);
        services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient());
        services.AddSingleton<IProductsDAO, DynamoDbProducts>();
    }
}