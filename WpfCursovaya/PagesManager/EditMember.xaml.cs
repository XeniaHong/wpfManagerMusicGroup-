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
    /// Логика взаимодействия для EditMember.xaml
    /// </summary>
    public partial class EditMember : Window
    {
        public EditMember()
        {
            InitializeComponent();
        }

        public EditMember(string _id, string _nick, string _name, string _lastn, string _birth, string _wei, string _hei, string _pos)
        {
            InitializeComponent();

            nick.Text += _nick;
            name.Text += _name;
            lasnam.Text += _lastn;
            bir.Text += _birth;
            wei.Text += _wei;
            hei.Text += _hei;
            pos.Text += _pos;

            id.Content += _id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSave_click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Вы действительно хотите изменить участника?", "Сохранение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string idM = Convert.ToString(id.Content);

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
                    string querry = String.Format($"DELETE FROM Members where memberId='" + idM + "'");
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

                    string query = String.Format($"INSERT INTO `Members`(`memberId`, `nickName`, `name`, `lastName`, `birth`, `weight`, `height`, `position`, `groupId`) VALUES ('{idM}', '{nic}', '{nam }', '{lasn}', '{birth}', '{weig}', '{heig}', '{posi}', '{groupNum}')");
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

                MessageBox.Show("Участник изменнен! Перезайдите, пожалуйста, на страницу.");
                this.Close();
            }

        }
    }
}

