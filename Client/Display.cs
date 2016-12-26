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

    class Display
    {
        /// <summary>
        /// Преобразование окон под различные экраны компьютера
        /// </summary>
        /// <param name="xc">координат по x</param>
        /// <param name="yc">координаты по y</param>
        public static Point customizedPoint(int xc, int yc)
        {
            int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; // высота экрана
            int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; // ширина экрана 
            // размер формы по X
            double fX = 1366;
            // размер формы по Y
            double fY = 757;
            // настройка под все  экраны
            double xS = x / 1920.0;
            // настройка под все  экраны
            double yS = y / 1080.0; 
            Point n = new Point(Convert.ToInt32(xc * xS * (WString.X / fX)), Convert.ToInt32(yc * yS * (WString.Y / fY)));
            return n;
        }
    }
}
