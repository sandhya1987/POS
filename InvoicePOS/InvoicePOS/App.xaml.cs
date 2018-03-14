using System.Windows;
using System.Threading;

namespace InvoicePOS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Thread SyncThread { get; set; }
    }
}
