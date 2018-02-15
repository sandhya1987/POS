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
using InvoicePOS.Models;
namespace InvoicePOS.UserControll.Estimate
{
    /// <summary>
    /// Interaction logic for ViewEstimate.xaml
    /// </summary>
    public partial class ViewEstimate : Window
    {
        EstimateInvoiceViewModel _EVM;
        public static TextBox TaxAmount;
        public static TextBox DiscountAmount;
        public static TextBox TotalAmount;
        public static TextBox TotalQty;
        public static TextBox TotalItem;
        
        public static TextBlock EstimateNo;
        public static TextBlock EstimateDate;
        
        
        public ViewEstimate()
        {
            _EVM = new EstimateInvoiceViewModel();
            var data = App.Current.Properties["ViewEstimate"];
            this.DataContext = _EVM;
            InitializeComponent();

            textBlock16.Text = _EVM.EstimateDate.ToString();
            EstimateDate = textBlock16;

            textBlock2.Text = _EVM.EstimateNo;
            EstimateNo = textBlock2;

            textBlock31.Text = _EVM.TotalTax.ToString();
            TotalAmount = textBlock31;

            textBlock33.Text = "";
            DiscountAmount = textBlock33;

            textBlock37.Text = "";
            TotalItem = textBlock37;

            textBlock40.Text = _EVM.TotalItemQty.ToString();
            TotalQty = textBlock40;

            textBlock43.Text = _EVM.TotalPrice.ToString();
            TotalAmount = textBlock43;
            //DataContext = this;
        }

    }
}
