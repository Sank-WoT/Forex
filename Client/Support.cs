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
    /// <summary>
    /// класс отвечает за создание сущности линии поддержки
    /// </summary>
    class Support
    {
       public double MINY = 0;
        public double MINX = 0;
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
        public Support(List<List<double>> poin, int tic, double pogr, List<DateTime> Date, int h, double MINY, double MINX, double MinH, List<double> massYFileBuy, Chart graphic) ////Метод уровня поддержки
        {
            int line2X;
            double line2Y;

            for (int g = 0; g < poin.Count; g++)
            {
                //// g != MinH Не просматривать точку, которая уже входит в уровень поддержки
                if (((MINY - poin[g][1]) >= -pogr) && g != MinH)
                {
                    // Очистка линии
                    graphic.Series[2].Points.Clear();
                    // 2 точка по игрик
                    line2Y = poin[g][1];
                    // 2 точка по икс 
                    line2X = Convert.ToInt32(poin[g][0]);
                    // Указывание типа координат по X
                    graphic.Series[2].XValueType = ChartValueType.Time;
                    // Указывание типа координат по X
                    graphic.Series[2].Points.AddXY(Date[0].ToOADate(), line2Y);
                    // 2 точка
                    graphic.Series[2].Points.AddXY(Date[tic].ToOADate(), line2Y);
                    // задание цвета 
                    graphic.Series[2].Color = Color.FromArgb(55, 0, 55);
                }
            }
            //// сравнение точки в пределах данной погрешности
            if (MINY > (massYFileBuy[tic] + pogr))
            {
                graphic.Series[2].Points.Clear();
            }
        }
    }
}
