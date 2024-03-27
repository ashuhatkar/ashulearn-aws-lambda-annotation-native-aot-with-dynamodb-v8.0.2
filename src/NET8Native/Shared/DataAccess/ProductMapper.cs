/*--****************************************************************************
  --* Project Name    : Shared
  --* Reference       : System.Globalization
  --*                   Amazon.DynamoDBv2.Model
  --*                   Shared.Models
  --* Description     : Data access using DynamoDB
  --* Configuration Record
  --* Review            Ver  Author           Date      Cr       Comments
  --* 001               001  A HATKAR         09/11/24  CR-XXXXX Original
  --****************************************************************************/
using System.Globalization;
using Amazon.DynamoDBv2.Model;
using Shared.Models;

namespace Shared.DataAccess;

public partial class ProductMapper
{
    public static string PK = "Id";
    public static string BARCODE = "Barcode";
    public static string NAME = "Name";
    public static string PRICE = "Price";

    public static Product ProductFromDynamoDB(Dictionary<string, AttributeValue> items)
    {
        var product = new Product(items[PK].S, items[BARCODE].S, items[NAME].S, decimal.Parse(items[PRICE].N));

        return product;
    }

    public static Dictionary<string, AttributeValue> ProductToDynamoDb(Product product)
    {
        Dictionary<string, AttributeValue> item = new Dictionary<string, AttributeValue>(4);
        item.Add(PK, new AttributeValue(product.Id));
        item.Add(BARCODE, new AttributeValue(product.Barcode));
        item.Add(NAME, new AttributeValue(product.Name));
        item.Add(PRICE, new AttributeValue()
        {
            N = product.Price.ToString(CultureInfo.InvariantCulture)
        });

        return item;
    }
}