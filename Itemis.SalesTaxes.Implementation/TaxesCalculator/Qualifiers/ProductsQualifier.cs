using Itemis.SalesTaxes.Abstraction.TaxesCalculator.Qualifiers;
using Itemis.SalesTaxes.Domain.Enums;
using Itemis.SalesTaxes.Domain.Models;

namespace Itemis.SalesTaxes.Implementation.TaxesCalculator.Qualifiers
{
    /// <summary>
    /// Product qualifier to set product category and recognized as imported.
    /// </summary>
    public class ProductsQualifier : IQualifier<Product>
    {
        /// <summary>
        /// Dictionary of keywords to define the product category
        /// </summary>
        private readonly Dictionary<ProductCategory, HashSet<string>> _categoriesKeywords;

        /// <summary>
        /// ctor with categories keywords.
        /// </summary>
        /// <param name="categoriesKeywords">Prepared categories keywords.</param>
        public ProductsQualifier(Dictionary<ProductCategory, HashSet<string>> categoriesKeywords)
        {
            _categoriesKeywords = categoriesKeywords;
        }

        // </inheritdoc>
        public void Qualify(Product product)
        {
            // we'll check in lower text to exclude errors related with upper case
            var productName = product.Name.ToLowerInvariant();

            // By the keyword "imported" in the name, set the attribute isImported
            if (productName.Contains("imported"))
            {
                product.SetIsImported(true);
            }

            // Set the product category for the available keywords for each individual category.
            // TODO:The algorithm is currently inefficient (n2), it needs to be reworked in the future.
            foreach (var key in _categoriesKeywords.Keys)
            {
                if (product.Category.HasValue)
                    break;

                foreach (var categoryKey in _categoriesKeywords[key])
                {
                    if (productName.Contains(categoryKey))
                    {
                        product.SetCategory(key);
                        break;
                    }
                }
            }
        }
    }
}
