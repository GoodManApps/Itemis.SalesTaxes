namespace Itemis.SalesTaxes.Domain.Models.Taxes
{
    /// <summary>
    /// Abstract class for any kind of tax
    /// </summary>
    public abstract class BaseTax
    {
        /// <summary>
        /// Tax size in percent
        /// </summary>
        public abstract float TaxSize { get; protected set; }

        /// <summary>
        /// Empty ctor
        /// </summary>
        public BaseTax()
        {
        }

        /// <summary>
        /// Ctor with tax size
        /// </summary>
        /// <param name="taxSize">Tax amount</param>
        public BaseTax(float taxSize)
        {
            TaxSize = taxSize;
        }

        /// <summary>
        /// Tax tax amount for price
        /// </summary>
        /// <param name="price">Price of some item</param>
        /// <returns></returns>
        public float GetTaxAmount(float price)
        {
            return (price / 100.0f) * TaxSize;
        }        
        
        /// <summary>
        /// Tax tax amount for price
        /// </summary>
        /// <param name="price">Price of some item</param>
        /// <returns></returns>
        public float GetTaxAmount(int price)
        {
            return GetTaxAmount((float)price) * TaxSize;
        }
    }
}
