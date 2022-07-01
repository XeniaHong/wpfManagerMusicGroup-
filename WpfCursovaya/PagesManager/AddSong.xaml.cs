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
    /// Логика взаимодействия для AddSong.xaml
    /// </summary>
    public partial class AddSong : Window
    {
        AppContext db;

        public AddSong()
        {
            InitializeComponent();

            db = new AppContext();
        }

        public AddSong(string _idalb)
        {
            InitializeComponent();

            idContent.Content += _idalb;
        }

        private void BtnSave_click(object sender, RoutedEventArgs e)
        {
            string grouId =Convert.ToString(idContent.Content);
            string nam = name.Text;
            string autho = auth.Text;


            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();

                string query = String.Format($"INSERT INTO `Songs`(`name`, `author`, `albumId`) VALUES ('{nam }','{autho}', '{grouId}')");
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

            MessageBox.Show("Добавлена песня!");
            this.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string gr = Convert.ToString(idContent.Content);
            SongEdit songEdit = new SongEdit(gr);
            songEdit.Show();
            this.Close();
        }
    }
}

