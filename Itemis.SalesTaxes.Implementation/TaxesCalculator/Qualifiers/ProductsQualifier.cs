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
        /// ctor
        /// </summary>
        public ProductsQualifier()
        {
            // TODO: Move to tests in the future.
            _categoriesKeywords = new Dictionary<ProductCategory, HashSet<string>>
            {
                { ProductCategory.Food, new HashSet<string>() { "chocolate" } },
                { ProductCategory.Books, new HashSet<string>() { "book" } },
                { ProductCategory.Medical, new HashSet<string>() { "pills" } }
            };
        }

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
            // By the keyword "imported" in the name, set the attribute isImported
            if (product.Name.ToLowerInvariant().Contains("imported"))
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
                    if (product.Name.Contains(categoryKey))
                    {
                        product.SetCategory(key);
                        break;
                    }
                }
            }
        }
    }
}
