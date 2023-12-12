using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace онлайн_аптека.classes
{
    internal class Basket
    {
        private static List<int> _basket = new List<int>(); //Новая корзина
        public static List<int> getBasket() //Получение данных из нее
        {
            return _basket;
        }

        public static void addproduct(int value) //Добавление в нее продуктов
        {
            _basket.Add(value);
        }

        public static void ClearBasket() //Очищение корзины
        {
            _basket = new List<int>();
        }

        public static void Delete(int id) //Удаление конкретного продукта из корзины
        {
            _basket.Remove(id);
        }
    }
}
