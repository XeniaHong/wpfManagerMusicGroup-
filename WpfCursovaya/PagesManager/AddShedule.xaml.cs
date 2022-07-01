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
using System.Data;
using System.Data.SQLite;

namespace WpfCursovaya.PagesManager
{
    /// <summary>
    /// Логика взаимодействия для AddShedule.xaml
    /// </summary>
    public partial class AddShedule : Page
    {
        AppContext db;

        public AddShedule()
        {
            InitializeComponent();

            db = new AppContext();
        }

        private void BtnSave_click(object sender, RoutedEventArgs e)
        {
           
            string shedulN = shedule.Text;
            string dateT = dateTime.Text;
            string placeS = placeSh.Text;
            string groups = GroupS.Text;
            int groupNum = 0;

            if (groups== "Monsta X")
            {
                groupNum = 1;
            } else if (groups == "WJSN")
            {
                groupNum = 2;
            } else { groupNum = 3; }


            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();

                string query = String.Format($"INSERT INTO `Shedule`(`shedule`, `data`, `placeShedule`, `groupId`) VALUES ('{shedulN }','{dateT }','{placeS}', '{groupNum}')");
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
            NavigationService.Navigate(new Uri("/PagesManager/SheduleEdit.xaml", UriKind.Relative));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/PagesManager/SheduleEdit.xaml", UriKind.Relative));
        }
    }
}
