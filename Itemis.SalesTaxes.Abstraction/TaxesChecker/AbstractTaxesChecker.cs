using Itemis.SalesTaxes.Domain.Models.Taxes;

namespace Itemis.SalesTaxes.Abstraction.TaxesChecker
{
    /// <summary>
    /// The abstract tax check establishes an interface 
    /// for the control part of two hierarchies - 
    /// the tax and the element for which the tax is to be calculated.
    /// </summary>
    /// <typeparam name="T">The type of element for which you want to calculate the tax</typeparam>
    public class AbstractTaxesChecker<T>
    {
        /// <summary>
        /// Concrete tax check implementation.
        /// Delegates all the real work to it.
        /// </summary>
        protected ITaxesChecker<T> _implementation;

        /// <summary>
        /// Ctor with implementation
        /// </summary>
        /// <param name="implementation">Tax check implementation</param>
        public AbstractTaxesChecker(ITaxesChecker<T> implementation)
        {
            _implementation = implementation;
        }

        /// <summary>
        /// Processing item for evaluate tax
        /// </summary>
        /// <param name="item">Item to evaluate tax</param>
        /// <returns>IEnumarable collection with applied taxes</returns>
        public virtual IEnumerable<BaseTax> ProcessItem(T item)
        {
            return _implementation.SetTaxes(item);
        }
    }
}
