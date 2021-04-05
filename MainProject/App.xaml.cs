using MainProject.MainWorkSpace;
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
            base.OnStartup(e);

            MainWindow view = new MainWindow();
            MainViewModel VM = new MainViewModel();
            view.DataContext = VM;
            view.Show();
        }
    }
}
