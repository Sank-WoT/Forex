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
        /// Запрос на добавлени данных
        /// </summary>
        /// <param name="Value">Наименование котировки</param>
        /// <param name="Time">Время</param>
        /// <param name="Buy">Покупка</param>
        /// <param name="Sell">Продажа</param>
        public void Insert(string Value, List<int> Time, List<double> Buy, List<double> Sell)
        {
            int Index = 0;
            int m = 0;
            // дополнительный сдвиг из за 1 повторяющегося числа
            while (m < (Sell.Count() - 1)) 
            {
                Console.WriteLine("Вошел");
                Console.WriteLine("Вошел" + (Time.Count() - 1));
                // запрос на добавление
                Insert800(Value, Time, Buy, Sell, ref Index);
                //  присвоение 
               m = Index;
            }
        }

        /// <summary>
        /// Сборка запроса в блоки по 800 добавлений
        /// </summary>
        /// <param name="Value">Наименование котировки</param>
        /// <param name="Time">Время</param>
        /// <param name="Buy">Покупка</param>
        /// <param name="Sell">Продажа</param>
        /// <param name="Index">Номер триады</param>
        private void Insert800(string Value, List<int> Time, List<double> Buy, List<double> Sell, ref int Index)
        {
            Console.WriteLine("Sell.Count() " + (Sell.Count() - 1));
            SqlConnection con = new SqlConnection(path);
            con.Open();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string comI = " VALUES";
            int i = 0;
            for (i = 0; i < 801; i++)
            {
                if((Index + i) != Sell.Count() - 1)
                {
                    // если не равно 800 работает
                    if (800 != i)
                    {
                        comI += "(" + Time[Index + i] + ", " + Buy[Index + i].ToString() + ", " + Sell[Index + i].ToString() + "), ";
                    }

                    if(800 == i)
                    {
                        comI += "(" + Time[Index + i] + ", " + Buy[Index + i].ToString() + ", " + Sell[Index + i ].ToString() + ");";
                    }
                }
                else
                {
                    comI += "(" + Time[Index + i] + ", " + Buy[Index + i].ToString() + ", " + Sell[Index + i].ToString() + ");"; break;
                }
            }
            // После окончания прибавить 800 
            Index+= i;
            Console.WriteLine("Index" + Index);
            // составление запроса
            string commands = "INSERT INTO [dbo].[" + Value + "] ([time],[bid],[ask]) " + comI ;
            SqlCommand command = new SqlCommand(commands, con);
            try
            {
                // выполняет sql-выражение и возвращает количество измененных записей
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка");
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
