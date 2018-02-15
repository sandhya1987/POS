using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InvoicePOS.Helpers
{
    class TextBoxHelpers : TextBox
    {
        // public static Regex numerciRegex = new Regex(@"^[0-9]\d{0,9}(\.\d{1,2})?%?$");
        //   public static Regex numerciRegex = new Regex(@"(\+|-)?[1-9][0-9]*(\.[0-9]*)?");
        //   public static Regex numerciRegex = new Regex(@"^(0{0,2}[1-9]|0?[1-9][0-9]|[1-9][0-9][0-9])$");


        public static Key dot_key = new Key();
        public static bool GetIsNumeric(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNumericProperty);
        }

        public static void SetIsNumeric(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNumericProperty, value);
        }

        public static readonly DependencyProperty IsNumericProperty =
            DependencyProperty.RegisterAttached("IsNumeric", typeof(bool), typeof(TextBoxHelpers), new PropertyMetadata(false,
         new PropertyChangedCallback((s, e) =>
         {
             TextBox targetTextbox = s as TextBox;
             if (targetTextbox != null)
             {
                 if ((bool)e.OldValue && !((bool)e.NewValue))
                 {
                     targetTextbox.PreviewTextInput -= targetTextbox_PreviewTextInput;

                 }
                 if ((bool)e.NewValue)
                 {
                     targetTextbox.PreviewTextInput += targetTextbox_PreviewTextInput;
                     targetTextbox.PreviewKeyDown += targetTextbox_PreviewKeyDown;
                     targetTextbox.TextChanged += new TextChangedEventHandler(targetTextbox_TextChanged);
                 }
             }
         })));

        static void targetTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //get the textbox that fired the event
            var textBox = sender as TextBox;
            //if (textBox == null) return;
            //var text="";
            //if (dot_key == Key.Decimal || dot_key == Key.OemPeriod)
            //{
            //    text = textBox.Text + ".";
            //}
            //else
            //{
            //    text = textBox.Text;
            //}
            //var output = new StringBuilder();
            ////use this boolean to determine if the dot already exists
            ////in the text so far.
            //var dotEncountered = false;
            ////loop through all of the text
            //if (textBox.Text == "0")
            //{
            //    textBox.Text = "";
            //}
            //for (int i = 0; i < text.Length; i++)
            //{
            //    var c = text[i];
            //    if (char.IsDigit(c))
            //    {
            //        //append any digit.
            //        output.Append(c);
            //    }
            //    else if (!dotEncountered && c == '.')
            //    {
            //        //append the first dot encountered
            //        output.Append(c);
            //        dotEncountered = true;
            //    }
            //    else
            //    {
            //        textBox.Text = "";
            //        MessageBox.Show("Incorrect format", "Price Format", MessageBoxButton.OK, MessageBoxImage.Error);
            //        return;

            //    }
            //}
            //if (textBox.Text != "")
            //{
            //    var newText = output.ToString();
            //    textBox.Text = newText;
            //    textBox.CaretIndex = newText.Length;
            //}
            //set the caret to the end of text



            textBox.Focus();
            String PText = String.Empty;

            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && count == 0))
                {
                    PText += c;
                    if (c == '.')
                        count += 1;
                }
            }
            textBox.Text = PText;
            textBox.CaretIndex = PText.Length;
            textBox.Focus();

            //TextBox tb = sender as TextBox;
            //LoginModel mod = new LoginModel();
            //if (tb == null && string.IsNullOrWhiteSpace(tb.Text))
            //{
            //    mod.AccessCode = Convert.ToInt32(tb.Text);
            //}
            //else if (tb != null || string.IsNullOrWhiteSpace(tb.Text))
            //{
            //    if (tb.Text == "0")
            //    {
            //        mod.AccessCode = Convert.ToInt32(tb.Text);
            //        tb.Text = "";
            //    }

            //    tb.Focus();
            //    tb.SelectionStart = tb.Text.Length;



            //}
            //if (numerciRegex.IsMatch(tb.Text) == false || (tb.Text != "" || tb.Text != null))
            //{
            //    if (dot_key == Key.Decimal)
            //    {
            //        MessageBox.Show("Enter digit after decimal point", "Decimal Point", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }
            //    else if (dot_key == Key.OemPeriod)
            //    {
            //        MessageBox.Show("Enter digit after decimal point", "Decimal Point", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }

            //    else if (tb.Text == "" || tb.Text == null || tb.Text == ".")
            //    {
            //        return;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Incorrect format", "Price Format", MessageBoxButton.OK, MessageBoxImage.Error);
            //        tb.Text = "";
            //        tb.Focus();
            //        tb.SelectionStart = tb.Text.Length;
            //        // mod.AccessCode = Convert.ToInt32(tb.Text);
            //        return;
            //    }
            //}
            //}

        }
        private bool IsActionKey(Key inKey)
        {
            return inKey == Key.Tab || inKey == Key.Enter || inKey == Key.Delete || inKey == Key.Back || inKey == Key.Return || Keyboard.Modifiers.HasFlag(ModifierKeys.Alt);
        }

        static void targetTextbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Decimal && e.Key != Key.NumPad0 && e.Key != Key.NumPad1 && e.Key != Key.NumPad2 && e.Key != Key.NumPad3 && e.Key != Key.NumPad4 && e.Key != Key.NumPad4
                    && e.Key != Key.NumPad5 && e.Key != Key.NumPad6 && e.Key != Key.NumPad7 && e.Key != Key.NumPad8 && e.Key != Key.NumPad9
                    && e.Key != Key.D0 && e.Key != Key.D1 && e.Key != Key.D2 && e.Key != Key.D3 && e.Key != Key.D4 && e.Key != Key.D5 && e.Key != Key.D6 && e.Key != Key.D7 && e.Key != Key.D8 && e.Key != Key.D9 && e.Key != Key.OemPeriod)
                {
                    e.Handled = e.Key == Key.Space;
                }
                dot_key = e.Key;
            }
            catch (Exception exx)
            { }
        }

        static void targetTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                Char newChar = e.Text.ToString()[0];
                if (dot_key != Key.Decimal && dot_key != Key.OemPeriod)
                {
                    e.Handled = !Char.IsNumber(newChar);
                }
                else
                {
                    e.Handled = Char.IsNumber(newChar);
                }
            }
            catch (Exception exx)
            { }
        }









        //is focus property

        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(TextBoxHelpers), new UIPropertyMetadata(false, OnIsFocusedChanged));

        public static bool GetIsFocused(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject dependencyObject, bool value)
        {
            dependencyObject.SetValue(IsFocusedProperty, value);
        }

        public static void OnIsFocusedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            TextBox textBox = dependencyObject as TextBox;
            bool newValue = (bool)dependencyPropertyChangedEventArgs.NewValue;
            bool oldValue = (bool)dependencyPropertyChangedEventArgs.OldValue;
            if (newValue && !oldValue && !textBox.IsFocused) textBox.Focus();
        }
    }
}
