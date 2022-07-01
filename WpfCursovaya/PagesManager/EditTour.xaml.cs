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
using System.Windows.Shapes;

namespace WpfCursovaya.PagesManager
{
    /// <summary>
    /// Логика взаимодействия для EditTour.xaml
    /// </summary>
    public partial class EditTour : Window
    {
        public EditTour()
        {
            InitializeComponent();
        }

        public EditTour(string _id, string _country, string _town, string _place, string _data, int _cost)
        {
            InitializeComponent();

            country.Text += _country;
            town.Text += _town;
            placeC.Text += _place;
            dataC.Text += _data;
            cost.Text += _cost;

            idContent.Content += _id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Вы действительно хотите изменить тур?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string countryC = country.Text;
                string townC = town.Text;
                string placeCo = placeC.Text;
                string dataCo = dataC.Text;
                string costC = cost.Text;
                var id = idContent.Content;

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
                    string querry = String.Format($"DELETE FROM Tour where tourId='" + id + "'");
                    SQLiteCommand cmd = new SQLiteCommand(querry, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                con = new SQLiteConnection("Data Source=appDb.db");
                try
                {
                    con.Open();

                    string query = String.Format($"INSERT `Tour`(`country`, `town`, `data`, `placeconcert`, `costTicket`, `groupId`) VALUES ('{countryC }','{townC }','{dataCo}', '{placeCo}', '{costC}', '{groupNum}')");
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

                MessageBox.Show("Мероприятие изменнено! Перезайдите, пожалуйста, на страницу.");
                this.Close();
            }

        }
    }
}
