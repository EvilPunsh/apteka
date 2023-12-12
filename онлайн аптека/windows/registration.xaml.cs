using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
using онлайн_аптека.classes;
using онлайн_аптека.dataBase;

namespace онлайн_аптека.windows
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Window
    {
        public registration()
        {
            InitializeComponent();
        }
        //Кнопка закрытия окна
        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CollapseButton_MouseDown(object sender, MouseButtonEventArgs e)//Кнопка чтобы свернуть окно
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Knopki_MouseDown(object sender, MouseButtonEventArgs e)//Панель, за которую можно передвигать окно
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)//Панель, за которую можно передвигать окно
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void PasswordBox_PasswordChanged1(object sender, RoutedEventArgs e)//Запись чтобы пароль был скрыт
        {
            if (TwoTextBox.Password.Length > 0)
            {
                Watermark.Visibility = Visibility.Collapsed;
            }
            else
            {
                Watermark.Visibility = Visibility.Visible;
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)//Перемещение на окно авторизации
        {
            authoriz window = new authoriz();
            window.Show();
            this.Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)//Запись чтобы пароль был скрыт для "повтора пароля"
        {
            if (TwoTextBoxTwo.Password.Length > 0)
            {
                Watermark1.Visibility = Visibility.Collapsed;
            }
            else
            {
                Watermark1.Visibility = Visibility.Visible;
            }
        }
        //Кнопка сохранения и его условия
        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            if (SurnameTextBox.Text == "" || NameTextBox.Text == "" || oneTextBox.Text == "" || TwoTextBox.Password == "" || TwoTextBoxTwo.Password == "")
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }

            if (TwoTextBox.Password != TwoTextBoxTwo.Password)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            if (pharmacy1Entities.GetContext().Клиент.ToList().Find(u => u.Логин == oneTextBox.Text) != null)
            {
                MessageBox.Show("Такой пользователь уже существуют!");
                return;
            }

            if (pharmacy1Entities.GetContext().Клиент.ToList().Find(u => u.Фамилия == SurnameTextBox.Text) != null)
            {
                if (pharmacy1Entities.GetContext().Клиент.ToList().Find(u => u.Имя == NameTextBox.Text) != null)
                {
                    if (pharmacy1Entities.GetContext().Клиент.ToList().Find(u => u.Отчество == PatronymicTextBox.Text) != null)
                    {
                        MessageBox.Show("Такой пользователь уже существуют!");
                        return;
                    }
                }
            }
            //Добавление нового клиента
            Клиент people = new Клиент
            {
                Логин = oneTextBox.Text,
                Пароль = TwoTextBox.Password,
                Фамилия = SurnameTextBox.Text,
                Имя = NameTextBox.Text,
                Отчество = PatronymicTextBox.Text,
            };

            AppData.db.Клиент.Add(people);
            try
            {

                AppData.db.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show("Object: " + validationError.Entry.Entity.ToString());
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + "");
                    }
                }
            }

            MessageBox.Show("Вы успешно зарегистрировались!");


        }
    }
}
