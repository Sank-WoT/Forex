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

namespace Client
{
    public partial class EURUSD : Form
    {
        public EURUSD()
        {
            InitializeComponent();
        }

        private void EURUSD_Load(object sender, EventArgs e)
        {
            if (WSrting.ENG == true)
            {
                label1.Text = "Size window Report EURUSD";
                button1.Text = "Ask";
            }
            if (WSrting.RUS == true)
            {
                label1.Text = "Размер окна отчет  Евро/Доллар";
                button1.Text = "Задать";
            }            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; //высота экрана
            int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; //ширина экрана  
            string X = numericUpDown1.Text;// задание размера окна по x
            string Y = numericUpDown2.Text;// задание размера окна по y
            WSrting.X = Convert.ToInt32(X);// присвоение введенных данных в текст бокс
            WSrting.Y = Convert.ToInt32(Y);// присвоение введенных данных в текст бокс

            if (WSrting.X < 700)
            {
                WSrting.X = 700;
            }// ограничение по минимальному размеру окна
            if (WSrting.Y < 500)
            {
                WSrting.Y = 500;
            }// ограничение по минимальному размеру окна
            if (WSrting.X > x)
            {
                WSrting.X = x;
            }// ограничение по максимальному размеру окна
            if (WSrting.Y > y)
            {
                WSrting.Y = y;
            }// ограничение по максимальному размеру окна

            string text = "EURUSD   " + WSrting.X + "      " + WSrting.Y;
            string pathFile = Application.StartupPath + "\\Setting\\WindowReportEURUSD.txt";

            if (!File.Exists(pathFile))//проверка на существование файла
            {
                FileInfo writel = new FileInfo(pathFile);//получаем путь  для записи и создания
                StreamWriter l = writel.CreateText();//создаем текст
                l.Close();//закрыть запись
            } //Создаем файл настроек окон первые две записи // необходимо придумать тег

            FileInfo write = new FileInfo(pathFile);//получаем путь 
            StreamWriter w = write.CreateText();//создаем текст  
            w.WriteLine(text);//добавляем текст 
            w.Close();//закрыть запись 
        }
    }
}// доделать
