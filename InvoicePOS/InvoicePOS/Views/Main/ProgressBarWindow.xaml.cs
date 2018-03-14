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
using System.ComponentModel;
using InvoicePOS.Helpers;

namespace InvoicePOS.Views.Main
{
    /// <summary>
    /// Interaction logic for ProgressBarWindow.xaml
    /// </summary>
    public partial class ProgressBarWindow : Window
    {
        public ProgressBarWindow()
        {   
            InitializeComponent();
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = 100;
        }

       

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            worker.RunWorkerAsync();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string server = "data source=34.209.147.13;initial catalog=NEW_POS_DB;user id=makrov;password=Qsefthuk786;Integrated Security=False";
                string client = "data source=localhost;initial catalog=NEW_POS_DB;user id=makrov;password=Qsefthuk786;Integrated Security=False";

                DBSynchronizer.Synchronize(server, client, sender);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.Value = e.ProgressPercentage;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Synchronization completed");
            this.Close();
        }
    }
}
