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
      
    public partial class Form1 : Form
    {
        string lifeTimeInfo = "";   // Вспомогательное поле для закрытия
        public static bool HelpClosing = true;
        public static bool SWindowClosing = true;
        public static bool WindowClosing = true;
        public static bool SChartClosing = true;
        public Form1()
        {

            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(OnClosing);

            int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; //высота экрана
            int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; //ширина экрана  
            double xS =(x / 1920.0);//настройка под все  экраны
            double yS =(y / 1080.0);//настройка под все  экраны
            this.Size = new Size(x, y);// задание размеров экрана
            windowToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W;// командная комбинация клавиш для откытия настроек окна
            chartToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;// командная комбинация клавиш для откытия настроек графика
            eURUSDToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.E;// командная комбинация клавиш для откытия настроек графика
            helpToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.H;// командная комбинация клавиш для откытия помощи
            if (HelpClosing == true)
            {
                helpToolStripMenuItem.CheckState = CheckState.Unchecked;
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void создателиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("                             Creators \n\nSerobabov Alexandr - executive Director \nBondarev Alexandr -  executive Director\nTamarenko Andrey - specialist in web technologies\n\n ©Product Project Mordor");// сообщение о создании директории
        }

        public void eURUSDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String readX, readY;
            int X = 0, Y = 0;
            if (WindowClosing == true)
            {
                SWindowClosing = false;
            string pathFile = "C:\\Users\\саша\\Documents\\GitHub\\Forex\\SettingWindow.txt";//Очень важно указать путь
            if (!File.Exists(pathFile))//проверка на существование файла
            {
                FileInfo writel = new FileInfo(pathFile);//получаем путь  для записи и создания
                StreamWriter l = writel.CreateText();//создаем текст
                string text = "EURUSD   " +1366 + "      " +757;
                l.WriteLine(text);
                l.Close();//закрыть запись
            } //Создаем файл настроек окон первые две записи // необходимо придумать тег
            FileInfo write = new FileInfo(pathFile);//получаем путь 
            StreamReader r1 = new StreamReader(pathFile);
            string text1 = r1.ReadToEnd();// получение прочтенной записи
            r1.Close();//закрыть чтение 
            Regex regex = new Regex(@"(EURUSD\s\s\s\d{1,20}\s\s\s\s\s\s\d{1,20})");//регулярное выражение для поиска размеров окна
            Regex regex1 = new Regex(@"(\d{1,20})");//регулярное выражение для поиска размеров окна
            MatchCollection m = regex.Matches(text1);
            string response = m[0].ToString();
            MatchCollection m1 = regex1.Matches(response);
            readX = m1[0].Value;
            readY = m1[1].Value;           
            X = Convert.ToInt32(readX);
            Y = Convert.ToInt32(readY);
            WSrting.X = X;
            WSrting.Y = Y;
            Console.WriteLine(WSrting.X); 
            Window f1 = new Window();
            f1.Show();//модольное окно 
            f1.Size = new Size(X, Y);//Задаем значение размера формы Window 
            f1.Location = new Point(0, 0);//размещение окна EURUSD
            }
        }// показать  график  EUR/USD

        public void windowToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (SWindowClosing == true)
            {
                SWindow f2 = new SWindow();
                SWindowClosing = false;
                f2.Show();
            }
           
        }// показать форму настройки окна

        public void chartToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (SChartClosing == true)
            { 
                SChartcs f3 = new SChartcs();
                SChartClosing = false;
                f3.Show();

            }

        }// показать форму настройки графика

        public void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HelpClosing == true)
            {
                Help f4 = new Help();
                HelpClosing = false;
                f4.Show();
            }
        }


        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void currencyPairsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы уже уходите?", "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                Activ.Form1 = false;
            }
               
            else
            {
                e.Cancel = false;// Продолжить закрытие
                this.Hide();// Скрыть форму до закрытия
            }
        }// разобраться сделать на английском
    }
}

