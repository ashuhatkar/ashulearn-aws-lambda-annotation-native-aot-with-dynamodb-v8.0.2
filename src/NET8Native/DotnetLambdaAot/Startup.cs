/*--****************************************************************************
  --* Project Name    : DotnetServerlessDemo
  --* Reference       : Amazon.DynamoDBv2
  --*                   Amazon.Lambda.Annotations
  --*                   Microsoft.Extensions.DependencyInjection
  --*                   Shared.DataAccess
  --* Description     : Startup class
  --* Configuration Record
  --* Review            Ver  Author           Date      Cr       Comments
  --* 001               001  A HATKAR         09/11/24  CR-XXXXX Original
  --****************************************************************************/
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
        //services.BuildServiceProvider(validateScopes: false);
        services.AddSingleton<IAmazonDynamoDB>(new AmazonDynamoDBClient());
        services.AddSingleton<IProductsDAO, DynamoDBProducts>();
    }
}