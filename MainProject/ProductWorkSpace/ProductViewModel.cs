using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.ProductWorkSpace
{
    class ProductViewModel:BaseViewModel
    {
        private PRODUCT product;

        public PRODUCT Product { get => product;
            set
            {
                if (value != this.product)
                {
                    product = value;
                    OnPropertyChanged("Product");
                }
            }
        }

        public System.Windows.Input.ICommand CommandUpdate;
        public void Update(PRODUCT prod)
        {
            long id = prod.ID;
            using (mainEntities db = new mainEntities())
            {
                PRODUCT item = db.PRODUCTs.Where(p => p.ID == id).First();
                item = this.Product;
                db.SaveChanges();
            }
        }

        public System.Windows.Input.ICommand CommandInsert;
        public void Insert(PRODUCT prod)
        {
            using (mainEntities db = new mainEntities())
            {
                db.PRODUCTs.Add(prod);
                db.SaveChanges();
            }
        }

        private void initCommand()
        {
            this.CommandInsert = new RelayingCommand<PRODUCT>(this.Insert);
            this.CommandUpdate = new RelayingCommand<PRODUCT>(this.Update);
        }

        public ProductViewModel()
        {
            initCommand();
        }

        public ProductViewModel(long id)
        {
            using (mainEntities db = new mainEntities())
            {
                this.product = db.PRODUCTs.Where(p => p.ID == id).First();
            }
            initCommand();
        }
    }
}
