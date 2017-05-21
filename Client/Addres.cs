using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Addres
    {
        /// <summary>
        /// Адрес коннекта
        /// </summary>
        public string addresSite { get; set; }
        /// <summary>
        /// формирование адреса
        /// </summary>
        /// <param name="addres">адрес сайта</param>
        /// <param name="Name">наименование валюты</param>
        /// <param name="Limit">Лимит значений</param>
        /// <param name="Time">Время</param>
        public Addres(string addres, string Name, int Limit, int Time)
        {
            // формируем строку адреса
            this.addresSite = $"{addres}?quote={Name}&time={Time}&limit={ Limit }";
            Console.WriteLine(addresSite);
        }

        public Addres(string addres, int idEvent)
        {
            eventAddres(addres, idEvent);
        }

        public Addres(string addres, int limit, double poslT, string value)
        {
            // формируем строку адреса
            this.addresSite = $"{addres}{poslT}&limit={limit}&sign={value}";
            Console.WriteLine(addresSite);
        }

        /// <summary>
        /// метод по формировании адрема сайта к Event
        /// </summary>
        /// <param name="addres">начало сайта</param>
        /// <param name="idEvent">номер id</param>
        private void eventAddres(string addres, int idEvent)
        {
            // формируем строку адреса к Event
            this.addresSite = $"{addres}?idEvent={idEvent}";
            Console.WriteLine(this.addresSite);
        }

        public Addres() { }

        public void eventAddresGroup(string addres, int idEventGroup)
        {
            // формируем строку адреса к Event
            this.addresSite = $"{addres}?idEventGroup={idEventGroup}";
            Console.WriteLine(this.addresSite);
        }
    }
}
