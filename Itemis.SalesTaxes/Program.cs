using Itemis.SalesTaxes.Domain.Models;
using Itemis.SalesTaxes.Processing;

var invoice = new Invoice<Product>();
var inputs = new List<string>();

var input = Console.ReadLine();
while (!string.IsNullOrWhiteSpace(input))
{
    inputs.Add(input);
    input = Console.ReadLine();
}

Console.WriteLine(invoice.GetBillInfo(inputs));