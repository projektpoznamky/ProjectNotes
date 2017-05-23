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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string username;
        private string password;
        public MainWindow()
        {
            InitializeComponent();
            passwordBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            passwordBox.VerticalContentAlignment = VerticalAlignment.Top;
            
            
            //loginPanel.Visibility = Visibility.Hidden;
        }

        private void DockPanel_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            username = usernameTextBox.Text;
            password = passwordBox.Password;

        }
    }
}
