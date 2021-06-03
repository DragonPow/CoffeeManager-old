using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MainProject
{
    public class DetailPro: BaseViewModel
    {
        private int _quantity;
        private PRODUCT pro;

        public int Quantity 
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public PRODUCT Pro
        {
            get => pro;
            set
            {
                if (pro != value)
                {
                    pro = value;
                    OnPropertyChanged();
                }
            }
        }

        public long TotalPrice { get => Pro.Price * Quantity; }
        
        public DetailPro()
        {
            Pro.PropertyChanged += (s, e) => OnPropertyChanged("TotalPrice"); 
        }
        public DetailPro(PRODUCT pro, int quan = 0) : base()
        {
            this.Pro = pro;
            this.Quantity = quan;
        }
    }
}