﻿#pragma checksum "..\..\..\..\src\views\PreferencesMenuUI.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AE8090E66FDD89421D92C7A2D05EF3B23653F42C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
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
using WinMaths.src.views;


namespace WinMaths.src.views {
    
    
    /// <summary>
    /// PreferencesMenuUI
    /// </summary>
    public partial class PreferencesMenuUI : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPopUpLogout;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame PreferencesMenu;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridMenu;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonCloseMenu;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonOpenMenu;
        
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
            System.Uri resourceLocater = new System.Uri("/WinMaths;component/src/views/preferencesmenuui.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
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
            
            #line 9 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            ((WinMaths.src.views.PreferencesMenuUI)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Panel_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ButtonPopUpLogout = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            this.ButtonPopUpLogout.Click += new System.Windows.RoutedEventHandler(this.ButtonPopUpLogout_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PreferencesMenu = ((System.Windows.Controls.Frame)(target));
            return;
            case 4:
            this.GridMenu = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.ButtonCloseMenu = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            this.ButtonCloseMenu.Click += new System.Windows.RoutedEventHandler(this.ButtonCloseMenu_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonOpenMenu = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            this.ButtonOpenMenu.Click += new System.Windows.RoutedEventHandler(this.ButtonOpenMenu_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 66 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            ((System.Windows.Controls.ListViewItem)(target)).MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.GraphicDephinition_MouseDoubleClick_1);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 68 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            ((MaterialDesignThemes.Wpf.PackIcon)(target)).MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.GraphicDephinition_MouseDoubleClick_1);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 72 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            ((System.Windows.Controls.ListViewItem)(target)).PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.GraphicTable_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 74 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            ((MaterialDesignThemes.Wpf.PackIcon)(target)).PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.GraphicTable_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 78 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            ((System.Windows.Controls.ListViewItem)(target)).PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Import_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 80 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            ((MaterialDesignThemes.Wpf.PackIcon)(target)).PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Import_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 84 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            ((System.Windows.Controls.ListViewItem)(target)).PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Export_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 86 "..\..\..\..\src\views\PreferencesMenuUI.xaml"
            ((MaterialDesignThemes.Wpf.PackIcon)(target)).PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.Export_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

