﻿#pragma checksum "..\..\query.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "95EF40E2DB960DA4D1BE1E772575A8A050A69CF5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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


namespace Wpf_DB_Bank {
    
    
    /// <summary>
    /// query
    /// </summary>
    public partial class query : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\query.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button q1;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\query.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button q2;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\query.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button q3;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\query.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid grid1;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\query.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid grid2;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\query.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid grid3;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\query.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox rank;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\query.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox salary;
        
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
            System.Uri resourceLocater = new System.Uri("/Wpf_DB_Bank;component/query.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\query.xaml"
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
            this.q1 = ((System.Windows.Controls.Button)(target));
            
            #line 7 "..\..\query.xaml"
            this.q1.Click += new System.Windows.RoutedEventHandler(this.q1_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.q2 = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\query.xaml"
            this.q2.Click += new System.Windows.RoutedEventHandler(this.q2_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.q3 = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\query.xaml"
            this.q3.Click += new System.Windows.RoutedEventHandler(this.q3_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.grid1 = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.grid2 = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.grid3 = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.rank = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.salary = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\query.xaml"
            this.salary.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.salary_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

