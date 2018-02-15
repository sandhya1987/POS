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
    public class TextBoxInteger : TextBox
    {

        public static bool GetIsNumeric(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNumericProperty);
        }

        public static void SetIsNumeric(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNumericProperty, value);
        }

        public static readonly DependencyProperty IsNumericProperty =
            DependencyProperty.RegisterAttached("IsNumber", typeof(bool), typeof(DecimalHelper), new PropertyMetadata(false,
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
            TextBox tb = sender as TextBox;

            //LoginModel mod = new LoginModel();
            //if (tb == null && string.IsNullOrWhiteSpace(tb.Text))
            //{
            //    mod.AccessCode = tb.Text;
            //}
            //else if (tb != null)
            //{
            //    if (!string.IsNullOrWhiteSpace(tb.Text))
            //    {
            //        mod.AccessCode = tb.Text;
            //        if (mod.AccessCode != "")
            //        {
            //            tb.Text = mod.AccessCode.ToString();
            //        }
            //        else
            //        {

            //            tb.Text = "";
            //        }
            //    }

            //    tb.Focus();
            //    tb.SelectionStart = tb.Text.Length;


           // }
        }

        static void targetTextbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        static void targetTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Char newChar = e.Text.ToString()[0];
            e.Handled = !Char.IsNumber(newChar);
        }
    }
}
