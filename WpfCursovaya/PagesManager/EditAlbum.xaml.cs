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
    /// Логика взаимодействия для EditAlbum.xaml
    /// </summary>
    public partial class EditAlbum : Window
    {
        public EditAlbum()
        {
            InitializeComponent();
        }

        public EditAlbum(string _id, string _name, string _release, string _position, string _sales, string _idGroup)
        {
            InitializeComponent();

            name.Text += _name;
            relea.Text += _release;
            pos.Text += _position;
            sal.Text += _sales;

            if (_idGroup == "Monsta X")
            {
                GroupS.Text = "Monsta X";
            }
            else if (_idGroup == "WJSN")
            {
                GroupS.Text = "WJSN";
            }
            else { GroupS.Text = "Solo"; }

            idContent.Content += _id;
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Вы действительно хотите изменить альбом?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var idc = idContent.Content;
                string nam = name.Text;
                string rel = relea.Text;
                string posit = pos.Text;
                string sales = sal.Text;

                string id = Convert.ToString(idc);

                string groups = GroupS.Text;
                int groupNum = 0;

                if (groups == "Monsta X")
                {
                    groupNum = 1;
                }
                else if (groups == "WJSN")
                {
                    groupNum = 2;
                }
                else { groupNum = 3; }


                var con = new SQLiteConnection("Data Source=appDb.db");
                try
                {
                    con.Open();
                    string querry = String.Format($"DELETE FROM Albums where idAlbum='" + id + "'");
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

                    string query = String.Format($"INSERT INTO `Albums`(`name`, `release`, `position`, `sales`, `idgroup`) VALUES ('{nam }','{rel }','{posit}', '{sales}', '{groupNum}')");
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

                MessageBox.Show("Альбом изменнен! Перезайдите, пожалуйста, на страницу.");
                this.Close();
            }

        }
    }
}

