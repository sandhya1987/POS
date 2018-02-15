using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.ViewModels
{
   public class DataCache
    {
        private static DataCache singletonInstance;

        // You can have freedom to choose the event-driven model here
        // Using traditional Event, EventAggregator, ReactiveX, etc
        public EventHandler OnMessageChanged;

        private DataCache()
        {

        }

        public static DataCache Instance
        {
            get { return singletonInstance ?? (singletonInstance = new DataCache()); }
        }

        public string OnScreenMessage { get; set; }

        public void AddStringToMessage(string c)
        {
            if (string.IsNullOrWhiteSpace(c)) return;

            OnScreenMessage += c;
            RaiseOnMessageChanged();
        }

        public void ClearMessage()
        {
            OnScreenMessage = string.Empty;
            RaiseOnMessageChanged();
        }

        private void RaiseOnMessageChanged()
        {
            if (OnMessageChanged != null)
                OnMessageChanged(null, null);
        }
    }
}
