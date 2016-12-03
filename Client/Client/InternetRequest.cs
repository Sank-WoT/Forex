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

    
    class InternetRequest : Internet
    {
        private string path;
        public InternetRequest(int NowTime, int limit, string sign)
            {
            path = "http://myfirstphpapp-skro.rhcloud.com/get_currency.php?time=" + NowTime + "&limit=" + limit + "&sign=" + sign;
            Console.WriteLine(path);
            }

        /// <summary>
        /// Парсинг и запрос данных на сервер
        /// </summary>
        public MatchCollection InternetData()
        {
            // запрос на сайт 
            var webReq1 = WebRequest.Create(path);
            // получение ответа
            WebResponse webRes1 = webReq1.GetResponse();
            // поток по которому получаем инфу
            Stream st = webRes1.GetResponseStream();
            // прочитать поток
            StreamReader sr = new StreamReader(st);
            // получение прочтенной записи
            string texts = sr.ReadToEnd();
            // регулярное выражение 
            Regex regex = new Regex(@"((\d{10,20})|(\d{1,20})\.(\d{1,4}))");
            MatchCollection M = regex.Matches(texts);
            return M;
        }
    }
}
