using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class VoucherView : UserControl, System.ComponentModel.INotifyPropertyChanged
    {
        public VoucherView()
        {
            InitializeComponent();
            dateStart.DisplayDateStart = DateTime.Now.Date;
        }

        private void cbx_auto_Checked(object sender, RoutedEventArgs e)
        {
            VoucherViewModel viewModel = (VoucherViewModel)this.DataContext;
            if (viewModel != null)
            {
                if (errorCount != 0)
                {
                    viewModel.GetAvaiableCode();
                }
                txtCode.IsEnabled = false;
            }
        }

        private void cbx_auto_Unchecked(object sender, RoutedEventArgs e)
        {
            txtCode.IsEnabled = true;
        }

        int errorCount = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        private void On_Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                errorCount++;
            }
            else
            {
                errorCount--;
            }
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
        }

        public bool IsValid { get => errorCount < 1; }

        public void useMode_Edit()
        {
            txtCode.IsEnabled = false;
            cbx_auto.IsEnabled = false;
        }

        public void useMode_Create()
        {
            txtCode.IsEnabled = true;
            cbx_auto.IsEnabled = true;
        }

        public void useMode_ReadOnly()
        {
            cbx_auto.IsEnabled = false;
            txtCode.IsReadOnly = true;
            txt_value.IsReadOnly = true;
            txtDescription.IsReadOnly = true;
            dateStart.IsEnabled = false;
            dateEnd.IsEnabled = false;
        }
    }
}
