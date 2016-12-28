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
    /// класс отвечающий за диапазон графика
    /// </summary>
    class GraphY
    {
        /// <summary>
        /// Метод отвечающий за диапазон графика
        /// </summary>
        /// <param name="char1">График</param>
        /// <param name="Nowcurency">текущее значение котировки</param>
        public void scopeY(Chart chart1, double Nowcurency)
        {
            chart1.ChartAreas[0].AxisY.Maximum = Nowcurency + Math.Round(Nowcurency * 0.002, 3);
            chart1.ChartAreas[0].AxisY.Minimum = Nowcurency - Math.Round(Nowcurency * 0.002, 3);
        }       
    }
}
