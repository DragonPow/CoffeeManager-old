using MainProject.Model;
using System.Collections.Generic;
using System.Windows.Input;

namespace MainProject
{
    class TableViewModel : BaseViewModel
    {
        #region Fields
        private TABLE _table;
        private List<DetailPro> _listPro;
        private List<DetailPro> _listtemporaryPro;
        private long Total;

        private ICommand _getTotal;
        private ICommand _payCommand;
        private ICommand _addDetailPro;
        private ICommand _saveDetailPro;
        private ICommand _removeDetailPro;

        #endregion

        #region properties

        public TABLE Table
        {
            get
            {
                if (_table == null)
                {
                    _table = new TABLE();
                }
                return _table;
            }
            set
            {
                if (value != _table)
                {
                    _table = value;
                    OnPropertyChanged();
                }
            }
        }

        internal List<DetailPro> ListPro
        {
            get => _listPro;
            set
            {
                if (value != _listPro)
                {
                    _listPro = value;
                    OnPropertyChanged();
                }
            }
        }

        internal List<DetailPro> ListtemporaryPro
        {
            get => _listtemporaryPro;
            set
            {
                if (value != _listtemporaryPro)
                {
                    _listtemporaryPro = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Init
        public TableViewModel(int Number)
        {
            Table = new TABLE() { NUMBER = Number };
            Total = 0;
            ListPro = new List<DetailPro>();
            ListtemporaryPro = new List<DetailPro>();
        }
        public TableViewModel(TABLE tab)
        {
            Table = tab;
            Total = 0;
            ListPro = new List<DetailPro>();
        }
        public TableViewModel()
        {
            Table = new TABLE() ;
            Total = 0;
            ListPro = new List<DetailPro>();
            ListtemporaryPro = new List<DetailPro>();
        }
        #endregion

        #region Command
        public ICommand Pay_Command
        {
            get
            {
                if (_payCommand == null)
                    _payCommand = new RelayingCommand<object>(a => Pay());
                return _payCommand;
            }

        }
        public void Pay()
        {
            SaveDetail();
            //Open BillWordSpace

        }

        public ICommand GetTotal
        {
            get
            {
                if (_getTotal == null)
                    _getTotal = new RelayingCommand<object>(a => getTotal());
                return _getTotal;
            }
        }

        public long getTotal()
        {
            foreach (DetailPro p in ListPro)
                Total += (long)p.Pro.PRICE * p.Quantity;
            return Total;
        }
// detailPro được thêm vào list tạm khi chưa click nút Save
        public ICommand AddDetailPro
        {
            get
            {
                if (_addDetailPro == null)
                {
                    _addDetailPro = new RelayingCommand<PRODUCT>(a => addDetailPro(a));
                }
                return _addDetailPro;
            }
        }


        public void addDetailPro(PRODUCT pro)
        {
            ListtemporaryPro.Add(new DetailPro(pro));
        }

// thêm detail pro sau khi nhấn nút Save

        public ICommand SaveDetailPro
        {
            get
            {
                if (_saveDetailPro != null)
                {
                    _saveDetailPro = new RelayingCommand<object>(a => SaveDetail());
                }
                return _saveDetailPro;
            }
        }

        public void SaveDetail()
        {
            if ( ListtemporaryPro != null)
            {
                ListPro.AddRange(ListtemporaryPro);
            }
        }

// Xóa detailPro đã chọn

        public ICommand RemoveDetailPro
        {
            get
            {
                if (_removeDetailPro == null)
                {
                    _removeDetailPro = new RelayingCommand<int>(a => RemoveDetail(a));
                }
                return _removeDetailPro;
            }
        }
        public void RemoveDetail(int number)
        {
            if (number < ListPro.Count)
            {
                ListPro.RemoveAt(number - 1);
            }
            else
                ListtemporaryPro.RemoveAt(number - ListPro.Count - 1);
        }

        #endregion

    }
}