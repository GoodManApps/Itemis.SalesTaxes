using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itemis.SalesTaxes.Domain.Models.Taxes
{
    /// <summary>
    /// Common tax for some region
    /// </summary>
    public sealed class CommonTax : BaseTax
    {
        private float _taxSize = 10f;

        // </inheritdoc>
        public override float TaxSize 
        { 
            get => _taxSize; 
            protected set => _taxSize = value; 
        }
    }
}
