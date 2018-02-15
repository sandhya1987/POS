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
using InvoicePOS.ViewModels;

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for InvoiceDeliveryDetails.xaml
    /// </summary>
    public partial class InvoiceDeliveryDetails : Window
    {
        EmployeeViewModel _EmployeeViewModel = new EmployeeViewModel();
        public static TextBox EmployeeName;
        public InvoiceDeliveryDetails()
        {
            InitializeComponent();
            _EmployeeViewModel = new EmployeeViewModel();
            this.DataContext = _EmployeeViewModel;
            EmployeeName = textBox1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textBox1.IsEnabled = true;
            textBox2.IsEnabled = true;
            textBox3.IsEnabled = true;
            textBox4.IsEnabled = true;
            textBox5.IsEnabled = true;
            textBox6.IsEnabled = true;
            UpdateBtn.Visibility = Visibility.Visible;
            EditBtn.Visibility = Visibility.Hidden;
        }

        
        
    }
}
