using Itemis.SalesTaxes.Domain.Interfaces;

namespace Itemis.SalesTaxes.Abstraction.Processing
{
    /// <summary>
    /// Item that can be taxeable
    /// </summary>
    public interface ITaxeable
    {
        /// <summary>
        /// Total tax info about this item
        /// </summary>
        /// <returns>Calculate total tax info including count of good or service</returns>
        float GetTaxAmount();

        /// <summary>
        /// Total price of this item
        /// </summary>
        /// <returns>Calculate total cost of this good or service including count of it</returns>
        float GetTotalAmount();

        /// <summary>
        /// Quantity to be purchased
        /// </summary>
        int Count { get; }

        /// <summary>
        /// An element is a product or service.
        /// </summary>
        IItem Item { get; }
    }
}
