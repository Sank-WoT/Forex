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
        public delegate int delLast(SqlConnection con);
        List<Event> Ev;
        List<EventGroup> EventGroup;
        /// <summary>
        ///  путь
        /// </summary>         
        protected string stringConnect;
        /// <summary>
        ///  данные конфигурации
        /// </summary>
        /// <param name="_stringConnect">Строка подключения/param>
        public Bd(string _stringConnect)
        {
            stringConnect = _stringConnect;
        }

        /// <summary>
        ///  загрузка котировок в бд
        /// </summary>
        /// <param name="bdValue">Котировка/param>
        /// <param name="number">Кол-во/param>
        public int bdLoadQuote(string bdValue, int number)
        {
            // путь 
            string pathFile = Application.StartupPath + "\\" + bdValue + ".txt";
            // Создание объекта БД                                                                                                                                                                     
     
            Internet IPair = new Internet();
            // запрашиваем данные
            string response = IPair.FirstConnectBD(bdValue, pathFile, number, stringConnect);
            // Лист времени
            List<int> BListTBuf = new List<int>();
            // Лист покупок
            List<double> BListBBuf = new List<double>();
            // Лист продаж
            List<double> BListSBuf = new List<double>();
            // новые котировки
            Parser BdParser = new Parser(response);
            // получаем данные
            BdParser.BDREqest(ref BListTBuf, ref BListBBuf, ref BListSBuf);
            // подключение
            SqlConnection con = new SqlConnection(stringConnect);
        
            // открыть подключение
            try
            {
                con.Open();
                Console.WriteLine("Подключение открыто");
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Ошибка подключения:{0}", ex.Message);
            }

            //Присвоение котировки
            Quotes per = new Quotes();
            per.Sell = BListSBuf;
            per.Buy = BListBBuf;
            per.TimeU = BListTBuf;

            // занести в БД
            insertQuotes reqestBdEURUSD = new insertQuotes(bdValue, per, con);
            // закрыть подключение
            con.Close();
            Console.WriteLine("Подключение закрыто");
            return reqestBdEURUSD.LenghtInsert; 
        }

        /// <summary>
        /// Метод загрузки данных
        /// </summary>
        /// <param name="BasaDan">База данных</param>
        /// <param name="value">Котировка</param>
        public Task LoadDataQuote (string value)
        {
            Task t;
            // поток подключения eurusd
            t = Task.Run(() =>
            {
                TaskConnect(value);
   
                bdLoadQuote(value, 1000000);
            });  
            return t;
        }

        // Получение данных
        private DataInet RequestEventInet(string stroka, int id)
        {
            DataInet data = new DataInet(stroka, id);
            return data;
        }

        private DataInet RequestEventGtoupInet(string stroka, int id)
        {
            DataInet data = new DataInet();
            data.DataInetGroup(stroka, id);
            return data;
        }

        /// <summary>
        /// Загрузка событий
        /// </summary>
        /// <param name="stroka"></param>
        /// <param name="idEvent">номер события</param>
        /// <returns></returns>
        public List<Event> LoadDataEvent(string stroka, int id, ParserEventFabric Parse)
        {
            // получаем данные
            Parse.Parse(RequestEventInet(stroka, id).CreateRequest());
            // создаем лист событий
            // получение событий
            this.Ev = Parse.getEvent();
            return Ev;
        }

        /// <summary>
        /// Загрузка группы событий
        /// </summary>
        /// <param name="stroka"></param>
        /// <param name="idEvent">номер события</param>
        /// <returns>данные групп событий</returns>
        public List<EventGroup> LoadDataGroupEvent(string stroka, int id, ParserEventGroupFabric Parse)
        {
            // получаем данные
            Parse.Parse(RequestEventGtoupInet(stroka, id).CreateRequest());
            // получение группы событий
            this.EventGroup = Parse.getEventGroup();
            return EventGroup;
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
            return stringConnect;
        }

       public void Select(string bdvalue, ref List<int> TimeL, ref List<double> sellL, ref List<double> buyL)
        {
            SqlConnection con = new SqlConnection(stringConnect);
            con.Open();
            // выбрать данные
            Select Triada = new Select(bdvalue, con,ref TimeL, ref sellL,ref buyL);
            con.Close();
        }
        
        /// <summary>
        /// Метод возвращающий из бд последнее id
        /// </summary>
        /// <returns></returns>
        public int SelectLastIdEvent()
        {
            Select Last = new Select();
            delLast dL = new delLast(Last.SelectLastIdEvent);
            return SelectLast(dL);
        }

        /// <summary>
        /// Метод возвращающий из бд последнее время id Group
        /// </summary>
        /// <returns></returns>
        public int SelectLastIdEventGroup()
        {
            Select Last = new Select();
            delLast dL = new delLast(Last.SelectLastIdEventGroup);
            return SelectLast(dL);
        }

        /// <summary>
        /// Абстрактный метод
        /// </summary>
        /// <param name="deleg">Делегат передающий конкретную функцию для запроса id  в БД</param>
        /// <returns></returns>
        public int SelectLast(delLast deleg)
        {
            SqlConnection con = new SqlConnection(stringConnect);
            con.Open();   
            int last;
            last = deleg(con);
            con.Close();
            return last;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id">Номер id с которого начинается выборка</param>
        /// <returns>Возвращает лист групп событий</returns>
        public List<EventGroup> SelectEventGroup(int Id)
        {
            SqlConnection con = new SqlConnection(stringConnect);
            con.Open();
            Select IdSelect = new Select();
            List<EventGroup> a = new List<EventGroup>();
            a = IdSelect.SelectAfterIdEventGroup("EventGroup", "id_EventGroup", Id, con);
            con.Close();
            return a;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <returns>Возвращает лист групп событий</returns>
        public List<EventGroup> SelectEventGroup(string name)
        {
            SqlConnection con = new SqlConnection(stringConnect);
            con.Open();
            Select IdSelect = new Select();
            List<EventGroup> a = new List<EventGroup>();
            a = IdSelect.SelectAfterIdEventGroup("EventGroup", "name", name, con);
            con.Close();
            return a;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Наименование</param>
        /// <returns>Возвращает лист групп событий</returns>
        public List<EventGroup> SelectSameEventGroup(int id)
        {
            SqlConnection con = new SqlConnection(stringConnect);
            con.Open();
            Select IdSelect = new Select();
            List<EventGroup> a = new List<EventGroup>();
            a = IdSelect.SelectSameIdEventGroup("EventGroup", "id_EventGroup", id, con);
            con.Close();
            return a;
        }

        /// <summary>
        /// Выбрать события от id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Event> SelectEvent(int Id)
        {
            SqlConnection con = new SqlConnection(stringConnect);
            con.Open();
            Select IdSelect = new Select();
            return IdSelect.SelectAfterIdEvent("Event", "id_Event", Id, con);
        }

        /// <summary>
        /// Выбрать события от Time
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Event> SelectEventTime(int Time)
        {
            SqlConnection con = new SqlConnection(stringConnect);
            con.Open();
            Select IdSelect = new Select();
            List <Event> a = new List<Event>();
            a = IdSelect.SelectAfterIdEvent("Event", "Time", Time, con);
            con.Close();
            return a;
        }

        /// <summary>
        /// Выбрать события по id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Event> SelectSameEvent(int Id)
        {
            SqlConnection con = new SqlConnection(stringConnect);
            con.Open();
            Select IdSelect = new Select();
            return IdSelect.SelectSameIdEvent("Event", "id_Event", Id, con);
        }

        /// <summary>
        /// 
        /// </summary>s
        /// <param name="stroka"></param>
        /// <param name="id"></param>
        /// <param name="Parse"></param>
        public void LoadInBdEvent(string stroka, int id, ParserEventFabric Parse)
        {
            LoadDataEvent(stroka, id, Parse);
            string bdValue = "Event";
            SqlConnection connection = new SqlConnection(stringConnect);
            connection.Open();
            InsertEvent reqestBd = new InsertEvent(bdValue, Ev, connection);
            connection.Close();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stroka"></param>
        /// <param name="id"></param>
        /// <param name="Parse"></param>
        public void LoadInBdEventGroup(string stroka, int id, ParserEventGroupFabric Parse)
        {
            LoadDataGroupEvent(stroka, id, Parse);
            string bdValue = "EventGroup";
            SqlConnection connection = new SqlConnection(stringConnect);
            connection.Open();
            InsertEventGroup reqestBd = new InsertEventGroup(bdValue, EventGroup, connection);
            connection.Close();
        }

    }
}