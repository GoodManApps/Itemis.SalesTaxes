using Itemis.SalesTaxes.Domain.Interfaces;
using Itemis.SalesTaxes.Domain.Models.Taxes;

namespace Itemis.SalesTaxes.Implementation.TaxesCalculator
{
    /// <summary>
    /// Item with list of needed taxes for it
    /// </summary>
    public class ItemWithTaxes
    {
        /// <summary>
        /// An element is a product or service.
        /// </summary>
        public readonly IItem Item;

        /// <summary>
        /// Quantity to be purchased
        /// </summary>
        public readonly int Count;

        /// <summary>
        /// List of required taxes
        /// </summary>
        private IEnumerable<BaseTax> _taxes;

        /// <summary>
        /// Ctor with params
        /// </summary>
        /// <param name="item">An element is a product or service.</param>
        /// <param name="count">Quantity to be purchased</param>
        /// <param name="taxes">List of required taxes</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ItemWithTaxes(IItem item, int? count, IEnumerable<BaseTax>? taxes = null)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Count = count ?? throw new ArgumentNullException(nameof(count));
            _taxes = taxes ?? new List<BaseTax>();
        }

        /// <summary>
        /// Change the list of taxes for this item
        /// </summary>
        /// <param name="taxes">New list of taxes</param>
        public void SetTaxes(IEnumerable<BaseTax> taxes)
        {
            _taxes = taxes;
        }

        /// <summary>
        /// Total tax info about this item
        /// </summary>
        /// <returns>Calculate total tax info including count of good or service</returns>
        public float GetTaxAmount()
        {
            var totalTaxSize = 0.0f;

            foreach (var tax in _taxes)
            {
                totalTaxSize += tax.TaxSize;
            }

            return (float)Math.Round(
                Math.Round(totalTaxSize / 100.0f * GetTotalAmount() * 20,
                    MidpointRounding.AwayFromZero) / 20, 1);
        }

        /// <summary>
        /// Total price of this item
        /// </summary>
        /// <returns>Calculate total cost of this good or service including count of it</returns>
        public float GetTotalAmount()
        {
            return Item.Price * Count;
        }
    }
}
