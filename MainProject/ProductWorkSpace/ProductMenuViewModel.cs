using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainProject.ProductWorkSpace
{
    class ProductMenuViewModel:BaseViewModel
    {
        private List<ProductViewModel> listProductVM;
        public List<ProductViewModel> ListProductVM
        {
            get => listProductVM;
            set
            {
                if (value != this.listProductVM)
                {
                    listProductVM = value;
                    OnPropertyChanged("ListProductVM");
                }
            }
        }

        private int selectedProductIndex;
        public int SelectedProductIndex
        {
            get => selectedProductIndex;
            set
            {
                if (value != selectedProductIndex)
                {
                    selectedProductIndex = value;
                    OnPropertyChanged("SelectedProductIndex");
                    OnPropertyChanged("SelectedProduct");
                }
            }
        }
        public ProductViewModel SelectedProduct { get => listProductVM[SelectedProductIndex]; }

        public ICommand CommandSearch;
        public void Search(string keyword)
        {
            List<ProductViewModel> rs = listProductVM.Where(prodVM => prodVM.Product.Name.Contains(keyword)).ToList();
            this.ListProductVM = rs;
        }

        public ICommand CommandGoDetail;
        public void GoDetail(ProductViewModel productViewModel)
        {
            // TODOs: Open product detail view
        }

        public ICommand CommandAddSelectedProduct;
       /* public void AddSelectedProduct(TableViewModel tableViewModel)
        {
            DetailPro detailPro = new DetailPro(SelectedProduct.Product, 1);
            tableViewModel.addDetailPro(detailPro.Pro);
        }*/

        public ICommand CommandRemoveSelectedProduct;
       /* public void RemoveSelectedProduct(TableViewModel tableViewModel)
        {
            tableViewModel.removeDetailPro(SelectedProduct.Product.ID);
        }*/

        private void initCommand()
        {
            this.CommandSearch = new RelayingCommand<string>(Search);
            this.CommandGoDetail = new RelayingCommand<ProductViewModel>(GoDetail);
         /*   this.CommandAddSelectedProduct = new RelayingCommand<TableViewModel>(AddSelectedProduct);*/
         /*   this.CommandRemoveSelectedProduct = new RelayingCommand<TableViewModel>(RemoveSelectedProduct);*/
        }

        public ProductMenuViewModel()
        {
            initCommand();
        }

        public ProductMenuViewModel(int startIndex, int count)
        {
            initCommand();

            Model.PRODUCT[] data;
            using (Model.mainEntities db = new Model.mainEntities())
            {
                data = db.PRODUCTs.Skip(startIndex).Take(count).ToArray();
            }

            this.listProductVM = new List<ProductViewModel>(data.Length);
            foreach (var prod in data)
            {
                this.listProductVM.Add(new ProductViewModel(prod.ID));
            }
        }
    }
}
