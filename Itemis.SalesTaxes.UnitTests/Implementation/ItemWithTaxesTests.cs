using Itemis.SalesTaxes.Domain.Models;
using Itemis.SalesTaxes.Domain.Models.Taxes;
using Itemis.SalesTaxes.Implementation.TaxesCalculator;

namespace Itemis.SalesTaxes.UnitTests.Implementation
{
    public  class ItemWithTaxesTests
    {
        [Theory]
        [InlineData("imported bottle of perfume", 47.50f)]
        public void ItemWithTaxes_Check_CalculateTax_ReturnTrue(string name, float price)
        {
            var product = new Product(name, price);

            var itemWithTaxes = new ItemWithTaxes(product, 1, 
                new List<BaseTax>() 
                { 
                    new CommonTax(), 
                    new ImportedTax() 
                });

            var taxAmount = itemWithTaxes.GetTaxAmount();
            var totalAmount = itemWithTaxes.GetTotalAmount();

            Assert.Equal(7.15f, taxAmount);
            Assert.Equal(54.65f, taxAmount + totalAmount);
        }
    }
}
