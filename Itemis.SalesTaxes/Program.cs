using Itemis.SalesTaxes.Abstraction.Processing;
using Itemis.SalesTaxes.Abstraction.Settings;
using Itemis.SalesTaxes.Modules;

using Microsoft.Extensions.Configuration;

using Ninject;

using System.Reflection;

// Build a config object, using env vars and JSON providers.
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("consoleText.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

// Create kernek instance and
// load bindings settings for products
// from central DI assembly in this kernel
var kernel = new StandardKernel();
var diAssembly = Assembly.GetAssembly(typeof(ProductTaxBindings));
kernel.Load(diAssembly);

// Get instance of IProductTaxSettings with needed type
var productTaxSettings = kernel.Get<IProductTaxSettings>();
var settingsType = productTaxSettings.GetType();

// Get settings from configuration provider
productTaxSettings = (IProductTaxSettings)config
    .GetRequiredSection(settingsType.Name.ToString())
    .Get(settingsType);

// if there is no settings, we can't start app
if (productTaxSettings == null)
{
    Console.WriteLine("Settings weren't loaded correctly. The results may be unexpected.");
    Console.WriteLine("The app is shuting down. Please, try to find a problem."); 
    Console.ReadLine();
    return;
}

Console.WriteLine("Settings loaded correctly.");
var paragraphs = config
    .GetSection("OutputParagraphs")
    .Get<IEnumerable<string>>();

foreach (var paragraph in paragraphs)
{
    Console.WriteLine(paragraph);
}

// We are waiting to procecess products in this invoice
var settingsParam = new Ninject.Parameters
    .ConstructorArgument(nameof(productTaxSettings), productTaxSettings);
var invoice = kernel.Get<IInvoice>(settingsParam);
string? exitCode;

// Repeat basket processing in loop while user won't stop
do
{
    var inputs = new List<string>();
    string? input;
    Console.WriteLine("Basket:");

    // Fill input data with each row
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