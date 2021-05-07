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

        #endregion

        #region Init

        public int Floors { get => _floors; set => _floors = value; }
        public TABLE SelectedTable { get => _selectedTable; set => _selectedTable = value; }

        public TableListViewModel()
        {
            _listTable = new List<TableViewModel>();
        }
        #endregion

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
            _listTable.Add(new TableViewModel(_listTable.Count + 1));
            DataController.AddTable(Floors, _listTable.Count );
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
        #endregion
    }
}