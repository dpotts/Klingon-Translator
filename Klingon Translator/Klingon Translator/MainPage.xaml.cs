using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Klingon_Translator.Resources;


namespace Klingon_Translator {

    public partial class MainPage : PhoneApplicationPage {

        // Constructor
        public MainPage() {
            InitializeComponent();
        }

        private void en2kl_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Translator.xaml?type=en2kl", UriKind.Relative));
        }

        private void kl2en_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Translator.xaml?type=kl2en", UriKind.Relative));
        }
    }
}