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
        public ICommand PaymentCommand
        {
            get
            {
                if (_paymentCommand == null)
                {
                    _paymentCommand = new RelayingCommand<Object>(para => Payment());
                }
                return _paymentCommand;
            }
        }

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
            TableCheckout = new TableModel();
            TimeCheckout = DateTime.Now;

            //Testing
            TableCheckout.AddFood(new FoodModel(0,"Thachj",10000,2));
            TableCheckout.AddFood(new FoodModel(1, "Nhuc", 20000, 3));
            TableCheckout.AddFood(new FoodModel(2, "Bang", 10000, 1));
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

        private void Payment()
        {
            using (var db = new mainEntities())
            {
                TableCheckout.Foods.Add(new FoodModel(0, "Thachj", 10000, 2));
                //TableCheckout.AddFood(new FoodModel(0, "Thachj", 10000, 2));
                //create new bill in database
            }
        }
        private void LoadDiscount()
        {
            Console.WriteLine("Load discount");
        }
    }
}
