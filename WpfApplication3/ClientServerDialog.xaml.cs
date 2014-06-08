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
    /// Interaction logic for ClientServerDialog.xaml
    /// </summary>
    public partial class ClientServerDialog : Window
    {
        
        public ClientServerDialog()
        {
            
                InitializeComponent();
        }

        private void okClickHandler(object sender, RoutedEventArgs e)
        {
           
            if(Server.IsChecked.Equals(true))
            {
                Application.Current.Properties["IsServer"] = true;
   
                //e.Cancel = true;

            }
            else if(Client.IsChecked.Equals(true))
            {
                Application.Current.Properties["IsServer"] = false;
            }


            this.Close();
           // System.Windows.MessageBox.Show(Server.Content.ToString());



            
        }










    }
}
