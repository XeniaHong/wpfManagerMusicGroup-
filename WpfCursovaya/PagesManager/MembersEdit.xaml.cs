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
    /// Логика взаимодействия для MembersEdit.xaml
    /// </summary>
    public partial class MembersEdit : Page
    {
        AppContext db;

        public MembersEdit()
        {
            InitializeComponent();

            db = new AppContext();

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listMem.SelectedItems.Count == 0) return;
            string iddel = ((DataRowView)listMem.SelectedItems[0]).Row["memberId"].ToString();
            var nick = ((DataRowView)listMem.SelectedItems[0]).Row["nickName"].ToString();
            string nam = ((DataRowView)listMem.SelectedItems[0]).Row["name"].ToString();
            string lnam = ((DataRowView)listMem.SelectedItems[0]).Row["lastName"].ToString();
            string bir = ((DataRowView)listMem.SelectedItems[0]).Row["birth"].ToString();
            string we = ((DataRowView)listMem.SelectedItems[0]).Row["weight"].ToString();
            string he = ((DataRowView)listMem.SelectedItems[0]).Row["height"].ToString();
            string pos = ((DataRowView)listMem.SelectedItems[0]).Row["position"].ToString();


            EditMember editMember = new EditMember(iddel, nick, nam, lnam, bir, we, he, pos);
            editMember.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PagesManager/AddMember.xaml", UriKind.Relative));

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить участника?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (listMem.SelectedItems.Count == 0) return;
                var iddel = ((DataRowView)listMem.SelectedItems[0]).Row["memberId"].ToString();
                string ided = Convert.ToString(iddel);
                MessageBox.Show(ided);




                if (listMem.SelectedItems.Count > 0)
                {
                    for (int i = listMem.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        DataRowView rowView = listMem.SelectedItems[i] as DataRowView;
                        rowView.Delete();


                    }
                }

                var con = new SQLiteConnection("Data Source=appDb.db");
                try
                {
                    con.Open();
                    string querry = String.Format($"DELETE FROM Members where memberId='" + ided + "'");
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
            NavigationService.Navigate(new Uri("/PagesManager/Members.xaml", UriKind.Relative));
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
        
    }
}
