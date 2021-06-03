using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Model
{
    public partial class DETAILBILL
    {
        public DETAILBILL(DetailPro product) : base()
        {
            this.PRODUCT = product.Pro;
            this.Amount = product.Quantity;
        }
    }
}
