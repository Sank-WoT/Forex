
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
    using Microsoft.Office.Interop.Excel;
    using System.Runtime.InteropServices;
    using Excel = Microsoft.Office.Interop.Excel;

    /// <summary>
    /// Класс отвечающий за формирование отчета
    /// </summary>
    public partial class Report : Form
    {
        Exel EReport = new Exel();
        private Excel.Application ObjExcel;
        private Excel.Workbook ObjWorkBook;
        private Excel.Worksheet ObjWorkSheet;
        private string fileName;
        /// <summary>
        /// Конструктор класс отчет
        /// </summary>
        public Report()
        {
            string Prognoz  = "";
            InitializeComponent();
            int Size = ReportTransit.data.Count();
            DateTime DateReport = new DateTime();
            for(int i = 0; i < Size; i ++)
            {
                if(ReportTransit.data[i][4] == 0)
                {
                    Prognoz = "Понижение";  
                }
                if (ReportTransit.data[i][4] == 1)
                {
                    Prognoz = "Повышение";
                }    
                DateReport = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(ReportTransit.data[i][3]);
                
                dataGridView1.Rows.Add(ReportTransit.data[i][0], ReportTransit.data[i][1], ReportTransit.data[i][2],DateReport, Prognoz);
            }          
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {          
        }

        private void DownloadHistoricalMarketDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Путь Exporta файла
            fileName = System.Windows.Forms.Application.StartupPath + "\\" + "FirstExel" + ".xls";  
            // Использование метода загрузки данных        
            EReport.ESave(dataGridView1);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Использование метода сохранения данных
            EReport.ELoad(dataGridView1);
        }

        }
    }
