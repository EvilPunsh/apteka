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
using онлайн_аптека.windows;

namespace онлайн_аптека.page
{
    /// <summary>
    /// Логика взаимодействия для clientMenu.xaml
    /// </summary>
    public partial class clientMenu : Page
    {
        public clientMenu()
        {
            InitializeComponent();
        }
        //Смена пользователя 
        private void ButtonChangeUser_Click(object sender, RoutedEventArgs e)
        {

            authoriz window = new authoriz();
            window.Show();
            this.Content = null;
        }
        //Перемещение на страницу каталога
        private void ButtonCatalog_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new clientCatalog());
        }

        
    }
}
