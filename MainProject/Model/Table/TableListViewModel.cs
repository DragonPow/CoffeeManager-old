using MainProject.DatabaseController;
using MainProject.MainWorkSpace.Bill;
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
        private int _currentfloors;
        private TABLECUSTOM _selectedTable;
        private DetailPro _selectedDetailPro;

        private ICommand _deleteTableCommand;
        private ICommand _InsertTableCommand;
        private ICommand _UpdateStatusTableCommand;
        private ICommand _SelectedTable;
        private ICommand _LoadTableByFloorsCommand;

        private ICommand _getTotal;
        private ICommand _payCommand;
        private ICommand _addDetailPro;
        private ICommand _removeDetailPro;

        private ICommand _clickTableCommand;

        private ICommand _plusQuantityCommand;
        private ICommand _minusQuantityCommand;
        private ICommand _ClickQuantityCommand;

        #endregion


        #region Init


        public TableListViewModel(int Floors = 1)
        {
            ListTable = DataController.LoadTable(Floors);
            SelectedTable = null;
        }
        #endregion

        #region Properties
        public int Floors
        {
            get => _currentfloors;
            set
            {
                if (value != _currentfloors)
                {
                    _currentfloors = value;
                    OnPropertyChanged();
                }
            }
        }
        public TABLECUSTOM SelectedTable
        {
            get => _selectedTable;
            set
            {
                if (value != _selectedTable)
                {
                    _selectedTable = value;
                    OnPropertyChanged();
                }
            }
        }

        public DetailPro SelectedDetailPro
        {
            get => _selectedDetailPro;
            set
            {
                if (value != _selectedDetailPro)
                {
                    _selectedDetailPro = value;
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
                if (_clickTableCommand == null)
                {
                    _clickTableCommand = new RelayingCommand<object>(para => ClickTable());
                }
                return _clickTableCommand;
            }
        }

        public void ClickTable()
        {
            TableDetailView view = new TableDetailView();
            view.DataContext = SelectedTable;

            //view is a user control
            //view.Show();
        }
        public ICommand DeleteTable
        {
            get
            {
                if (_deleteTableCommand == null)
                {
                    _deleteTableCommand = new RelayingCommand<int>(a => Delete(a));
                }
                return _deleteTableCommand;
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
                if (_InsertTableCommand == null)
                {
                    _InsertTableCommand = new RelayingCommand<object>(a => Insert());
                }
                return _InsertTableCommand;
            }
        }

        public void Insert()
        {
            ListTable.Add(new TABLECUSTOM() { table = new TABLE() { NUMBER = ListTable.Count + 1 } });
            //DataController.AddTable(Floors, ListTable.Count + 1 );
        }

        ICommand UpdateStatusTable
        {
            get
            {
                if (_UpdateStatusTableCommand == null)
                {
                    _UpdateStatusTableCommand = new RelayingCommand<int>(a => Update(a));
                }
                return _UpdateStatusTableCommand;
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
                if (_LoadTableByFloorsCommand == null)
                {
                    _LoadTableByFloorsCommand = new RelayingCommand<int>(a => LoadTable(a));
                }
                return _LoadTableByFloorsCommand;
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
            BillView billView = new BillView();
            billView.DataContext = SelectedTable;
            billView.Show();
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
                Total +=  p.Pro.PRICE * p.Quantity;
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
            SelectedTable.Total += pro.PRICE;
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
            SelectedTable.Total -= SelectedDetailPro.Pro.PRICE * SelectedDetailPro.Quantity;
            if (number < SelectedTable.ListPro.Count)
            {
                SelectedTable.ListPro.RemoveAt(number - 1);
            }
        }

       
        public ICommand PlusQuantity
        {
            get
            {
                if (PlusQuantity == null)
                    _plusQuantityCommand = new RelayingCommand<object>(a => Plus());
                return _plusQuantityCommand;
            }
        }

        public void Plus()
        {
            SelectedTable.Total += SelectedDetailPro.Pro.PRICE;
            ++SelectedDetailPro.Quantity;

        }
        public ICommand MinusQuantity
        {
            get
            {
                if (_minusQuantityCommand == null)
                    _minusQuantityCommand = new RelayingCommand<object>(a => Minus());

                return _minusQuantityCommand;
            }

        }

        public void Minus()
        {
            SelectedTable.Total -= SelectedDetailPro.Pro.PRICE;
            --SelectedDetailPro.Quantity;
        }

        public ICommand ClickQuantityCommand
        {
            get
            {
                if (_ClickQuantityCommand == null)
                {
                    _clickTableCommand = new RelayingCommand<int>(a => ChangeQuantityCommand(a));
                }
                return _clickTableCommand;
            }
        }    

        public void ChangeQuantityCommand(int Number)
        {
            SelectedTable.Total -= SelectedDetailPro.Quantity * SelectedDetailPro.Pro.PRICE;
            SelectedDetailPro.Quantity = Number;
            SelectedTable.Total += Number * SelectedDetailPro.Pro.PRICE;
        }

        #endregion

    }
}