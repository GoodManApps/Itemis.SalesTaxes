using Itemis.SalesTaxes.Domain.Models.Taxes;

namespace Itemis.SalesTaxes.Abstraction.TaxesChecker
{
    /// <summary>
    /// The interface for implementing the tax check for the element.
    /// </summary>
    /// <typeparam name="T">Item Type</typeparam>
    public interface ITaxesChecker<T>
    {
        /// <summary>
        /// Set tax types
        /// </summary>
        /// <param name="el">The element for which to check taxes.</param>
        /// <returns>Collection of applied taxes for an element</returns>
        IEnumerable<BaseTax> SetTaxes(T el);
    }
}
