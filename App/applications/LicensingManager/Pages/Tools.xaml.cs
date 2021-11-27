using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LicensingManager.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Tools : Page
    {
        public Tools()
        {
            this.InitializeComponent();
            MyFrame.Content = new Fingerprint();
        }

        private void fingerprintButton_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Fingerprint();
        }

        private void productButton_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Product();
        }

        private void licenseButton_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new License();
        }
    }
}
