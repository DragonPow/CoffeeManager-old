using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MainProject.ViewModel
{
    public class BillViewModel : BaseViewModel
    {
        #region Fields

        private BILL _CurrentBill;
        //private string _CodeDiscount;
        //private DateTime _Time;
        //private int _BillCode;
        private int _Discount;
        private TABLECUSTOM _Current_table;

        //private ObservableCollection<DETAILBILL> _ListDetailBill;      
        //StoreInfor : namestore, phone, address

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
                    if (_CurrentBill != null)
                    {
                        CurrentBill.PropertyChanged += (s, e) =>
                        {
                            if (e.PropertyName == "VoucherCODE")
                            {
                                OnPropertyChanged("Discount");
                            }
                        };
                    }
                    OnPropertyChanged();
                }
            }
        }

        //public DateTime Time
        //{
        //    get { return _Time; }
        //    set
        //    {
        //        if (value != _Time)
        //        {
        //            _Time = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        public long Discount
        {
            get
            {
                long discount = 0;
                if (CurrentBill.VOUCHER != null) discount += CurrentBill.VOUCHER.Percent * CurrentTable.Total;
                return discount;
            }
        }
        //public int BillCode
        //{
        //    get { return _BillCode; }
        //    set
        //    {
        //        if (value != _BillCode)
        //        {
        //            _BillCode = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        //public string CodeDiscount
        //{
        //    get { return _CodeDiscount; }
        //    set
        //    {
        //        if (value != _CodeDiscount)
        //        {
        //            _CodeDiscount = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        public long TotalPrice
        {
            get => CurrentTable.Total - Discount;
        }
        public TABLECUSTOM CurrentTable
        {
            get => _Current_table;
            set
            {
                if (value != _Current_table)
                {
                    _Current_table = value;
                    if (_Current_table != null)
                    {
                        _Current_table.PropertyChanged += (s, e) =>
                        {
                            if (e.PropertyName == "Total")
                            {
                                OnPropertyChanged("TotalPrice");
                            }
                        };
                    }
                    OnPropertyChanged();

                }
            }
        }

        //public ObservableCollection<DETAILBILL> ListDetailBill
        //{
        //    get => _ListDetailBill;
        //    set
        //    {
        //        if (_ListDetailBill != value)
        //        {
        //            _ListDetailBill = value;
        //            OnPropertyChanged();

        //        }
        //    }
        //}


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
                    _PaymentCommand = new RelayingCommand<Object>(para => Payment());
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
            CurrentBill.CheckoutDay = DateTime.Now;
            CurrentBill.VOUCHER = new VOUCHER();
            //CurrentBill.EMPLOYEE = Current account logging


            /*  CurrentBill.DETAILBILLs.Add(new DETAILBILL() { Amount = 2, PRODUCT = new PRODUCT() { Name = "Thach", Price = 1000 } });      */
        }

        public BillViewModel(TABLECUSTOM table) : base()
        {
            CurrentBill.TABLE = table.table;

            //add product to list product of bill
            foreach (var p in table.ListPro)
            {
                CurrentBill.DETAILBILLs.Add(new DETAILBILL(p));
            }
        }
        #endregion

        private void Payment()
        {
            using (var db = new mainEntities())
            {
                //var t = db.VOUCHERs.Where(v => (v.CODE == CurrentBill.VOUCHER.CODE && v.DELETED == 0 && v.BeginTime <= DateTime.Now && v.EndTime >= DateTime.Now)).FirstOrDefault();
                //if (t != null || CurrentBill.VOUCHER.CODE == "")
                //{
                //    if (CurrentBill.VOUCHER.CODE != "")
                //    {
                //        CurrentBill.ID_Voucher = t.ID;
                //        CurrentBill.VOUCHER = t;
                //        Discount = (int)(CurrentTable.Total * t.Percent) / 100;
                //    }
                //    CurrentBill.ID_Tables = CurrentTable.table.ID;
                //    CurrentBill.TotalPrice = CurrentTable.Total - Discount;

                //    db.BILLs.Add(CurrentBill);

                //    CurrentTable.ListPro = null;
                //    CurrentBill.VOUCHER.CODE = null;

                //    Window window = WindowService.Instance.FindWindowbyTag("BillView").FirstOrDefault();
                //    window.Close();
                //}

                db.BILLs.Add(CurrentBill);
                db.SaveChanges();

                //Xuất đơn ra PDF
                //do something here...

                var window = WindowService.Instance.FindWindowbyTag("BillView").FirstOrDefault();
                window.Close();
            }
        }
        private void LoadDiscount()
        {
            using (var db = new mainEntities())
            {
                var _voucher = db.VOUCHERs.Where(voucher => voucher.CODE == CurrentBill.VOUCHER.CODE && voucher.DELETED == 0).FirstOrDefault();
                if (_voucher == null)
                {
                    //the voucher is not exists
                    WindowService.Instance.OpenMessageBox("Không tồn tại mã giảm giá này", "Thông báo", MessageBoxImage.Information);
                    CurrentBill.VOUCHER.CODE = null;
                    return;
                }
                else if (_voucher.EndTime < DateTime.Now)
                {
                    //the voucher is expiried
                    WindowService.Instance.OpenMessageBox("Mã thẻ đã hết hạn sử dụng", "Thông báo", MessageBoxImage.Information);
                    CurrentBill.VOUCHER.CODE = null;
                    return;
                }
                else
                {
                    //the voucher is exactly
                    CurrentBill.VOUCHER = _voucher;
                }
            }
        }
    }
}
