using Itemis.SalesTaxes.Domain.Enums;

namespace Itemis.SalesTaxes.Abstraction.Settings
{
    /// <summary>
    /// Interface of settings for product tax calculation
    /// </summary>
    public interface IProductTaxSettings
    {
        /// <summary>
        /// Categories that are exempt from specific taxes
        /// </summary>
        public Dictionary<string, HashSet<ProductCategory>> ExemptForTaxes { get; set; }

        /// <summary>
        /// Dictionary of keywords to define the product category
        /// </summary>
        public Dictionary<ProductCategory, HashSet<string>> CategoriesKeywords { get; set; }
    }
}
