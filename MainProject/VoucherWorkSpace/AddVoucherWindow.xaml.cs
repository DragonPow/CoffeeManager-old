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

namespace MainProject.VoucherWorkSpace
{
    /// <summary>
    /// Interaction logic for AddVoucherWindow.xaml
    /// </summary>
    public partial class AddVoucherWindow : Window
    {
        public AddVoucherWindow()
        {
            InitializeComponent();
            voucherView.DataContext = new VoucherViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VoucherViewModel viewModel = (VoucherViewModel)voucherView.DataContext;
            if (viewModel.DateStart > viewModel.DateEnd)
            {
                MessageBox.Show("Error", "ERROR: Invalid time", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (viewModel.IsAuto)
            {
                viewModel.RandomCode();
            }

            Console.WriteLine(viewModel.Code);
            Console.WriteLine(viewModel.ValueString);
            Console.WriteLine(viewModel.DateStart.ToShortDateString() + "  " + viewModel.DateStart.ToShortTimeString());
            Console.WriteLine(viewModel.DateEnd.ToLongDateString() + "  " + viewModel.DateEnd.ToShortTimeString());
            Console.WriteLine(viewModel.Description);
        }
    }
}
