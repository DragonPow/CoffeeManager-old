using MainProject.ApplicationWorkSpace;
using MainProject.MainWorkSpace.Bill;
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

            //This is testing Startup

            //TestingView view = new TestingView();
            BillView view = new BillView();
            MainWorkSpace.Bill.BillViewModel viewModel = new MainWorkSpace.Bill.BillViewModel();
            //TestingViewModel viewModel = new TestingViewModel();


            //This is main Startup

            //ApplicationView view = new ApplicationView();
            //ApplicationViewModel viewModel = new ApplicationViewModel();

            view.DataContext = viewModel;
            view.ShowDialog();
        }
    }
}
