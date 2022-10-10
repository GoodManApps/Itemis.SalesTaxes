using Itemis.SalesTaxes.Domain.Models;
using Itemis.SalesTaxes.Processing;
using Itemis.SalesTaxes.Settings;

using Microsoft.Extensions.Configuration;

// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("consoleText.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

// Get values from the config given their key and their target type.
ProductTaxSettings settings = config
    .GetRequiredSection(nameof(ProductTaxSettings))
    .Get<ProductTaxSettings>();

if (settings == null)
{
    Console.WriteLine("Settings weren't loaded correctly. The results may be unexpected.");
    Console.WriteLine("You are seeing default text. It's better to stop app and find a problem.");
}
else
{
    Console.WriteLine("Settings loaded correctly.");

    var paragraphs = config
        .GetSection("OutputParagraphs")
        .Get<List<string>>();

    foreach (var paragraph in paragraphs)
    {
        Console.WriteLine(paragraph);
    }
}

// We are waiting to procecess products in this invoice
var invoice = new Invoice<Product>();
var exitCode = string.Empty;

do
{
    var inputs = new List<string>();
    string? input = string.Empty;
    Console.WriteLine("Basket:");

    do
    {
        input = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(input))
            inputs.Add(input);

    } while (!string.IsNullOrWhiteSpace(input));


    Console.WriteLine(invoice.GetBillInfo(inputs));
    Console.WriteLine("Do you need to process another basket? (y/n)");
    exitCode = Console.ReadLine();

} while (!string.IsNullOrWhiteSpace(exitCode) && exitCode.ToLowerInvariant() != "n");