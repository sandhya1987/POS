﻿#pragma checksum "..\..\..\..\Views\InvoiceSettings.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "42BA4FC9A983BBE970B94847029B8E1A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace InvoicePOS.Views {
    
    
    /// <summary>
    /// InvoiceSettings
    /// </summary>
    public partial class InvoiceSettings : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\..\Views\InvoiceSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Operation3;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\..\Views\InvoiceSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SalesEx3;
        
        #line default
        #line hidden
        
        
        #line 181 "..\..\..\..\Views\InvoiceSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SalesEx4;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\..\Views\InvoiceSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SalesEx5;
        
        #line default
        #line hidden
        
        
        #line 183 "..\..\..\..\Views\InvoiceSettings.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SalesEx6;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/InvoicePOS;component/views/invoicesettings.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\InvoiceSettings.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 35 "..\..\..\..\Views\InvoiceSettings.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.Operation2Checked);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\..\Views\InvoiceSettings.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.Operation2UnChecked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Operation3 = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 3:
            
            #line 47 "..\..\..\..\Views\InvoiceSettings.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.OnNumberFormatDecimalPlaceKeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 49 "..\..\..\..\Views\InvoiceSettings.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.OnNumberFormatDecimalPlaceKeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SalesEx3 = ((System.Windows.Controls.CheckBox)(target));
            
            #line 180 "..\..\..\..\Views\InvoiceSettings.xaml"
            this.SalesEx3.Checked += new System.Windows.RoutedEventHandler(this.SalesEx3_Checked);
            
            #line default
            #line hidden
            
            #line 180 "..\..\..\..\Views\InvoiceSettings.xaml"
            this.SalesEx3.Unchecked += new System.Windows.RoutedEventHandler(this.SalesEx3_UnChecked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SalesEx4 = ((System.Windows.Controls.CheckBox)(target));
            
            #line 181 "..\..\..\..\Views\InvoiceSettings.xaml"
            this.SalesEx4.Checked += new System.Windows.RoutedEventHandler(this.SalesEx4_Checked);
            
            #line default
            #line hidden
            
            #line 181 "..\..\..\..\Views\InvoiceSettings.xaml"
            this.SalesEx4.Unchecked += new System.Windows.RoutedEventHandler(this.SalesEx4_UnChecked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SalesEx5 = ((System.Windows.Controls.CheckBox)(target));
            
            #line 182 "..\..\..\..\Views\InvoiceSettings.xaml"
            this.SalesEx5.Checked += new System.Windows.RoutedEventHandler(this.SalesEx5_Checked);
            
            #line default
            #line hidden
            
            #line 182 "..\..\..\..\Views\InvoiceSettings.xaml"
            this.SalesEx5.Unchecked += new System.Windows.RoutedEventHandler(this.SalesEx5_UnChecked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SalesEx6 = ((System.Windows.Controls.CheckBox)(target));
            
            #line 183 "..\..\..\..\Views\InvoiceSettings.xaml"
            this.SalesEx6.Checked += new System.Windows.RoutedEventHandler(this.SalesEx6_Checked);
            
            #line default
            #line hidden
            
            #line 183 "..\..\..\..\Views\InvoiceSettings.xaml"
            this.SalesEx6.Unchecked += new System.Windows.RoutedEventHandler(this.SalesEx6_UnChecked);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 236 "..\..\..\..\Views\InvoiceSettings.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.OnNumberFormatDecimalPlaceKeyDown);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 238 "..\..\..\..\Views\InvoiceSettings.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.OnNumberFormatDecimalPlaceKeyDown);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 242 "..\..\..\..\Views\InvoiceSettings.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.OnNumberFormatDecimalPlaceKeyDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

