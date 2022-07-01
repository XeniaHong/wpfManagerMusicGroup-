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
    /// Логика взаимодействия для TourEdit.xaml
    /// </summary>
    public partial class TourEdit : Page
    {
        AppContext db;
        public TourEdit()
        {
            InitializeComponent();

            db = new AppContext();
        }
        
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridShedules.SelectedItems.Count == 0) return;
            string iddel = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["tourId"].ToString();
            var country = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["country"].ToString();
            string town = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["town"].ToString();
            string data = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["data"].ToString();
            string place = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["placeConcert"].ToString();
            string costT = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["costTicket"].ToString();
            int cost = Convert.ToInt32(costT);
            string shedulEd = Convert.ToString(country);


            EditTour editTour = new EditTour(iddel, country, town, place, data, cost);
            editTour.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PagesManager/AddTour.xaml", UriKind.Relative));

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить пользователя?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (DataGridShedules.SelectedItems.Count == 0) return;
                var iddel = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["tourId"].ToString();
                string ided = Convert.ToString(iddel);
                MessageBox.Show(ided);




                if (DataGridShedules.SelectedItems.Count > 0)
                {
                    for (int i = DataGridShedules.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        DataRowView rowView = DataGridShedules.SelectedItems[i] as DataRowView;
                        rowView.Delete();


                    }
                }

                var con = new SQLiteConnection("Data Source=appDb.db");
                try
                {
                    con.Open();
                    string querry = String.Format($"DELETE FROM Tour where tourId='" + ided + "' ");
                    SQLiteCommand cmd = new SQLiteCommand(querry, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PagesManager/Tour.xaml", UriKind.Relative));
        }


        public void refresh()
        {
            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Tour ";
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
            string searWord = Search.Text;

            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Tour  where town like '%" + searWord + "%' OR country like '%" + searWord + "%'";
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
