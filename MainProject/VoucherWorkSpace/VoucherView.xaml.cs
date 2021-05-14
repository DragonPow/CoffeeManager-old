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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainProject.VoucherWorkSpace
{
    /// <summary>
    /// Interaction logic for VoucherView.xaml
    /// </summary>
    public partial class VoucherView : UserControl
    {
        public VoucherView()
        {
            InitializeComponent();

            this.dateStart.DisplayDateStart = DateTime.Now.Date;
        }

        private void cbx_auto_Checked(object sender, RoutedEventArgs e)
        {
            VoucherViewModel viewModel = (VoucherViewModel)this.DataContext;
            viewModel.GetAvaiableCode();
            txtCode.IsEnabled = false;
        }

        private void cbx_auto_Unchecked(object sender, RoutedEventArgs e)
        {
            txtCode.IsEnabled = true;
        }
    }
}
