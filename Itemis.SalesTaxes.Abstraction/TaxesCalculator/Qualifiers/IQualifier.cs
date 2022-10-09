using Itemis.SalesTaxes.Domain.Interfaces;

namespace Itemis.SalesTaxes.Abstraction.TaxesCalculator.Qualifiers
{
    /// <summary>
    /// The qualifier interface for the element.
    /// </summary>
    /// <typeparam name="T">The element type to be qualified.</typeparam>
    public interface IQualifier<T>
        where T : IItem
    {
        /// <summary>
        /// Perform item qualification.
        /// </summary>
        /// <param name="item">Element for qualification.</param>
        void Qualify(T item);
    }
}
