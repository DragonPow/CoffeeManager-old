﻿using System;
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
        }

        private void txt_value_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            VoucherViewModel viewModel = (VoucherViewModel)textBox.DataContext;
            textBox.Text = (viewModel.Value);
        }
    }
}
