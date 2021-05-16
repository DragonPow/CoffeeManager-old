using MainProject.DatabaseController;
using MainProject.MainWorkSpace.Table;
using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MainProject
{
    class TableListViewModel : BaseViewModel
    {
        #region Field
        private ObservableCollection<TABLECUSTOM> _listTable;
        private int _floors;
        private TABLECUSTOM _selectedTable;

        private ICommand _deleteTable;
        private ICommand _InsertTable;
        private ICommand _UpdateStatusTable;
        private ICommand _SelectedTable;
        private ICommand _LoadTableByFloors;

        private ICommand _getTotal;
        private ICommand _payCommand;
        private ICommand _addDetailPro;
        private ICommand _removeDetailPro;

        private ICommand _clickTableCommand;
        #endregion

        #region Init


        public TableListViewModel(int Floors = 1)
        {
            ListTable = DataController.LoadTable(Floors);
            SelectedTable = null;
            //Insert();
            //Insert();
        }
        #endregion

        #region Properties

        public int Floors { get => _floors;
            set
            {
                if (value!=_floors)
                {
                    _floors = value;
                    OnPropertyChanged();
                }
            }
        }
        public TABLECUSTOM SelectedTable 
        { 
            get => _selectedTable;
            set
            {
                if (value!=_selectedTable)
                {
                    _selectedTable = value;
                    OnPropertyChanged();
                }
            }
        }
        public ObservableCollection<TABLECUSTOM> ListTable
        {
            get
            {
                if (_listTable == null)
                {
                    _listTable = new ObservableCollection<TABLECUSTOM>();
                }
                return _listTable;
            }
            set
            {
                if (_listTable != value)
                {
                    _listTable = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Command
        public ICommand ClickTableCommand
        {
            get
            {
                if (_clickTableCommand==null)
                {
                    _clickTableCommand = new RelayingCommand<object>(para => ClickTable());
                }
                return _clickTableCommand;
            }
        }
        public ICommand DeleteTable
        {
            get
            {
                if (_deleteTable == null)
                {
                    _deleteTable = new RelayingCommand<int>(a => Delete(a));
                }
                return _deleteTable;
            }

        }

        private void Delete(int number)
        {

            ListTable.RemoveAt(number - 1);
            for (int i = number; i < ListTable.Count; ++i)
                --ListTable[i].table.NUMBER;
            DataController.DeleteTable(number, Floors);
        }

        public ICommand InsertTable
        {
            get
            {
                if (_InsertTable == null)
                {
                    _InsertTable = new RelayingCommand<object>(a => Insert());
                }
                return _InsertTable;
            }
        }

        public void Insert()
        {
            ListTable.Add(new TABLECUSTOM() { table = new TABLE() {NUMBER = ListTable.Count + 1 } });
            //DataController.AddTable(Floors, ListTable.Count + 1 );
        }

        ICommand UpdateStatusTable
        {
            get
            {
                if (_UpdateStatusTable == null)
                {
                    _UpdateStatusTable = new RelayingCommand<int>(a => Update(a));
                }
                return _UpdateStatusTable;
            }
        }

        public void ClickTable()
        {
            //SelectedTable = new TABLECUSTOM() { table};
            Console.WriteLine("ok");
            TableDetailView view = new TableDetailView();
            view.DataContext = SelectedTable;
            view.Show();
        }
        public void Update(int Number)
        {

        }

        public ICommand Selected_Table
        {
            get
            {
                if (_SelectedTable == null)
                {
                    _SelectedTable = new RelayingCommand<TABLECUSTOM>(a => Selected(a));
                }
                return _SelectedTable;
            }
        }

        public void Selected(TABLECUSTOM table)
        {
            this.SelectedTable = table;
        }

        public ICommand LoadTableByFloors
        {
            get
            {
                if (_LoadTableByFloors == null)
                {
                    _LoadTableByFloors = new RelayingCommand<int>(a => LoadTable(a));
                }
                return _LoadTableByFloors;
            }
        }

        public void LoadTable(int Floors)
        {
            ListTable = DataController.LoadTable(Floors);
        }
   
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
            // SaveDetail();
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
            long Total = 0;
            foreach (DetailPro p in SelectedTable.ListPro)
                Total += (long) p.Pro.PRICE * p.Quantity;
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
            SelectedTable.ListPro.Add(new DetailPro(pro));
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
            if (number < SelectedTable.ListPro.Count)
            {
                SelectedTable.ListPro.RemoveAt(number - 1);
            }
        }
        #endregion
    }
}