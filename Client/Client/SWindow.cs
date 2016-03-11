﻿using System;
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


namespace Client
{
    public partial class SWindow : Form
    {
        public SWindow()
        {
            InitializeComponent();
            textBox1.Text = "700"; // значение отображаемые в текст боксе X
            textBox2.Text = "500"; // значение отображаемые в текст боксе Y
        }

        private void SWindow_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; //высота экрана
           int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; //ширина экрана  
           string X = textBox1.Text;// задание размера окна по x
           string Y = textBox2.Text;// задание размера окна по y
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
           string pathFile = "C:\\Users\\саша\\Documents\\GitHub\\Forex\\SettingWindow.txt";
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
}