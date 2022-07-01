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
using System.Windows.Shapes;

namespace WpfCursovaya.PagesManager
{
    /// <summary>
    /// Логика взаимодействия для ProfileEdit.xaml
    /// </summary>
    public partial class ProfileEdit : Window
    {
        public ProfileEdit()
        {
            InitializeComponent();
        }

        public ProfileEdit(string _id, string _log, string _pass)
        {
            InitializeComponent();

            UserName.Text += _log;
            Pass.Text += _pass;

            id.Content += _id;
        }

        private void Edit_click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите изменить Профиль(Логин и пароль)?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {


                this.Close();
            }
        }
    }
}
