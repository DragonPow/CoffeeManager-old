using MainProject.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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

        private const PackIconKind _iconDisplay = PackIconKind.Home;

        ICommand AddProductToTable;

        #endregion

        #region  propertities

        public ProductViewModel Productviewmodel { get => _Productviewmodel; set { if (_Productviewmodel != value) { _Productviewmodel = value; OnPropertyChanged(); } } }
        public TableViewModel Tableviewmodel { get => _Tableviewmodel; set { if (_Tableviewmodel != value) { _Tableviewmodel = value; OnPropertyChanged(); } } }

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
            Productviewmodel = new ProductViewModel() {Tableviewmodel = Tableviewmodel};
        }
        #endregion

        #region Command


        #endregion
    }
}
