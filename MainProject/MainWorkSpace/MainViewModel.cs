using MainProject.Model;
using MainProject.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

namespace MainProject.MainWorkSpace
{
    class MainViewModel : BaseViewModel, IMainWorkSpace
    {
        #region Feld
        public string NameWorkSpace => "Home";

        private ProductViewModel _Productviewmodel;
        private TableViewModel _Tableviewmodel;
        private ObservableCollection<TYPE_PRODUCT> _ListType;

        private const PackIconKind _iconDisplay = PackIconKind.Home;

        ICommand AddProductToTable;

        #endregion

        #region  propertities

        public ProductViewModel Productviewmodel { get => _Productviewmodel; set { if (_Productviewmodel != value) { _Productviewmodel = value; OnPropertyChanged(); } } }
        public TableViewModel Tableviewmodel { get => _Tableviewmodel; set { if (_Tableviewmodel != value) { _Tableviewmodel = value; OnPropertyChanged(); } } }
        public ObservableCollection<TYPE_PRODUCT> ListType
        {
            get => _ListType;
            set
            {
                if (_ListType != value)
                {
                    _ListType = value;
                    OnPropertyChanged();

                }
            }
        }

        public PackIcon IconDisplay
        {
            get
            {
                return new PackIcon() { Kind = _iconDisplay, Width=30, Height=30 };
            }
        }


        #endregion

        #region Init
        public MainViewModel()
        {

            Tableviewmodel = new TableViewModel();
            Productviewmodel = new ProductViewModel() { Tableviewmodel = Tableviewmodel };

            using (var db = new mainEntities())
            {
                ListType = new ObservableCollection<TYPE_PRODUCT>(db.TYPE_PRODUCT.ToList());
            }
        }
        #endregion

        #region Command


        #endregion
    }
}
