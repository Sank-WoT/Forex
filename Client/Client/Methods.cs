namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
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

    /// <summary>
    /// Class Methods
    /// </summary>
    public class Methods
    {
        /// <summary>
        /// Convert date
        /// </summary>
        /// <param name="a">List seconds</param>
        /// <param name="dinet">List date</param>
        /// <returns>new dinet </returns>
     public List<DateTime> Convert(List<double> a, List<DateTime> dinet)
        {
            for (int i = 0; i < a.Count; i++)
            {
                DateTime date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(a[i]);
                dinet.Add(date); 
            }

                return dinet;
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

        public double TradeBuy(bool buy, List<double> bufferS, int tic)
        {
            double valueT;
            string text;
            SoundPlayer player = new SoundPlayer();
            try
            {
                player.SoundLocation = Application.StartupPath + "/Music/test.wav"; // путь адреса музыки
                player.Play(); // Проигрывание звука
            }
            catch
            {
            }

            if (buy == false)
            {
                valueT = bufferS[tic - 1]; // Запомнить значение продажи
                text = "покупка";
            }
            else
            {
                valueT = bufferS[tic - 1]; // Запомнить значение продажи
                text = "продажа";
            }

            if (WString.RUS == true)
            {
            MessageBox.Show("Cовершена " + text + " по цене =" + valueT);
            }

            if (WString.ENG == true)
            {
                if(text == "продажа")
                {
                 MessageBox.Show("committed " + "selling" + " for the price =" + valueT);
                }
                if (text == "покупка")
                {
                    MessageBox.Show("done " + "buing" + " for the price =" + valueT);
                }
            }
            return valueT;          
        }

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
            //// Regex regex2 = new Regex(@"[0-2]+:[0-9]+:[0-9]+"); // регулярное выражение для поиска последнего времени в файле
            //// MatchCollection m2 = regex2.Matches(Time);
            //// string Time1 = m2[0].Value;
            return dayOffTheWeek;
        }
    }
}
