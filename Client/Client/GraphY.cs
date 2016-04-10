using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO; //для класса 
using System.Text.RegularExpressions;
using System.Net;
using System.Globalization;
using System.Threading;
using System.Net.Sockets;
using EnumDialogResult = System.Windows.Forms.DialogResult;
using System.Media;


namespace Client
{
    class GraphY
    {
        public void Y(Chart chart1, double Y)
        {
            chart1.ChartAreas[0].AxisY.Maximum = Y + 0.01;
            chart1.ChartAreas[0].AxisY.Minimum = Y - 0.01;
        }       
    }
}
