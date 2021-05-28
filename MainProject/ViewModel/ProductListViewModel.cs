
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;




namespace MainProject.Model.Product
{
    class ProductViewModel : BaseViewModel
    {
        #region Field

        private ObservableCollection<PRODUCT> _ListProduct;
        private int _IndexCurrentproduct;
        private string _Type;
        string _SearchProduct;
        long _Id;
        string _Name;
        string _Detail;
        string _Image;
        long _Price;
        string _TypePro;

        private ICommand _AddProduct;
        private ICommand _DeletePro;
        private ICommand _SearchByName;
        private ICommand _SearchByType;
        private ICommand _LoadViewUpdateProduct;
        private ICommand _OpenViewDetailProduct;
        private ICommand _UpdateProduct;
        private ICommand _LoadAddProview;
        private ICommand _ExitAddProview;
        private ICommand _ExitUpdateProduct;
        private ICommand _ExitDetailProduct;

        #endregion
        #region Init

        public ProductViewModel()
        {

            using (var db = new mainEntities())
            {
                if (Type == null) return;

                if (Type == "Tất cả")
                {
                    ListPoduct = new ObservableCollection<PRODUCT>(db.PRODUCTs.Where(p => (p.DELETED == 0)).ToList());
                }
                else
                {
                    ListPoduct = new ObservableCollection<PRODUCT>(db.PRODUCTs.Where(p => ((p.TYPE == Type) && (p.DELETED == 0))).ToList());
                }

            }
        }

        #endregion

        #region Properties

        public ObservableCollection<PRODUCT> ListPoduct { get => _ListProduct; set { if (value != _ListProduct) { _ListProduct = value; OnPropertyChanged(); } } }

        public int IndexCurrentProduct { get => _IndexCurrentproduct; set { if (_IndexCurrentproduct != value) { _IndexCurrentproduct = value; OnPropertyChanged(); } } }

        public string SearchProduct { get => _SearchProduct; set { if (_SearchProduct != value) { _Type = value; OnPropertyChanged(); } } }
        public string Type { get => _Type; set { if (_Type != value) { _Type = value; OnPropertyChanged(); } } }
        public long Id { get => _Id; set { if (_Id != value) { _Id = value; OnPropertyChanged(); } } }
        public string Name { get => _Name; set { if (_Name != value) { _Name = value; OnPropertyChanged(); } } }
        public string Detail { get => _Detail; set { if (_Detail != value) { _Detail = value; OnPropertyChanged(); } } }
        public string Image { get => _Image; set { if (_Image != value) { _Image = value; OnPropertyChanged(); } } }
        public long Price { get => _Price; set { if (_Price != value) { _Price = value; OnPropertyChanged(); } } }
        public string TypePro { get => _TypePro; set { if (_TypePro != value) { _TypePro = value; OnPropertyChanged(); } } }


        #endregion

        #region Icommand


        public ICommand LoadAddProductView
        {
            get
            {
                if (_LoadAddProview == null)
                {
                    _LoadAddProview = new RelayingCommand<Object>(a => Loadaddproview());
                }
                return _LoadAddProview;
            }
        }


        public void Loadaddproview()
        {
            //open view Add_pro(a)
        }

        public ICommand AddProduct
        {
            get
            {
                if (_AddProduct == null)
                {
                    _AddProduct = new RelayingCommand<Object>(a => Add(a));
                }
                return _AddProduct;
            }
        }


        public void Add(object a)
        {

            PRODUCT p = new PRODUCT() { NAME = Name, IMAGE = Image, PRICE = Price, DETAIL = Detail, DELETED = 0, TYPE = TypePro };

            using (var db = new mainEntities())
            {
                {
                    db.PRODUCTs.Add(p);
                    db.SaveChanges();
                }
            }

            ListPoduct.Add(p);

        }

        public ICommand ExitAddProductView
        {
            get
            {
                if (_ExitAddProview == null)
                {
                    _ExitAddProview = new RelayingCommand<Object>(a => Exitaddproview(a));
                }
                return _ExitAddProview;
            }
        }


        public void Exitaddproview(Object a)
        {
            //Exit view Add_pro(a)

        }


        public ICommand DeleteProduct
        {
            get
            {
                if (_DeletePro == null)
                {
                    _DeletePro = new RelayingCommand<Object>(a => DeletePro());
                }
                return _DeletePro;
            }
        }

