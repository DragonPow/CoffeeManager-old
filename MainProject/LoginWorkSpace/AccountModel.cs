using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.LoginWorkSpace
{
    public class AccountModel : BaseViewModel
    {
        private string _userName;
        private string _password;

        public string UserName
        {
            get => _userName;
            set
            {
                if (value != _userName)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                if (value!=_password)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
