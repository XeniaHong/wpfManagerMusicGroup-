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

namespace WpfCursovaya
{
    /// <summary>
    /// Логика взаимодействия для SettingsApp.xaml
    /// </summary>
    public partial class SettingsApp : Window
    {
        public SettingsApp()
        {
            InitializeComponent();
        }



      

        private void exitAccount_Click(object sender, RoutedEventArgs e) //выход из приложения
        {
            areYouSure questoin = new areYouSure();
            questoin.Show();
        }

        private void BackProgramm_Click(object sender, RoutedEventArgs e) // выход обратно в приложение
        {
            this.Close();
        }
    }
}
