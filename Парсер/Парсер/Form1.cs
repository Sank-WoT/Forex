using System;
using System.Collections.Generic;
using System.IO; //для класса 
using System.Text.RegularExpressions;
using System.Net;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Net.Sockets;
namespace Парсер
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();     
        }
        void requst(string value)
        {
            WebProxy wp = new WebProxy("51.254.201.174", 80); //задаем параметры прокси
            string pathDirectory = "C:\\Forex";//Путь к директории
                       if (!Directory.Exists(pathDirectory))//Проверка  на существование директории
                       {
                             Directory.CreateDirectory(pathDirectory);// создание директории 
                             MessageBox.Show("Директория создана путь : " + pathDirectory);// сообщение о создании директории
                       }
                       string pathFile = pathDirectory +"\\"+ value + ".txt";//Путь к файлу
                       if (!File.Exists(pathFile))//проверка на существование файла
                       {
                           FileInfo writel = new FileInfo(pathFile);//получаем путь 
                           StreamWriter l = writel.CreateText();//создаем текст
                           l.WriteLine();//добавляем текст
                           l.Close();//закрыть запись
                           MessageBox.Show(" Файл создан путь: " + pathFile);// сообщение о создании файла
                       } //Развертывание сервера в заранее известном каталоге 


                       WebRequest webReq = WebRequest.Create("http://www.teletrade.ru/analytics/quote/" + value+"/");//запрос на сайт 
            webReq.Proxy = wp;//меняем на прокси
            WebResponse webRes = webReq.GetResponse();//получение ответа
            Stream st = webRes.GetResponseStream();//поток по которому получаем инфу
            StreamReader sr = new StreamReader(st);//прочитать поток
            string response = sr.ReadToEnd(); // присвоение прочтенного к стринг        
           // Regex regex = new Regex(@"((\d{1,4})\.(\d{2,4}))");//регулярное выражение 
          //  Match match = regex.Match(response);
          //  string curTimeLong = DateTime.Now.ToLongTimeString();//Время
           // response = match.Value;//Для продажи
           // match = match.NextMatch();//Для покупки
          //  response += "  " + match.Value + "   " + curTimeLong;//Объединение данных
          //  Console.WriteLine(response);//создан для дебага
            textBox1.Text = response;//вывод в текст бокс

            StreamReader r = new StreamReader(pathFile);
            string text = r.ReadToEnd();// получение прочтенной записи
            r.Close();//закрыть чтение 


            FileInfo write = new FileInfo(pathFile);//получаем путь 
            StreamWriter w = write.CreateText();//создаем текст
            text +="  "+response;// слияние прочитанного с вновь внесенным данные покупки 
            w.WriteLine(text);//добавляем текст
            w.Close();//закрыть запись
        }

        private void Start_Click(object sender, EventArgs e)
        {
            while(true)
           {

              // string[] value = new string[45] { "EURUSD", "AUDCAD", "EURCAD", "AUDCHF", "AUDDKK", "AUDJPY", "AUDNOK", "AUDSEK", "AUDNZD", "AUDSGD", "AUDUSD", "CADCHF", "CADJPY", "CADJPY", "CHFSGD", 
              // "EURAUD","EURCAD","EURCHF","EURGBP","EURJPY","EURNOK", "EURPLN","EURSEK","EURSGD", "GBPCHF", "GBPDKK", "GBPJPY","GBPNOK", "GBPSEK","GBPSGD", "GBPUSD","NZDJPY","NZDSGD","NZDUSD", "SGDJPY",
               //"USDCAD","USDCHF","USDDKK","USDJPY", "USDNOK", "USDPLN","USDSEK", "USDSGD", "USDZAR", "USDRUR"};//1 адрес пара евро долар// 2 адрес пара Автралийский доллар и канадский
               //3 адрес пара Евро канадский доллар// 4 адрес пара Автралийский доллар и швейцарская крона//5 адрес пара Автралийский доллар и датская крона//6 адрес пара Автралийский доллар и японская иена
               //7 адрес пара Автралийский доллар и норвежская крона //8 адрес пара Автралийский доллар и шведская крона //9 адрес пара Автралийский доллар и сингапурский доллар //10 адрес пара Автралийский доллар и доллар США 
               //11 адрес пара Автралийский доллар и доллар США   //12 адрес пара Канадский   доллар и швейцарский франк       //13 адрес пара Канадский   доллар и сингапурский доллар //14 адрес пара Евро и Автралийский доллар
               //15 адрес пара Евро и  канадский доллар //16 адрес пара Евро и швейцарский франк //17 адрес пара Евро и английский франк //18 адрес пара Евро и японская йена //19 адрес пара Евро и норвежская крона  //20 адрес пара Евро и польский злотый
               //21 адрес пара Евро и   шведская крона //22 адрес пара Евро и  сингапурский доллар //23 адрес пара Английский фунт и  швейцарский фунт //24 адрес пара Английский фунт и  датская крона  //25 адрес пара Английский фунт и   японская йена
               //26 адрес пара Английский фунт  и норвежская крона  //27 адрес пара Английский фунт  и шведская крон //28 адрес пара Английский фунт  и сингапурский доллар
              // string[] value = new string[9] { "eur-usd", "gbp-usd", "usd-jpy", "usd-chf", "aud-usd", "nzd-usd", "eur-jpy", "usd-rub", "eur-rub" };
               string[] value = new string[1] { "eurusd" };
                for (int i = 0 ; i < 1 ; i++ ) 
               {
                requst(value[i]);
               }               
            }// необходимо добавить изменение Ip 
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
