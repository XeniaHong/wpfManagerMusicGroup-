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
    /// Логика взаимодействия для AddMember.xaml
    /// </summary>
    public partial class AddMember : Page
    {
        AppContext db;

        public AddMember()
        {
            InitializeComponent();

            db = new AppContext();
        }

        private void BtnSave_click(object sender, RoutedEventArgs e)
        {

            string nic = nick.Text;
            string nam = name.Text;
            string lasn = lasnam.Text;
            string birth = bir.Text;
            string weig = wei.Text;
            string heig = hei.Text;
            string posi = pos.Text;
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

                string query = String.Format($"INSERT INTO `Members`(`nickName`, `name`, `lastName`, `birth`, `weight`, `height`, `position`, `groupId`) VALUES ('{nic }', '{nam }', '{lasn}', '{birth}', '{weig}', '{heig}', '{posi}', '{groupNum}')");
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

            MessageBox.Show("Участник добавлено!");
            NavigationService.Navigate(new Uri("/PagesManager/MembersEdit.xaml", UriKind.Relative));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/PagesManager/MembersEdit.xaml", UriKind.Relative));
        }
    }
}

