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
    /// Логика взаимодействия для BasketView.xaml
    /// </summary>
    public partial class BasketView : Page
    {
        private List<Товар> basketItems = new List<Товар>();
        //Отображение товаров, добавленных в корзину
        public BasketView()
        {
            InitializeComponent();
            DeliveryInput.ItemsSource = pharmacy1Entities.GetContext().Аптеки.ToList();
            basketItems = new List<Товар>();
            foreach (int КодТовара in Basket.getBasket())
            {
                basketItems.Add(pharmacy1Entities.GetContext().Товар.Find(КодТовара));
            }
            BasketListView.ItemsSource = basketItems;
            updateResultSum();
        }

        private void updateResultSum()//Расчет суммы
        {
            resultSum.Content = $"Итого: { basketItems.Sum(Товар => Товар.Цена)}";
        }
        //Функционал удаления из корзины
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.ComponentModel.IEditableCollectionView items = BasketListView.Items;
            Товар selectedItem = BasketListView.SelectedItems[0] as Товар;
            Basket.Delete((int)selectedItem.КодТовара);
            items.Remove(BasketListView.SelectedItem);
            updateResultSum();
        }

        private void MakeOrderButton_Click(object sender, RoutedEventArgs e) //Оформление заказа
        {
            if (DataTransfer.КодКлиента != 0)
            {
                if (MessageBox.Show($"Вы точно хотите оформить заказ", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (DeliveryInput.Text != null)
                    {
                        Заказ orderData = new Заказ();

                        int deliveryID = int.Parse(DeliveryInput.SelectedValue.ToString());

                        orderData.КодКлиента = DataTransfer.КодКлиента;

                        orderData.КодАптеки = deliveryID;

                        pharmacy1Entities.GetContext().Заказ.Add(orderData);


                        try
                        {
                            pharmacy1Entities.GetContext().SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        MessageBox.Show("Заказ оформлен!");
                        basketItems = new List<Товар>();
                        BasketListView.ItemsSource = basketItems;
                        updateResultSum();
                        Basket.ClearBasket();
                    }
                    else
                    {
                        MessageBox.Show("Выберите пункт выдачи!");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, авторизуйтесь, чтобы оформить заказ!");
                return;
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e) //Возврат в каталог товаров
        {
            NavigationService.Navigate(new clientCatalog());
        }
        

    }
}
