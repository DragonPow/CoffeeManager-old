﻿using MainProject.Model;
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
        private string _CodeDiscount;
        private int _Discount;
        private long _Total;
        private TABLECUSTOM _Current_table;

        private ObservableCollection<DETAILBILL> _ListDetailBill;      
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
                    OnPropertyChanged();
                }
            }
        }

        public int Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        public int BillCode
        {
            get {
                    using (var db = new mainEntities())
                    {
                        return db.BILLs.Count() + 1;
                    }               
                 }
        }
        public long Total
        {
            get { return _Total; }
            set { 
                if ( _Total != value)
                {
                    _Total = value;
                    OnPropertyChanged();
                }    
                }
        }

        public string CodeDiscount
        {
            get { return _CodeDiscount; }
            set
            {
                if (value != _CodeDiscount)
                {
                    _CodeDiscount = value;
                    OnPropertyChanged();
                }
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

            foreach( var p in CurrentTable.ListPro)
            {
                CurrentBill.DETAILBILLs.Add(new DETAILBILL() { PRODUCT = p.Pro, Amount = p.Quantity });
            }

            CurrentBill.TABLE = CurrentTable.table;
            Discount = 0;
            CurrentBill.CheckoutDay = DateTime.Now;
            Total = CurrentTable.Total;          
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

                if (CodeDiscount != "" && Discount == 0)
                {
                  
                }                    
                   
                    CurrentBill.ID_Tables = CurrentTable.table.ID;                 
                   
                    db.BILLs.Add(CurrentBill);

                    CurrentTable.ListPro = null;
                    CurrentTable.Total = 0;
                  
                    view.Close();
                                    
                    //Xuất đơn ra PDF                                
            }
        }
        private void LoadDiscount()
        {
            using (var db = new mainEntities())
            {

                var t = db.VOUCHERs.Where(v => (v.CODE == CodeDiscount && v.DELETED == 0 && v.BeginTime <= DateTime.Now && v.EndTime >= DateTime.Now)).FirstOrDefault();
                if (t != null)
                {
                    CurrentBill.ID_Voucher = t.ID;
                    CurrentBill.VOUCHER = t;
                    Discount = (int)(CurrentTable.Total * t.Percent) / 100;
                    Total -= Discount;
                }
                else
                {
                    Total = CurrentTable.Total;
                    CurrentBill.ID_Voucher = null;
                    CurrentBill.VOUCHER = null;
                    Discount = 0;
                    WindowService.Instance.OpenMessageBox("Nhập sai mã code", "Lỗi", System.Windows.MessageBoxImage.Error);
                }
            }
        }
    }
}
