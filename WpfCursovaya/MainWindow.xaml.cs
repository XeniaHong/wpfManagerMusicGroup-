using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfCursovaya
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = login.Text;
            string pass = password.Password;

            //проверяем пустые ли поля
            if (login.Text.Length > 0)
            {
                if (password.Password.Length > 0)
                {
                }
                else MessageBox.Show("Введите пароль!");
            }
            else MessageBox.Show("Введите логин!");

            User authUser = null;
            using(AppContext db = new AppContext())
            {
                authUser = db.Users.Where(b => b.Login == loginUser && b.Password == pass).FirstOrDefault();
            }

            if (authUser != null)
            {
                AppMain appScreen = new AppMain(login.Text);
                appScreen.Show();
                this.Close();
            }
            else MessageBox.Show("Логин или пароль введены неправильно. Попробуйте снова!");

            /* SqlConnection sqlCon = new SqlConnection(@"Data Source=(localdb)\MsSqlLocalDb;Initial Catalog=AppDb;Integrated Security=True");
               try
               {
                   if (sqlCon.State == ConnectionState.Closed)
                       sqlCon.Open();
                   String querry = "SELECT COUNT(1) FROM loginUser WHERE UserName=@Login AND Password=@Password";
                   SqlCommand sqlCmd = new SqlCommand(querry, sqlCon);
                   sqlCmd.CommandType = CommandType.Text;
                   sqlCmd.Parameters.AddWithValue("@Login", login.Text);
                   sqlCmd.Parameters.AddWithValue("@Password", password.Password);
                   int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                   if(count == 1)
                   {
                       AppMain appScreen = new AppMain(login.Text);
                       appScreen.Show();
                       this.Close();
                   }
                   else
                   {
                       MessageBox.Show("Логин или парроль введены неправильно. Попробуйте снова!");
                   }
               }
               catch(Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
               finally
               {
                   sqlCon.Close();
               }
               */

        }


        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            RegistrationScreen reginScreen = new RegistrationScreen();
            reginScreen.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppMainSimple app = new AppMainSimple();
            app.Show();
            this.Close();

            
        }
    }
}
