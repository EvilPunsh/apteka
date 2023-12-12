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

namespace онлайн_аптека.windows
{
    /// <summary>
    /// Логика взаимодействия для WindowWithFrameForClient.xaml
    /// </summary>
    public partial class WindowWithFrameForClient : Window
    {
        public WindowWithFrameForClient()
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
    }
}
