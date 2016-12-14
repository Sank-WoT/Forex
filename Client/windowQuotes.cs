namespace Client
{
    using System;
    using System.Drawing;
    using System.IO; // для класса 
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Globalization;
    class windowQuotes
    {
        /// <summary>
        /// Cоздание размеров окна
        /// </summary>
        /// <param name="x">размер по кординате x</param>
        /// <param name="y">размер по кординате y</param>
        /// <param name="WindowClosing">Закрытие окна</param>
        public void get(ref int x, ref int y, ref bool WindowClosing)
        {
            // Задание размеров окна
            WindowClosing = false;
            // путь
            string pathFile = Application.StartupPath + "\\SettingWindow.txt";
            // проверка на существование файла
            writeFile(pathFile);
            // получаем путь 
            FileInfo write = new FileInfo(pathFile);
            StreamReader Sr = new StreamReader(pathFile);
            // получение прочтенной записи
            string textRead = Sr.ReadToEnd();
            // закрыть чтение 
            Sr.Close();
            // регулярное выражение для поиска размеров окна
            Regex regex = new Regex(@"(EURUSD\s\s\s\d{1,20}\s\s\s\s\s\s\d{1,20})");
            // регулярное выражение для поиска размеров окна
            Regex regex1 = new Regex(@"(\d{1,20})");
            MatchCollection String = regex.Matches(textRead);
            string response = String[0].ToString();
            MatchCollection number = regex1.Matches(response);
            // координата по X
            x = Convert.ToInt32(number[0].Value);
            // координата по Y
            y = Convert.ToInt32(number[1].Value);
        }

        public void writeFile(string pathFile)
        {
            WorkFile workFile = new WorkFile();
            string text = "EURUSD   " + 1366 + "      " + 757;
            // проверка и создания файла
            workFile.CreateFile(pathFile, text);
        }

    }
}
