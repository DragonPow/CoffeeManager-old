using MainProject.DatabaseController;
using MainProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainProject
{
    class TableListViewModel : BaseViewModel
    {

        #region Field

        private List<TableViewModel> _listTable;
        private int _floors;
        private TABLE _selectedTable;

        private ICommand _deleteTable;
        private ICommand _InsertTable;
        private ICommand _UpdateStatusTable;
        private ICommand _SelectedTable;
        private ICommand _LoadTableByFloors;

        #endregion

        #region Init


        public TableListViewModel()
        {
            Floors = 0;
            ListTable = new List<TableViewModel>();
            SelectedTable = new TABLE();          
        }
        #endregion

        #region Properties

        public int Floors { get => _floors; set => _floors = value; }
        public TABLE SelectedTable { get => _selectedTable; set => _selectedTable = value; }
        public List<TableViewModel> ListTable
        {
            get
            {
                if (_listTable == null)
                {
                    _listTable = new List<TableViewModel>();
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

            _listTable.RemoveAt(number - 1);
            for (int i = number; i < _listTable.Count; ++i)
                --_listTable[i].Table.NUMBER;
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
            ListTable.Add(new TableViewModel(ListTable.Count + 1));
            DataController.AddTable(Floors, ListTable.Count + 1 );
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

        

        public void Update(int Number)
        {

        }

        public ICommand Selected_Table
        {
            get
            {
                if (_SelectedTable == null)
                {
                    _SelectedTable = new RelayingCommand<TABLE>(a => Selected(a));
                }
                return _SelectedTable;
            }
        }

        public void Selected(TABLE table)
        {
            this._selectedTable = table;
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
            List<TABLE> TableList = DataController.LoadTable(Floors);
            ListTable = new List<TableViewModel>(TableList.Count);
            foreach (TABLE t in TableList)
            {
                ListTable.Add(new TableViewModel(t));
            }
        }
        #endregion
    }
}