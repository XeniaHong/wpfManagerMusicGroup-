using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

namespace WpfCursovaya.PagesManager
{
    /// <summary>
    /// Логика взаимодействия для EditSong.xaml
    /// </summary>
    public partial class EditSong : Window
    {
        public EditSong()
        {
            InitializeComponent();
        }

        public EditSong(string _id, string _name, string _auth, string _idGroup)
        {
            InitializeComponent();

            name.Text += _name;
            auth.Text += _auth;

            idContent.Content += _id;
            idgro.Content += _idGroup;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Вы действительно хотите изменить песню?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string idSh = name.Text;
                string autho = auth.Text;
                string id = Convert.ToString(idContent.Content);
                string idgr = Convert.ToString(idgro.Content);

                //string groups = GroupS.Text;
                //int groupNum = 0;

                //if (groups == "Monsta X")
                //{
                //    groupNum = 1;
                //}
                //else if (groups == "WJSN")
                //{
                //    groupNum = 2;
                //}
                //else { groupNum = 3; }


                var con = new SQLiteConnection("Data Source=appDb.db");
                try
                {
                    con.Open();
                    string querry = String.Format($"DELETE FROM Songs where idSong='" + id + "'");
                    SQLiteCommand cmd = new SQLiteCommand(querry, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                con = new SQLiteConnection("Data Source=appDb.db");
                try
                {
                    con.Open();

                    string query = String.Format($"INSERT INTO `Songs`(`idSong`, `name`, `author`, `albumId`) VALUES ('{id }','{idSh }','{autho}', '{idgr}')");
                    SQLiteCommand command = new SQLiteCommand(query, con);
                    command.ExecuteNonQuery();
                }

                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    con.Close();
                }

                MessageBox.Show("Песня изменнена!");
                this.Close();
            }


        }
    }
}

