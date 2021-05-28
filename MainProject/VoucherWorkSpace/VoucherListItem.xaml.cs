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
    /// Interaction logic for VoucherListItem.xaml
    /// </summary>
    public partial class VoucherListItem : UserControl
    {
        public VoucherListItem()
        {
            InitializeComponent();
        }

        public VoucherListItem(VoucherViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(VoucherViewModel.DateEnd))
            {
                CheckDate();
            }
        }

        public void CheckDate()
        {
            VoucherViewModel viewModel = (VoucherViewModel)this.DataContext;
            if (DateTime.Now > viewModel.DateEnd)
            {
                txtStatus.Foreground = Brushes.Red;
                txtStatus.Text = "Đã hết hạn";
            }
            else
            {
                txtStatus.Foreground = Brushes.Green;
                txtStatus.Text = "Còn hiệu lực";
            }
        }
    }
}
