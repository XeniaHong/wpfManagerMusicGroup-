using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Members.xaml
    /// </summary>
    public partial class Members : Page
    {
        public Members()
        {
            InitializeComponent();
            
        }

        public void refresh()
        {
            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Members";
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd.CommandText, con))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    listMem.ItemsSource = dataTable.AsDataView();

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void list_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searAl = Search.Text;

            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Members  where name like '%" + searAl + "%'";
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd.CommandText, con))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    listMem.ItemsSource = dataTable.AsDataView();

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void Word_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("К сожаления эта функция не работает");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PagesManager/MembersEdit.xaml", UriKind.Relative));
        }
    }
}
