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
   public class Draw
    {
       /// <summary>
       /// Метод склейки буферных данных и файловых 
       /// </summary>
       /// <param name="Buffer">double значения 1 листа.</param>
        /// <param name="File">double значения 2 листа.</param>
        /// <param name="tic">int время которое прошло с запуска формы.</param>
       /// <returns>Объединенный лист</returns>
       public List<double> MainValue(List<double> Buffer, List<double> File, int tic, int ChisloZagruz)
       {
           List<double> Value = new List<double>();
            int atic;
            atic = tic;
            tic *= ChisloZagruz;
            for (int h = 0 ; tic >= h; h++)
           {
               Value.Add(File[File.Count - 1 - tic + h]);
           }

           for (int h = 0; atic >= h; h++)
           {
               Value.Add(Buffer[h]);
           }

           return Value;
       }

        public List<double> MainValue(List<double> Buffer, List<double> File)
        {
            List<double> Value = new List<double>();
            Value = File;
            Value.Add(Buffer[Buffer.Count - 1]);
            return Value;
        }

       /// <summary>
       ///  Метод склейки буферных данных и файловых 
       /// </summary>
       /// <param name="Ftime">DateTime значения 1 листа.</param>
       /// <param name="ITime">DateTime значения 2 листа.</param>
       /// <param name="tic">int время которое прошло с запуска формы.</param>
       /// <returns>Объединенный лист</returns>
        public List<DateTime> MainTime(List<DateTime> Ftime, List<DateTime> ITime, int tic, int ChisloZagruz)
       {
           List<DateTime> Value = new List<DateTime>();
            int atic;
            atic = tic;
            tic *= ChisloZagruz;
            for (int h = 0; tic >= h; h++)
           {
               Value.Add(Ftime[Ftime.Count - 1 - tic + h]);
           }

           for (int h = 0; atic >= h; h++)
           {
               Value.Add(ITime[h]);
           }

           return Value;
       }
        public List<DateTime> MainTime(List<DateTime> Ftime, List<DateTime> ITime)
        {
            List<DateTime> Value = new List<DateTime>();
            Value = Ftime;
            Value.Add(ITime[ITime.Count - 1]);
            return Value;
        }

    }
} // Додумать по поводу выхода индекса за пределы листа
