using InvoicePOS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicePOS.ViewModels
{
   public class ChildViewModel : INotifyPropertyChanged
    {
        #region Data

        readonly ReadOnlyCollection<ChildViewModel> _children;
        readonly ChildViewModel _parent;
       readonly ReportGroupModel _person;

        bool _isExpanded;
        bool _isSelected;

        #endregion // Data

        #region Constructors

        public ChildViewModel(ReportGroupModel person)
            : this(person, null)
        {
        }

        private ChildViewModel(ReportGroupModel person, ChildViewModel parent)
        {
            _person = person;
            _parent = parent;

            _children = new ReadOnlyCollection<ChildViewModel>(
                    (from child in _person.Children
                     select new ChildViewModel(child, this))
                     .ToList<ChildViewModel>());
        }

        #endregion // Constructors

        #region Person Properties

        public ReadOnlyCollection<ChildViewModel> Children
        {
            get { return _children; }
        }

        public string GROUP_NAME
        {
            get { return _person.GROUP_NAME; }
        }

        #endregion // Person Properties

        #region Presentation Members

        #region IsExpanded

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is expanded.
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (value != _isExpanded)
                {
                    _isExpanded = value;
                    this.OnPropertyChanged("IsExpanded");
                }

                // Expand all the way up to the root.
                if (_isExpanded && _parent != null)
                    _parent.IsExpanded = true;
            }
        }

        #endregion // IsExpanded

        #region IsSelected

        /// <summary>
        /// Gets/sets whether the TreeViewItem 
        /// associated with this object is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion // IsSelected

        #region NameContainsText

        public bool NameContainsText(string text)
        {
            if (String.IsNullOrEmpty(text) || String.IsNullOrEmpty(this.GROUP_NAME))
                return false;

            return this.GROUP_NAME.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1;
        }

        #endregion // NameContainsText

        #region Parent

        public ChildViewModel Parent
        {
            get { return _parent; }
        }

        #endregion // Parent

        #endregion // Presentation Members        

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // INotifyPropertyChanged Members
    }
}
