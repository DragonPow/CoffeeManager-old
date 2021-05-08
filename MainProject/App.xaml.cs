using MainProject.ApplicationWorkSpace;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MainProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", "../../");

            base.OnStartup(e);
            TestingView view = new TestingView();
            TestingViewModel viewModel = new TestingViewModel();
            view.DataContext = viewModel;
            //LoginWorkSpace.LoginViewModel VM = new LoginWorkSpace.LoginViewModel();
            //view.DataContext = VM;
            view.Show();
        }
    }
}
