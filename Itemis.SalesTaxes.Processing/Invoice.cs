using Itemis.SalesTaxes.Domain.Models;
using Itemis.SalesTaxes.Implementation.TaxesCalculator.Strategies;
using Itemis.SalesTaxes.Implementation.TaxesCalculator;
using System.Globalization;
using System.Text;
using Itemis.SalesTaxes.Abstraction.Settings;
using Itemis.SalesTaxes.Abstraction.Processing;

namespace Itemis.SalesTaxes.Processing
{
    /// <summary>
    /// Invoice realization for basket with any kind items - goods or services
    /// </summary>
    /// <typeparam name="TIn">The type of goods or services that are processed in this invoice.</typeparam>
    public class Invoice<TIn>: IInvoice
    {
        /// <summary>
        /// Product tax settings for relative types
        /// </summary>
        private readonly IProductTaxSettings _productTaxSettings;

        /// <summary>
        /// Ctor of invoice with product tax settings
        /// </summary>
        /// <param name="productTaxSettings">Product tax settings</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Invoice(IProductTaxSettings productTaxSettings)
        {
            _productTaxSettings = productTaxSettings ?? 
                throw new ArgumentNullException(nameof(productTaxSettings));
        }
    
        // </inheritdoc>
        public string GetBillInfo(IEnumerable<string> items, CultureInfo? formatProvider = null)
        {
            formatProvider ??= CultureInfo.InvariantCulture;

            // the count of additional rows in final bill (need to calculate capacity of StringBuilder)
            const int additionalRowCount = 2;

            var builderCapacity = (items.Count() + additionalRowCount) * items.Average(_ => _.Length);
            StringBuilder billResult = new(Convert.ToInt32(builderCapacity));

            // Get prepared data to sum taxes
            var itemsWithTaxes = GetItemsWithTaxes(items);

            var totalAmount = 0.0f;
            var totalTaxes = 0.0f;

            // iterate each item in list
            foreach (var item in itemsWithTaxes)
            {
                // get total price of this item
                var sumPrice = item.GetTotalAmount();

                // get total tax info about this item
                var sumTax = item.GetTaxAmount();

                totalAmount += sumPrice + sumTax;
                totalTaxes += sumTax;

                billResult.AppendLine($"{item.Count} {item.Item.Name}: " +
                    $"{(sumPrice + sumTax).ToString("0.00", formatProvider)}");
            }

            billResult.AppendLine($"Sales Taxes: {totalTaxes.ToString("0.00", formatProvider)}");
            billResult.AppendLine($"Total: {totalAmount.ToString("0.00", formatProvider)}");

            return billResult.ToString();
        }

        // </inheritdoc>
        public IEnumerable<ITaxeable> GetItemsWithTaxes(IEnumerable<string> items)
        {
            switch (typeof(TIn).Name)
            {
                case nameof(Product):
                    {
                        TaxesCalculator<IEnumerable<string>, IEnumerable<ItemWithTaxes>> taxCalculator = new(
                            new ProductStrategy(_productTaxSettings));
                        var result = taxCalculator.CalculateTaxes(items);

                        return result;
                    }
                default: throw new ArgumentException($"This type [{typeof(TIn)} doesn't support yet]");
            }
        }
    }
}
