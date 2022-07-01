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
    /// Логика взаимодействия для AlbumEdit.xaml
    /// </summary>
    public partial class AlbumEdit : Page
    {
        AppContext db;

        public AlbumEdit()
        {
            InitializeComponent();

            db = new AppContext();
        }
       
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridShedules.SelectedItems.Count == 0) return;
            var iddel = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["idAlbum"].ToString();
            string namw = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["name"].ToString();
            string release = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["release"].ToString();
            string pos = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["position"].ToString();
            string sal = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["sales"].ToString();
            string grid = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["idGroup"].ToString();
            string ided = Convert.ToString(iddel);
            MessageBox.Show(ided);

            EditAlbum editAlbum = new EditAlbum(iddel, namw, release, pos, sal, grid);
            editAlbum.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PagesManager/AddAlbum.xaml", UriKind.Relative));

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить пользователя?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (DataGridShedules.SelectedItems.Count == 0) return;
                var iddel = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["idAlbum"].ToString();
                var namw = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["name"].ToString();
                var release = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["release"].ToString();
                var pos = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["position"].ToString();
                var sal = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["sales"].ToString();
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
                    string querry = String.Format($"DELETE FROM Albums where idAlbum='" + ided + "'");
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
            NavigationService.Navigate(new Uri("/PagesManager/Album.xaml", UriKind.Relative));
        }


        public void refresh()
        {
            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Albums ";
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
                cmd.CommandText = @"SELECT * FROM Albums  where name like '%" + searWord + "%'";
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
