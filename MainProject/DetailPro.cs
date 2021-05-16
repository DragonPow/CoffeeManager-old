using MainProject.Model;

namespace MainProject
{
    class DetailPro 
    {
        private int _quantity;
        private PRODUCT pro;

        public int Quantity { get => _quantity; set => _quantity = value; }
        public PRODUCT Pro { get => pro; set => pro = value; }

        public DetailPro(PRODUCT pro, int quan = 0)
        {
            this.Pro = pro;
            this.Quantity = quan;
        }

        /* private ICommand _plusQuantity;
         private ICommand _minusQuantity;


         

         public ICommand PlusQuantity
         {
             get
             {
                 if (PlusQuantity == null)
                     _plusQuantity = new RelayingCommand<object>(a => Plus());
                 return _plusQuantity;
             }
         }

         public ICommand MinusQuantity
         {
             get
             {
                 if (_minusQuantity == null)
                     _minusQuantity = new RelayingCommand<object>(a => Minus());
                 return _minusQuantity;
             }

         }

         

         public void Plus()
         {
             ++Quantity;
         }

         public void Minus()
         {
             --Quantity;
         }*/
    }
}