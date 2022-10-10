using Itemis.SalesTaxes.Domain.Enums;
using Itemis.SalesTaxes.Domain.Models;
using Itemis.SalesTaxes.Domain.Models.Taxes;
using Itemis.SalesTaxes.Implementation.TaxesChecker;

namespace Itemis.SalesTaxes.UnitTests.Implementation
{
    public class ProductsTaxCheckerTests
    {
        private const float _price = 0.35f;
        private readonly Dictionary<string, HashSet<ProductCategory>> _exemptForTaxes = new()
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

        [Theory]
        [InlineData("Extra blouse", ProductCategory.Clothes)]
        [InlineData("Sneakers imported", ProductCategory.Shoes)]
        [InlineData("Hot coffee", ProductCategory.Drink)]
        public void ProductsTaxChecker_Check_CommonTax_ReturnTrue(string name, ProductCategory category)
        {
            var product = new Product(name, _price);
            product.SetCategory(category);

            var productsTaxChecker = new ProductsTaxChecker(_exemptForTaxes);
            var taxes = productsTaxChecker.SetTaxes(product);

            Assert.True(taxes.OfType<CommonTax>().Any());
        }

        [Theory]
        [InlineData("Extra pills", ProductCategory.Medical)]
        [InlineData("Imported Chekhov's book", ProductCategory.Books)]
        [InlineData("Chocolate box", ProductCategory.Food)]
        public void ProductsTaxChecker_Check_CommonTax_ReturnFalse(string name, ProductCategory category)
        {
            var product = new Product(name, _price);
            product.SetCategory(category);

            var productsTaxChecker = new ProductsTaxChecker(_exemptForTaxes);
            var taxes = productsTaxChecker.SetTaxes(product);

            Assert.False(taxes.OfType<CommonTax>().Any());
        }

        [Theory]
        [InlineData("Sneakers imported", ProductCategory.Shoes)]
        [InlineData("Imported Chekhov's book", ProductCategory.Books)]
        public void ProductsTaxChecker_Check_ImportedTax_ReturnTrue(string name, ProductCategory category)
        {
            var product = new Product(name, _price);
            product.SetCategory(category);
            product.SetIsImported(true);

            var productsTaxChecker = new ProductsTaxChecker(_exemptForTaxes);
            var taxes = productsTaxChecker.SetTaxes(product);

            Assert.True(taxes.OfType<ImportedTax>().Any());
        }

        [Theory]
        [InlineData("Extra pills", ProductCategory.Medical)]
        [InlineData("Chocolate box", ProductCategory.Food)]
        [InlineData("Extra blouse", ProductCategory.Clothes)]
        public void ProductsTaxChecker_Check_ImportedTax_ReturnFalse(string name, ProductCategory category)
        {
            var product = new Product(name, _price);
            product.SetCategory(category);

            var productsTaxChecker = new ProductsTaxChecker(_exemptForTaxes);
            var taxes = productsTaxChecker.SetTaxes(product);

            Assert.False(taxes.OfType<ImportedTax>().Any());
        }
    }
}
