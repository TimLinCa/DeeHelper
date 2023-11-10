﻿#pragma checksum "..\..\..\Options\GeneralOptions.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9A0745A72D3586C3780A2E956D827100C567203956EFB3315253B6080B0C3FA0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DeeHelper.Options;
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


namespace DeeHelper.Options {
    
    
    /// <summary>
    /// GeneralOptions
    /// </summary>
    public partial class GeneralOptions : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Options\GeneralOptions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BusnissPathTextBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Options\GeneralOptions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ActionCodeStartTextBox;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Options\GeneralOptions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ActionCodeEndTextBox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Options\GeneralOptions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox VaildationCodeStartTextBox;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Options\GeneralOptions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox VaildationCodeEndTextBox;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Options\GeneralOptions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ScanBusinessObjectsCheckBox;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Options\GeneralOptions.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SortUsingLine;
        
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
            System.Uri resourceLocater = new System.Uri("/DeeHelper;component/options/generaloptions.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Options\GeneralOptions.xaml"
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
            this.BusnissPathTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            
            #line 20 "..\..\..\Options\GeneralOptions.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BusnissTierFolder);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ActionCodeStartTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 29 "..\..\..\Options\GeneralOptions.xaml"
            this.ActionCodeStartTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ACStartChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ActionCodeEndTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\..\Options\GeneralOptions.xaml"
            this.ActionCodeEndTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.ACEndChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.VaildationCodeStartTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 48 "..\..\..\Options\GeneralOptions.xaml"
            this.VaildationCodeStartTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.VCStartChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.VaildationCodeEndTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 58 "..\..\..\Options\GeneralOptions.xaml"
            this.VaildationCodeEndTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.VCEnbChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ScanBusinessObjectsCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 61 "..\..\..\Options\GeneralOptions.xaml"
            this.ScanBusinessObjectsCheckBox.Click += new System.Windows.RoutedEventHandler(this.ScanBusinessObjectClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SortUsingLine = ((System.Windows.Controls.CheckBox)(target));
            
            #line 64 "..\..\..\Options\GeneralOptions.xaml"
            this.SortUsingLine.Click += new System.Windows.RoutedEventHandler(this.SortReferenceClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
