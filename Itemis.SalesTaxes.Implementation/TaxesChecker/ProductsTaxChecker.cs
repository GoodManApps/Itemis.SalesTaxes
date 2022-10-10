using Itemis.SalesTaxes.Abstraction.TaxesChecker;
using Itemis.SalesTaxes.Domain.Enums;
using Itemis.SalesTaxes.Domain.Models.Taxes;
using Itemis.SalesTaxes.Domain.Models;

namespace Itemis.SalesTaxes.Implementation.TaxesChecker
{
    /// <summary>
    /// Implementing tax checks for products.
    /// </summary>
    class ProductsTaxChecker : ITaxesChecker<Product>
    {
        /// <summary>
        /// Categories that are exempt from specific taxes
        /// </summary>
        private readonly Dictionary<string, HashSet<ProductCategory>> _exemptForTaxes;

        /// <summary>
        /// сtor
        /// </summary>
        public ProductsTaxChecker()
        {
            // TODO: Move to tests in the future.
            _exemptForTaxes = new Dictionary<string, HashSet<ProductCategory>>
            {
                {
                    nameof(CommonTax),
                    new HashSet<ProductCategory>()
                    {
                        ProductCategory.Food,
                        ProductCategory.Books,
                        ProductCategory.Medical
                    }
                }
            };
        }

        /// <summary>
        /// ctor with categories collection that are exempt from specific taxes.
        /// </summary>
        /// <param name="exemptForTaxes">Prepared categories 
        /// collection that are exempt from specific taxes.</param>
        public ProductsTaxChecker(Dictionary<string, HashSet<ProductCategory>> exemptForTaxes)
        {
            _exemptForTaxes = exemptForTaxes;
        }

        // </inheritdoc>
        public IEnumerable<BaseTax> SetTaxes(Product product)
        {
            // initial return result
            List<BaseTax> taxes = new();

            // checks product for null, if product doesn't exist return empty list
            if (product == null) 
                return taxes;

            // if we can't qualify product category, then set common tax rule
            if (!product.Category.HasValue)
            {
                taxes.Add(new CommonTax());
            }
            // opposite, if product has category and
            // this category doesn't exist in exempt taxes list,
            // add common tax
            else if (_exemptForTaxes.ContainsKey(nameof(CommonTax)) &&
                !_exemptForTaxes[nameof(CommonTax)].Contains(product.Category.Value))
            {
                taxes.Add(new CommonTax());
            }

            // if product imported, then add additional imported tax
            if (product.IsImported)
                taxes.Add(new ImportedTax());

            return taxes;
        }
    }
}
