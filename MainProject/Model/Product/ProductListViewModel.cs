using MainProject.DatabaseController;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MainProject.Model.Product
{
    class ProductListViewModel : BaseViewModel
    {
        #region Field

        private ObservableCollection<PRODUCT> _listProView;
        private PRODUCT _chosseproduct;
        private string _type;

        private ICommand _addProduct;
        private ICommand _deletePro;
        private ICommand _searchByName;
        private ICommand _searchByType;
        private ICommand _OpenViewUpdateProduct;
        private ICommand _OpenViewDetailProduct;
        private ICommand _UpdateProduct;

        #endregion
        #region Init

        public ProductListViewModel()
        {
            ListPoView = DataController.LoadProduct();
            Type = "";
            ChosseProduct = new PRODUCT();
        }

        #endregion

        #region Properties

        public ObservableCollection<PRODUCT> ListPoView
        {
            get
            {
                if (_listProView == null)
                {
                    _listProView = new ObservableCollection<PRODUCT>();
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

        public PRODUCT ChosseProduct
        {
            get => _chosseproduct;
            set
            {
                if (_chosseproduct != value)
                {
                    _chosseproduct = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged();
                }
            }
        }


        #endregion

        #region Icommand

        public ICommand AddProduct
        {
            get 
            {
                if (_addProduct ==null)
                {
                    _addProduct = new RelayingCommand<PRODUCT>(a => Add(a));
                }
                return _addProduct;
            }
        }   
        
        public void Add(PRODUCT p)
        {
            ListPoView.Add(p);
            DataController.UpdateProduct(p);
        }

        public ICommand DeleteProduct
        {
            get
            {
                if (_deletePro == null)
                {
                    _deletePro = new RelayingCommand<PRODUCT>(a => DeletePro(a));
                }
                return _deletePro;
            }
        }

        public void DeletePro(PRODUCT Pro)
        {
            ListPoView.Remove(Pro);
            DataController.DeleteProduct(Pro);

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
           SearchType(Type);
          for ( int i = 0; i < ListPoView.Count; ++i)
            {
                if (!ListPoView[i].NAME.Contains(name)) ListPoView.RemoveAt(i);
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
            this.Type = Type;
            List<PRODUCT> ProList = DataController.SearchProByType(Type);        
            ListPoView = new ObservableCollection<PRODUCT>();

            foreach (PRODUCT p in ProList)
            {
                this.ListPoView.Add(p);
            }
        }

        public ICommand OpenViewUpdateProduct
        {
            get
            {
                if (_OpenViewUpdateProduct == null)
                {
                    _OpenViewUpdateProduct = new RelayingCommand<PRODUCT>(a => OpenViewUpdate(a));
                }
                return _OpenViewUpdateProduct;
            }
        }

        public void OpenViewUpdate(PRODUCT P)
        {

           ChosseProduct = P;
            //open new view to update product
        }

        public ICommand OpenViewDetailProduct
        {
            get
            {
                if (_OpenViewDetailProduct == null)
                {
                    _OpenViewDetailProduct = new RelayingCommand<PRODUCT>(a => OpenViewDetail(a));
                }
                return _OpenViewDetailProduct;
            }
        }

        public void OpenViewDetail(PRODUCT p)
        {
            ChosseProduct = p;
            //open new View detail product
        }

        public ICommand UpdateProduct
        {
            get
            {
                if (_UpdateProduct == null)
                {
                    _UpdateProduct = new RelayingCommand<PRODUCT>(a => Update(a));
                }
                return _UpdateProduct;
            }
        }

        public void Update(PRODUCT pro)
        {
            ChosseProduct = pro;
            DataController.UpdateProduct(pro);
        }
        #endregion
    }
}
