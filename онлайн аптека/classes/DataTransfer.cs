using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace онлайн_аптека.classes
{
    public class DataTransfer //Класс для хранения данных о клиенте
    {
        public static int КодКлиента { get; set; }
        public static string Имя { get; set; }
        public static string Фамилия { get; set; }
        public static string Отчество { get; set; }
    }
}
