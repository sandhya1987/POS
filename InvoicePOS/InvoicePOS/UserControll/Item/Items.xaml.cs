using InvoicePOS.Helpers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;




namespace InvoicePOS.UserControll.Item
{
    /// <summary>
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class Items : UserControl
    {
        ItemViewModel _ItemViewModel;
        public static DataGrid ListGridRef;

        public Items()
        {
           
            InitializeComponent();
            _ItemViewModel = new ItemViewModel();
            this.DataContext = _ItemViewModel;
           
        }

        //private void grdItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    var str = (ItemModel)dataGridView1.SelectedItem;
        //    _ItemViewModel.View_Item();
        //}

      
        #region IMainWindow Members

        private Stack<BackNavigationEventHandler> _backFunctions
            = new Stack<BackNavigationEventHandler>();

        //void IModalService.NavigateTo(UserControl uc, BackNavigationEventHandler backFromDialog)
        //{
        //    foreach (UIElement item in modalGrid.Children)
        //        item.IsEnabled = false;
        //    modalGrid.Children.Add(uc);

        //    _backFunctions.Push(backFromDialog);
        //}

        //void IModalService.GoBackward(bool dialogReturnValue)
        //{
        //    modalGrid.Children.RemoveAt(modalGrid.Children.Count - 1);

        //    UIElement element = modalGrid.Children[modalGrid.Children.Count - 1];
        //    element.IsEnabled = true;

        //    BackNavigationEventHandler handler = _backFunctions.Pop();
        //    if (handler != null)
        //        handler(dialogReturnValue);
        //}


        public static IModalService ModalService
        {
            get { return (IModalService)Application.Current.MainWindow; }
        }
        #endregion

        private void dataGrid1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemModel tempItems = ((DataGrid)sender).SelectedItem as ItemModel;
            App.Current.Properties["GetItemDetails"] = tempItems;
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
            

            for (int j = 0; j < dataGridView1.Columns.Count; j++) 
            {
                Microsoft.Office.Interop.Excel.Range excelRange = (Microsoft.Office.Interop.Excel.Range)excelSheet.Cells[1,j+1];
                excelRange.Value2 = dataGridView1.Columns[j].Header;
            }

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Items.Count; j++)
                {

                    TextBlock b = dataGridView1.Columns[i].GetCellContent(dataGridView1.Items[j]) as TextBlock;
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
            //private void copyAlltoClipboard()
            //{
            //    dataGridView1.SelectAll();
            //    dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            //    DataObject dataObj = dataGridView1.GetClipboardContent();
            //    if (dataObj != null)
            //        Clipboard.SetDataObject(dataObj);
            //}
            //private void Button_Click_1(object sender, EventArgs e)
            //{
            //    copyAlltoClipboard();
            //    Microsoft.Office.Interop.Excel.Application xlexcel;
            //    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            //    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            //    object misValue = System.Reflection.Missing.Value;
            //    xlexcel = new Microsoft.Office.Interop.Excel.Application();
            //    xlexcel.Visible = true;
            //    xlWorkBook = xlexcel.Workbooks.Add(misValue);
            //    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //    Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            //    CR.Select();
            //    xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
            //}
    }
}
