using Itemis.SalesTaxes.Domain.Enums;
using Itemis.SalesTaxes.Domain.Models;
using Itemis.SalesTaxes.Implementation.TaxesCalculator.Qualifiers;

namespace Itemis.SalesTaxes.UnitTests.Implementation
{
    public class ProductQualifierTests
    {
        private readonly Dictionary<ProductCategory, HashSet<string>> CategoriesKeywords = new()
            {
                { ProductCategory.Food, new HashSet<string>() { "chocolate" } },
                { ProductCategory.Books, new HashSet<string>() { "book" } },
                { ProductCategory.Medical, new HashSet<string>() { "pills" } }
            };

        [Theory]
        [InlineData("Just product")]
        [InlineData("Great product")]
        public void ProductQualifier_Process_IsImprorted_ReturnFalse(string name)
        {
            var price = 0.35f;

            var product = new Product(name, price);

            Assert.False(product.IsImported);

            var productsQualifier = new ProductsQualifier(CategoriesKeywords);
            productsQualifier.Qualify(product);

            Assert.False(product.IsImported);
        }

        [Theory]
        [InlineData("Just imported product")]
        [InlineData("Imported product")]
        public void ProductQualifier_Process_IsImprorted_ReturnTrue(string name)
        {
            var price = 0.35f;

            var product = new Product(name, price);

            Assert.False(product.IsImported);

            var productsQualifier = new ProductsQualifier(CategoriesKeywords);
            productsQualifier.Qualify(product);

            Assert.True(product.IsImported);
        }
    }
}
