using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for IPAddressDialog.xaml
    /// </summary>
    public partial class IPAddressDialog : Window
    {
        public IPAddressDialog()
        {
            InitializeComponent();
        }

        private void setIPAddress(object sender, RoutedEventArgs e)
        {
            String addy = IPAddress.Text;
            Application.Current.Properties["ServerIP"] = addy;
            System.Windows.MessageBox.Show(addy);
            this.Close();
        }

    }
}
