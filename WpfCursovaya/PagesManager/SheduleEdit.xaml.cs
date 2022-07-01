using System;
using System.Collections.Generic;
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
using System.Data.SQLite;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;

namespace WpfCursovaya.PagesManager
{
    /// <summary>
    /// Логика взаимодействия для SheduleEdit.xaml
    /// </summary>
    public partial class SheduleEdit : Page
    {
        AppContext db;

        public SheduleEdit()
        {
            InitializeComponent();

            db = new AppContext();
                        
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridShedules.SelectedItems.Count == 0) return;
            string iddel = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["sheduleId"].ToString();
            var shedulE = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["shedule"].ToString();
            string data = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["data"].ToString();
            string place = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["placeShedule"].ToString();
          
            string shedulEd = Convert.ToString(shedulE);


            EditShedule editShedule = new EditShedule(iddel, shedulEd, data, place);
            editShedule.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PagesManager/AddShedule.xaml", UriKind.Relative));
           
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить пользователя?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (DataGridShedules.SelectedItems.Count == 0) return;
                var iddel = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["sheduleId"].ToString();
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
                    string querry = String.Format($"DELETE FROM Shedule where sheduleId='" + ided + "'");
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
            NavigationService.Navigate(new Uri("/PagesManager/Shedule.xaml", UriKind.Relative));
        }


        public void refresh()
        {
            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Shedule ";
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
                cmd.CommandText = @"SELECT * FROM Shedule  where shedule like '%" + searWord + "%'";
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
