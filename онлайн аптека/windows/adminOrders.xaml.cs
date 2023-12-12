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
using онлайн_аптека.dataBase;

namespace онлайн_аптека.windows
{
    /// <summary>
    /// Логика взаимодействия для adminOrders.xaml
    /// </summary>
    public partial class adminOrders : Page
    {
        //вывод данных из бд
        public static pharmacy1Entities DataEntitiesЗаказ { get; set; }
        public ObservableCollection<Заказ> ListЗаказ { get; set; }
        public adminOrders()
        {
            InitializeComponent();
            DataEntitiesЗаказ = new pharmacy1Entities();
            ListЗаказ = new ObservableCollection<Заказ>();
            DataGridorders.ItemsSource = pharmacy1Entities.GetContext().Заказ.ToList();
        }
        private void GetЗаказ()
        {
            var Заказ = DataEntitiesЗаказ.Заказ;
            var queryorders = from orders in Заказ
                             orderby orders.КодЗаказа
                             select orders;
            foreach (Заказ зак in queryorders)
            {
                ListЗаказ.Add(зак);
            }
            DataGridorders.ItemsSource = ListЗаказ;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetЗаказ();
            DataGridorders.SelectedIndex = 0;
        }
        //Перезаписать

        private void RewriteЗаказ()
        {
            DataEntitiesЗаказ = new pharmacy1Entities();
            ListЗаказ.Clear();
            GetЗаказ();
        }

        //Удаление

        private bool isDirty = true;
        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Заказ зак = DataGridorders.SelectedItem as Заказ;
            if (зак != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные?", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesЗаказ.Заказ.Remove(зак);
                    DataGridorders.SelectedIndex = DataGridorders.SelectedIndex == 0 ? 1 : DataGridorders.SelectedIndex - 1;
                    ListЗаказ.Remove(зак);
                    DataEntitiesЗаказ.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления");
            }
        }
        private void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;

        }
        //Редактирование
        private void EditCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;

        }
        private void EditCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGridorders.IsReadOnly = false;
            DataGridorders.BeginEdit();
            isDirty = false;
        }
        //Сохранение
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataEntitiesЗаказ.SaveChanges();
            isDirty = true;
            DataGridorders.IsReadOnly = true;
        }
        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }
        //Отмена
        private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RewriteЗаказ();
            DataGridorders.IsReadOnly = true;
            isDirty = true;
        }
        private void UndoCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }
        //Новая запись
        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var Заказ = new Заказ();
            Заказ.КодАптеки = 0;
            Заказ.КодКлиента = 0;
            try
            {
                DataGridorders.IsReadOnly = false;
                DataGridorders.BeginEdit();
                DataEntitiesЗаказ.Заказ.Add(Заказ);
                ListЗаказ.Add(Заказ);
                isDirty = false;
            }
            catch
            {
                throw new ApplicationException("Ошибка добавления данных");
            }
        }
        private void NewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = isDirty;
        }
        //Обновление
        private void RefreshCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RewriteЗаказ();
            DataGridorders.IsReadOnly = false;
            isDirty = false;
        }
        //Возвращение на страницу меню у администратора
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new adminMenu());
        }
    }
}
