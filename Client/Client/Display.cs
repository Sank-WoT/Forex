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
        /// <param name="pathDirectory">путь к директории</param>
        public static Point customizedPoint(int xc, int yc)
        {
            int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; // высота экрана
            int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; // ширина экрана 
            double fX = 1366; // форма
            double fY = 757; // форма
            double xS = x / 1920.0; // настройка под все  экраны
            double yS = y / 1080.0; // настройка под все  экраны
            Point n = new Point(Convert.ToInt32(xc * xS * (WString.X / fX)), Convert.ToInt32(yc * yS * (WString.Y / fY)));
            return n;
        }
    }
}
