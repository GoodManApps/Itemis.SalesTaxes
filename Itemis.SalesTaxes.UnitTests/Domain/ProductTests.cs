using Itemis.SalesTaxes.Domain.Models;

namespace Itemis.SalesTaxes.UnitTests.Domain
{
    public class ProductTests
    {
        private const float _price = 0.35f;

        [Fact]
        public void Product_Shouldnt_Change_Param_ReturnTrue()
        {
            var name = "Prod 1";

            var product = new Product(name, _price);

            Assert.Equal(name, product.Name);
            Assert.Equal(_price, product.Price);
        }

        [Theory]
        [InlineData("Just product")]
        [InlineData("Imported product")]
        [InlineData("Great imported product")]
        public void Product_Check_IsImprorted_ReturnFalse(string name)
        {
            var product = new Product(name, _price);

            Assert.False(product.IsImported);
        }
    }
}
