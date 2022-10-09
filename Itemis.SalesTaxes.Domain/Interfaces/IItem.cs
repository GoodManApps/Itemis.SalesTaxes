namespace Itemis.SalesTaxes.Domain.Interfaces
{
    /// <summary>
    /// Interface for any type of item that we can process
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Name of item
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Price of item in conventional units
        /// </summary>
        float Price { get; }
    }
}
