using Itemis.SalesTaxes.Abstraction.Processing;
using Itemis.SalesTaxes.Domain.Interfaces;
using Itemis.SalesTaxes.Domain.Models.Taxes;

namespace Itemis.SalesTaxes.Implementation.TaxesCalculator
{
    /// <summary>
    /// Item with list of needed taxes for it
    /// </summary>
    public class ItemWithTaxes: ITaxeable
    {
        /// <summary>
        /// An element is a product or service.
        /// </summary>
        private readonly IItem _item;

        /// <summary>
        /// Quantity to be purchased
        /// </summary>
        private readonly int _count;

        /// <summary>
        /// List of required taxes
        /// </summary>
        private IEnumerable<BaseTax> _taxes;

        // </inheritdoc>
        public int Count { get => _count; }

        // </inheritdoc>
        public IItem Item { get => _item; }

        /// <summary>
        /// Ctor with params
        /// </summary>
        /// <param name="item">An element is a product or service.</param>
        /// <param name="count">Quantity to be purchased</param>
        /// <param name="taxes">List of required taxes</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ItemWithTaxes(IItem item, int? count, IEnumerable<BaseTax>? taxes = null)
        {
            _item = item ?? throw new ArgumentNullException(nameof(item));
            _count = count ?? throw new ArgumentNullException(nameof(count));
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

        // </inheritdoc>
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

        // </inheritdoc>
        public float GetTotalAmount()
        {
            return Item.Price * Count;
        }
    }
}
