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
using онлайн_аптека.page;

namespace онлайн_аптека.windows
{
    /// <summary>
    /// Логика взаимодействия для adminCatalog.xaml
    /// </summary>
    public partial class adminCatalog : Page
    {
        //вывод данных из бд
        public static pharmacy1Entities DataEntitiesТовар { get; set; }
        public ObservableCollection<Товар> ListТовар { get; set; }
        public adminCatalog()
        {
            InitializeComponent();
            DataEntitiesТовар = new pharmacy1Entities();
            ListТовар = new ObservableCollection<Товар>();
            DataGridtovar.ItemsSource = pharmacy1Entities.GetContext().Товар.ToList();
        }

        private void GetТовар()
        {
            var Товар = DataEntitiesТовар.Товар;
            var querytovar = from tovar in Товар
                             orderby tovar.КодТовара
                             select tovar;
            foreach (Товар тов in querytovar)
            {
                ListТовар.Add(тов);
            }
            DataGridtovar.ItemsSource = ListТовар;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetТовар();
            DataGridtovar.SelectedIndex = 0;
        }
        //Перезаписать
        private void RewriteТовар()
        {
            DataEntitiesТовар = new pharmacy1Entities();
            ListТовар.Clear();
            GetТовар();
        }

        //Удаление
        private bool isDirty = true;
        private void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Товар тов = DataGridtovar.SelectedItem as Товар;
            if (тов != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные?", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    DataEntitiesТовар.Товар.Remove(тов);
                    DataGridtovar.SelectedIndex = DataGridtovar.SelectedIndex == 0 ? 1 : DataGridtovar.SelectedIndex - 1;
                    ListТовар.Remove(тов);
                    DataEntitiesТовар.SaveChanges();
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
            DataGridtovar.IsReadOnly = false;
            DataGridtovar.BeginEdit();
            isDirty = false;
        }
        //Сохранение
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataEntitiesТовар.SaveChanges();
            isDirty = true;
            DataGridtovar.IsReadOnly = true;
        }
        private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }
        //Отмена
        private void UndoCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RewriteТовар();
            DataGridtovar.IsReadOnly = true;
            isDirty = true;
        }
        private void UndoCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !isDirty;
        }
        //Новая запись
        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var Товар = new Товар();
            Товар.Название = "не задано";
            Товар.Цена = 0;
            Товар.Количество = 1;
            Товар.Описание = "не задано";
            Товар.Тип = "не задано";
            try
            {
                DataGridtovar.IsReadOnly = false;
                DataGridtovar.BeginEdit();
                DataEntitiesТовар.Товар.Add(Товар);
                ListТовар.Add(Товар);
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
            RewriteТовар();
            DataGridtovar.IsReadOnly = false;
            isDirty = false;
        }
        //Возвращение на страницу меню у администратора
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new adminMenu());
        }
    }
}
