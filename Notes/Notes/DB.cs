using CryptSharp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes
{
    class DB
    {
        private MySqlConnection connection;
        private MySqlCommand command;


        public DB()
        {
            //db_connect(); //hned při vytvoření instance se připoj do dtb

        }

        public void db_connect()
        {       //připojit se do dtb

            //připojit na local dtb
            connection = new MySqlConnection("Server=10.0.7.17; Port=3306; Database=projektpoznamky; Uid=poznamky; Pwd=12345");
            connection.Open(); //otevřít dtb
        }
        public void db_close()
        {   //ukončit komunikaci s databází
            connection.Close();
        }




        public void add_note_db(Poznamka note, int id_user)
        { //přidat novou poznámku do databáze

            db_connect(); //pro každý nový příkaz musí být nové připojení do dtb

            command = connection.CreateCommand(); //zahájit vytváření příkazu

            //získat hodnoty z přidaného objektu
            String name_note = note.getName();
            String text_note = note.getText();

            command.CommandText = "INSERT INTO notes(name_note, text_note, id_user) VALUES('" + name_note + "', '" + text_note + "', " + id_user + ")"; //znění příkazu

            command.ExecuteNonQuery(); //provést

            db_close(); //ukončit dtb    
        }




        public MySqlDataReader db_select_notes(int id_user)
        {
            db_connect(); //připojit se
            command = connection.CreateCommand();

            // command.CommandText = "SELECT id_note, name_note, text_note, date_note FROM notes";

            string select = "SELECT id_note, name_note, text_note, date_note FROM notes WHERE id_user = " + id_user;

            MySqlCommand cmd = new MySqlCommand(select, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            //nesmím zavřít dtb jinak by se nevykonal reader v Form1.cs

            return reader;
        }


        public void db_delete_note(int id_note)
        {
            db_connect();

            string delete = "DELETE FROM notes WHERE id_note = " + id_note;

            MySqlCommand cmd = new MySqlCommand(delete, connection);
            cmd.ExecuteNonQuery();

            db_close();


        }


        public void db_update_note(int id_note, string name_note, string text_note)
        {
            //update dané poznámky, předá se mi z formuláře id, jméno a text
            db_connect();

            string update = "UPDATE notes SET name_note = '" + name_note + "', text_note ='" + text_note + "' WHERE id_note =" + id_note;

            MySqlCommand cmd = new MySqlCommand(update, connection);

            cmd.ExecuteNonQuery();

            db_close();

        }






        public int db_login(String username, String password)
        {


            db_connect();

            string select_count = "SELECT COUNT(id_user) AS c FROM users WHERE username = '" + username + "'";

            MySqlCommand cmd_count = new MySqlCommand(select_count, connection); //proveď počet

            int count = 0;

            //kolik je uživatelů se splněním podmínke?
            MySqlDataReader r = cmd_count.ExecuteReader();
            while (r.Read())
            {
                count = int.Parse(r.GetString("c"));
            }

            r.Close(); //uzavřít MySqlDataReader!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //jestli je 1 uživatel s platnými údaji, můžu předat k přihlášení jeho id, jinak se vrátí 0, která nikdy v dtb není (jako id)
            if (count == 1)
            {
                int id_user = 0;
                string passwd;
                string select_find = "SELECT id_user, password FROM users WHERE username = '" + username + "'";
                MySqlCommand cmd = new MySqlCommand(select_find, connection); //proveď zjištění id

                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    id_user = int.Parse(read.GetString("id_user"));
                    passwd = read.GetString("password");

                    if (Crypter.CheckPassword(password, passwd))
                    {
                        return id_user;
                    }
                    else
                    {
                        return 0;
                    }
                }
                db_close();
                return id_user; //předej id

            }
            else
            {
                db_close();
                return 0; //předej nulu
            }






            /*
            //přihlášení uživatele
            db_connect();

            //parametr hesla zahešuj do sha1
            string passwd = getSHA1(password);
          

            //zjisti počet uživatelů s platnými přihlašovacími údaji
            string select_count = "SELECT COUNT(id_user) AS c FROM users WHERE username = '" + username + "' AND password = '" + passwd + "'";

            //zjisti id uživatele s platnými podmínkami
            string select_login = "SELECT id_user FROM users WHERE username = '" + username + "' AND password = '" + passwd +"'";

            MySqlCommand cmd_count = new MySqlCommand(select_count, connection); //proveď počet
            MySqlCommand cmd = new MySqlCommand(select_login, connection); //proveď zjištění id

            int count = 0;

            //kolik je uživatelů se splněním podmínke?
            MySqlDataReader r = cmd_count.ExecuteReader(); 
            while (r.Read())
            {
                count = int.Parse(r.GetString("c"));
            }

            r.Close(); //uzavřít MySqlDataReader!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //jestli je 1 uživatel s platnými údaji, můžu předat k přihlášení jeho id, jinak se vrátí 0, která nikdy v dtb není (jako id)
            if(count == 1)
            {
                int id_user = 0;
                // cmd.ExecuteNonQuery();

                //jaké je id_user z výběru uživatelů?
                MySqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    id_user = int.Parse(read.GetString("id_user"));
                }
                db_close();
                return id_user; //předej id

            }
            else
            {
                db_close();
                return 0; //předej nulu
            } 

            */

        }



        /*

        public string getSHA1(string text)
        {
            //překlad textu na SHA1
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            sh.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] re = sh.Hash;
            StringBuilder sb = new StringBuilder();

            foreach(byte b in re)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();

        }
        */



    }
}
