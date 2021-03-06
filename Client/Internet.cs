﻿namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Globalization;
    using System.IO; // для класса 
    using System.Linq;
    using System.Media;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using EnumDialogResult = System.Windows.Forms.DialogResult;
    using Gettext;
    /// <summary>
    ///  Класс отечающий за работу с интернетом
    /// </summary>
    public class Internet
    {
        /// <summary>
        /// Этот метод проверяет интернет соединение и переводит программу в автономный режим в случае его отсутсвия +
        /// </summary>
        ///
        /// <param name="value">Название котировки</param>
        /// <param name="object"></param>
        /// <param name="internetActionFinished">оповещении о Окончание запроса </param>
        public bool TryCon(string value, object sync, bool internetActionFinished)
        {
            bool inet;
            inet = ReqestInet(value, internetActionFinished);
            lock (sync)
            {
                internetActionFinished = true;
            }
            return inet;
        }

        /// <summary>
        /// Этот метод проверяет интернет соединение и переводит программу в автономный режим в случае его отсутсвия +
        /// </summary> 
        /// <param name="value">Название котировки</param>
        /// <param name="internetActionFinished">оповещении о Окончание запроса </param>
        public bool ReqestInet(string value, bool internetActionFinished)
        {
            bool inet;
            try
            {
                // запрос на сайт 
                var webReq = WebRequest.Create("http://myfirstphpapp-skro.rhcloud.com/get_currency.php?time=" + "0" + "&limit=" + "1" + "&sign=" + value); 
                // получение ответа
                WebResponse webRes = webReq.GetResponse(); 
                webRes.Close();
                inet = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Gettext._("Lack of or inaccessible internet site go offline"));
                inet = false;
            }
            return inet;
        }

        /// <summary>
        /// Этот метод находит из базы данных  последнее время котировки и делает запрос на сайт и сохраняет в БД
        /// </summary>
        /// <param name="pathFile">Путь к файлу</param>
        /// <param name="value">Выбранная котировка</param>
        /// <param name="number">кол-во чисел</param>
        public string FirstConnectBD(string value, string pathFile, int number, string patch)
        {
            string response = "";
            WorkFile a = new WorkFile();
            List<int> BListT = new List<int>();
            List<double> BListB = new List<double>();
            List<double> BListS = new List<double>();
            // Создание объекта БД 
            BdReqest reqestBdEURUSD = new BdReqest(patch);
            // Выбор записей
            SqlConnection con = new SqlConnection(patch);
            con.Open();
            // выбор данных из бд
            reqestBdEURUSD.CommandSelect(ref BListT, ref BListB, ref BListS, value, con);
            con.Close();
            con.Dispose();
            Console.WriteLine("Last   Time in BD " + BListT[BListT.Count - 1]);
            // (правильно)
            response = ConnectIBD(BListT[BListT.Count - 1], number, value);
            return response;
        }

        /// <summary>
        /// Этот метод находит из файла последнее время котировки и делает запрос на сайт сохраняя в тот же файл
        /// </summary>
        /// <param name="pathFile">Путь к файлу</param>
        /// <param name="value">Выбранная котировка</param>
        /// 
        public void FirstConnect(string value, string pathFile)
        {
            WorkFile a = new WorkFile();
            int lastUpdate = 0;
            // выбрать из файла последнее время
            lastUpdate = a.GetPoslTime(lastUpdate, pathFile); 
             // загрузить 100000 записей                   
            ConnectI(lastUpdate, 1000000, pathFile, value);
        }

         /// <summary>
        /// Этот метод получает последнее время сохраненное в файле, путь и  валюту для запроса на сервер за данными и 
        /// их сохранение в локальный файл
        /// </summary>
        /// <param name="poslTime">Последнее сохранившиеся время</param>
        /// <param name="limit">Предел кол-ва записей с сервера</param>
        /// <param name="value">Выбранная котировка</param>
        /// 
        public string ConnectIBD(int poslTime, int limit,  string value)
        {
            WorkFile rEURUSD = new WorkFile(); // Класс методов
            string response = "";
            if (InetConnect.Inet == true)
            {
                StreamReader DataReader;
                // Получение данных при коннекте
                DataReader = Conection(limit, poslTime, value);
                // присвоение прочтенного к стринг
                try
                {
                     response = DataReader.ReadToEnd();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }               
            }
            return response;
        }


        /// Этот метод получает последнее время сохраненное в файле, путь и  валюту для запроса на сервер за данными и 
        /// их сохранение в локальный файл
        /// </summary>
        /// <param name="poslTime">Последнее сохранившиеся время</param>
        /// <param name="limit">Предел кол-ва записей с сервера</param>
        /// <param name="pathFile">Путь к файлу</param>
        /// <param name="value">Выбранная котировка</param>
        public void ConnectI(int poslTime, int limit, string pathFile, string value)
        {
            // Первый коннект
            #region Получение данных из файла запись их  в text1              
            StreamReader r1 = new StreamReader(pathFile);
            // получение прочтенной записи
            string textfile = r1.ReadToEnd();
            // закрыть чтение 
            r1.Close(); // закрыть чтение 
            #endregion

            if (InetConnect.Inet == true)
            {
                #region Добавление данных в файл из буфера 
                StreamReader DataReader;
                // Получение данных при коннекте
                DataReader = Conection(limit, poslTime, value);
                // присвоение прочтенного к стринг 
                string response = DataReader.ReadToEnd();
                // добавление в  файл новых значений
                textfile += response;
                // получаем путь 
                FileInfo write = new FileInfo(pathFile);
                // создаем текст 
                StreamWriter DataWriter = write.CreateText();
                // добавляем текст 
                DataWriter.WriteLine(textfile);
                // закрыть запись
                DataWriter.Close();
                #endregion
            }
        }

        /// <summary>
        /// Метод отвечающий за запрос за данными +
        /// </summary>
        /// <param name="limit">Предел кол-ва записей с сервера</param>
        /// <param name="value">Выбранная котировка</param>
        /// <param name="poslT">Последнее время</param>
        /// <returns>Возвращает полученные записи из интернета</returns>
        public StreamReader Conection(int limit, double poslT, string value)
        {
            Addres ConnectQuotes = new Addres("http://myfirstphpapp-skro.rhcloud.com/get_currency.php?time=", limit, poslT, value);
            var webReq = WebRequest.Create(ConnectQuotes.addresSite); // запрос на сайт 
            // получение ответа
            WebResponse webRes = webReq.GetResponse();
            // поток по которому получаем инфу
            Stream st = webRes.GetResponseStream();
            // прочитать поток
            StreamReader DataReader = new StreamReader(st);
            return DataReader;
        }
        /// <summary>
        /// Метод загрузки данных +
        /// </summary>
        /// <param name="value">Котировка</param>
        public string TaskConnect(string value)
        {
            Internet IPair = new Internet();
            // Путь к файлу c котировками usdjpy
            string pathFile = Application.StartupPath + "\\" + value + ".txt";
            // первое подключении
            IPair.FirstConnect(value, pathFile);
            return pathFile;
        }
    }
}
