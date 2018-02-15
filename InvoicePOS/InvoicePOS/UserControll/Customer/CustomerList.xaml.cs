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
using InvoicePOS.Helpers;
using InvoicePOS.UserControll.Item;
using InvoicePOS.UserControll.Payment;
using InvoicePOS.Models;

namespace InvoicePOS.UserControll.Customer
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class CustomerList : UserControl
    {
        CustomerViewModel _CVM = new CustomerViewModel();        
        public CustomerList()
        {
            InitializeComponent();
            _CVM = new CustomerViewModel();
            this.DataContext = _CVM;
           
        }

        private void grdCustomers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var str = (CustomerModel)dataGrid1.SelectedItem;
            _CVM.View_Customer();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {

            //dataGridView1.SelectAllCells();
            //dataGridView1.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            //ApplicationCommands.Copy.Execute(null, dataGridView1);
            //String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //String result = (string)Clipboard.GetData(DataFormats.Text);
            //dataGridView1.UnselectAllCells();
            //System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"C:\Users\test.xlsx");
            //file1.WriteLine(result.Replace(',', ' '));
            //file1.Close();
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook excelBook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Sheets[1];


            for (int j = 0; j < dataGrid1.Columns.Count; j++)
            {
                Microsoft.Office.Interop.Excel.Range excelRange = (Microsoft.Office.Interop.Excel.Range)excelSheet.Cells[1, j + 1];
                excelRange.Value2 = dataGrid1.Columns[j].Header;
            }

            for (int i = 0; i < dataGrid1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGrid1.Items.Count; j++)
                {

                    TextBlock b = dataGrid1.Columns[i].GetCellContent(dataGrid1.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range excelRange = (Microsoft.Office.Interop.Excel.Range)excelSheet.Cells[j + 2, i + 1];
                    if (b != null)
                    {
                        excelRange.Value2 = b.Text;
                    }
                    else
                    {
                        excelRange.Value2 = "";
                    }
                }
            }
            MessageBox.Show("Exporting DataGrid data to Excel file created");
        }
    }
}
