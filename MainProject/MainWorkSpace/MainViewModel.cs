using MainProject.LoginWorkSpace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MainProject.MainWorkSpace
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields
        private List<IMainWorkSpace> _mainWorkSpaces;
        private IMainWorkSpace _currentWorkSpace;
        #endregion //Fields



        #region Constructors
        public MainViewModel()
        {
            //Show LoginView before
            LoginWinDow LoginView = new LoginWinDow();
/*            LoginViewModel loginViewModel = new LoginViewModel();
            LoginView.DataContext = loginViewModel;*/
            LoginView.ShowDialog();

            //Add list MainWorkSpace here

            //Define current workspace

        }
        #endregion //Constructors



        #region Properties
        public List<IMainWorkSpace> MainWorkSpaces
        {
            get
            {
                if (_mainWorkSpaces == null)
                {
                    _mainWorkSpaces = new List<IMainWorkSpace>();
                }
                return _mainWorkSpaces;
            }
        }

        public IMainWorkSpace CurrentWorkSpace
        {
            get => _currentWorkSpace;
            set
            {
                if (value!=_currentWorkSpace)
                {
                    _currentWorkSpace = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion //Propeties
    }
}
