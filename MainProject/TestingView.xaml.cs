using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MainProject
{
    /// <summary>
    /// Interaction logic for TestingView.xaml
    /// </summary>
    public partial class TestingView : Window
    {
        public TestingView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(((MainProject.MainWorkSpace.Bill.BillViewModel)ListView.DataContext).Bills.Count());
        }
    }
}
