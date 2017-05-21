namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Класс для работы с объектом база данных
    /// </summary>
    public class BdReqest : Bd
    {
        /// <summary>
        /// Конструктор запросов для объекта БД
        /// </summary>
        /// <param name="_patch">Путь к БД</param>

        public int LenghtInsert;
        public int CompleteInsert;

        public BdReqest(string _patch) : base(_patch)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        /// <summary>
        /// Запрос на извлечение данных из БД
        /// </summary>
        /// <param name="ListT">Лист времени</param>
        /// <param name="ListB">Лист покупок</param>
        /// <param name="ListS">Лист продаж</param>
        /// <param name="Value">Наименование котировки</param>
        public void CommandSelect(ref List<int> ListT, ref List<double> ListB, ref List<double> ListS, string Value, SqlConnection con)
        {
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
                reader.Close();
                // command.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Запрос последнего времени из бд
        /// </summary>
        /// <param name="ListT">Наименование котировки</param>
        /// <param name="Value">Наименование котировки</param>
        public void CommandSelect(ref List<int> ListT,string Value, SqlConnection con)
        {
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
        }

        /// <summary>
        /// Запрос последнего времени из бд
        /// </summary>
        /// <param name="Value">Наименование котировки</param>
        public int lastTime(string  Value, SqlConnection con)
        {
            string commands = "SELECT time FROM " + Value;
            SqlCommand command = new SqlCommand(commands, con);
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine("Last   " + reader.GetInt32(0));
            return reader.GetInt32(0);
        }

        /// <summary>
        /// Запрос на добавлени данных
        /// </summary>
        /// <param name="Value">Наименование котировки</param>
        /// <param name="Time">Время</param>
        /// <param name="Buy">Покупка</param>
        /// <param name="Sell">Продажа</param>
        public void formInsert(int Time, double Bid, double Ask, string Value, SqlConnection con)
        {
            string comI = "INSERT INTO [dbo].[" + Value + "] ([time],[bid],[ask]) ";
            comI += "VALUES(" + Time + ", " + Bid + ", " + Ask + "); ";
            SqlCommand command = new SqlCommand(comI, con);
            try
            {
                Console.WriteLine("запрос "  + comI);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка   " + ex);
            }
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
