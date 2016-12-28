namespace Client
{
    using System;
    using System.Drawing;
    using System.IO; // для класса 
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Data.SqlClient;
    /// <summary>
    ///  Класс базы данных
    /// </summary>
    public class Bd
    {
        /// <summary>
        ///  путь
        /// </summary>         
        protected string patch;

        /// <summary>
        ///  данные конфигурации
        /// </summary>
        /// <param name="_patch">Путь/param>
        public Bd(string _patch)
        {
            patch = _patch;
        }

        /// <summary>
        ///  загрузка в бд
        /// </summary>
        /// <param name="bdValue">Котировка/param>
        /// <param name="number">Кол-во/param>
        public void bdLoad(string bdValue, int number)
        {
            string pathFile = Application.StartupPath + "\\" + bdValue + ".txt";
            // Создание объекта БД                                                                                                                                                                     
            BdReqest reqestBdEURUSD = new BdReqest(patch);
            Internet IPair = new Internet();
            string response = IPair.FirstConnectBD(bdValue, pathFile, number, patch);
            List<int> BListTBuf = new List<int>();
            List<double> BListBBuf = new List<double>();
            List<double> BListSBuf = new List<double>();
            Parser BdParser = new Parser(response);
            // Присвоили данные к листам
            BdParser.BDREqest(ref BListTBuf, ref BListBBuf, ref BListSBuf);
            Console.WriteLine("" + bdValue);
            // Важный запрос добавления осталось это проверить
            SqlConnection con = new SqlConnection(patch);
            con.Open();
            reqestBdEURUSD.Insert(bdValue, BListTBuf, BListBBuf, BListSBuf, con);
            con.Close();
            Console.WriteLine("" + bdValue);
        }

        /// <summary>
        /// Метод загрузки данных
        /// </summary>
        /// <param name="BasaDan">База данных</param>
        /// <param name="value">Котировка</param>
        public Task LoadData (string value)
        {
            Task t;
            // поток подключения eurusd
            t = Task.Run(() =>
            {
                TaskConnect(value);
            });

            // загружаем eurusd
            bdLoad(value, 1000000);
            return t;
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

        public string getPatch()
        {
            return patch;
        }
    }
}