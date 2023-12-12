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
    /// Логика взаимодействия для adminClient.xaml
    /// </summary>
    public partial class adminClient : Page
    {
        //вывод данных из бд
        public static pharmacy1Entities DataEntitiesКлиент { get; set; }
        public ObservableCollection<Клиент> ListКлиент { get; set; }
        public adminClient()
        {
            InitializeComponent();
            DataEntitiesКлиент = new pharmacy1Entities();
            ListКлиент = new ObservableCollection<Клиент>();
            DataGridDataClient.ItemsSource = pharmacy1Entities.GetContext().Клиент.ToList();
        }
        private void GetКлиент()
        {
            var Клиент = DataEntitiesКлиент.Клиент;
            var queryclients = from clients in Клиент
                              orderby clients.КодКлиента
                              select clients;
            foreach (Клиент кл in queryclients)
            {
                ListКлиент.Add(кл);
            }
            DataGridDataClient.ItemsSource = ListКлиент;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetКлиент();
            DataGridDataClient.SelectedIndex = 0;
        }
        //Перезаписать
        private void RewriteКлиент()
        {
            DataEntitiesКлиент = new pharmacy1Entities();
            ListКлиент.Clear();
            GetКлиент();
        }

        //Удаление
        private bool isDirty = true;
        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Клиент кл = DataGridDataClient.SelectedItem as Клиент;
            if (кл != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные?", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesКлиент.Клиент.Remove(кл);
                    DataGridDataClient.SelectedIndex = DataGridDataClient.SelectedIndex == 0 ? 1 : DataGridDataClient.SelectedIndex - 1;
                    ListКлиент.Remove(кл);
                    DataEntitiesКлиент.SaveChanges();
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
            DataGridDataClient.IsReadOnly = false;
            DataGridDataClient.BeginEdit();
            isDirty = false;
        }
        //Сохранение
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataEntitiesКлиент.SaveChanges();
            isDirty = true;
            DataGridDataClient.IsReadOnly = true;
        }
        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }
        //Отмена
        private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RewriteКлиент();
            DataGridDataClient.IsReadOnly = true;
            isDirty = true;
        }
        private void UndoCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }
        //Новая запись
        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var Клиент = new Клиент();
            Клиент.Фамилия = "не задана";
            Клиент.Имя = "не задано";
            Клиент.Отчество = "не задано";
            Клиент.Логин = "не задан";
            Клиент.Пароль = "не задан";

            try
            {
                DataGridDataClient.IsReadOnly = false;
                DataGridDataClient.BeginEdit();
                DataEntitiesКлиент.Клиент.Add(Клиент);
                ListКлиент.Add(Клиент);
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
            RewriteКлиент();
            DataGridDataClient.IsReadOnly = false;
            isDirty = false;
        }
        //Возвращение на страницу меню у администратора
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new adminMenu());
        }
    }
}
