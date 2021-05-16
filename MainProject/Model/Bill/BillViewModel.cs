using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainProject
{
    class BillViewModel:BaseViewModel
    {
        #region field

        TABLECUSTOM _table;

        ICommand _payCommand;
        ICommand _cancelCommand;

        #endregion

        #region Property 

        public TABLECUSTOM Table
        {
            get
            {
                if ( _table == null)
                {
                    _table = new TABLECUSTOM();
                }
                return _table;
            }
            set
            {
                if (_table != value)
                {
                    _table = value;
                    OnPropertyChanged();
                }
            }    
        }
        #endregion

        #region Icommand

        public ICommand PayCommand
        {
            get
            {
                if( _payCommand == null)
                {
                    _payCommand = new RelayingCommand<Object>(a => Pay());
                }
                return _payCommand;
            }
        }

        public void Pay()
        {

        }

        public ICommand CancelCommand
        {
            get
            {
                if ( _cancelCommand == null)
                {
                    _cancelCommand = new RelayingCommand<object>(a => Cancel());//object se la BillView dang chay
                }
                return _cancelCommand;
            }
        }

        public void Cancel()
        {
        }
        #endregion
    }
}
