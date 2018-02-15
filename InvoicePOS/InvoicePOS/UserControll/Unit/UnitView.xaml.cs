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

namespace InvoicePOS.UserControll.Unit
{
    /// <summary>
    /// Interaction logic for UnitView.xaml
    /// </summary>
    public partial class UnitView : Window
    {
        UnitViewModel _UnitModel;
        public UnitView()
        {
            InitializeComponent();
            _UnitModel = new UnitViewModel();
            this.DataContext = _UnitModel;
        }
    }
}
