using MainProject.DatabaseController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainProject.Model.Product
{
    class ProductListViewModel : BaseViewModel
    {
        #region Field

        private List<ProductViewModel> _listProView;
        private ProductViewModel _productView;
        private string Type;

        private ICommand _addProView;
        private ICommand _deleteProView;
        private ICommand _searchByName;
        private ICommand _searchByType;

        #endregion
        #region Init

        public ProductListViewModel()
        {
            ListPoView = new List<ProductViewModel>();
            Type = "";
            ProductView = new ProductViewModel();
        }

        #endregion

        #region Properties

        public List<ProductViewModel>  ListPoView
        {
            get
            {
                if (_listProView == null)
                {
                    _listProView = new List<ProductViewModel>();
                }
                return _listProView;
            }
            set
            {
                if (value != _listProView)
                { 
                    _listProView = value;
                    OnPropertyChanged();
                }
            }
        }

        public ProductViewModel ProductView
        {
            get
            {
                if (_productView == null)
                {
                    _productView = new ProductViewModel();
                }
                return _productView;
            }
            set
            {
                if (_productView != value)
                {
                    _productView = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        #region Icommand

        public ICommand DeleteProduct
        {
            get
            {
                if (_deleteProView == null)
                {
                    _deleteProView = new RelayingCommand<ProductViewModel>(a => DeletePro(a));
                }
                return _deleteProView;
            }
        }

        public void DeletePro(ProductViewModel Pro)
        {
            ListPoView.Remove(Pro);
            DataController.DeleteProduct(Pro.Prodduct);

        }

        public ICommand SearchByName
        {
            get
            {
                if(_searchByName == null)
                {
                    _searchByName = new RelayingCommand<string>(a => SearchName(a));
                }
                return _searchByName;
            }
        }

        public void SearchName(string name)
        {
          for ( int i = 0; i < ListPoView.Count; ++i)
            {
                if (!ListPoView[i].Prodduct.NAME.Contains(name)) ListPoView.RemoveAt(i);
            }
        }

        public ICommand SearchByType
        {
            get
            {
                if (_searchByType == null)
                {
                    _searchByType = new RelayingCommand<string>(a => SearchType(a));
                }
                return _searchByType;
            }
        }
        public void SearchType(string Type)
        {
            List<PRODUCT> ProList = DataController.SearchProByType(Type);
            this.Type = Type;
            ListPoView = new List<ProductViewModel>(ProList.Count);
            foreach (PRODUCT p in ProList)
            {
                this.ListPoView.Add(new ProductViewModel(p));
            }
        }
        #endregion
    }
}
