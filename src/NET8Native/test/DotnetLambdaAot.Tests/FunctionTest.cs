using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using DotNetLambdaAot;

namespace DotnetLambdaAot.Tests;

public class FunctionTest
{
    #region Fields

    private readonly Function _function;

    #endregion

    #region Ctor

    public FunctionTest(Function function)
    {
        _function = function;
    }

    #endregion

    #region Methods

    [Fact]
    public async Task TestToUpperFunction()
    {
        // Invoke the lambda function and confirm the string was upper cased.
        var context = new TestLambdaContext();
        var products = await _function.GetProductAsync("1", "123");

        //Assert.Equal(products);
    }

    #endregion
}