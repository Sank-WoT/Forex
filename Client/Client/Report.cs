
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

    public partial class Report : Form
    {
        public Report()
        {
            int Size;
            String Prognoz  = "1";
            InitializeComponent();
            Size = ReportTransit.data.Count();
            DateTime DateReport = new DateTime();
            for(int i = 0; i < Size; i ++)
            {
                if(ReportTransit.data[i][4] == 0)
                {
                    Prognoz = "Повышение";  
                }
                if (ReportTransit.data[i][4] == 1)
                {
                    Prognoz = "Понижение";
                }    
                DateReport = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(ReportTransit.data[i][3]);
                
                dataGridView1.Rows.Add(ReportTransit.data[i][0], ReportTransit.data[i][1], ReportTransit.data[i][2],DateReport,Prognoz);
            }          
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {          
        }
    }
}
