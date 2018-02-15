using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using InvoicePOS.Models;

namespace InvoicePOS.ViewModels
{
    public class FolderItem : SimpleObject
    {
        #region Name

        /// <summary>
        /// The name that can be displayed or used as an
        /// ID to perform more complex styling.
        /// </summary>
        private string name;


        /// <summary>
        /// The name that can be displayed or used as an
        /// ID to perform more complex styling.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                //ignore if values are equal
                if (value == name) return;

                name = value;
                OnPropertyChanged("Name");
            }
        }

        #endregion

        #region Items

        /// <summary>
        /// The child items of the folder.
        /// </summary>
        private IEnumerable items;


        /// <summary>
        /// The child items of the folder.
        /// </summary>
        public IEnumerable Items
        {
            get { return items; }
            set
            {
                //ignore if values are equal
                if (value == items) return;

                items = value;
                OnPropertyChanged("Items");
            }
        }

        #endregion


        public FolderItem()
        {
        }



        /// <summary>
        /// This method is invoked by WPF to render the object if
        /// no data template is available.
        /// </summary>
        /// <returns>Returns the value of the <see cref="Name"/>
        /// property.</returns>
        public override string ToString()
        {
            return string.Format("{0}: {1}", GetType().Name, Name);
        }

    }
}
