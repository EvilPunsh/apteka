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
using онлайн_аптека.classes;
using онлайн_аптека.dataBase;
using онлайн_аптека.windows;

namespace онлайн_аптека
{
    /// <summary>
    /// Логика взаимодействия для authoriz.xaml
    /// </summary>
    public partial class authoriz : Window
    {
        public int tries { get; private set; }

        public authoriz()
        {
            InitializeComponent();
        }
        //Кнопка закрытия окна
        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        //Кнопка чтобы свернуть окно
        private void CollapseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        //Панель, за которую можно передвигать окно
        private void Knopki_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        //Панель, за которую можно передвигать окно
        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)//Запись чтобы пароль был скрыт
        {
            if (twoTextBox.Password.Length > 0)
            {
                Watermark.Visibility = Visibility.Collapsed;
            }
            else
            {
                Watermark.Visibility = Visibility.Visible;
            }
        }
        
        private void Registration_Click(object sender, RoutedEventArgs e)//Перемещение в регистрацию
        {
            registration window = new registration();
            window.Show();
            this.Close();
        }
        private void ButtonEnter_Click(object sender, RoutedEventArgs e)//Условия для входа
        {
            if (oneTextBox.Text == "" || twoTextBox.Password == "")
            {
                MessageBox.Show("Не все поля заполнены");
                return;
            }
            Клиент userData = new Клиент();

            

            userData = pharmacy1Entities.GetContext().Клиент.Where(u => u.Логин == oneTextBox.Text && u.Пароль == twoTextBox.Password).FirstOrDefault();

            
            if (tries >= 3 && userData == null)//Блокировка если 3 или более раз неправильно пытаться войти в систему
            {
                DateTime date = DateTime.Now;
                MessageBox.Show($"Вы неправильно ввели данные более трех раз, поэтому система будет заблокирована на 10 секунд");
                while (date.AddSeconds(10) > DateTime.Now)
                {
                    oneTextBox.IsEnabled = false;
                    twoTextBox.IsEnabled = false;
                }
                oneTextBox.IsEnabled = true;
                twoTextBox.IsEnabled = true;
            }



            if (userData != null)
            {
                
                    if (oneTextBox.Text == "admin")
                    {
                        MessageBox.Show("Вы успешно вошли в систему как администратор!");
                        WindowWithFrame window = new WindowWithFrame();
                        window.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Вы успешно вошли в систему. Добро пожаловать");
                        WindowWithFrameForClient WindowWithFrameForClient = new WindowWithFrameForClient();
                        WindowWithFrameForClient.Show();
                        this.Close();
                    }
            }
            else
            {
                MessageBox.Show("Отказано в доступе. Проверьте корректность данных!", "Внимание");
                tries++;
                return;
            }

            DataTransfer.КодКлиента = userData.КодКлиента;
            DataTransfer.Имя = userData.Имя;
            DataTransfer.Фамилия = userData.Фамилия;
            DataTransfer.Отчество = userData.Отчество;

        }


        private void ButtonEnterAsGuest_Click(object sender, RoutedEventArgs e)//Вход для гостя
        {
            MessageBox.Show("Вы входите как гость, поэтому вам доступен только просмотр каталога. Для возможности совершения покупки войдите в аккаунт", "Внимание");
            WindowWithFrameForClient window1 = new WindowWithFrameForClient();
            window1.Show();
            this.Close();
        }
    }
}
