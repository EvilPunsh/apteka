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
using онлайн_аптека.page;

namespace онлайн_аптека.windows
{
    /// <summary>
    /// Логика взаимодействия для adminMenu.xaml
    /// </summary>
    public partial class adminMenu : Page
    {
        public adminMenu()
        {
            InitializeComponent();
        }
        private void ButtonChangeUser_Click(object sender, RoutedEventArgs e)//Смена пользователя
        {

            authoriz window = new authoriz();
            window.Show();
            this.Content = null;
        }

        private void ButtonCatalog_Click(object sender, RoutedEventArgs e)//Перемещение в каталог у администратора
        {
            NavigationService.Navigate(new adminCatalog());
        }
        private void ButtonOrders_Click(object sender, RoutedEventArgs e)//Перемещение в заказы у администратора
        {
            NavigationService.Navigate(new adminOrders());
        }
        private void ButtonClient_Click(object sender, RoutedEventArgs e)//Перемещение в пользователей у администратора
        {
            NavigationService.Navigate(new adminClient());
        }
    }
}
