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

namespace WpfCursovaya.PagesMember
{
    /// <summary>
    /// Логика взаимодействия для Tour.xaml
    /// </summary>
    public partial class Tour : Page
    {
        public Tour()
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
                cmd.CommandText = "SELECT * FROM Tour";
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
            string searTown = SearchCountry.Text;


            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Tour  where town like '%" + searTown + "%' OR country like '%" + searTown + "%'";
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
    }
}
