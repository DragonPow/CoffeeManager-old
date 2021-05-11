using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MainProject.Model
{
    public class TableModel : BaseViewModel
    {
        #region Fields
        private string _name;
        private int _id;
        private ObservableCollection<FoodModel> _foods;
        #endregion


        #region Properties
        public string Name
        {
            get { return _name; }
            private set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }


        public ObservableCollection<FoodModel> Foods
        {
            get { return _foods; }
            private set
            {
                if (value != _foods)
                {
                    _foods = value;
                    OnPropertyChanged();
                }
            }
        }

        public int ID
        {
            get { return _id; }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public long TotalPrice
        {
            get => Foods.Sum(t => (long)t.TotalPrice);
        }
        #endregion


        #region Constructors
        public TableModel()
        {
            Foods = new ObservableCollection<FoodModel>();
            Foods.CollectionChanged += Foods_CollectionChanged;
        }

        public TableModel(int ID, string Name, ObservableCollection<FoodModel> listFood) : this()
        {
            this.ID = ID;
            this.Name = Name;
            Foods = listFood;
            //foreach (FoodModel food in listFood)
            //{
            //    Foods.Add(food);
            //}
        }
        #endregion


        #region Helper methods
        private void Foods_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("TotalPrice");
        }

        public void AddFood(FoodModel newfood)
        {
            var index = Foods.IndexOf(Foods.FirstOrDefault(t => t.Name == newfood.Name));
            Console.WriteLine(index);
            if (index >= 0)
            {
                Foods[index].Quantity += newfood.Quantity;
                OnPropertyChanged("TotalPrice");
            }
            else
            {
                Foods.Add(newfood);
            }
        }
        #endregion
    }
}
