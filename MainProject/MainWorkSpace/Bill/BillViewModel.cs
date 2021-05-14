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
        private int _id;
        private DateTime _timeCheckout;
        private TABLE _tableCheckout;
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
        public int ID
        {
            get { return _id; }
            private set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime TimeCheckout
        {
            get { return _timeCheckout; }
            private set
            {
                if (value != _timeCheckout)
                {
                    _timeCheckout = value;
                    OnPropertyChanged();
                }
            }
        }

        public TABLE TableCheckout
        {
            get { return _tableCheckout; }
            set
            {
                if (value != _tableCheckout)
                {
                    _tableCheckout = value;
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
                    //Test list bill
                    _bills = new List<BILL>()
                    {
                        //new BILL(){ID=1,TotalPrice=1000},
                        //new BILL(){ID=2,TotalPrice=2000},
                        //new BILL(){ID=3,TotalPrice=3000},
                    };
                }
                return _loadDiscountCommand;
            }
        }
        #endregion


        #region Constructors
        public BillViewModel()
        {
            //Constructors missing
        }

        public BillViewModel(TABLE table, int ID = -1) : this()
        {
            this.ID = ID;
            this.TableCheckout = table;
        }
        public BillViewModel(BillViewModel bill)
        {
            this.ID = bill.ID;
            this.TableCheckout = bill.TableCheckout;
            this.TimeCheckout = bill.TimeCheckout;
        }
        #endregion

        private void Payment(BillView view)
        {
            using (var db = new mainEntities())
            {
                //Payment missing
            }
            view.Close();
        }
        private void LoadDiscount()
        {
            Console.WriteLine("Load discount");
        }
    }
}
