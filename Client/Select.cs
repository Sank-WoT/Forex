using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Select
    {
        /// <summary>
        /// Выбрать все записи
        /// </summary>
        /// <param name="Value">Наименование котировки</param>
        public Select(string Value, SqlConnection connection, ref List<int> TimeL, ref List<double> sellL, ref List<double> buyL)
        {
            string commanda = "SELECT *  FROM dbo.[" + Value + "];";
            SqlCommand command = new SqlCommand(commanda, connection);
            SqlDataReader reader = command.ExecuteReader();
            // если есть данные
            if (reader.HasRows) 
            {
                // построчно считываем данные
                while (reader.Read()) 
                {
                    TimeL.Add((int)reader.GetValue(0));
                    sellL.Add((double)reader.GetValue(1));
                    buyL.Add((double) reader.GetValue(2));
                }
            }
        }

        public Select()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }

        /// <summary>
        /// Выбор из таблицы от id 
        /// </summary>
        /// <param name="name">наименованеи таблицы</param>
        /// <param name="col">наименование колонки</param>
        /// <param name="Id">начало отчета</param>
        /// <returns>Возвращаеся запрос выборки</returns>
        public string SelectAfterId(string name, string col, int Id)
        {
            List<EventGroup> EvGroup = new List<Client.EventGroup>();
            string commanda = "SELECT *  FROM dbo.[" + name + "] WHERE "+col+">"+Id+";";
            return commanda;
        }

        /// <summary>
        /// Выбор из таблицы по id 
        /// </summary>
        /// <param name="name">наименованеи таблицы</param>
        /// <param name="col">наименование колонки</param>
        /// <param name="Id">начало отчета</param>
        /// <returns>Возвращаеся запрос выборки</returns>
        public string SelectSameId(string name, string col, int Id)
        {
            string commanda = "SELECT *  FROM dbo.[" + name + "] WHERE " + col + "=" + Id + ";";
            return commanda;
        }

        /// <summary>
        /// Выбор из таблицы по id 
        /// </summary>
        /// <param name="name">наименованеи таблицы</param>
        /// <param name="col">наименование колонки</param>
        /// <param name="Id">начало отчета</param>
        /// <returns>Возвращаеся запрос выборки</returns>
        public string SelectAfterId(string name, string col, string nameC)
        {
            string commanda = "SELECT *  FROM dbo.[" + name + "] WHERE " + col + "=" + nameC + ";";
            return commanda;
        }


        /// <summary>
        /// Метод производит запрос к БД для получения группы событий
        /// </summary>
        /// <param name="name">наименованеи таблицы</param>
        /// <param name="col">наименование колонки</param>
        /// <param name="Id">начало отчета</param>
        /// <param name="con">Переменнаяя соединения</param>
        /// <returns>Возвращает группу событий</returns>
        public List<EventGroup> SelectAfterIdEventGroup(string name, string col, int Id, SqlConnection con)
        {
            SqlCommand command = new SqlCommand(SelectAfterId(name, col, Id), con);
            return FormListEventGroup(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="col"></param>
        /// <param name="Id"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public List<Event> SelectAfterTimeEvent(string name, string col, int Id, SqlConnection con)
        {
            SqlCommand command = new SqlCommand(SelectAfterId(name, col, Id), con);
            return FormListEvent(command);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="col"></param>
        /// <param name="Id"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public List<EventGroup> SelectSameIdEventGroup(string name, string col, int Id, SqlConnection con)
        {
            SqlCommand command = new SqlCommand(SelectSameId(name, col, Id), con);
            return FormListEventGroup(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="col"></param>
        /// <param name="Id"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public List<EventGroup> SelectAfterIdEventGroup(string name, string col, string Id, SqlConnection con)
        {
            Console.WriteLine(SelectAfterId(name, col, Id), con);
            SqlCommand command = new SqlCommand(SelectAfterId(name, col, Id), con);
         
            return FormListEventGroup(command);
        }

        /// <summary>
        /// формирователь листа EventGrou[
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<EventGroup> FormListEventGroup(SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();
            List<EventGroup> Group = new List<EventGroup>();
            // если есть данные
            if (reader.HasRows)
            {
                // построчно считываем данные
                while (reader.Read())
                {
                    EventGroup group = new EventGroup();
                    group.idEventGroup = (int) reader.GetValue(0);
                    group.name = (string) reader.GetValue(1);
                    group.unitNumberst = (string) reader.GetValue(2);
                    Group.Add(group);
                }
            }
            return Group;
        }

        /// <summary>
        /// Метод производит запрос к БД для получения событий
        /// </summary>
        /// <param name="name">наименованеи таблицы</param>
        /// <param name="col">наименование колонки</param>
        /// <param name="Id">начало отчета</param>
        /// <param name="con">Переменнаяя соединения</param>
        /// <returns>Возвращает группу событий</returns>
        public List<Event> SelectAfterIdEvent(string name, string col, int Id, SqlConnection con)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            SqlCommand command = new SqlCommand(SelectAfterId(name, col, Id), con);
            return FormListEvent(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="col"></param>
        /// <param name="nameC"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public List<Event> SelectAfterIdEvent(string name, string col, string nameC, SqlConnection con)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            SqlCommand command = new SqlCommand(SelectAfterId(name, col, nameC), con);
            return FormListEvent(command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="col"></param>
        /// <param name="id"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        public List<Event> SelectSameIdEvent(string name, string col, int id, SqlConnection con)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            SqlCommand command = new SqlCommand(SelectSameId(name, col, id), con);
            return FormListEvent(command);
        }

        /// <summary>
        /// Формирователь лисьа событий
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private List<Event>  FormListEvent(SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();
            List<Event> LEven = new List<Event>();
            // если есть данные
            if (reader.HasRows)
            {
                // построчно считываем данные
                while (reader.Read())
                {
                    Event Even = new Event();
                    Even.idEvent = (int) reader.GetValue(0);
                    Even.idCountry = (int) reader.GetValue(1);
                    Even.IdEvent_Group = (int) reader.GetValue(2);
                    Even.Time = (int) reader.GetValue(3);
                    if ((string) reader.GetValue(4) != "")
                    {
                        Even.ExpectNumber = double.Parse(reader.GetValue(5).ToString());
                    }
                    else
                    {
                        Even.FactNumber = 0;
                    }
                    if ((string) reader.GetValue(5) != "")
                    {
                        Even.ExpectNumber = double.Parse(reader.GetValue(5).ToString());
                    }
                    else
                    {
                        Even.ExpectNumber = 0;
                    }
                    Even.Pot = (string) reader.GetValue(6);
                    LEven.Add(Even);
                }
            }
            return LEven;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="row"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public int SelectLast(SqlConnection connection, string row, string table)
        {
            LastId l = new LastId();
            SqlCommand command = new SqlCommand(l.Last(row, table), connection);
            SqlDataReader reader = command.ExecuteReader();
            // если есть данные
            int last = -1;
            if (reader.HasRows)
            {           
                while (reader.Read())
                {      
                    last = int.Parse(reader.GetValue(0).ToString());
                }
                return last;
            }
            return last;
        }

        /// <summary>
        /// Возвращает последнее значение id_Even из таблицы Event
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public int SelectLastIdEvent(SqlConnection connection)
        {
            return SelectLast(connection, "Id_Event", "Event");
        }

        /// <summary>
        /// Возвращает последнее значение id_EventGroup из таблицы EventGroup
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public int SelectLastIdEventGroup(SqlConnection connection)
        {
            return SelectLast(connection, "id_EventGroup", "EventGroup");
        }
    }
}
