using Itemis.SalesTaxes.Abstraction.Processing;
using Itemis.SalesTaxes.Abstraction.Settings;
using Itemis.SalesTaxes.Domain.Models;
using Itemis.SalesTaxes.Implementation.Settings;
using Itemis.SalesTaxes.Processing;
using Ninject.Modules;

namespace Itemis.SalesTaxes.Modules
{
    /// <summary>
    /// Bindings rules for abstraction with realization for products
    /// </summary>
    public class ProductTaxBindings : NinjectModule
    {
        // </inheritdoc>
        public override void Load()
        {
            Bind<IProductTaxSettings>().To<ProductTaxSettings>();
            Bind<IInvoice>().To<Invoice<Product>>();
        }
    }
}
