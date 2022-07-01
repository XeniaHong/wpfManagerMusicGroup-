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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCursovaya.PagesManager
{
    /// <summary>
    /// Логика взаимодействия для AddAlbum.xaml
    /// </summary>
    public partial class AddAlbum : Page
    {
        AppContext db;

        public AddAlbum()
        {
            InitializeComponent();

            db = new AppContext();
        }

        private void BtnSave_click(object sender, RoutedEventArgs e)
        {

            string nam = name.Text;
            string rel = relea.Text;
            string posit = pos.Text;
            string sales = sal.Text;
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

            MessageBox.Show("Мероприятие добавлено!");
            NavigationService.Navigate(new Uri("/PagesManager/AlbumEdit.xaml", UriKind.Relative));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/PagesManager/AlbumEdit.xaml", UriKind.Relative));
        }
    }
}

