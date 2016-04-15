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

    public class Methods
    {
     public   List<DateTime> Convert(List<double> A, List<DateTime> DINET)
        {
            for (int i = 0; i < A.Count;i++ )
            {
                DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(A[i]);
                DINET.Add(Date); 
            }
                return DINET;
        }

        public List<double> ZoomNumver(int ZOOM, List<double> Buy, List<double> Time, double nowTime)
     {
            List<double> Number = new List<double>();;
            int colvo = 0;
            while (Time[Time.Count - 1 - colvo]>nowTime - ZOOM)
            {
                colvo++;
            }
            while (colvo > 0)
            {
                Number.Add(Time[Time.Count - 1 - colvo]);
                colvo --;
            }
            return Number;
     } // локализация уровней сопротивления и поддержки под временные уровни
    }
}
