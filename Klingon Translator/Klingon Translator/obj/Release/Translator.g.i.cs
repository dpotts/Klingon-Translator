﻿#pragma checksum "C:\Users\Daniel\Documents\Klingon-Translator\Klingon Translator\Klingon Translator\Translator.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "69A8F11FB0D331353E03C2C510131076"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Klingon_Translator {
    
    
    public partial class Translator : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox txtToTrans;
        
        internal System.Windows.Controls.TextBlock to_language_name;
        
        internal System.Windows.Controls.TextBlock lblTranslatedText;
        
        internal System.Windows.Controls.RadioButton klingon;
        
        internal System.Windows.Controls.RadioButton kronos;
        
        internal System.Windows.Controls.TextBlock from_language_name;
        
        internal System.Windows.Controls.HyperlinkButton back_to_main;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Klingon%20Translator;component/Translator.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.txtToTrans = ((System.Windows.Controls.TextBox)(this.FindName("txtToTrans")));
            this.to_language_name = ((System.Windows.Controls.TextBlock)(this.FindName("to_language_name")));
            this.lblTranslatedText = ((System.Windows.Controls.TextBlock)(this.FindName("lblTranslatedText")));
            this.klingon = ((System.Windows.Controls.RadioButton)(this.FindName("klingon")));
            this.kronos = ((System.Windows.Controls.RadioButton)(this.FindName("kronos")));
            this.from_language_name = ((System.Windows.Controls.TextBlock)(this.FindName("from_language_name")));
            this.back_to_main = ((System.Windows.Controls.HyperlinkButton)(this.FindName("back_to_main")));
        }
    }
}

