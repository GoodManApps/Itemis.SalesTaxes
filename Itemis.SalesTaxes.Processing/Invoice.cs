using Itemis.SalesTaxes.Domain.Models;
using Itemis.SalesTaxes.Implementation.TaxesCalculator.Strategies;
using Itemis.SalesTaxes.Implementation.TaxesCalculator;
using System.Globalization;
using System.Text;

namespace Itemis.SalesTaxes.Processing
{
    /// <summary>
    /// Invoice for input information
    /// </summary>
    /// <typeparam name="TIn">The type of goods or services that are processed in this invoice.</typeparam>
    public class Invoice<TIn>
    {
        /// <summary>
        /// Get bill info by input list of goods or services
        /// </summary>
        /// <param name="items">List of goods or services to get bill</param>
        /// <param name="formatProvider">Transformation provider for culture-specific data</param>
        /// <returns>Formatted bill</returns>
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

                billResult.AppendLine($"{item.Count} {item.Item.Name}: {(sumPrice + sumTax).ToString("0.00", formatProvider)}");
            }

            billResult.AppendLine($"Sales Taxes: {totalTaxes.ToString("0.00", formatProvider)}");
            billResult.AppendLine($"Total: {totalAmount.ToString("0.00", formatProvider)}");

            return billResult.ToString();
        }

        /// <summary>
        /// Get a list of goods or services along with taxes
        /// </summary>
        /// <param name="items">List of goods or services to get bill</param>
        /// <returns>A list of goods or services along with taxes</returns>
        /// <exception cref="ArgumentException">Some type params could not be supported</exception>
        private IEnumerable<ItemWithTaxes> GetItemsWithTaxes(IEnumerable<string> items)
        {
            switch (typeof(TIn).Name)
            {
                case nameof(Product):
                    {
                        TaxesCalculator<IEnumerable<string>, IEnumerable<ItemWithTaxes>> taxCalculator = new(
                            new ProductStrategy());
                        var result = taxCalculator.CalculateTaxes(items);

                        return result;
                    }
                default: throw new ArgumentException($"This type [{typeof(TIn)} doesn't support yet]");
            }
        }
    }
}
