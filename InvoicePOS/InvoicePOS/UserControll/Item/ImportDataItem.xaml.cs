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
using System.IO;
using System.Windows;
using Microsoft.Win32;
using InvoicePOS.Models;
using System.Data;

namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for ImportDataItem.xaml
    /// </summary>
    public partial class ImportDataItem : Window
    {
        ItemViewModel _ItemViewModel = new ItemViewModel();
        ItemModel _ItemModel = new ItemModel();
        public static TextBox TextFile;
        public ImportDataItem()
        {
            InitializeComponent();
            _ItemViewModel = new ItemViewModel();
            this.DataContext = _ItemViewModel;

            //TextFile = txtbox1;
        }

        //private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    OpenFileDialog openfile = new OpenFileDialog();
        //    openfile.DefaultExt = ".xlsx";
        //    openfile.Filter = "(.xlsx)|*.xlsx";
        //    //openfile.ShowDialog();

        //    var browsefile = openfile.ShowDialog();

        //    if (browsefile == true)
        //    {
        //        TextFile.Text = openfile.FileName;

        //        Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
        //        //Static File From Base Path...........
        //        //Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "TestExcel.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        //        //Dynamic File Using Uploader...........
        //        Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(TextFile.Text.ToString(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
        //        Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
        //        Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

        //        string strCellData = "";
        //        double douCellData;
        //        int rowCnt = 0;
        //        int colCnt = 0;

        //        DataGrid dt = new DataGrid();
        //        for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
        //        {
        //            string strColumn = "";
        //            strColumn = (string)(excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
        //            //dt.Columns.Add(strColumn, typeof(string));
        //        }

        //        for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
        //        {
        //            string strData = "";
        //            for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
        //            {
        //                try
        //                {
        //                    strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
        //                    strData += strCellData + "|";
        //                }
        //                catch (Exception ex)
        //                {
        //                    douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
        //                    strData += douCellData.ToString() + "|";
        //                }
        //            }
        //            strData = strData.Remove(strData.Length - 1, 1);
        //            //dt.Rows.Add(strData.Split('|'));
        //        }

        //        //dataGridView1.ItemsSource = dt.DefaultView;

        //        excelBook.Close(true, null, null);
        //        excelApp.Quit();
        //    }
        //}

        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();
            TextFile.Text = openfile.FileName;
        }

        private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
        {
            //OpenFileDialog openfile = new OpenFileDialog();


            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            //Static File From Base Path...........
            //Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "TestExcel.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            //Dynamic File Using Uploader...........
            Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(TextFile.Text.ToString(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
            Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

            string strCellData = "";
            double douCellData;
            int rowCnt = 0;
            int colCnt = 0;

            DataTable dt = new DataTable();
            for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
            {
                string strColumn = "";
                strColumn = (string)(excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                //dt.Header = strColumn;
                //dt.Binding = new Binding(strColumn);
                //datagrid.Columns.Add(dt);
                dt.Columns.Add(strColumn, typeof(string));

            }
            string strData = "";
            DataGridRow dr = new System.Windows.Controls.DataGridRow();
            for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
            {

                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    try
                    {
                        strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                        strData += strCellData + "|";
                    }
                    catch (Exception ex)
                    {
                        douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                        strData += douCellData.ToString() + "|";
                    }
                }
                strData = strData.Remove(strData.Length - 1, 1);
                ObservableCollection<ItemModel> Listgrid = new ObservableCollection<ItemModel>();
                
                dt.Rows.Add(strData.Split('|'));
                //datagrid.Rows.Add(strData);
                //datagrid.Items.Add(strData);
                    
            }

            //datagrid.ItemsSource = dt.DefaultView;
            excelBook.Close(true, null, null);
            excelApp.Quit();
        }

       
    }
}
