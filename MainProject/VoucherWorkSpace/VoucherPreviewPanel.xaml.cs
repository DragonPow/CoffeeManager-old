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
    /// Interaction logic for VoucherPreviewPanel.xaml
    /// </summary>
    public partial class VoucherPreviewPanel : UserControl
    {
        public VoucherPreviewPanel()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
            AddVoucherWindow window = new AddVoucherWindow();
            window.Show();
        }
    }
}
