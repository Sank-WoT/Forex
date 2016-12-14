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
        /// <summary>
        /// Метод  для создания файла
        /// </summary>
        /// <param name="pathFile">Путь к файлу </param>
        public bool CreateFile(string pathFile)
        {
            bool a = true;
            //// проверка на существование файла
            if (!File.Exists(pathFile))
            {
                // получаем путь 
                FileInfo writel = new FileInfo(pathFile);
                // создаем текст
                StreamWriter l = writel.CreateText();
                // закрыть запиcь
                l.Close();
                a = false;
                // развертывание файла в дебаге
            }        
            return a;
        }

        /// <summary>
        /// Метод  для создания файла
        /// </summary>
        /// <param name="pathFile">Путь к файлу </param>
        public void CreateFile(string pathFile, string text)
        {
            //// проверка на существование файла
            if (!File.Exists(pathFile))
            {
                // получаем путь 
                FileInfo writel = new FileInfo(pathFile);
                // создаем текст
                StreamWriter l = writel.CreateText();
                // закрыть запиcь
                l.WriteLine(text);
                l.Close();
                // развертывание файла в дебаге
            }
        }

        /// <summary>
        /// Метод  для поиска последнего времени в файле
        /// </summary>
        /// <param name="poslTime">Текущее последнее значение времени</param>
        /// <param name="pathFile">Путь к файлу </param>
        /// <returns>Возвращение последнее значение времени</returns>
        public int GetPoslTime(int poslTime, string pathFile)
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
                poslTime = Convert.ToInt32(m1[m1.Count - 1].Value);
            } // недопускает присвоение при значении прочтенного в файле меньше 1.  

            return poslTime;
        }

        /// <summary>
        /// Функция для прочтения файла
        /// </summary>
        /// <returns>Текст прочтенного из файла</returns>
        public string ReadFile(string pathFile)
        {
            WorkFile FilePair = new WorkFile(); // Создание объекта работы с файлами
            FilePair.CreateFile(pathFile); // Создание не существующего файла
            StreamReader r = new StreamReader(pathFile);
            string text = r.ReadToEnd(); // при прочтенной записи
            r.Close(); // закрыть чтение 
            return text;
        }

        /// <summary>
        /// Метод  для  парсинга данных и добавление значений в массив
        /// </summary>
        /// <param name="textDataQuote">Строка с триадами</param>
        /// <param name="massYInetBuy">Массив значений покупок</param>
        /// <param name="massYInetSell">Массив значений продажи</param>
        /// <param name="Times">Массив звремени</param>
        /// <returns>Возвращение кол-ва записей из стринга</returns>
        /// 
        public List<int> read(string textDataQuote, List<double> massYInetBuy, List<double> massYInetSell)
        {
            int colvo = 0;
            List<int> Times = new List<int>();
            Regex regex = new Regex(@"(\d{10,20})");//регулярное выражение 
            MatchCollection m = regex.Matches(textDataQuote);
            Regex regex1 = new Regex(@"((\d{0,5})\.(\d{1,4}))");//регулярное выражение 
            MatchCollection m1 = regex1.Matches(textDataQuote);
            while (colvo < m.Count)
            {
                try
                {
                    //Время в UNIXTime
                    Times.Add(Convert.ToInt32(m[colvo].Value) - 3600);
                }
                catch
                {
                    //Время в UNIXTime
                    Times.Add(Convert.ToInt32(m[colvo].Value) - 3600);
                }
                //порядковый номер даты в списке
                colvo++;
            }
            colvo = 0;
            while (colvo + 1 < m1.Count)
            {
                //значение покупка
                massYInetBuy.Add(Convert.ToDouble(m1[colvo].Value));
                //порядковый номер
                colvo++; 
                //значение  продажа
                massYInetSell.Add(Convert.ToDouble(m1[colvo].Value));
                //порядковый номер 
                colvo++;
            }
            return Times;
        }
    }
}
