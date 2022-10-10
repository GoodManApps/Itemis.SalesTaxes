using Itemis.SalesTaxes.Abstraction.Settings;
using Itemis.SalesTaxes.Domain.Enums;

namespace Itemis.SalesTaxes.Implementation.Settings
{
    /// <summary>
    /// Settings for product tax calculation
    /// </summary>
    public class ProductTaxSettings: IProductTaxSettings
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
