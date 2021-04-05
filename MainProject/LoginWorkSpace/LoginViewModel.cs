using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MainProject.LoginWorkSpace
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private AccountModel _currentAccount;
        private ICommand _loginCommand;
        #endregion //Fiedls



        #region Constructors
        public LoginViewModel()
        {
        }
        #endregion //Constructors



        #region Properties
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayingCommand<Object>(a => Login()); ;
                }
                return _loginCommand;
            }
        }
        public AccountModel CurrentAccount
        {
            get
            {
                if(_currentAccount==null)
                {
                    _currentAccount = new AccountModel();
                }
                return _currentAccount;
            }
            set
            {
                if (value!=_currentAccount)
                {
                    _currentAccount = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion //Properties


        #region Helper methods
        private void Login()
        {
            //
            //#ACCESS DATABASE TO FIND ACCOUNT
            //#If find account => login MainView, else throw error
            //
            Console.WriteLine("UserName: " + CurrentAccount.UserName);
            Console.WriteLine("Password: " + CurrentAccount.Password);
        }
        #endregion //Helper methods
    }
}
