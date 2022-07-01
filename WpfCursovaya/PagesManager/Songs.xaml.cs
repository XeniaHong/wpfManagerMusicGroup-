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
using System.Windows.Shapes;

namespace WpfCursovaya.PagesManager
{
    /// <summary>
    /// Логика взаимодействия для Songs.xaml
    /// </summary>
    public partial class Songs : Window
    {
        public Songs()
        {
            InitializeComponent();
        }
        public Songs(string _idGr)
        {
            InitializeComponent();

            string groupId = _idGr;
            groupi.Content += groupId;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string gr = Convert.ToString(groupi.Content);
            SongEdit songEdit = new SongEdit(gr);
            songEdit.Show();
            this.Close();
        }

        public void refresh()
        {
            var group = groupi.Content;
            string alId = Convert.ToString(group);
            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Songs where albumId = '"+ alId+ "'";
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd.CommandText, con))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    DataGridShedules.ItemsSource = dataTable.AsDataView();

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searAl = Search.Text;
            var group = groupi.Content;
            string alId = Convert.ToString(group);

            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Songs  where name like '%" + searAl + "%' AND albumId = '" + alId + "'";
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd.CommandText, con))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    DataGridShedules.ItemsSource = dataTable.AsDataView();

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

      
    }
}
