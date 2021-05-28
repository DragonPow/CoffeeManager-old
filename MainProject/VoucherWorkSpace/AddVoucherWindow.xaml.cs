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
            btnSubmit.Click += BtnCreate_Click;
        }

        public AddVoucherWindow(VoucherViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            voucherView.useMode_Edit();
            btnSubmit.Content = "Lưu thay đổi";
            btnSubmit.Click += BtnEdit_Click;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            VoucherViewModel viewModel = (VoucherViewModel)voucherView.DataContext;
            if (viewModel.DateStart > viewModel.DateEnd)
            {
                MessageBox.Show("Error", "ERROR: Invalid time", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (viewModel.UpdateToDB())
            {
                this.Tag = "OK";
                this.Close();
            }
            else
            {
                MessageBox.Show("LỖI: Voucher không tồn tại", "Thất bại", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            VoucherViewModel viewModel = (VoucherViewModel)voucherView.DataContext;
            if (viewModel.DateStart > viewModel.DateEnd)
            {
                MessageBox.Show("Error", "ERROR: Invalid time", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                viewModel.SaveToDB(viewModel);
                this.Tag = "OK";
                this.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("LỖI: CSDL từ chối dữ liệu", "Thất bại", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            Console.WriteLine(viewModel.Code);
            Console.WriteLine(viewModel.ValueString);
            Console.WriteLine(viewModel.DateStart.ToShortDateString() + "  " + viewModel.DateStart.ToShortTimeString());
            Console.WriteLine(viewModel.DateEnd.ToLongDateString() + "  " + viewModel.DateEnd.ToShortTimeString());
            Console.WriteLine(viewModel.Description);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
