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
        private int _name;
        private ObservableCollection<FoodModel> _foods;

        public int Name
        {
            get { return _name; }
            private set 
            { 
                if (value!=_name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }


        public ObservableCollection<FoodModel> Foods
        {
            get { return _foods; }
            private set {
                if (value!=_foods)
                {
                    _foods = value;
                    OnPropertyChanged();
                }
            }
        }

        public long TotalPrice
        {
            get => Foods.Sum(t => (long)t.TotalPrice);
        }

        public TableModel()
        {
            Foods = new ObservableCollection<FoodModel>();
            Foods.CollectionChanged += Foods_CollectionChanged;
        }

        private void Foods_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("TotalPrice");
        }

        public TableModel(int ID, ObservableCollection<FoodModel> listFood) : this()
        {
            this.Name = ID;
            foreach (FoodModel food in listFood)
            {
                Foods.Add(food);
            }
        }
        public void AddFood(FoodModel newfood)
        {
            var index = Foods.IndexOf(Foods.FirstOrDefault(t => t.Name == newfood.Name));
            Console.WriteLine(index);
            if (index>=0)
            {
                Foods[index].Quantity += newfood.Quantity;
                OnPropertyChanged("TotalPrice");
            }
            else
            {
                Foods.Add(newfood);
            }
        }
    }
}