        public void DeletePro()
        {
            if (IndexCurrentProduct == null) return;

            using (var db = new mainEntities())
            {
                PRODUCT product = db.PRODUCTs.Where(p => (p.ID == ListPoduct.ElementAt(IndexCurrentProduct).ID) && (p.DELETED == 0)).FirstOrDefault();
                if (product != null)
                {
                    product.DELETED = 1;
                }
                db.SaveChanges();
            }

            ListPoduct.Remove(ListPoduct.ElementAt(IndexCurrentProduct));
        }

        public ICommand SearchByName
        {
            get
            {
                if (_SearchByName == null)
                {
                    _SearchByName = new RelayingCommand<Object>(a => SearchName());
                }
                return _SearchByName;
            }
        }

        public void SearchName()
        {

            if (SearchProduct == "")
            {
                using (var db = new mainEntities())
                {
                    ListPoduct = new ObservableCollection<PRODUCT>(db.PRODUCTs.ToList());
                }
            }
            else
            {
                using (var db = new mainEntities())
                {
                    var listpro = db.PRODUCTs.Where(p => (ConvertToUnSign(p.NAME).ToLower().Contains(ConvertToUnSign(SearchProduct).ToLower()) && p.DELETED == 0));
                    if (listpro == null) return;
                    ListPoduct = new ObservableCollection<PRODUCT>(listpro.ToList());
                }
            }
        }

        public ICommand SearchByType
        {
            get
            {
                if (_SearchByType == null)
                {
                    _SearchByType = new RelayingCommand<Object>(a => SearchType());
                }
                return _SearchByType;
            }
        }
        public void SearchType()
        {
            using (var db = new mainEntities())
            {
                var listpro = db.PRODUCTs.Where(p => (p.TYPE == Type && p.DELETED == 0));
                if (listpro == null) return;
                ListPoduct = new ObservableCollection<PRODUCT>(listpro.ToList());
            }
        }

        public ICommand LoadViewUpdateProduct
        {
            get
            {
                if (_LoadViewUpdateProduct == null)
                {
                    _LoadViewUpdateProduct = new RelayingCommand<Object>(a => LoadViewUpdate());
                }
                return _LoadViewUpdateProduct;
            }
        }

        public void LoadViewUpdate()
        {
            //open viewUpdatate
        }

        public ICommand UpdateProduct
        {
            get
            {
                if (_UpdateProduct == null)
                {
                    _UpdateProduct = new RelayingCommand<Object>(a => Update());
                }
                return _UpdateProduct;
            }
        }

        public void Update()
        {
            using (var db = new mainEntities())
            {
                PRODUCT pro = db.PRODUCTs.Where(p => (p.ID == ListPoduct.ElementAt(IndexCurrentProduct).ID) && (p.DELETED == 0)).FirstOrDefault();
                PRODUCT pr = new PRODUCT() { NAME = Name, IMAGE = Image, PRICE = Price, DETAIL = Detail, DELETED = 0, TYPE = TypePro };

                pro.DELETED = 1;
                db.PRODUCTs.Add(pr);
                db.SaveChanges();

               // ListPoduct.ElementAt(IndexCurrentProduct) = pr;
               
            }
        }

        public ICommand ExitUpdateProduct
        {
            get
            {
                if (_ExitUpdateProduct == null)
                {
                    _ExitUpdateProduct = new RelayingCommand<Object>(a => ExitUpdate(a));
                }
                return _ExitUpdateProduct;
            }
        }
            

        public void ExitUpdate(Object a)
        {
            //Exit view Update_pro(a)
        }

        public ICommand OpenViewDetailProduct
        {
            get
            {
                if (_OpenViewDetailProduct == null)
                {
                    _OpenViewDetailProduct = new RelayingCommand<PRODUCT>(a => OpenViewDetail());
                }
                return _OpenViewDetailProduct;
            }
        }

        public void OpenViewDetail()
        {
            //open new View detail product
        }
        public ICommand ExitDetailProduct
        {
            get
            {
                if (_ExitDetailProduct == null)
                {
                    _ExitDetailProduct = new RelayingCommand<Object>(a => ExitDetail(a));
                }
                return _ExitDetailProduct;
            }
        }


        public void ExitDetail(object a)
        {
            //open view Add_pro(a)
        }

        #endregion

        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }
    }
}
