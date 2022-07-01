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
using System.Windows.Shapes;


namespace WpfCursovaya
{
    /// <summary>
    /// Логика взаимодействия для RegistrationScreen.xaml
    /// </summary>
    public partial class RegistrationScreen : Window
    {
        AppContext db;

        public RegistrationScreen()
        {
            InitializeComponent();

            db = new AppContext();
        }


        private void backToLogin_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = login.Text;
            string pass = password.Password;

            //проверяем введеные пароли
            if (password.Password != repeat_password.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
            }
            // проверяем пустые ли поля
            if (login.Text.Length > 0)
            {
                if (password.Password.Length > 0)
                {
                    if (repeat_password.Password.Length > 0)
                    {
                        User authUser = null;
                        using (AppContext db = new AppContext())
                        {
                            authUser = db.Users.Where(b => b.Login == loginUser && b.Password == pass).FirstOrDefault();
                        }                        

                        if (authUser == null )
                        {
                            MessageBox.Show("Пользователь зарегистрирован!");
                            User users = new User(loginUser, pass);


                            db.Users.Add(users);
                            db.SaveChanges();

                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close();
                        }
                        else 
                        {
                            MessageBox.Show("Такой логин уже существует!");
                            MessageBox.Show("Придумайте другой логин");
                        }
                    }
                    else MessageBox.Show("Повторите пароль!");
                } else MessageBox.Show("Введите пароль!");
            } else MessageBox.Show("Введите логин!");

          

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
