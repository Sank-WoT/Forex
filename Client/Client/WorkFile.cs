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
    public class WorkFile
    {

        public bool CreateFile(string pathFile)
        {
            bool a = true;
            //// проверка на существование файла
            if (!File.Exists(pathFile))
            {
                FileInfo writel = new FileInfo(pathFile); // получаем путь 
                StreamWriter l = writel.CreateText(); // создаем текст
                l.Close(); // закрыть запись
                a = false;
            } // развертывание файла в дебаге            
            return a;
        }

        public double GetPoslTime(double poslTime, string pathFile)
        {
            // преобразовение типа доубле к американскому стандарту
            #region Получение данных по котировкам из файла "eurusd.txt"  запись их в переменную text
            WorkFile RFilePair = new WorkFile(); // Создание объекта работы с файлами 
            string text = RFilePair.ReadFile(pathFile); // прочтение файла
            #endregion

            Regex regex1 = new Regex(@"(\d{10,20})"); // регулярное выражение для поиска последнего времени в файле
            MatchCollection m1 = regex1.Matches(text);

            if (m1.Count != 0)
            {
                poslTime = Convert.ToDouble(m1[m1.Count - 1].Value);
            } // недопускает присвоение при значении прочтенного в файле меньше 1.  

            return poslTime;
        } //// Получение данных 

        public int  read(string text1, List<double> massYInetA, List<double> massYInetB, List<double> Times)
        {
            int colvo = 0;
            Regex regex = new Regex(@"(\d{10,20})");//регулярное выражение 
            MatchCollection m = regex.Matches(text1);
            Regex regex1 = new Regex(@"((\d{0,5})\.(\d{1,4}))");//регулярное выражение 
            MatchCollection m1 = regex1.Matches(text1);
            while (colvo < m.Count)
            {                
                Times.Add(Convert.ToDouble(m[colvo].Value));//Время в UNIX
                colvo++;//порядковый номер даты в списке  
            }
            colvo = 0;
            while (colvo + 1 < m1.Count)
            {
                massYInetA.Add(Convert.ToDouble(m1[colvo].Value));//число покупка
                colvo++;//порядковый номер  
                massYInetB.Add(Convert.ToDouble(m1[colvo].Value)); //значение  продажа
                colvo++;//порядковый номер 
            }                                                            
            return colvo;
        }// функция для прочтения файла и добавление значеений в массив

        public string ReadFile(string pathFile)
        {
            WorkFile FilePair = new WorkFile(); // Создание объекта работы с файлами
            FilePair.CreateFile(pathFile); // Создание не существующего файла
            StreamReader r = new StreamReader(pathFile);
            string text = r.ReadToEnd(); // при прочтенной записи
            r.Close(); // закрыть чтение 
            return text;
        }
    }
}
