namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Класс для работы с объектом база данных
    /// </summary>
    public class BdReqest : Bd
    {
        // Путь к БД
        private string path;

        /// <summary>
        /// Конструктор для объекта БД
        /// </summary>
        /// <param name="_path">Путь к БД</param>
        public BdReqest(string _path)
        {
            // устанавливаем английскую клавиатуру
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            // принять путь к файлу
            path = _path;
        }

        /// <summary>
        /// Запрос на извлечение данных
        /// </summary>
        /// <param name="ListT">Лист времени</param>
        /// <param name="ListB">Лист покупок</param>
        /// <param name="ListS">Лист продаж</param>
        /// <param name="Value">Наименование котировки</param>
        public void CommandSelect(ref List<int> ListT, ref List<double> ListB, ref List<double> ListS, string Value)
        {
            SqlConnection con = new SqlConnection(path);
            con.Open();
            SqlCommand command = new SqlCommand(Select(Value), con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ListT.Add(reader.GetInt32(0));
                    ListB.Add(reader.GetDouble(1));
                    ListS.Add(reader.GetDouble(2));
                }
            }
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            con.Close();
        }

        /// <summary>
        /// Запрос последнего времени из бд
        /// </summary>
        /// <param name="ListT">Наименование котировки</param>
        /// <param name="Value">Наименование котировки</param>
        public void CommandSelect(ref List<int> ListT,string Value)
        {
            SqlConnection con = new SqlConnection(path);
            con.Open();
            SqlCommand command = new SqlCommand(Select(Value), con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ListT.Add(reader.GetInt32(0));
                }
            }
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            con.Close();
        }

        /// <summary>
        /// Запрос последнего времени из бд
        /// </summary>
        /// <param name="Value">Наименование котировки</param>
        public int lastTime(string  Value)
        {
            SqlConnection con = new SqlConnection(path);
            string commands = "SELECT time FROM " + Value;
            con.Open();
            SqlCommand command = new SqlCommand(commands, con);
            SqlDataReader reader = command.ExecuteReader();
            return reader.GetInt32(0);
        }


        /// <summary>
        /// Запрос на извлечение данных
        /// </summary>
        /// <param name="Time">Время</param>
        /// <param name="Buy">Покупка</param>
        /// <param name="Sell">Продажа</param>
        /// <param name="Value">Наименование котировки</param>
        public void CommandInsert(string Value, List<int> Time, List<double> Buy, List<double> Sell)
        {
            SqlConnection con = new SqlConnection(path);
            con.Open();
            SqlCommand command = new SqlCommand(Insert(Value, Time, Buy, Sell), con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            con.Close();
        }

        /// <summary>
        /// Запрос на добавлени данных
        /// </summary>
        /// <param name="Value">Наименование котировки</param>
        /// <param name="Time">Время</param>
        /// <param name="Buy">Покупка</param>
        /// <param name="Sell">Продажа</param>
        private string Insert(string Value, List<int> Time, List<double> Buy, List<double> Sell)
        {
            int Index = 0;
            int m = 0;
            string command = "";
            // дополнительный сдвиг из за 1 повторяющегося числа
            while (m < (Time.Count() - 2)) 
            {
                command +=  Insert800(Value, Time, Buy, Sell, ref Index);
                m = Index;         
            }
            return command;
        }

        /// <summary>
        /// Сборка запроса в блоки по 800 добавлений
        /// </summary>
        /// <param name="Value">Наименование котировки</param>
        /// <param name="Time">Время</param>
        /// <param name="Buy">Покупка</param>
        /// <param name="Sell">Продажа</param>
        /// <param name="Index">Номер триады</param>
        private string Insert800(string Value, List<int> Time, List<double> Buy, List<double> Sell, ref int Index)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string comI = " VALUES";
            int i = 0;
            for (i = 0; i < 801; i++)
            {
                if((Index + i + 1) <= Sell.Count() - 2)
                {
                    if (800 != i)
                    {
                        comI += "(" + Time[Index + i + 1] + ", " + Buy[Index + i + 1].ToString() + ", " + Sell[Index + i + 1].ToString() + "), ";
                    }
                    else
                    {
                        comI += "(" + Time[Index + i + 1] + ", " + Buy[Index + i + 1].ToString() + ", " + Sell[Index + i + 1].ToString() + ");";
                    }
                }
                else
                {
                    comI += "(" + Time[Index + i + 1] + ", " + Buy[Index + i + 1].ToString() + ", " + Sell[Index + i + 1].ToString() + ");"; break;
                }
            }
            Index+= i;
            string  command = "INSERT INTO [dbo].[" + Value + "] ([time],[bid],[ask]) " + comI ;
            return command;
        }

        /// <summary>
        /// Запрос на добавлени данных
        /// </summary>
        /// <param name="Value">Наименование котировки</param>
        /// <param name="Time">Время</param>
        /// <param name="Buy">Покупка</param>
        /// <param name="Sell">Продажа</param>
        public void formInsert(int Time, double Bid, double Ask, string Value)
        {
            string comI = "INSERT INTO [dbo].[" + Value + "] ([time],[bid],[ask]) ";
            comI += "VALUES(" + Time + ", " + Bid + ", " + Ask + "); ";
            SqlConnection con = new SqlConnection(path);
            con.Open();
            SqlCommand command = new SqlCommand(comI, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка   " + ex);
            }
            con.Close();
        }

        /// <summary>
        /// Выбрать все записи
        /// </summary>
        /// <param name="Value">Наименование котировки</param>
        private string Select(string Value)
        {
            string command = "SELECT *  FROM dbo.[" + Value + "];";
            return command;
        }

    }
}
