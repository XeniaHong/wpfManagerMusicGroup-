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
    /// Логика взаимодействия для AppMainSimple.xaml
    /// </summary>
    public partial class AppMainSimple : Window
    {
        public AppMainSimple()
        {
            InitializeComponent();

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { timeous.Content = DateTime.Now.ToString(); };
            timer.Start();
        }

        private void Exit_click(object sender, RoutedEventArgs e)
        {
            areYouSure questoin = new areYouSure();
            questoin.Show();
        }

        private void Setting_click(object sender, RoutedEventArgs e)
        {
            SettingsApp settingPage = new SettingsApp();
            settingPage.Show();

        }



        private void BtnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string tag = btn.Tag.ToString();

            //$ "" - интерполяция строк (#4.6)
            // pageName = string.Format("Page{0}.xaml", tag); вместо этой строки можно написать как внизу

            string pageName = $"{tag}.xaml";

            var uri = new Uri("PagesMember/" + pageName, UriKind.Relative);
            mainFrame.Navigate(uri);

        }
    }
}

