namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
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
    class Resistance
    {
        public double MAXY = 0;
        public double MAXX = 0;
        /// <summary>
        /// Формирование линий поддержки
        /// </summary>
        /// <param name="poin">Точки минимумов и максимов</param>
        /// <param name="tic">Прошедшее время с начала запуска</param>
        /// <param name="pogr">Некоторая погрешность в пределах которой формируется линия</param>
        /// <param name="h">???</param>
        /// <param name="MINY">???</param>
        /// <param name="MINX">???</param>
        /// <param name="MinH">???</param>
        public  Resistance(List<List<double>> poin, int tic, double pogr, List<DateTime> Date, int h, double MAXY, double MAXX, double MaxH, List<double> massYFileBuy, Chart graphic) ////Метод уровня сопротивления
        {
            int line2X;
            double line2Y;
            if (MAXY < poin[h][1]) 
            {
                MAXY = poin[h][1];
                MAXX = poin[h][0];
                MaxH = h;
            }

            for (int g = 0; g < poin.Count; g++)
            {
                if (((MAXY - poin[g][1]) <= pogr) && g != MaxH)
                {
                    graphic.Series[1].Points.Clear(); 
                    // Очистка линии
                    line2Y = poin[g][1];
                    // 2 точка по игрик
                    line2X = Convert.ToInt32(poin[g][0]); 
                    // 2 точка по икс

                    graphic.Series[1].XValueType = ChartValueType.Time; // Указывание типа координат по X
                    graphic.Series[1].Points.AddXY(Date[0].ToOADate(), line2Y); // 1 точка
                    graphic.Series[1].Points.AddXY(Date[tic].ToOADate(), line2Y); // 2 точка
                    graphic.Series[1].Color = Color.FromArgb(255, 0, 0); // задание цвета 
                }
            }

            if (MAXY < (massYFileBuy[tic] - pogr))
            {
                graphic.Series[1].Points.Clear();
            } 
            // если за рамками погрешности
        }
    }
}
