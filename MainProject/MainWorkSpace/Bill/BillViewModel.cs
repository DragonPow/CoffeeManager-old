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
        private TableModel _tableCheckout;
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

        public TableModel TableCheckout
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
                }
                return _loadDiscountCommand;
            }
        }
        #endregion


        #region Constructors
        public BillViewModel()
        {
            //Testing
            ObservableCollection<FoodModel> foods = new ObservableCollection<FoodModel>()
            {
                new FoodModel(0, "Thachj", 10000, 2),
                new FoodModel(1, "Nhuc", 20000, 3),
                new FoodModel(2, "Bang", 10000, 1)
            };
            //Tesing

            TableCheckout = new TableModel(0,"1",foods);
            TimeCheckout = DateTime.Now;

        }

        public BillViewModel(TableModel table, int ID = -1) : this()
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
                BILL newBill = new BILL();
                newBill.TotalPrice = TableCheckout.TotalPrice;
                newBill.ID_TABLES = TableCheckout.ID;
                db.BILLs.Add(newBill);
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
