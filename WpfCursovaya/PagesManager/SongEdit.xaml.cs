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
    /// Логика взаимодействия для SongEdit.xaml
    /// </summary>
    public partial class SongEdit : Window
    {
        AppContext db;

        public SongEdit()
        {
            InitializeComponent();

            db = new AppContext();

        }
        public SongEdit(string _id)
        {
            InitializeComponent();

            string id = _id;
            groupi.Content += id;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridShedules.SelectedItems.Count == 0) return;
            string iddel = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["idSong"].ToString();
            string name = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["name"].ToString();
            string auth = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["author"].ToString();
            string album = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["albumId"].ToString();

            EditSong editSong = new EditSong(iddel,name, auth, album);
            editSong.Show();
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string grId = Convert.ToString(groupi.Content);

            AddSong addSong = new AddSong(grId);
            addSong.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить пользователя?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (DataGridShedules.SelectedItems.Count == 0) return;
                var iddel = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["idSong"].ToString();
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
                    string querry = String.Format($"DELETE FROM Songs where idSong='" + ided + "'");
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
            string gr = Convert.ToString(groupi.Content);
            Songs songEdit = new Songs(gr);
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
                cmd.CommandText = "SELECT * FROM Songs where albumId = '" + alId + "'";
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
            var group = groupi.Content;
            string alId = Convert.ToString(group);

            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Songs  where name like '%" + searWord + "%' AND albumId = '" + alId + "'";
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
