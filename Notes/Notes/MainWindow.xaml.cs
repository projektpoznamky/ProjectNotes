using MySql.Data.MySqlClient;
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
        private DB db = new DB();
        private int id_user;
        private string username;
        private string password;
        
        public MainWindow()
        {
            InitializeComponent();
            passwordBox.HorizontalContentAlignment = HorizontalAlignment.Center;
            passwordBox.VerticalContentAlignment = VerticalAlignment.Top;

            db.db_connect();
            
            
            
            //loginPanel.Visibility = Visibility.Hidden;
        }

        private void DockPanel_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void list_load()
        {
            MySqlDataReader reader;

            reader = db.db_select_notes(id_user);

            while (reader.Read())
            {
                //Vytváření poznámky
                //reader.GetString("id_note"), reader.getString("name_note"), reader.GetString("date_note")
            }

            db.db_close();
        }

        private void list()
        {
            MySqlDataReader reader;

            reader = db.db_select_notes(id_user);

            while (reader.Read())
            {
                //Vytváření poznámky
                //reader.GetString("id_note"), reader.getString("name_note"), reader.GetString("date_note")
            }

            
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            id_user = -1;
            username = usernameTextBox.Text;
            password = passwordBox.Password;

            if ((username != null && username.Length < 5) && (password != null && password.Length < 41))
            {
                id_user = db.db_login(username, password);
            }

            if(id_user != 0 && !(id_user < 0))
            {
                //Správné přihlášení
                usernameTextBox.Visibility = Visibility.Hidden;
                passwordBox.Visibility = Visibility.Hidden;
                LoginButton.Visibility = Visibility.Hidden;
                LoginPanel.Visibility = Visibility.Hidden;

            }
            

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            MyNotesButton.Background.Opacity = 35;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void MyNotesButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
