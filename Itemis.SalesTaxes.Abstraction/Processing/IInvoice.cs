using System.Globalization;

namespace Itemis.SalesTaxes.Abstraction.Processing
{
    /// <summary>
    /// Interface of invoice for basket with any kind items - goods or services
    /// </summary>
    public interface IInvoice
    {
        /// <summary>
        /// Get bill info by input list of goods or services
        /// </summary>
        /// <param name="items">List of goods or services to get bill</param>
        /// <param name="formatProvider">Transformation provider for culture-specific data</param>
        /// <returns>Formatted bill</returns>
        string GetBillInfo(IEnumerable<string> items, CultureInfo? formatProvider = null);

        /// <summary>
        /// Get a list of goods or services along with taxes
        /// </summary>
        /// <param name="items">List of goods or services to get bill</param>
        /// <returns>A list of goods or services along with taxes</returns>
        /// <exception cref="ArgumentException">Some type params could not be supported</exception>
        IEnumerable<ITaxeable> GetItemsWithTaxes(IEnumerable<string> items);
    }
}
