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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using InvoicePOS.Models;

namespace InvoicePOS.UserControll.TaxManagement
{
    /// <summary>
    /// Interaction logic for TaxList.xaml
    /// </summary>
    public partial class TaxList : UserControl
    {
        TaxViewModel ViewModel;
        public static DataGrid CatGridRef;
        public static DataGrid ListGridRef;
        public static DataGrid ListGrdMainRef;
        //ObservableCollection<TaxManagementModel> ListGrid1 = new ObservableCollection<TaxManagementModel>();
        ObservableCollection<TaxManagementModel> ListGrid = new ObservableCollection<TaxManagementModel>();
        List<TaxManagementModel> ListGridBuss = new List<TaxManagementModel>();
        public TaxList()
        {
            InitializeComponent();
            ViewModel = new TaxViewModel();
            this.DataContext = ViewModel;
            //dataGrid.ItemsSource = ListGrid1;
            //CatGridRef = dataGrid;

            dataGrid.ItemsSource = ListGrid;
            ListGridRef = dataGrid;

            dataGrid1.ItemsSource = ListGridBuss;
            ListGrdMainRef = dataGrid1;

        }

        
    }
}
