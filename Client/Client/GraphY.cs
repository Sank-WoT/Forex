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

    class GraphY
    {
        public void Y(Chart chart1, double Y)
        {
            chart1.ChartAreas[0].AxisY.Maximum = Y + 0.01;
            chart1.ChartAreas[0].AxisY.Minimum = Y - 0.01;
        }       
    }
}
