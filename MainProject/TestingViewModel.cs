using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProject
{
    class TestingViewModel : BaseViewModel
    {
        private List<string> _mylistTest;
        private string _myTest;
        public List<string> MyListTest
        {
            get
            {
                if (_mylistTest==null)
                {
                    _mylistTest = new List<string>()
                    {
                        "Thach","Nhuc","Bang"
                    };
                }
                return _mylistTest;
            }
            set
            {
                if (value != _mylistTest)
                {
                    _mylistTest = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MyTest
        {
            get => _myTest; 
            set
            {
                if (value != _myTest)
                {
                    _myTest = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
