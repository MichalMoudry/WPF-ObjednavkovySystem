﻿#pragma checksum "..\..\..\..\Views\Pages\ShellPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "398631B98E95A99A40DBD603E9425EC5C8772D1A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Tento kód byl generován nástrojem.
//     Verze modulu runtime:4.0.30319.42000
//
//     Změny tohoto souboru mohou způsobit nesprávné chování a budou ztraceny,
//     dojde-li k novému generování kódu.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.IconPacks;
using ObjednavkovySystem.Views.Pages;
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


namespace ObjednavkovySystem.Views.Pages {
    
    
    /// <summary>
    /// ShellPage
    /// </summary>
    public partial class ShellPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\..\..\Views\Pages\ShellPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView OrdersList;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Views\Pages\ShellPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddOrderButton;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Views\Pages\ShellPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView CustomersList;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\Views\Pages\ShellPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddCustomerButton;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\..\Views\Pages\ShellPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView CarsList;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\Views\Pages\ShellPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddCarButton;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\..\Views\Pages\ShellPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EmployeesList;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\..\Views\Pages\ShellPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddEmployeeButton;
        
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
            System.Uri resourceLocater = new System.Uri("/ObjednavkovySystem;component/views/pages/shellpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Pages\ShellPage.xaml"
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
            this.OrdersList = ((System.Windows.Controls.ListView)(target));
            
            #line 31 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.OrdersList.Loaded += new System.Windows.RoutedEventHandler(this.List_Loaded);
            
            #line default
            #line hidden
            
            #line 31 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.OrdersList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.List_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AddOrderButton = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.AddOrderButton.Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CustomersList = ((System.Windows.Controls.ListView)(target));
            
            #line 67 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.CustomersList.Loaded += new System.Windows.RoutedEventHandler(this.List_Loaded);
            
            #line default
            #line hidden
            
            #line 67 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.CustomersList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.List_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AddCustomerButton = ((System.Windows.Controls.Button)(target));
            
            #line 82 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.AddCustomerButton.Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CarsList = ((System.Windows.Controls.ListView)(target));
            
            #line 102 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.CarsList.Loaded += new System.Windows.RoutedEventHandler(this.List_Loaded);
            
            #line default
            #line hidden
            
            #line 102 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.CarsList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.List_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.AddCarButton = ((System.Windows.Controls.Button)(target));
            
            #line 117 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.AddCarButton.Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.EmployeesList = ((System.Windows.Controls.ListView)(target));
            
            #line 137 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.EmployeesList.Loaded += new System.Windows.RoutedEventHandler(this.List_Loaded);
            
            #line default
            #line hidden
            
            #line 137 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.EmployeesList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.List_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.AddEmployeeButton = ((System.Windows.Controls.Button)(target));
            
            #line 152 "..\..\..\..\Views\Pages\ShellPage.xaml"
            this.AddEmployeeButton.Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

