﻿#pragma checksum "..\..\..\..\Views\RegisterView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8B5C67E7D17AA103124E69F24CCC35F78EF6B379"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using OnlineRestaurantWpf.ViewModels;
using OnlineRestaurantWpf.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace OnlineRestaurantWpf.Views {
    
    
    /// <summary>
    /// RegisterView
    /// </summary>
    public partial class RegisterView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 63 "..\..\..\..\Views\RegisterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBoxRegister;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Views\RegisterView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox ConfirmPasswordBoxRegister;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/OnlineRestaurantWpf;component/views/registerview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\RegisterView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PasswordBoxRegister = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 63 "..\..\..\..\Views\RegisterView.xaml"
            this.PasswordBoxRegister.PasswordChanged += new System.Windows.RoutedEventHandler(this.PasswordBoxRegister_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ConfirmPasswordBoxRegister = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 66 "..\..\..\..\Views\RegisterView.xaml"
            this.ConfirmPasswordBoxRegister.PasswordChanged += new System.Windows.RoutedEventHandler(this.ConfirmPasswordBoxRegister_PasswordChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

