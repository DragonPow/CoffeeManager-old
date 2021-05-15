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
        private BILL _currentBill;
        private ICommand _paymentCommand;
        private ICommand _loadDiscountCommand;
        #endregion
        private int _discount;

        public int Discount
        {
            get { return _discount; }
            set 
            {
                if (value!=_discount)
                {
                    _discount = value;
                    OnPropertyChanged();
                }
            }
        }


        #region Properties
        public BILL CurrentBill
        {
            get
            {
                return _currentBill;
            }
            private set
            {
                if (value != _currentBill)
                {
                    _currentBill = value;
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
                if (_paymentCommand == null)
                {
                    _paymentCommand = new RelayingCommand<BillView>(para => Payment(para));
                }
                return _paymentCommand;
            }
        }

        /// <summary>
        /// Auto load discount percent when eligible
        /// </summary>
        public ICommand LoadDiscountCommand
        {
            get
            {
                if (_loadDiscountCommand==null)
                {
                    _loadDiscountCommand = new RelayingCommand<Object>(para => LoadDiscount());
                }
                return _loadDiscountCommand;
            }
        }
        #endregion


        #region Constructors
        public BillViewModel()
        {
            CurrentBill = new BILL();
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
                var newBill = new BILL();
                db.BILLs.Add(CurrentBill);
                db.SaveChanges();
            }
            view.Close();
        }
        private void LoadDiscount()
        {
            Console.WriteLine("Load discount");
        }
    }
}
