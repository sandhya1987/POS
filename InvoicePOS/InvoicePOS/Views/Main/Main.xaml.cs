using InvoicePOS.Models;
using InvoicePOS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InvoicePOS.Views.Autocomplete;
using InvoicePOS.Helpers;
using InvoicePOS.Views.CashRegister;
using System.Printing;
using System.IO;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using Apitron.PDF.Rasterizer;

namespace InvoicePOS.Views.Main
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        MainViewModel ViewModel;
        public static List<ItemModel> ItemModelList = new List<ItemModel>();
        public static DataGrid ListGridRef;
        public static TextBox ListQnt;
        public static TextBox GrossamountReff;
        public static CustomerAutoComplete CustomerMainReff;
        public static TextBox ItemMainReff;
        public static TextBox ItemSearchMainReff;
        public static AutoCompleteBusinessLoc BussLocationMainReff;
        public static TextBox DiscountMainReff;
        public static TextBox VatMainReff;
        public static TextBox NetAmountMainReff;
        public static TextBox ItemTotalMainReff;
        public static ItemScrCodeAutoComplete ScrRef;
        public static ItemNameAutoComplete NameRef1;
        public static TextBlock CashRegisterName;
        public static TextBlock BusinessLocName;
        public static TextBlock BusinessAddress;
        private FixedDocumentSequence _document;
        public Main()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();

            SetLanguageDictionary();
            this.DataContext = ViewModel;
            ObservableCollection<ItemModel> ListGrid = new ObservableCollection<ItemModel>();

            CashRegisterName = textBlock14;

            var ScrCodeList = App.Current.Properties["AutoScrCodeList"] as List<SearchCodeAutoListModel>;
            foreach (var item in ScrCodeList)
            {
                textBox9.AddItem(new SearchCodeAutoListModel
                {
                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName
                });
            }

            ScrRef = textBox9;

            var ItemNameList = App.Current.Properties["AutoItemNameList"] as List<ItemNameAutoListModel>;
            foreach (var item in ItemNameList)
            {
                textBox2.AddItem(new ItemNameAutoListModel
                {
                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName
                });
            }

            


            NameRef1 = textBox2;

            var CustNameList = App.Current.Properties["AutoCustList"] as List<CustomerAutoCompleteListModel>;
            foreach (var item in CustNameList)
            {
                textBox6.AddItem(new CustomerAutoCompleteListModel
                {
                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName
                });
            }

            CustomerMainReff = textBox6;


            var BussinessNameList = App.Current.Properties["BussLocList"] as List<AutoBussinessModel>;
            foreach (var item in BussinessNameList)
            {
                textBox8.AddItem(new AutoBussinessModel
                {
                    DisplayId = item.DisplayId,
                    DisplayName = item.DisplayName
                });
            }

            BussLocationMainReff = textBox8;


            textBox4.Text = "";
            ListQnt = textBox4;
           // Select_BarCodee.Text = "";
            Grossamount.Text = "";
            GrossamountReff = Grossamount;

            dataGrid1.ItemsSource = ListGrid;
            ListGridRef = dataGrid1;

            textBox7.Text = "";
            CustomerMainReff = textBox6;

            textBox.Text = "";
            VatMainReff = textBox;


            netamount.Text = "";
            NetAmountMainReff = netamount;

            //textBox2.Text = "";
            //ItemMainReff = textBox2;

            //textBox9.Text = "";
            //ItemSearchMainReff = textBox9;ItemTotalMainReff

            textBox5.Text = "";
            ItemTotalMainReff = textBox5;

            //textBox6.Text = "";
            BussLocationMainReff = textBox8;


            textBox3.Text = "";
            DiscountMainReff = textBox3;
            //textBox3_Copy1.Text = "";
            //SaleUnitRef = textBox3_Copy1;

            textBlock3.Text = "";
            BusinessLocName = textBlock3;

            textBlock21.Text = "";
            BusinessAddress = textBlock21;
        }
        public ICommand _CashClick;
        public ICommand CashClick
        {
            get
            {
                if (_CashClick == null)
                {
                    _CashClick = new DelegateCommand(Cash_Click);
                }
                return _CashClick;
            }
        }

        public void Cash_Click()
        {

            // WelComePage.ItemPRef.Background = System.Windows.Media.Brushes.Red;


            //App.Current.Properties["ITemAdd"] = 1;
            //App.Current.Properties["Action"] = 123;
            //App.Current.Properties["itemName"] = null;
            //App.Current.Properties["barcode"] = null;
            //App.Current.Properties["BussLocation"] = null;
            //App.Current.Properties["Qunt"] = null;
            //App.Current.Properties["Godown"] = null;
            CashReg IA = new CashReg();
            IA.Show();
            // ModalService.NavigateTo(new ItemAdd(), delegate(bool returnValue) { });


        }
        
        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("..\\Resources\\EnglishDictionary.xaml", UriKind.Relative);
                    break;
                case "de-AT":
                    dict.Source = new Uri("..\\Resources\\GermanDictionary.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri("..\\Resources\\EnglishDictionary.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dict);

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button2_Copy_Click(object sender, RoutedEventArgs e)
        {
            PrintGrid(this.GridEstimate);
        }

        private void PrintGrid(Grid grid)
        {
            if (File.Exists("C:\\test\\FixedDocumentSequence.xps"))
            {
                File.Delete("C:\\test\\FixedDocumentSequence.xps");
            }
            var xpsDocument = new XpsDocument("C:\\test\\FixedDocumentSequence.xps", FileAccess.ReadWrite);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            writer.Write(this.GridEstimate);
            System.Windows.Documents.FixedDocumentSequence Document = xpsDocument.GetFixedDocumentSequence();
            xpsDocument.Close();
            //var windows = new PrintWindow(Document);
            //windows.ShowDialog();

            //Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //dlg.FileName = "MyReport"; // Default file name
            //dlg.DefaultExt = ".xps"; // Default file extension
            //dlg.Filter = "XPS Documents (.xps)|*.xps"; // Filter files by extension

            //// Show save file dialog box
            //Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results


            PrintDialog print = new PrintDialog();
            //pDialog.PageRangeSelection = PageRangeSelection.AllPages;
            //pDialog.UserPageRangeEnabled = true;
            var data = this.GridEstimate;

            //Nullable<Boolean> print = pDialog.ShowDialog();
            if (print.ShowDialog() == true)
            {
                PrintCapabilities capabilities = print.PrintQueue.GetPrintCapabilities(print.PrintTicket);
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / grid.ActualWidth,
                                        capabilities.PageImageableArea.ExtentHeight / grid.ActualHeight);
                Transform oldTransform = grid.LayoutTransform;

                grid.LayoutTransform = new ScaleTransform(scale, scale);



                //string path = "C:\\test\\FixedDocumentSequence.xps";

                //System.IO.Packaging.Package package = System.IO.Packaging.Package.Open(path, System.IO.FileMode.Create);
                //System.Windows.Xps.Packaging.XpsDocument document = new System.Windows.Xps.Packaging.XpsDocument(package);
                //System.Windows.Xps.XpsDocumentWriter writer = System.Windows.Xps.Packaging.XpsDocument.CreateXpsDocumentWriter(document);
                //FixedDocument doc = new FixedDocument();
               
                //FixedPage page1 = new FixedPage();
                //PageContent page1Content = new PageContent();
                //((System.Windows.Markup.IAddChild)page1Content).AddChild(page1);

                ////add the element from the scrollable control to the fixed page
                ////page1.Children.Add(….);

                //doc.Pages.Add(page1Content);
                //writer.Write(doc);
                //document.Close();
                //package.Close();
                //XpsDocument xpsDocument = new XpsDocument("C:\\test\\FixedDocumentSequence.xps", FileAccess.ReadWrite);
                //FixedDocumentSequence fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
                print.PrintVisual(grid, "Test print job");
            }
        }

        private void PrintCharts(Grid grid)
        {
            PrintDialog print = new PrintDialog();
            if (print.ShowDialog() == true)
            {
                PrintCapabilities capabilities = print.PrintQueue.GetPrintCapabilities(print.PrintTicket);

                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / grid.ActualWidth,
                                        capabilities.PageImageableArea.ExtentHeight / grid.ActualHeight);

                Transform oldTransform = grid.LayoutTransform;

                grid.LayoutTransform = new ScaleTransform(scale, scale);

                Size oldSize = new Size(grid.ActualWidth, grid.ActualHeight);
                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
                grid.Measure(sz);
                ((UIElement)grid).Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight),
                    sz));
                Nullable<Boolean> printShow = print.ShowDialog();
                print.PrintVisual(grid, "Print Results");
                grid.LayoutTransform = oldTransform;
                grid.Measure(oldSize);

                ((UIElement)grid).Arrange(new Rect(new Point(0, 0),
                    oldSize));
                
                if (printShow == true)
                {
                    XpsDocument xpsDocument = new XpsDocument("C:\\FixedDocumentSequence.xps", FileAccess.ReadWrite);
                    FixedDocumentSequence fixedDocSeq = xpsDocument.GetFixedDocumentSequence();
                    //printShow.PrintDocument(fixedDocSeq.DocumentPaginator, "Test print job");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
