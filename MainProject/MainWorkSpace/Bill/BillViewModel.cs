using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.MainWorkSpace.Bill
{
    class BillViewModel : BaseViewModel
    {
        private List<BILL> _bills;
        public List<BILL> Bills
        {
            get
            {
                if (_bills == null)
                {
                    _bills = new List<BILL>()
                    {
                        //new BILL(){ID=1,TotalPrice=1000},
                        //new BILL(){ID=2,TotalPrice=2000},
                        //new BILL(){ID=3,TotalPrice=3000},
                    };
                }
                return _bills;
            }
        }
        public BillViewModel() { }
    }
}
