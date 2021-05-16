using MainProject.Model;

namespace MainProject
{
    class DetailPro: BaseViewModel
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
        public PRODUCT Pro { get => pro; set => pro = value; }

        public DetailPro(PRODUCT pro, int quan = 0)
        {
            this.Pro = pro;
            this.Quantity = quan;
        }

    }
}