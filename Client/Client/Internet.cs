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
    using System.Media;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using EnumDialogResult = System.Windows.Forms.DialogResult;
    public class Internet
    {
        public bool TryCon(bool inet, string value, object sync, bool internetActionFinished)
        {
            try
            {
                var webReq = WebRequest.Create("http://currency-dred95.rhcloud.com/get_currency.php?time=" + "0" + "&limit=" + "1" + "&sign=" + value); // запрос на сайт 
                WebResponse webRes = webReq.GetResponse(); // получение ответа
                webRes.Close();
                inet = true;
            }
            catch (Exception ex)
            {
                if (WString.RUS == true)
                {
                    MessageBox.Show("Отсутсвие интернета или недоступен сайт переход в автономный режим");
                }
                if (WString.ENG == true)
                {
                    MessageBox.Show("Lack of or inaccessible internet site go offline");
                }
                inet = false;
            } // Временная мера по отсутвию интернета
            lock (sync)
            {
                internetActionFinished = true;
            }
            return inet;
        }
        public void FirstConnect(string value,string pathFile)
        {
        WorkFile a = new WorkFile();
        double lastUpdate = 0;
        lastUpdate = a.GetPoslTime(lastUpdate,pathFile); // выбрать из файла последнее время                    
        ConnectI(lastUpdate, 1000000, pathFile, value);
        }
        

        public void ConnectI(double poslTime, int limit, string pathFile, string value)
        {
            #region Получение данных из файла запись их  в text1              
            StreamReader r1 = new StreamReader(pathFile);
            string text1 = r1.ReadToEnd(); // получение прочтенной записи
            r1.Close(); // закрыть чтение 
            #endregion

            if (InetConnect.Inet == true)
            {
                #region Добавление данных в файл из буфера 
                StreamReader sr;
                sr = Conection(limit, poslTime, value); // Получение данных при коннекте
                string response = sr.ReadToEnd(); // присвоение прочтенного к стринг 
                text1 += response; // добавление в  файл новых значений
                FileInfo write = new FileInfo(pathFile); // получаем путь 
                StreamWriter w = write.CreateText(); // создаем текст  
                w.WriteLine(text1); // добавляем текст 
                w.Close(); // закрыть запись 
                #endregion
            }
        }
        public StreamReader Conection(int limit, double poslT, string value)
    {
        var webReq = WebRequest.Create("http://currency-dred95.rhcloud.com/get_currency.php?time=" + poslT + "&limit=" + limit + "&sign=" + value); // запрос на сайт 
        WebResponse webRes = webReq.GetResponse(); // получение ответа
        Stream st = webRes.GetResponseStream(); // поток по которому получаем инфу
        StreamReader sr = new StreamReader(st); // прочитать поток
        Console.WriteLine("http://currency-dred95.rhcloud.com/get_currency.php?time=" + poslT + "&limit=" + limit + "&sign=" + value);
        return sr;
    }
    }

   

}
