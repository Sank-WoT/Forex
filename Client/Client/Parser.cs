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
    class Parser
    {
        private string response;
        /// <summary>
        /// Конструктор получение текста для парсинга
        /// </summary>
        /// <param name="text">текст для парсинга</param>
        public Parser(string text)
        {
            this.response = text;
        }
        /// <summary>
        /// Конструктор получение текста для парсинга
        /// </summary>
        /// <param name="text">текст для парсинга</param>
        public void BDREqest( ref List<int> BListTBuf, ref List<double> BListBBuf, ref List<double> BListSBuf)
        {
            int colvo = 0;
            Regex regex = new Regex(@"(\d{10,20})");//регулярное выражение 
            MatchCollection m = regex.Matches(response);
            Regex regex1 = new Regex(@"((\d{0,5})\.(\d{1,4}))");//регулярное выражение 
            MatchCollection m1 = regex1.Matches(response);
            while (colvo < m.Count)
            {
                BListTBuf.Add(Convert.ToInt32(m[colvo].Value));//Время в UNIX
                colvo++;//порядковый номер даты в списке  
            }
            colvo = 0;
            while (colvo + 1 < m1.Count)
            {
                CultureInfo temp_culture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                BListBBuf.Add(double.Parse(m1[colvo].ToString()));//число покупка
                colvo++;//порядковый номер  
                BListSBuf.Add(double.Parse(m1[colvo].ToString())); //значение  продажа
                colvo++;//порядковый номер 
                Thread.CurrentThread.CurrentCulture = temp_culture;
            }
        }
         
    }
}
