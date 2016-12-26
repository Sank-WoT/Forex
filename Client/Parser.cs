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
    public class Parser
    {
        private string response;
        public string response_property
        {
            get { return response; }
        }
        /// <summary>
        /// Конструктор получение текста для парсинга +
        /// </summary>
        /// <param name="text">текст для парсинга</param>
        public Parser(string text)
        {
            this.response = text;
        }
        /// <summary>
        /// Метод принимающий листы для заполнения данными +
        /// </summary>
        /// <param name="ListTBuf">Время</param>
        /// <param name="BListBBuf">Лист принимающий значение покупки</param>
        /// <param name="BListSBuf">Лист принимающий значение продажи</param>
        public void BDREqest( ref List<int> ListTBuf, ref List<double> BListBBuf, ref List<double> BListSBuf)
        {
            int colvo = 0;
            //регулярное выражение 
            Regex regex = new Regex(@"(\d{10,20})");
            MatchCollection m = regex.Matches(response);
            //регулярное выражение
            Regex regex1 = new Regex(@"((\d{0,5})\.(\d{1,4}))"); 
            MatchCollection m1 = regex1.Matches(response);
            while (colvo < m.Count)
            {
                //Время в UNIX
                ListTBuf.Add(Convert.ToInt32(m[colvo].Value));
                //порядковый номер даты в списке
                colvo++;  
            }
            colvo = 0;
            while (colvo + 1 < m1.Count)
            {
                CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                //число продажа
                BListBBuf.Add(double.Parse(m1[colvo].ToString()));
                //порядковый номер 
                colvo++;
                //значение покупка
                BListSBuf.Add(double.Parse(m1[colvo].ToString()));
                //порядковый номер
                colvo++; 
                Thread.CurrentThread.CurrentCulture = temp_culture;
            }
        }
         
    }
}
