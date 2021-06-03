using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Model
{
    public partial class BILL
    {
        public void OnVoucherChange()
        {
            if (this.VOUCHER!=null)
            {
                this.VOUCHER.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "CODE")
                    {
                        OnPropertyChanged("VoucherCODE");
                    }
                };
            }
        }
    }
}
