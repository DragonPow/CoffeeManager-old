using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Model
{
    public class FoodModel : BaseViewModel
    {
        #region Fields
        private string _name;
        private int _quantity = 0;
        private int _id;
        private int _price = 0;
        #endregion


        #region Properties
        public string Name
        {
            get { return _name; }
            private set { 
                if (value!=_name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { 
                if (value!=_quantity)
                {
                    _quantity = value;
                    OnPropertyChanged();
                    OnPropertyChanged("TotalPrice");
                }
            }
        }

        public int ID
        {
            get { return _id; }
            private set { 
                if (value!=_id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Price
        {
            get { return _price; }
            private set { 
                if (value!=_price)
                {
                    _price = value;
                    OnPropertyChanged();
                    OnPropertyChanged("TotalPrice");
                }
            }
        }

        public int TotalPrice
        {
            get => Price * Quantity;
        }
        #endregion


        #region Constructors
        public FoodModel()
        {

        }

        public FoodModel(int ID, string Name, int Price, int Quantity)
        {
            this.ID = ID;
            this.Name = Name;
            this.Price = Price;
            this.Quantity = Quantity;
        }

        public FoodModel(FoodModel food)
        {
            this.ID = food.ID;
            this.Name = food.Name;
            this.Price = food.Price;
            this.Quantity = food.Quantity;
        }
        #endregion
    }
}
