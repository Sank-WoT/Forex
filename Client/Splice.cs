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
    /// Класс соединяющий буферные значения с файловыми
    /// </summary>
   public class Splice
    {
        /// <summary>
        /// Метод склейки буферных данных и файловых +
        /// </summary>
        /// <param name="Buffer">double значения 1 листа.</param>
        /// <param name="File">double значения 2 листа.</param>
        /// <param name="tic">int время которое прошло с запуска формы.</param>
        /// <param name="ChisloZagruz">число загрузок из файла.</param>
        /// <returns>Объединенный лист</returns>
        public List<double> glue( List<double> File, List<double> Buffer, int tic, int ChisloZagruz)
       {
           List<double> Value = new List<double>();
            for (int h = 0 ; ChisloZagruz > h; h++)
           {
               Value.Add(File[File.Count  - ChisloZagruz + h]);
           }

           for (int h = 0; tic > h; h++)
           {
               Value.Add(Buffer[h]);
           }

           return Value;
       }

        /// <summary>
        /// Метод склейки и последнего числа буфера и файла  +
        /// </summary>
        /// <param name="Buffer">double значения 1 листа.</param>
        /// <param name="File">double значения 2 листа.</param>
        /// <returns>Объединенный лист</returns>
        public List<double> glue(List<double> Buffer, List<double> File)
        {
            List<double> Value = new List<double>();
            Value = File;
            Value.Add(Buffer[Buffer.Count - 1]);
            return Value;
        }

        /// <summary>
        ///  Метод склейки буферного времени и файлового  +
        /// </summary>
        /// <param name="Ftime">DateTime значения 1 листа  файловых.</param>
        /// <param name="ITime">DateTime значения 2 листа буферных.</param>
        /// <param name="tic">int время которое прошло с запуска формы.</param>
        /// <param name="ChisloZagruz">число загрузок из файла.</param>
        /// <returns>Объединенный лист</returns>
        public List<DateTime> glue(List<DateTime> Ftime, List<DateTime> ITime, int tic, int ChisloZagruz)
       {
           List<DateTime> Value = new List<DateTime>();
            int atic;
            for (int h = 0; ChisloZagruz > h; h++)
           {
               Value.Add(Ftime[Ftime.Count  - ChisloZagruz + h]);
           }

           for (int h = 0; tic >= h; h++)
           {
               Value.Add(ITime[h]);
           }

           return Value;
       }

        /// <summary>
        /// Метод склейки и последнего числа буфера и файла  +
        /// </summary>
        /// <param name="Buffer">double значения 1 листа.</param>
        /// <param name="File">double значения 2 листа.</param>
        /// <returns>Объединенный лист</returns>
        public List<DateTime> glue(List<DateTime> Ftime, List<DateTime> ITime)
        {
            List<DateTime> Value = new List<DateTime>();
            Value = Ftime;
            Value.Add(ITime[ITime.Count - 1]);
            return Value;
        }

    }
}
