using Itemis.SalesTaxes.Domain.Enums;
using Itemis.SalesTaxes.Domain.Interfaces;

namespace Itemis.SalesTaxes.Domain.Models
{
    /// <summary>
    /// Product that we operate (sell/buy/store, etc)
    /// </summary>
    public class Product : IItem
    {
        /// <summary>
        /// Ctor of Product with needed fields
        /// </summary>
        /// <param name="name">Name of a product</param>
        /// <param name="price">Price of a product</param>
        public Product(string name, float price)
        {
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Name of a product
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Price of a product
        /// </summary>
        public float Price { get; protected set; }

        /// <summary>
        /// Shows that our product are from abroad
        /// </summary>
        public bool IsImported { get; protected set; }

        /// <summary>
        /// Category of a product
        /// </summary>
        public ProductCategory? Category { get; protected set; }

        /// <summary>
        /// Set another category for this product
        /// </summary>
        /// <param name="category">New category</param>
        public void SetCategory(ProductCategory category)
        {
            Category = category;
        }

        /// <summary>
        /// Set new flag isImported state
        /// </summary>
        /// <param name="isImported">new flag isImported state</param>
        public void SetIsImported(bool isImported)
        {
            IsImported = isImported;
        }
    }
}