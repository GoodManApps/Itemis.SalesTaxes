using Itemis.SalesTaxes.Abstraction.TaxesCalculator;
using Itemis.SalesTaxes.Abstraction.TaxesChecker;
using Itemis.SalesTaxes.Abstraction.Settings;
using Itemis.SalesTaxes.Domain.Models;
using Itemis.SalesTaxes.Implementation.TaxesCalculator.Qualifiers;
using Itemis.SalesTaxes.Implementation.TaxesChecker;

using System.Globalization;
using System.Text.RegularExpressions;

namespace Itemis.SalesTaxes.Implementation.TaxesCalculator.Strategies
{
    /// <summary>
    /// Tax calculation strategy for products. 
    /// Implements an algorithm following the basic Strategy interface.
    /// </summary>
    public class ProductStrategy : ITaxesStrategy<IEnumerable<string>, IEnumerable<ItemWithTaxes>>
    {
        /// <summary>
        /// Template for parsing information from input data about the product
        /// </summary>
        readonly Regex ParsingTemplate = new(
            @"^(?<count>[0-9]?)\s*(?<name>\D*)at\s*(?<price>[0-9]{0,4}\.*[0-9]{0,2})$",
                RegexOptions.IgnoreCase |
                RegexOptions.ExplicitCapture |
                RegexOptions.CultureInvariant |
                RegexOptions.Singleline |
                RegexOptions.Compiled);

        /// <summary>
        /// Products qualifier
        /// </summary>
        private readonly ProductsQualifier _productsQualifier;

        /// <summary>
        /// Product tax settings for tax checker and product qualifier
        /// </summary>
        private readonly IProductTaxSettings _productTaxSettings;

        /// <summary>
        /// Ctor with product tax settings
        /// </summary>
        /// <param name="productTaxSettings">Product tax settings</param>
        public ProductStrategy(IProductTaxSettings productTaxSettings)
        {
            _productTaxSettings = productTaxSettings;
            _productsQualifier = new ProductsQualifier(_productTaxSettings.CategoriesKeywords);
        }

        // </inheritdoc>
        public IEnumerable<ItemWithTaxes> EvaluateTaxes(IEnumerable<string> items)
        {
            var listProducts = new List<ItemWithTaxes>(items.Count());

            foreach (var item in items)
            {
                var prod = GetProduct(item.Trim(), out var count);

                // Here we can log that can't parse product
                if (prod == null) continue;

                var productWithTaxes = new ItemWithTaxes(prod, count);

                // Here wa can retrieve data from db or any source to get full info without using our qualifier
                if (!prod.Category.HasValue)
                    _productsQualifier.Qualify(prod);

                var taxesChecker = new AbstractTaxesChecker<Product>(
                    new ProductsTaxChecker(_productTaxSettings.ExemptForTaxes));

                var taxes = taxesChecker.ProcessItem(prod);

                productWithTaxes.SetTaxes(taxes);

                listProducts.Add(productWithTaxes);
            }

            return listProducts;
        }

        /// <summary>
        /// Get product from parsed information
        /// </summary>
        /// <param name="description">Description of a product</param>
        /// <param name="count">Count of a product in invoice</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Some information shoud be existed in input data</exception>
        private Product? GetProduct(string description, out int count)
        {
            count = 0;
            var matches = ParsingTemplate.Matches(description);
            if (!matches.Any())
                return null;

            var productMatch = matches.First();
            var name = productMatch.Groups["name"]?.Value ?? "Unknown product";

            if (!int.TryParse(productMatch.Groups[nameof(count)]?.Value, out count))
            {
                throw new ArgumentNullException(nameof(count), "Не удалось получение количество продукции");
            }

            if (!float.TryParse(productMatch.Groups["price"]?.Value,
                NumberStyles.Float, CultureInfo.InvariantCulture, out var price))
            {
                throw new ArgumentNullException("price", "Не удалось получение количество продукции");
            }

            var product = new Product(name, price);
            return product;
        }
    }
}
