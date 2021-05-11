using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject.Model
{
    public class AccountModel : BaseViewModel
    {
        private string _name;
        private string _password;

        public string Name { 
            get => _name; 
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password { 
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public AccountModel() { }
        public AccountModel(string Name, string Password)
        {
            this.Name = Name;
            this.Password = Password;
        }
        public AccountModel(AccountModel newAccount)
        {
            this.Name = newAccount.Name;
            this.Password = newAccount.Password;
        }

        public static bool isContain(AccountModel account)
        {
            bool contain = false;
            using (mainEntities db = new mainEntities())
            {
                contain = db.EMPLOYEEs.Where(t => (t.Name == account.Name && t.Password == account.Password)).Count() != 0;
            }
            return contain;
        }
    }
}
