using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using онлайн_аптека.windows;

namespace онлайн_аптека.page
{
    /// <summary>
    /// Логика взаимодействия для client.xaml
    /// </summary>
    public partial class clientCatalog : Page
    {
        public clientCatalog()//Вывод ФИО пользователя или "Неавторизованный пользователь" для гостя
        {
            InitializeComponent();
            ProductList.ItemsSource = pharmacy1Entities.GetContext().Товар.ToList();
            if (DataTransfer.КодКлиента != 0)
            {
                UserNameBox.Content = DataTransfer.Фамилия + " " + DataTransfer.Имя + " " + DataTransfer.Отчество;
            }
            else
            {
                UserNameBox.Content = "Неавторизованный пользователь";
            }

            var allTypes = pharmacy1Entities.GetContext().Товар.ToList();
            allTypes.Insert(0, new Товар
            {
                Название = "Все типы"
            });
            
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)//Добавление товара в корзину
        {
            var current = (Товар)ProductList.SelectedItem;
            Basket.addproduct((int)current.КодТовара);
            this.checkBasketCount();
        }
        
        private void checkBasketCount()//Скрытая кнопка очисты корзины
        {
            ClearBasket.Visibility = Visibility.Hidden;
            ButtonBorder.Visibility = Visibility.Hidden;
            if (Basket.getBasket().Count > 0)
            {
                ClearBasket.Visibility = Visibility.Visible;
                ButtonBorder.Visibility = Visibility.Visible;
            }
        }

        private void userNameBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)//Если клиент вошел в аккаунт, то по кнопке переместить в окно заказов
        {
            if(DataTransfer.КодКлиента != 0)
            {
                NavigationService.Navigate(new userOrdersWindow());
            }
            else
            {
                MessageBox.Show("Войдите в аккаунт и сделайте заказ, чтобы увидеть список ваших заказов");
                return;
            }
        }
        private void ClearBasketButtonClick(object sender, RoutedEventArgs e)//Очистка корзины
        {
            Basket.ClearBasket();
            this.checkBasketCount();
        }
        private void ToBasketClick(object sender, RoutedEventArgs e)//Перемещение в корзину
        {
            NavigationService.Navigate(new BasketView());
        }

        
        private void Search_TextChanged(object sender, TextChangedEventArgs e)//Поиск товаров по названию
        {
            ProductList.ItemsSource = pharmacy1Entities.GetContext().Товар.Where(u => u.Название.Contains(Search.Text)).ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) //Возврат в меню покупателя
        {
            NavigationService.Navigate(new clientMenu());
        }
    }
}
