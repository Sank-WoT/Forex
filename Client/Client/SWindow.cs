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
  
    /// <summary>
    /// the main window class SWindow().
   /// </summary>
    public partial class SWindow : Form
    {
        /// <summary>
        /// Main 
        /// </summary>
        public SWindow()
        {
            this.InitializeComponent();    
            this.FormClosing += new FormClosingEventHandler(this.OnClosing);
            numericUpDown1.Text = "700"; // значение отображаемые в текст боксе X            
            numericUpDown2.Text = "500"; // значение отображаемые в текст боксе Y    
        }
  
        /// <summary>
        /// operating method when the form loads.
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void SWindow_Load(object sender, EventArgs e)
        {
            if (WString.ENG == true)
            {   
                // Присвоение элемеyтам текста на английском
                label1.Text = "Size window  EUR/USD";
                button1.Text = "Ask";
            }

            if (WString.RUS == true)
            {
                // Присвоение элемегтам текста на русском
                label1.Text = "Размер окна  Евро/Доллар";
                button1.Text = "Задать";
            }             
        }

        /// <summary>
        /// method listen button1_Click
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">EventArgs</param>
        private void Button1_Click(object sender, EventArgs e)
        {
           int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; // высота экрана
           int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; // ширина экрана  
           string X = numericUpDown1.Text; // задание размера окна по x
           string Y = numericUpDown2.Text; // задание размера окна по y
           WString.X = Convert.ToInt32(X); // присвоение введенных данных в текст бокс
           WString.Y = Convert.ToInt32(Y); // присвоение введенных данных в текст бокс    
           #region Ограничения экрана по X и Y
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
           string pathFile = Application.StartupPath + "\\SettingWindow.txt";
           //// Создаем файл настроек окон первые две записи 
           //// проверка на существование файла
           if (!File.Exists(pathFile)) 
           {            
               FileInfo writel = new FileInfo(pathFile); // получаем путь  для записи и создания               
               StreamWriter l = writel.CreateText(); // создаем текст               
               l.Close(); // закрыть запись
           }      

           FileInfo write = new FileInfo(pathFile); // получаем путь  
           StreamWriter w = write.CreateText(); // создаем текст             
           w.WriteLine(text); // добавляем текст 
           w.Close(); // закрыть запись
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Method Closing Form
        /// </summary>
        /// <param name="sender"> object </param>
        /// <param name="e"> FormClosingEventArgs </param>
        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.SWindowClosing = true;
        }

        private void Label1_Click(object sender, EventArgs e)
        {
        }

        private void Label3_Click(object sender, EventArgs e)
        {
        }

        private void Label2_Click(object sender, EventArgs e)
        {
        }
    }
}