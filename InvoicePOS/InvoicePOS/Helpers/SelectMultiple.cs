using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace InvoicePOS.Helpers
{
    public class SelectMultiple
    {
        public static IList GetSelectedItems(DependencyObject obj)
        {
            return (IList)obj.GetValue(SelectedItemsProperty);
        }

        public static void SetSelectedItems(DependencyObject obj, IList value)
        {
            obj.SetValue(SelectedItemsProperty, value);
            App.Current.Properties["Multi_List"] = value;
        }

        /// <summary>
        /// Attach this property to expose the read-only SelectedItems property of a MultiSelector for data binding.
        /// </summary>
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached("SelectedItems", typeof(IList), typeof(SelectMultiple), new UIPropertyMetadata(new List<object>() as IList, OnSelectedItemsChanged));



        static SelectionChangedEventHandler GetSelectionChangedHandler(DependencyObject obj)
        {
            return (SelectionChangedEventHandler)obj.GetValue(SelectionChangedHandlerProperty);
        }
        static void SetSelectionChangedHandler(DependencyObject obj, SelectionChangedEventHandler value)
        {
            obj.SetValue(SelectionChangedHandlerProperty, value);
        }
        static readonly DependencyProperty SelectionChangedHandlerProperty =
            DependencyProperty.RegisterAttached("SelectionChangedHandler", typeof(SelectionChangedEventHandler), typeof(SelectMultiple), new UIPropertyMetadata(null));


        //d is MultiSelector (d as ListBox not supported)
        static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            if (GetSelectionChangedHandler(d) != null)
                return;

            if (d is MultiSelector)//DataGrid
            {
                MultiSelector multiselector = d as MultiSelector;
                SelectionChangedEventHandler selectionchanged = null;
                if (GetSelectedItems(d) != null)
                {
                    foreach (var selected in GetSelectedItems(d) as IList)
                        multiselector.SelectedItems.Add(selected);
                }
                selectionchanged = (sender, e) =>
                {
                    SetSelectedItems(d, multiselector.SelectedItems);
                };
                SetSelectionChangedHandler(d, selectionchanged);
                multiselector.SelectionChanged += GetSelectionChangedHandler(d);
            }
            else if (d is ListBox)
            {
                ListBox listbox = d as ListBox;
                SelectionChangedEventHandler selectionchanged = null;

                selectionchanged = (sender, e) =>
                {
                    SetSelectedItems(d, listbox.SelectedItems);
                };
                SetSelectionChangedHandler(d, selectionchanged);
                listbox.SelectionChanged += GetSelectionChangedHandler(d);
            }

        }
    }
}
