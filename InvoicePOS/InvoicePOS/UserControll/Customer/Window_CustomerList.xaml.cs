﻿using InvoicePOS.ViewModels;
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

namespace InvoicePOS.UserControll.Customer
{
    /// <summary>
    /// Interaction logic for Window_CustomerList.xaml
    /// </summary>
    public partial class Window_CustomerList : Window
    {
        CustomerViewModel _CVM;
        public Window_CustomerList()
        {
            InitializeComponent();
            _CVM = new CustomerViewModel();
            this.DataContext = _CVM;
        }
        private void Win_grdCustomer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var str = (TaxManagement)dataGrid1.SelectedItem;
            _CVM.Select_Ok();
        }
    }
}
