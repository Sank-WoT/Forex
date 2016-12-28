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

namespace Client
{
    /// <summary>
    /// Класс для изменения чарта и задание линий
    /// </summary>
    class ChartQ
    {
        public ChartQ()
        {
        }

        /// <summary>
        /// Метод задания параметров линии
        /// </summary>
        /// <param name="ChartArea">Название чарта к которому привязана линия</param>
        /// <param name="XValueType">Тип чарта</param>
        /// <param name="Line">тип линии</param>
        /// <param name="BorderWidth">толщина линии</param>
        public Series extSeries(string ChartArea, ChartValueType XValueType, SeriesChartType Line, int BorderWidth)
        {
            Series series = new Series();
            series.ChartArea = ChartArea;
            series.XValueType = XValueType;
            series.ChartType = Line;
            series.BorderWidth = BorderWidth;
            return series;
        }

        /// <summary>
        /// Метод задания параметров линии
        /// </summary>
        public Chart Quote(Chart graphic, string value)
        {
            Series series1;
            series1 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            // параметры главной линии
            graphic.Series.Add(series1);

            Series series2;
            series2 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series2); // параметры  линии поддержки

            Series series3;
            series3 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series3); // параметры  линии поддержки

            Series series4;
            series4 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series4); // параметры  линии поддержки// параметры  линии SMA

            Series series5;
            series5 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series5); // параметры  линии 

            Series series6;
            series6 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series6);  // параметры  линии МAX

            Series series7;
            series7 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series7);  // параметры  линии МIN

            graphic.ChartAreas[0].BackColor = Color.FromArgb(255, 255, 255); // цвет внутренней области
            graphic.BackColor = Color.FromArgb(255, 255, 255); // цвет внешней области

            graphic.ChartAreas[0].BackColor = Color.FromArgb(255, 255, 255); // цвет внутренней области
            graphic.BackColor = Color.FromArgb(255, 255, 255); // цвет внешней области

            // Установка отображения даты
            graphic.ChartAreas[0].AxisX.LabelStyle.Format = "dd//hh:mm:ss tt";
            graphic.ChartAreas[0].AxisX.IsStartedFromZero = true;

            graphic.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Seconds;
            graphic.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            graphic.ChartAreas[0].AxisX.Interval = 0;
            graphic.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            graphic.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            graphic.ChartAreas[0].CursorX.IsUserEnabled = true;
            graphic.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            graphic.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Seconds;

            // цвет нижней линии 
            graphic.ChartAreas[0].AxisX.LineColor = Color.FromArgb(0, 0, 0);
            // цвет  линии 
            graphic.ChartAreas[0].AxisX2.LineColor = Color.FromArgb(0, 0, 0);
            // цвет надписей координат по X
            graphic.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.FromArgb(0, 0, 0);
            graphic.ChartAreas[0].AxisX.Title = "Date, Time";
            graphic.ChartAreas[0].AxisY.Title = "Attitude " + value;
            // цвет боковой  линии по Y
            graphic.ChartAreas[0].AxisY.LineColor = Color.FromArgb(0, 0, 0);
            // цвет боковой  линии по Y
            graphic.ChartAreas[0].AxisY2.LineColor = Color.FromArgb(0, 0, 0);
            // цвет надписей координат по Y
            graphic.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.FromArgb(0, 0, 0);
            graphic.Series[0].XValueType = ChartValueType.Time;
            return graphic;
        }
    }
}
