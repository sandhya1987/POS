using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using InvoicePOS.Models;

namespace InvoicePOS.Views
{
    /// <summary>
    /// Interaction logic for AutoCompleteTextBox.xaml
    /// </summary>
    public partial class AutoCompleteTaxPaid : Canvas
    {
        #region Members
        private VisualCollection controls;
        private TextBox textBox;
        private ComboBox comboBox;
        private ObservableCollection<AutoCompleteTax> autoCompletionList;
        private System.Timers.Timer keypressTimer;
        private delegate void TextChangedCallback();
        private bool insertText;
        private int delayTime;
        private int searchThreshold;
        #endregion

        #region Constructor
        public AutoCompleteTaxPaid()
        {
            controls = new VisualCollection(this);
            InitializeComponent();

            autoCompletionList = new ObservableCollection<AutoCompleteTax>();
            searchThreshold = 1;        // default threshold to 1 char

            // set up the key press timer
            keypressTimer = new System.Timers.Timer();
            keypressTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);

            // set up the text box and the combo box
            comboBox = new ComboBox();
            comboBox.IsSynchronizedWithCurrentItem = true;
            comboBox.IsTabStop = false;
            comboBox.SelectionChanged += new SelectionChangedEventHandler(comboBox_SelectionChanged);

            textBox = new TextBox();
            textBox.TextChanged += new TextChangedEventHandler(textBox_TextChanged);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;

            controls.Add(comboBox);
            controls.Add(textBox);
        }
        #endregion

        #region Methods
        public string Text
        {
            get { return textBox.Text; }
            set
            {
                insertText = true;
                textBox.Text = value;
            }
        }

        public int DelayTime
        {
            get { return delayTime; }
            set { delayTime = value; }
        }

        public int Threshold
        {
            get { return searchThreshold; }
            set { searchThreshold = value; }
        }

        public void AddItem(AutoCompleteTax entry)
        {
            autoCompletionList.Add(entry);
            comboBox.DisplayMemberPath = entry.DisplayName;
            comboBox.SelectedValuePath = entry.DisplayId.ToString();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != comboBox.SelectedItem)
            {              
                ComboBoxItem cbItem = (ComboBoxItem)comboBox.SelectedItem;
                foreach (var item in autoCompletionList)
                {
                    if (item.DisplayName == cbItem.Content.ToString())
                    {
                        App.Current.Properties["TaxPaidid"] = item.DisplayId;
                    }
                }
                
                insertText = true;
               // ComboBoxItem cbItem = (ComboBoxItem)comboBox.SelectedItem;
                textBox.Text = cbItem.Content.ToString();
                App.Current.Properties["TaxPaidName"] = textBox.Text;

            }
        }

        private void TextChanged()
        {
            try
            {
                comboBox.Items.Clear();
                if (textBox.Text.Length >= searchThreshold)
                {
                    foreach (var entry in autoCompletionList)
                    {
                        foreach (string word in entry.KeywordStrings)
                        {
                            if (word.StartsWith(textBox.Text, StringComparison.CurrentCultureIgnoreCase))
                            {
                                ComboBoxItem cbItem = new ComboBoxItem();
                                cbItem.Content = entry.ToString();
                                comboBox.Items.Add(cbItem);
                                comboBox.Focus();
                                break;
                            }
                        }
                    }
                    comboBox.IsDropDownOpen = comboBox.HasItems;
                }
                else
                {
                    comboBox.IsDropDownOpen = false;
                }
            }
            catch { }
        }

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            keypressTimer.Stop();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new TextChangedCallback(this.TextChanged));
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // text was not typed, do nothing and consume the flag
            if (insertText == true) insertText = false;

            // if the delay time is set, delay handling of text changed
            else
            {
                if (delayTime > 0)
                {
                    keypressTimer.Interval = delayTime;
                    keypressTimer.Start();
                }
                else TextChanged();
            }
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            textBox.Arrange(new Rect(arrangeSize));
            comboBox.Arrange(new Rect(arrangeSize));
            return base.ArrangeOverride(arrangeSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return controls[index];
        }

        protected override int VisualChildrenCount
        {
            get { return controls.Count; }
        }
        #endregion
    }
}
