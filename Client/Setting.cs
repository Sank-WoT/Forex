﻿namespace Client
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
    /// <summary>
    /// Класс для задания размеров окнаы
    /// </summary>
    public partial class Setting : Form
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public Setting()
        {
            this.InitializeComponent();
            numericUpDown3.Value = 5;
        }

    /// <summary>
    /// Метод работающего окна 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
        private void EURUSD_Load(object sender, EventArgs e)
        {
            if (WString.Langue["ENG"] == true)
            {
                label1.Text = "Size window Report EURUSD";
                button1.Text = "Ask";
            }
            if (WString.Langue["RUS"] == true)
            {
                label1.Text = "Размер окна отчет  Евро/Доллар";
                button1.Text = "Задать";
            }
            SpeedDraw.Speed = (int)numericUpDown3.Value;         
        }

        private void Label1_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Сохранение параметров окна ReportEURUSD в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; // высота экрана
            int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; // ширина экрана  
            string X = numericUpDown1.Text; // задание размера окна по x
            string Y = numericUpDown2.Text; // задание размера окна по y
            WString.X = Convert.ToInt32(X); // присвоение введенных данных в текст бокс
            WString.Y = Convert.ToInt32(Y); // присвоение введенных данных в текст бокс
            #region Ограничения размера окна по X и Y

            if (WString.X < 700)
            {
                WString.X = 700; // ограничение по минимальному размеру окна
            }

            if (WString.Y < 500)
            {
                WString.Y = 500; // ограничение по минимальному размеру окна
            }

            if (WString.X > x)
            {
                WString.X = x; // ограничение по максимальному размеру окна
            }

            if (WString.Y > y)
            {
                WString.Y = y; // ограничение по максимальному размеру окна
            }
            #endregion
            string text = "EURUSD   " + WString.X + "      " + WString.Y;
            string pathFile = Application.StartupPath + "\\Setting\\WindowReportEURUSD.txt";
            //// проверка на существование файла
            if (!File.Exists(pathFile)) 
            {
                FileInfo writel = new FileInfo(pathFile); // получаем путь  для записи и создания
                StreamWriter l = writel.CreateText(); // создаем текст
                l.Close(); // закрыть запись
            } ////Создаем файл настроек окон первые две записи // необходимо придумать тег
            FileInfo write = new FileInfo(pathFile); // получаем путь 
            StreamWriter w = write.CreateText(); // создаем текст  
            w.WriteLine(text); // добавляем текст 
            w.Close(); // закрыть запись 
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
