using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainProject.MainWorkSpace.Bill
{
    public class BillViewModel : BaseViewModel
    {
        #region Fields

        private BILL _CurrentBill;
        private string _Discount;
        private TABLECUSTOM _Current_table;
        private ObservableCollection<DETAILBILL> _ListDetailBill;      
        //StoreInfor 

        private ICommand _PaymentCommand;
        private ICommand _CheckDiscountCommand;

        #endregion


        #region Properties
        public BILL CurrentBill
        {
            get
            {
                return _CurrentBill;
            }
            private set
            {
                if (value != _CurrentBill)
                {
                    _CurrentBill = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Discount
        {
            get { return _Discount; }
            set
            {
                if (value != _Discount)
                {
                    _Discount = value;
                    OnPropertyChanged();
                }
            }
        }

        public long TotalPrice
        {
            get
            {
                return 1;//CurrentBill.DETAILBILLs.Sum(bill => bill.AMOUNT * bill.PRODUCT.PRICE);
            }
        }
        public TABLECUSTOM CurrentTable
        {
            get => _Current_table;
            set
            {
                if (value != _Current_table)
                {
                    _Current_table = value;
                    OnPropertyChanged();

                }
            }
        }

        public ObservableCollection<DETAILBILL> ListDetailBill
        {
            get => _ListDetailBill;
            set
            {
                if (_ListDetailBill != value)
                {
                    _ListDetailBill = value;
                    OnPropertyChanged();
                   
                }
            }
        }
       

        #endregion


        #region Commands
        /// <summary>
        /// Payment bill and quit the form
        /// </summary>
        public ICommand PaymentCommand
        {
            get
            {
                if (_PaymentCommand == null)
                {
                    _PaymentCommand = new RelayingCommand<BillView>(para => Payment(para));
                }
                return _PaymentCommand;
            }
        }

        /// <summary>
        /// Auto load discount percent when eligible
        /// </summary>
        public ICommand CheckDiscountCommand
        {
            get
            {
                if (_CheckDiscountCommand == null)
                {
                    _CheckDiscountCommand = new RelayingCommand<Object>(para => LoadDiscount());
                }
                return _CheckDiscountCommand;
            }
        }
        #endregion


        #region Constructors
        public BillViewModel()
        {
            CurrentBill = new BILL();
            
          /*  CurrentBill.DETAILBILLs.Add(new DETAILBILL() { Amount = 2, PRODUCT = new PRODUCT() { Name = "Thach", Price = 1000 } });      */      

            foreach( var p in CurrentTable.ListPro)
            {
                CurrentBill.DETAILBILLs.Add(new DETAILBILL() { PRODUCT = p.Pro, Amount = p.Quantity });
            }
            CurrentBill.TABLE = CurrentTable.table;


        }

        public BillViewModel(BILL bill)
        {
            CurrentBill = bill;
        }
        #endregion

        private void Payment(BillView view)
        {
            using (var db = new mainEntities())
            {
                long Sale = 0;
                var t = db.VOUCHERs.Where(v => (v.CODE == Discount && v.DELETED == 0)).FirstOrDefault();
                if ( t!= null || Discount == "")
                {
                    if ( Discount != "")
                    {
                        CurrentBill.ID_Voucher = t.ID;
                        CurrentBill.VOUCHER = t;
                        Sale = (long)(CurrentTable.Total * t.Percent) / 100;

                    }   
                    
                    CurrentBill.ID_Tables = CurrentTable.table.ID;                 
                    CurrentBill.TotalPrice = CurrentTable.Total - Sale;
                    CurrentBill.CheckoutDay = DateTime.Now;
                   
                    db.BILLs.Add(CurrentBill);

                    CurrentTable.ListPro = null;
                    CurrentTable.Total = 0;
                    
                    view.Close();
                                    
                    //Xuất đơn ra PDF
                }
                else
                {
                    //open messegnbox thông báo lỗi sai mã voucher
                }                    
            }
        }
        private void LoadDiscount()
        {
            Console.WriteLine("Load discount");
        }
    }
}
