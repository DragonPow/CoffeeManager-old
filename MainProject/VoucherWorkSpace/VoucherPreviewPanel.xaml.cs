using MainProject.Model;
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
        public List<VoucherViewModel> Vouchers { get; set; }

        public VoucherPreviewPanel()
        {
            InitializeComponent();

            Vouchers = new List<VoucherViewModel>();

            using (Model.mainEntities db = new Model.mainEntities())
            {
                foreach (var v in db.VOUCHERs)
                {
                    VoucherViewModel viewModel = VoucherViewModel.from(v);
                    Vouchers.Add(viewModel);
                }
            }
            InvalidListData();

            btnAdd.Click += btnAdd_Click;
        }

        public void InvalidListData()
        {
            wrapPanel.Children.Clear();
            foreach (var v in Vouchers)
            {
                VoucherListItem item = new VoucherListItem(v);
                item.Margin = new Thickness(5);
                item.Width = 300;
                var menu = createContextMenu(item);
                menu.Tag = v;
                item.ContextMenu = menu;
                item.MouseDoubleClick += Item_MouseDoubleClick;
                item.CheckDate();
                wrapPanel.Children.Add(item);
            }
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
               VoucherViewModel viewModel = (sender as VoucherListItem).DataContext as VoucherViewModel;
                EditVoucher(ref viewModel);
            }
        }

        ContextMenu createContextMenu(object host)
        {
            ContextMenu itemMenu = new ContextMenu();
            itemMenu.FontSize = 15;
            itemMenu.Margin = new Thickness(0);
            itemMenu.StaysOpen = true;
            itemMenu.Padding = new Thickness(0);

            MenuItem itemDelete = new MenuItem();
            itemDelete.Header = "Xóa khỏi danh sách";
            itemDelete.Click += ItemDelete_Click1;
            MenuItem itemEdit = new MenuItem();
            itemEdit.Header = "Sửa thông tin";
            itemEdit.Click += ItemEdit_Click;
            List<MenuItem> items = new List<MenuItem>() { itemEdit, itemDelete };
            itemMenu.Items.Add(items[0]);
            itemMenu.Items.Add(items[1]);
            return itemMenu;
        }

        private void ItemEdit_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            VoucherViewModel viewModel = (menuItem.Parent as ContextMenu).Tag as VoucherViewModel;
            EditVoucher(ref viewModel);
        }

        private void ItemDelete_Click1(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            VoucherViewModel viewModel = (menuItem.Parent as ContextMenu).Tag as VoucherViewModel;
            DeleteVoucher(viewModel);
        }

        void EditVoucher(ref VoucherViewModel viewModel)
        {
            VoucherViewModel temp = new VoucherViewModel(viewModel);
            AddVoucherWindow addVoucher = new AddVoucherWindow(temp);
            addVoucher.ShowDialog();
            if ((addVoucher.Tag as String) == "OK")
            {
                viewModel.Value = temp.Value;
                viewModel.DateStart = temp.DateStart;
                viewModel.DateEnd =temp.DateEnd;
                viewModel.Description = temp.Description;
                MessageBox.Show("Cập nhật Voucher thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        void DeleteVoucher(VoucherViewModel viewModel)
        {
            MessageBoxResult boxResult = MessageBox.Show("Voucher sẽ bị xóa và không thể khôi phục", "Xác nhận xóa Voucher"
                , MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (boxResult == MessageBoxResult.OK)
            {
                if (viewModel.DeleteFromDB())
                {
                    Vouchers.Remove(viewModel);
                    InvalidListData();
                    MessageBox.Show("Voucher đã bị xóa", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        void CreateVoucher()
        {
            AddVoucherWindow window = new AddVoucherWindow();
            VoucherViewModel viewModel = new VoucherViewModel();
            window.DataContext = viewModel;
            window.ShowDialog();
            if ((window.Tag as String) == "OK")
            {
                Vouchers.Add(viewModel);
                InvalidListData();
                MessageBox.Show("Tạo Voucher thành công", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            CreateVoucher();
        }
    }
}
