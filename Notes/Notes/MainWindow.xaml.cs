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
            ListNotesPanel.Visibility = Visibility.Hidden;
            



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
                list();
            }

            db.db_close();
        }

        private void list()
        {
            ListNotesPanel.Children.Clear();
            MySqlDataReader reader;

            reader = db.db_select_notes(14);

            while (reader.Read())
            {
                //Vytváření poznámky
                //reader.GetString("id_note"), reader.getString("name_note"), reader.GetString("date_note")

                Border border = new Border();
                Grid grid = new Grid();
                TextBlock nameBlock = new TextBlock();

                RowDefinition gridRow1 = new RowDefinition();
                RowDefinition gridRow2 = new RowDefinition();

                grid.RowDefinitions.Add(gridRow1);
                grid.RowDefinitions.Add(gridRow2);


                ColumnDefinition gridCol1 = new ColumnDefinition();
                ColumnDefinition gridCol2 = new ColumnDefinition();
                gridCol2.Width = GridLength.Auto;

                grid.ColumnDefinitions.Add(gridCol1);
                grid.ColumnDefinitions.Add(gridCol2);


                border.Background = new SolidColorBrush(Colors.Transparent);
                border.BorderThickness = new Thickness(2);
                border.BorderBrush = new SolidColorBrush(Colors.Azure);
                border.CornerRadius = new CornerRadius(2);
                border.Padding = new Thickness(5);
                border.Margin = new Thickness(5);
                border.Width = 800;
                border.Height = 80;


                nameBlock.Text = reader.GetString("name_note");
                nameBlock.Foreground = new SolidColorBrush(Colors.Red);
                Grid.SetRow(nameBlock, 0);
                Grid.SetColumn(nameBlock, 0);
                grid.Children.Add(nameBlock);

                TextBlock txtBlock2 = new TextBlock();
                txtBlock2.Text = reader.GetString("text_note");


                txtBlock2.VerticalAlignment = VerticalAlignment.Top;
                Grid.SetRow(txtBlock2, 1);
                Grid.SetColumn(txtBlock2, 0);
                grid.Children.Add(txtBlock2);

                border.Child = grid;
                ListNotesPanel.Children.Add(border);

                Scroll.Content = ListNotesPanel;
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
                ListNotesPanel.Visibility = Visibility.Hidden;

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
            ListNotesPanel.Visibility = Visibility.Visible;
            list();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            list();
        }

        private void CreateDynamicBorder()
        {
             Border border = new Border();
             Grid grid = new Grid();
             TextBlock nameBlock = new TextBlock();

             RowDefinition gridRow1 = new RowDefinition();
             RowDefinition gridRow2 = new RowDefinition();

             grid.RowDefinitions.Add(gridRow1);
             grid.RowDefinitions.Add(gridRow2);


             ColumnDefinition gridCol1 = new ColumnDefinition();
             ColumnDefinition gridCol2 = new ColumnDefinition();
            

             grid.ColumnDefinitions.Add(gridCol1);
             grid.ColumnDefinitions.Add(gridCol2);


             border.Background = new SolidColorBrush(Colors.Transparent);
             border.BorderThickness = new Thickness(2);
             border.BorderBrush = new SolidColorBrush(Colors.Azure);
             border.CornerRadius = new CornerRadius(2);
             border.Padding = new Thickness(5);
             border.Margin = new Thickness(5);
             border.Width = 800;
             border.Height = 70;


             nameBlock.Text = "Name: ";
             nameBlock.Foreground = new SolidColorBrush(Colors.Red);
             Grid.SetRow(nameBlock, 0);
             Grid.SetColumn(nameBlock, 0);
             grid.Children.Add(nameBlock);

            TextBlock txtBlock2 = new TextBlock();
            txtBlock2.Text = "Note: ";
            
            
            txtBlock2.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(txtBlock2, 1);
            Grid.SetColumn(txtBlock2, 0);
            grid.Children.Add(txtBlock2);

            border.Child = grid;
            ListNotesPanel.Children.Add(border);

             Scroll.Content = ListNotesPanel; 

        }
    }
}
