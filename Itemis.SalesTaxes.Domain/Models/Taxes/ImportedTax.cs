using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itemis.SalesTaxes.Domain.Models.Taxes
{
    /// <summary>
    /// Tax for imported production
    /// </summary>
    public sealed class ImportedTax : BaseTax
    {
        private float _taxSize = 5f;

        // </inheritdoc>
        public override float TaxSize { get => _taxSize; protected set => _taxSize = value; }
    }
}
