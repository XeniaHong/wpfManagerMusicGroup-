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
    /// Логика взаимодействия для EditShedule.xaml
    /// </summary>
    public partial class EditShedule : Window
    {
        public EditShedule()
        {
            InitializeComponent();
        }

        public EditShedule(string _id, string _name, string _data, string _place)
        {
            InitializeComponent();

            shedule.Text += _name;
            dataTime.Text += _data;
            placeShedul.Text += _place;

            idContent.Content += _id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Вы действительно хотите изменить мероприятие?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string idSh = shedule.Text;
                string date = dataTime.Text;
                string placesh = placeShedul.Text;
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
                    string querry = String.Format($"DELETE FROM Shedule where sheduleId='" + id + "'");
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

                    string query = String.Format($"INSERT INTO `Shedule`(`sheduleId`, `shedule`, `data`, `placeShedule`, `groupId`) VALUES ('{id }','{idSh }','{date}', '{placesh}', '{groupNum}')");
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

                MessageBox.Show("Мероприятие изменнено! Перезайдите, пожалуйста, в таблицу.");
                this.Close();
            }

               
        }
    }
}
