using InvoicePOS.Models;
using InvoicePOS.ViewModels;
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

namespace InvoicePOS.UserControll.Category
{
    /// <summary>
    /// Interaction logic for CatagoryView.xaml
    /// </summary>
    public partial class CatagoryView : Window
    {
        CategoryViewModel _CategoryViewModel;
        public CatagoryView()
        {
            InitializeComponent();
            _CategoryViewModel=new CategoryViewModel();
            this.DataContext = _CategoryViewModel;
        }
    }
}
