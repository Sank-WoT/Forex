using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Client
{
    public class DataInet : IDatatInet
    {
        public string _name;
        /// <summary>
        /// Объект адресс
        /// </summary>
        private Addres addres;
        /// <summary>
        /// Получение адреса
        /// <param name="addresSite">адрес сайта</param>
        /// <param name="Name">наименование валюты</param>
        /// <param name="Limit">Лимит значений</param>
        /// <param name="Time">Время</param>
         public DataInet(string addresSite, int idEvent)
        {
            addres = new Addres(addresSite, idEvent);
        }

        public DataInet() { }
        
        public void DataInetGroup(string addresSite, int idEventGroup)
        {
             addres = new Addres();
             // формирование адреса
             addres.eventAddresGroup(addresSite, idEventGroup);
        }

        public DataInet(string addresSite, string name, int limit, int time)
        {
            addres = new Addres(addresSite, name, limit, time);
            _name = name;
        }

        /// <summary>
        /// Прочтение потока данных
        /// </summary>
        /// <returns> возвращение результата</returns>
        public StreamReader CreateRequest()
        {
            var webReq = WebRequest.Create(addres.addresSite);
            WebResponse webRes = webReq.GetResponse();
            // поток по которому получаем инфу
            Stream st = webRes.GetResponseStream();
            // прочитать поток
            StreamReader DataReader = new StreamReader(st);
            return DataReader;
        }
    }
}