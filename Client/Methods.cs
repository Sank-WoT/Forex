namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using System.IO; //для класса 
    using System.Text.RegularExpressions;
    using System.Net;
    using System.Globalization;
    using System.Threading;
    using System.Net.Sockets;
    using EnumDialogResult = System.Windows.Forms.DialogResult;
    using System.Media;

    /// <summary>
    /// Class Methods
    /// </summary>
    public class Methods
    {
        /// <summary>
        /// Convert UnixTime in DateTime
        /// </summary>
        /// <param name="a">List seconds</param>
        /// <param name="dinet">List date</param>
        /// <returns>new dinet </returns>
        public List<DateTime> ConvertD(List<int> a)
       {
            List<DateTime> dinet = new List<DateTime>();
            for (int i = 0; i < a.Count; i++)
            {
                //   конвертирование в Датетайм
                DateTime date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(a[i]);
                dinet.Add(date);
            }
            // Проверка конвертора на время получаемое в date1
            DateTime date1 = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(a[a.Count - 1]);
            Console.WriteLine("Это date1 = " + date1);
            return dinet;
        }

        /// <summary>
        /// Convert DateTime in UnixTime
        /// </summary>
        /// <param name="a">List Date</param>
        /// <param name="dinet">List seconds</param>
        /// <returns>new dinet </returns>
        public List<int> ConvertU(List<DateTime> a)
        {
            List<int> dinet = new List<int>();
            for (int i = 0; i < a.Count; i++)
            {
                // конвертирование в UNIXTIME
                int unixTime = (int)(a[i] - new DateTime(1970, 1, 1,0, 0, 0, 0)).TotalSeconds;
                dinet.Add(unixTime);
            }

            return dinet;
        }

        /// <summary>
        /// Проверка соединения с интернетом и уведомление пользователя
        /// </summary>
        /// <param name="value">List seconds</param>
        /// <param name="inet">List date</param>
        /// <param name="internetActionFinished">List date</param>
        /// <param name="sync">List date</param>
        public bool TryCon(bool inet, string value, bool internetActionFinished, object sync)
        {
            try
            {
                var webReq = WebRequest.Create("http://currency-dred95.rhcloud.com/get_currency.php?time=" + "0" + "&limit=" + "1" + "&sign=" + value); // запрос на сайт 
                WebResponse webRes = webReq.GetResponse(); // получение ответа
                webRes.Close();
                inet = true;
            }
            catch (Exception ex)
            {
                if (WString.Langue["RUS"] == true)
                {
                    MessageBox.Show("Отсутсвие интернета или недоступен сайт переход в автономный режим");
                }
                if (WString.Langue["ENG"] == true)
                {
                    MessageBox.Show("Lack of or inaccessible internet site go offline");
                }
                inet = false;
            }
            lock (sync)
            {
                internetActionFinished = true;
            }
            return inet;
        }

        /// <summary>
        /// different Zoom
        /// </summary>
        /// <param name="zoom">Needed increase</param>
        /// <param name="time">begin time</param>
        /// <param name="nowTime">now time</param>
        /// <returns>Zoom</returns>
        public List<double> ZoomNumver(int zoom, List<double> time, double nowTime)
     {
            List<double> number = new List<double>();
            int colvo = 0;
            while (time[time.Count - 1 - colvo] > nowTime - zoom)
            {
                colvo++;
            }

            while (colvo > 0)
            {
                number.Add(time[time.Count - 1 - colvo]);
                colvo--;
            }

            return number;
     } // локализация уровней сопротивления и поддержки под временные уровни

       

        /// <summary>
        /// Method the selection day of the week
        /// </summary>
        /// <param name="nowDate">now Time</param>
        /// <returns>Day of the week</returns>
        public string TradeStop(DateTime nowDate)
        {
            string time;
            time = nowDate.ToString("R");
            Console.WriteLine(time);
            Regex regex1 = new Regex(@"[a-zA-Z]+"); // регулярное выражение для поиска последнего времени в файле
            MatchCollection m1 = regex1.Matches(time);
            string dayOffTheWeek = m1[0].Value;
            return dayOffTheWeek;
        }
    }
}
