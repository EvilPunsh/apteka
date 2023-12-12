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
using онлайн_аптека.classes;
using онлайн_аптека.dataBase;
using онлайн_аптека.page;

namespace онлайн_аптека.windows
{
    /// <summary>
    /// Логика взаимодействия для userOrdersWindow.xaml
    /// </summary>
    public partial class userOrdersWindow : Page
    {
        public userOrdersWindow()
        {
            InitializeComponent();
            userOrders.ItemsSource = pharmacy1Entities.GetContext().Заказ.Where(u => u.КодКлиента == DataTransfer.КодКлиента).ToList();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e) //Возврат в каталог товаров
        {
            NavigationService.Navigate(new clientCatalog());
        }
    }
}
