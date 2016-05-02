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

    public delegate void CountDelegeate(object sender, EventArgs e); // работа с делегатами
    /// <summary>
    /// main class EURUSD.
    /// </summary>
    public   partial  class Windowd : Form
    {  
        //// Calsulations calculations = new Calsulations(); додумать
        ChartArea area = new ChartArea(); // Создание области
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        int Leaves = 0, Zoom = 100;
        double shag = 1; // интервал для координаты X
        int danoe = 0;
        int colvo = 0;

        double poslchislo;
        double poslchislo1;
        bool inet;
        bool poluch = false;

        public List<double> BUY = new List<double>();
        public List<double> SELL = new List<double>();
        List<double> massY = new List<double>();
        List<double> massYInetA = new List<double>(); // лист значений отношения валюты по продаже из файла
        List<double> Times = new List<double>();
        List<double> massYInetB = new List<double>(); // лист значений отношения валюты по покупке из файла
        public List<double> Buffer = new List<double>();
        public List<double> BufferS = new List<double>();

        List<DateTime> DTIME = new List<DateTime>(); // буфферное время

        List<DateTime> DINET = new List<DateTime>(); // время из файла

        List<DateTime> MainT = new List<DateTime>();
        List<double> MainV = new List<double>(); 

        List<double> sred = new List<double>(); // Точки по X SMAr
        double poslTime = 0; // последнее время
        string[] value = new string[1] { "eurusd" };  // Переменная для отправки в адрес URL и файлу
        int tic = 0; // Переменная отслеживающая кол-во прошедших секунд с запуска формы

        #region Параметры текущей формы
        int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; // высота экрана
        int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; // ширина экрана 
        double fX = 1366; // стандартный размер формы
        double fY = 757; // стандартный размер формы
        int cX = 1058;
        int cY = 684;
        #endregion

        List<int> PoinX = new List<int>(); // данные точки
        StreamReader q;
   public Windowd()
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
                MessageBox.Show("Отсутсвие интернета или недоступен сайт переход в автономный режим");
                inet = false;
            } // Временная мера по отсутвию интернета
        
            double fX = 1366;
            double fY = 757;
            double xS = x / 1920.0; // настройка под все  экраны
            double yS = y / 1080.0; // настройка под все  экраны

            poslTime = Conect(value[0], poslTime, 1000000); // соединение для получения посоеднего времени
                    
            this.InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(OnClos);
            
            chart1 = new Chart(); // Создание чарта
            chart1.Parent = this;
          
            area.Name = "myGraph";
            area.AxisX.MajorGrid.Interval = shag; // доработать интервал по координате X 1= 1 день
            chart1.Location = new Point(0, 10); // размещение чарта
            this.chart1.Size = new System.Drawing.Size(Convert.ToInt32(cX * xS * (WSrting.X / fX)), Convert.ToInt32(cY * yS * (WSrting.X / fX))); // размеры чарта
            chart1.ChartAreas.Add(area); // передача 

            #region Задание параметров кнопкам button9, button8, button10, button1, button7
            button9.Location = new Point(Convert.ToInt32(1100 * xS * (WSrting.X / fX)), Convert.ToInt32(27 * yS * (WSrting.Y / fY))); // клавиша buy         
            button8.Location = new Point(Convert.ToInt32(1157 * xS * (WSrting.X / fX)), Convert.ToInt32(27 * yS * (WSrting.Y / fY))); // клавиша price     
            button10.Location = new Point(Convert.ToInt32(1274 * xS * (WSrting.X / fX)), Convert.ToInt32(27 * yS * (WSrting.Y / fY))); // клавиша sell   
            button1.Location = new Point(Convert.ToInt32(1100 * xS * (WSrting.X / fX)), Convert.ToInt32(72 * yS * (WSrting.Y / fY))); // клавиша value
            button7.Location = new Point(Convert.ToInt32(1216 * xS * (WSrting.X / fX)), Convert.ToInt32(72 * yS * (WSrting.Y / fY))); // клавиша value
            button9.Size = new Size(Convert.ToInt32(58 * xS * (WSrting.X / fX)), Convert.ToInt32(46 * yS * (WSrting.Y / fY)));
            button8.Size = new Size(Convert.ToInt32(118 * xS * (WSrting.X / fX)), Convert.ToInt32(46 * yS * (WSrting.Y / fY)));
            button10.Size = new Size(Convert.ToInt32(58 * xS * (WSrting.X / fX)), Convert.ToInt32(46 * yS * (WSrting.Y / fY)));
            button1.Size = new Size(Convert.ToInt32(117 * xS * (WSrting.X / fX)), Convert.ToInt32(46 * yS * (WSrting.Y / fY)));
            button7.Size = new Size(Convert.ToInt32(117 * xS * (WSrting.X / fX)), Convert.ToInt32(46 * yS * (WSrting.Y / fY)));
            #endregion
          
            label1.Location = new Point(Convert.ToInt32(1098 * xS * (WSrting.X / fX)), Convert.ToInt32(118 * yS * (WSrting.Y / fY)));
            label2.Location = new Point(Convert.ToInt32(1098 * xS * (WSrting.X / fX)), Convert.ToInt32(280 * yS * (WSrting.Y / fY)));

            checkBox1.Location = new Point(Convert.ToInt32(1101 * xS * (WSrting.X / fX)), Convert.ToInt32(149 * yS * (WSrting.Y / fY)));
            checkBox2.Location = new Point(Convert.ToInt32(1101 * xS * (WSrting.X / fX)), Convert.ToInt32(172 * yS * (WSrting.Y / fY)));
            checkBox3.Location = new Point(Convert.ToInt32(1101 * xS * (WSrting.X / fX)), Convert.ToInt32(305 * yS * (WSrting.Y / fY)));
            checkBox4.Location = new Point(Convert.ToInt32(1101 * xS * (WSrting.X / fX)), Convert.ToInt32(195 * yS * (WSrting.Y / fY)));            
            
            Graph(); // Вызов метода объявления линий

            #region вызов Методов локализации формы
            tTip(); // локализация всплывающих подсказок
            CheckBox(); // переводчик 
            Button(); // переводчик  кнопок
            Menu(); // переводчик меню       
            #endregion
        }
        /// <summary>
        /// Method create file 
        /// </summary>
        /// <param name="pathFile">String</param>
      public void CreateFile(string pathFile)
        {
            //// проверка на существование файла
            if (!File.Exists(pathFile)) 
            {
                FileInfo writel = new FileInfo(pathFile); // получаем путь 
                StreamWriter l = writel.CreateText(); // создаем текст
                l.Close(); // закрыть запись
            } // развертывание файла в дебаге
        }

        public StreamReader Conection(int limit, double poslT, string value)
        { 
            var webReq = WebRequest.Create("http://currency-dred95.rhcloud.com/get_currency.php?time=" + poslT + "&limit=" + limit + "&sign=" + value); // запрос на сайт 
            WebResponse webRes = webReq.GetResponse(); // получение ответа
            Stream st = webRes.GetResponseStream(); // поток по которому получаем инфу
            StreamReader sr = new StreamReader(st); // прочитать поток
            return sr; 
        }

        public double Conect(string value, double poslTime, int limit)
        { 
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); // преобразовение типа доубле к американскому стандарту
            Console.WriteLine(WSrting.ENG); // Дебаг выбjра языка

            #region Получение данных по котировкам из файла "eurusd.txt"  запись их в переменную text
            string pathDirectory = Application.StartupPath; // Путь к директории
            string pathFile = pathDirectory + "\\" + value + ".txt"; // Путь к файлу c котировками eurusd
            CreateFile(pathFile); // Создание не существующего файла
            StreamReader r = new StreamReader(pathFile);
            string text = r.ReadToEnd(); // получение прочтенной записи
            r.Close(); // закрыть чтение   
            #endregion

            Regex regex1 = new Regex(@"(\d{10,20})"); // регулярное выражение для поиска последнего времени в файле
            MatchCollection m1 = regex1.Matches(text);

            if (m1.Count != 0)
            {
                poslTime = Convert.ToDouble(m1[m1.Count - 1].Value);
                Console.WriteLine("poslTime");
                Console.WriteLine(poslTime); // Дебаг
            } // недопускает присвоение при значении прочтенного в файле меньше 1.  
          
            //// WebProxy wp = new WebProxy("151.236.216.251", 10000); //задаем параметры прокси
       
                #region Получение данных из файла запись их  в text1              
                StreamReader r1 = new StreamReader(pathFile);
                string text1 = r1.ReadToEnd(); // получение прочтенной записи
                r1.Close(); // закрыть чтение 
                #endregion

                if (this.inet == true)
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

            if (tic == 0)
            {
            readFile rEURUSD = new readFile(); // вызов класса чтения из файла
            colvo = rEURUSD.read(text1, massYInetA, massYInetB, Times, colvo); // функция чтения из файла
            Console.WriteLine(massYInetB[(colvo / 3) - 1]); ////  Дебаг прочитанного
            Methods cEURUSD = new Methods(); // Метод конвертации времени в тип Date Time
            DINET = cEURUSD.Convert(Times, DINET); // Конвертируем время из  формата UNIX в DataTime
            }

            return poslTime;
        } //// Получение данных Необходимо создать вторую ветку с помощью try catch которая будет работать при отсутствии интеренета.

        //// Отображение галочек  в меню
        public void Activ(object sender)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem.CheckState == CheckState.Checked)
            {
                menuItem.CheckState = CheckState.Unchecked; // Снять отметку
            }
            else if (menuItem.CheckState == CheckState.Unchecked)
            {
                menuItem.CheckState = CheckState.Checked; // поставить отметку
            }
        } 

       public void CheckBox()
        {
            #region checkBox1, checkBox2, checkBox3, label1, label2 Присвоение английского языка
            if (WSrting.ENG == true)
            {
                checkBox1.Text = "Levels suport and resistance";
                checkBox2.Text = "SMA";
                checkBox3.Text = "Line coordinates"; 
                label1.Text = "Methods";
                label2.Text = "Tools";
            }
            #endregion

            #region checkBox1, checkBox2, checkBox3, label1, label2 Присвоение русского языка
            if (WSrting.RUS == true)
            {     
                checkBox1.Text = "Уровни сопротивления и поддержки";
                checkBox2.Text = "Скользящая кривая";
                checkBox3.Text = "Координатные линии";
                label1.Text = "Meтоды";
                label2.Text = "Инструменты";
            }
                #endregion
        } // перевод методов и инструментов
        
        public void tTip()
       {
            toolTip1.AutoPopDelay = 1000; // время сколько показывается надпись
            toolTip1.InitialDelay = 1000; // время сколько показывается надпись
            toolTip1.ReshowDelay = 1000; // время сколько показывается надпись
            toolTip1.ShowAlways = true;

            toolTip2.AutoPopDelay = 1000; // время сколько показывается надпись
            toolTip2.InitialDelay = 1000; // время сколько показывается надпись
            toolTip2.ReshowDelay = 1000; // время сколько показывается надпись
            toolTip2.ShowAlways = true;

            if (WSrting.ENG == true)
            {
                toolTip1.SetToolTip(this.checkBox2, "Click to activate the displaying of the moving line.");
                toolTip1.SetToolTip(this.checkBox3, "Press to activate the display lines value at the point.");
                toolTip1.SetToolTip(this.checkBox1, "Click to activate the display of support and resistance levels.");
            }

            if (WSrting.RUS == true)
            {
                toolTip1.SetToolTip(this.checkBox2, "Нажмите чтобы активировать отображение уровней скользящей прямой");
                toolTip1.SetToolTip(this.checkBox3, "Нажмите чтобы активировать отображение линий значение в точке.");
                toolTip1.SetToolTip(this.checkBox1, "Нажмите чтобы активировать отображение уровней поддержки и сопротивления.");
            }

            toolTip3.AutoPopDelay = 1000; // время сколько показывается надпись
            toolTip3.InitialDelay = 1000; // время сколько показывается надпись
            toolTip3.ReshowDelay = 1000; // время сколько показывается надпись
            toolTip3.ShowAlways = true;
        }

        public void Button()
        {
            #region Локализация button10 button9 button1 button7
            if (WSrting.ENG == true)
            {
                button10.Text = "Sell"; // текст клавиши покупки
                button9.Text = "Buy"; // текст клавиши покупки
                button1.Text = "Value Buy"; // текст клавиши покупки
                button7.Text = "Value Sell"; // текст клавиши покупки            
            }

            if (WSrting.RUS == true)
            {
                button9.Text = "Покупка"; // текст клавиши покупки 
                button10.Text = "Продажа"; // текст клавиши покупки 
                button1.Text = "Значение покупки"; // текст клавиши покупки
                button7.Text = "Значение продажа"; // текст клавиши покупки
            }
            #endregion
        }

        public void Menu()
        {
            if (WSrting.ENG == true)
            {
               timeLevelToolStripMenuItem.Text = "Time intervall";
               reportToolStripMenuItem.Text = "Report";              
               createReportToolStripMenuItem.Text = "Creaty report";
               settingToolStripMenuItem.Text = "Settings";
               secondToolStripMenuItem.Text = "Second";
               minutesToolStripMenuItem.Text = "5 Minutes";
               minutesToolStripMenuItem1.Text = "30 Minutes";
               hourToolStripMenuItem.Text = "Hour";
               dayToolStripMenuItem.Text = "Day";
               weekToolStripMenuItem.Text = "Week";
               monthToolStripMenuItem.Text = "Month";
               toCloseTheDealToolStripMenuItem.Text = "To close the deal"; 
            }

            if (WSrting.RUS == true)
            {
               timeLevelToolStripMenuItem.Text = "Временные интервалы";
               reportToolStripMenuItem.Text = "Отчет";
               createReportToolStripMenuItem.Text = "Создать отчет";
               settingToolStripMenuItem.Text = "Настройки";
               secondToolStripMenuItem.Text = "Секундный";
               minutesToolStripMenuItem.Text = "5 Минут";
               minutesToolStripMenuItem1.Text = "30 Минут";
               hourToolStripMenuItem.Text = "Час";
               dayToolStripMenuItem.Text = "День";
               weekToolStripMenuItem.Text = "Неделя";
               monthToolStripMenuItem.Text = "Месяц";
               toCloseTheDealToolStripMenuItem.Text = "Закрыть сделку"; 
            }
        }
    
        public void Graph()
        {  
            Series series1 = new Series();
            series1.ChartArea = "myGraph";
            series1.XValueType = ChartValueType.DateTime;
            series1.ChartType = SeriesChartType.Line;
            series1.BorderWidth = 2;
            chart1.Series.Add(series1); // параметры главной линии

            Series series2 = new Series();
            series2.ChartArea = "myGraph";
            series2.XValueType = ChartValueType.DateTime;
            series2.ChartType = SeriesChartType.Line;
            series2.BorderWidth = 2;
            chart1.Series.Add(series2); // параметры  линии поддержки

            Series series3 = new Series();
            series3.ChartArea = "myGraph";
            series3.XValueType = ChartValueType.DateTime;
            series3.ChartType = SeriesChartType.Spline;
            series3.BorderWidth = 2;
            chart1.Series.Add(series3); // параметры  линии сопротивления

            Series series4 = new Series();
            series4.ChartArea = "myGraph";
            series4.XValueType = ChartValueType.DateTime;
            series4.ChartType = SeriesChartType.Line;
            series4.BorderWidth = 2;
            chart1.Series.Add(series4); // параметры  линии SMA

            Series series5 = new Series();
            series5.ChartArea = "myGraph";
            series5.XValueType = ChartValueType.DateTime;
            series5.ChartType = SeriesChartType.Line;
            series5.BorderWidth = 2;
            chart1.Series.Add(series5); // параметры  линии 

            Series series6 = new Series();
            series6.ChartArea = "myGraph";
            series6.XValueType = ChartValueType.DateTime;
            series6.ChartType = SeriesChartType.Line;
            series6.BorderWidth = 2;
            chart1.Series.Add(series6); // параметры  линии МAX

            Series series7 = new Series();
            series7.ChartArea = "myGraph";
            series7.XValueType = ChartValueType.DateTime;
            series7.ChartType = SeriesChartType.Line;
            series7.BorderWidth = 2;
            chart1.Series.Add(series7); // параметры  линии МAX

            chart1.ChartAreas[0].BackColor = Color.FromArgb(255, 255, 255); // цвет внутренней области
            chart1.BackColor = Color.FromArgb(255, 255, 255); // цвет внешней области

            chart1.ChartAreas[0].AxisX.LineColor = Color.FromArgb(0, 0, 0); // цвет нижней линии 
            chart1.ChartAreas[0].AxisX2.LineColor = Color.FromArgb(0, 0, 0); // цвет  линии 
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.FromArgb(0, 0, 0); // цвет надписей координат по X

            chart1.ChartAreas[0].AxisY.LineColor = Color.FromArgb(0, 0, 0); // цвет боковой  линии по Y
            chart1.ChartAreas[0].AxisY2.LineColor = Color.FromArgb(0, 0, 0); // цвет боковой  линии по Y
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.FromArgb(0, 0, 0); // цвет надписей координат по Y

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "dd//hh:mm:ss tt"; // Установка отображения даты
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;

            chart1.ChartAreas[0].AxisX.Title = "Date, Time";
            chart1.ChartAreas[0].AxisY.Title = "Attitude EUR/USD";

            chart1.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chart1.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.Interval = 0;

            GraphY EURUSD = new GraphY();
            EURUSD.Y(chart1, massYInetB[massYInetB.Count - 1]); // доработать класс

            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
        } // График динамического создание линий

        public void Setting()
        {
            double xS = x / 1920.0; // настройка под все  экраны
            double yS = y / 1080.0; // настройка под все  экраный
            if (checkBox3.Checked == true)
            {
                label_X.Visible = true;
                label_Y.Visible = true;
                lab_Cur.Visible = true;
                label_X.Size = new Size(Convert.ToInt32(980 * xS * (WSrting.X / fX)), 1);
                label_Y.Size = new Size(1, Convert.ToInt32(610 * xS * (WSrting.Y / fY)));
                label_X.BackColor = Color.FromArgb(0, 0, 0); // цвет линии по X
                label_Y.BackColor = Color.FromArgb(0, 0, 0); // цвет линии по Y
                this.chart1.MouseMove += new MouseEventHandler(this.chart1_MouseMove);
            }

            if (checkBox3.Checked == false)
            {
                label_X.Visible = false; // убрать отображение линии по X
                label_Y.Visible = false; // убрать отображение линии по Y
                lab_Cur.Visible = false; // убрать отображение значений
            }
        }

        public int Update(int D, List<DateTime> DateT)
        {
            #region вызов Методов локализации формы
            tTip(); // локализация всплывающих подсказок
            CheckBox(); // переводчик 
            Button(); // переводчик  кнопок
            Menu(); // переводчик меню 
            #endregion

            chart1.MouseWheel += new MouseEventHandler(this.chart1_MouseWheel); // событие вращения колесика
            chart1.Focus(); // необходим фокус
            List<List<double>> poin = new List<List<double>>(); // Точки изменения тренда
            Setting();

            if (checkBox1.Checked == false)
            {
                chart1.Series[2].Points.Clear();
                chart1.Series[1].Points.Clear(); // Очистка точек линий
            }
           
            chart1.Series[0].XValueType = ChartValueType.Time; // установление типа по икс время

            #region Склейка даннях из файла с буфером
            Draw q = new Draw();
            MainT = q.MainTime(DINET, DateT, tic);
            MainV = q.MainValue(Buffer, massYInetB, tic);
            #endregion

            chart1.Series[0].Points.Clear();
            chart1.Series[4].Points.AddXY(DateT[0].ToOADate(), Buffer[0]); // создаем костыль для графика чарт

            for (int hl = 0; MainT.Count - 1 >= hl; hl++)
            {
                chart1.Series[0].Points.AddXY(MainT[hl].ToOADate(), MainV[hl]);
            }

            poin = IntervalResistance(tic, Buffer); // Получение точек смены тренда  // данные из буфера // доработать
            
            if (checkBox1.Checked == true)
            {
                Resis(poin, tic, 0.0001, DateT); // рисуем уровни
            } 
            
            chart1.Update(); // обновление данных

            int Z = 10; // Кол-во  точек берущихся в расчет в построении графика SMA
            SMA(MainT, Z, MainV); // Вызов метода для построения SMA

            button8.Text = Convert.ToString(Buffer[tic]); // вывод значений на кнопку  по времени
            button1.Text = Convert.ToString(Buffer[tic]); // вывод значений на кнопку  по времени
            button7.Text = Convert.ToString(BufferS[tic]); // вывод значений на кнопку  по времени
            ZoomT(Zoom); // Вызов метода  регуирования уровней времени
            tic++; // Подсчет тикового времени
            return tic;
        } // метод обновления данных

      public void ZoomT(int ize)
        {
            chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[4].Points[0].XValue - (ize * 0.00001157407) + (tic * 0.00001157407); // ограничение по X минимум   NowTime
            chart1.ChartAreas[0].AxisX.Maximum = chart1.Series[4].Points[0].XValue + (ize * 0.00001157407) + (tic * 0.00001157407); // ограничение по X максимум  под 60 секунд 0,00001157407
        } // функция необходимая для уровней рассмотренного времени

      public List<double> SMA(List<DateTime> Dat, int Sglag, List<double> Buff)
        {
            List<double> sreds = new List<double>(); // Точки по X SMAr
            double p = 0;

            if (Buff.Count >= Sglag)
            {
                for (int j = 0; j  <= (Buff.Count - Sglag); j++)
                {
                    for (int i = 0; i <= Sglag - 1; i++)
                    {
                        p += Buff[i + j]; // складываем кол - во точек
                    }

                    sreds.Add(p / Sglag); // Добавление в лист средних значений по Sglag точкам 
                    p = 0;
                }  
            }

            if (checkBox2.Checked == true)
            {
                if (Buff.Count >= Sglag)
            {
                chart1.Series[3].Points.Clear();

                for (int h = 0; h <= sreds.Count - 1; h++)
                {
                    chart1.Series[3].XValueType = ChartValueType.Time;
                    chart1.Series[3].Color = Color.FromArgb(255, 100, 100); // задание цвета
                    chart1.Series[3].Points.AddXY(Dat[Sglag - 1 + h].ToOADate(), sreds[h]); // добавление точек в линию
                }
            }
            }

            if (checkBox2.Checked == false)
            {
                chart1.Series[3].Points.Clear();
            }

            return sreds;
        } ////  вычисление точки СМА исправлено под буфер и чтения с файла

      public List<List<double>> IntervalResistance(int tic, List<double> massYInetBuy) //// подавать интервал
        {
            int Pervoe = 0;
            int trend = 0; // переменная тренда
            List<List<double>> poin = new List<List<double>>(); // данные точки
            List<double> koordPoint = new List<double>();

            for (int i = 1; tic > i; i++)
            {
                if (trend != 1 && (massYInetB[i - 1] - massYInetBuy[i]) > 0)
                {
                    koordPoint = new List<double>(); // Координата точки
                    trend = 1; // положительный тренд
                    koordPoint.Add(i - 1); // заполнение точек по икс    
                    koordPoint.Add(massYInetB[i - 1]); // заполнение точек по игрик  
                    if (Pervoe == 0)
                    {
                        danoe = -1;
                        Pervoe++;
                    } // узнаем первое значение минимум оно или максимум
                    poin.Add(koordPoint); // добавление координаты точки
                }

                if (trend != -1 && (massYInetB[i - 1] - massYInetB[i]) < 0)
                { 
                    koordPoint = new List<double>();  // Координата точки
                    trend = -1; // положительный тренд
                    koordPoint.Add(i-1); // заполнение точек по икс    
                    PoinX.Add(i-1);
                    koordPoint.Add(massYInetB[i]-1); // заполнение точек по игрик  
                    if (Pervoe == 0)
                    {
                        danoe = 1;
                        Pervoe++;
                    } // узнаем первое значение минимум оно или максимум
                    poin.Add(koordPoint); // добавление координаты точки
                }
            }

            return poin;
        }

      public void Resis(List<List<double>> poin, int tic, double pogr, List<DateTime> Date)
        {
            int[] line1X = new int[3];
            int[] line2X = new int[3];
            double[] line1Y = new double[3];
            double[] line2Y = new double[3];
            int[] urowen = new int[1];
            double MAXY = -50, MINY = 1000, MAXX = 0, MINX = 0, MinH = 0, MaxH = 0;

            for (int h = 1; h < poin.Count; h++)
            {
                //// poin[h][1] точка по игрик
                if (MAXY < poin[h][1]) 
                {
                    MAXY = poin[h][1];
                    MAXX = poin[h][0];
                    MaxH = h;
                }

                if (MINY > poin[h][1])
                {
                    MINY = poin[h][1];
                    MINX = poin[h][0];
                    MinH = h;
                }

                MAXY = Resistance(poin, tic, pogr, Date, h, MAXY, MAXX, MaxH); // Нахождение точки максимума удовлетворяющему условию разброса и повторения более 2 раз
                MINY = Support(poin, tic, pogr, Date, h, MINY, MINX, MinH); // Нахождение точки минимума удовлетворяющему условию разброса и повторения более 2 раз
            }
        } // Вызов 2х методов для рисования и получения значений MAXY, MINY по которым происходит построение линий

        public double Support(List<List<double>> poin, int tic, double pogr, List<DateTime> Date, int h, double MINY, double MINX, double MinH) ////Метод уровня поддержки
        {
            int line2X;
            double line2Y;

            for (int g = 0; g < poin.Count; g++)
            {
                //// g != MinH Не просматривать точку, которая уже входит в уровень поддержки
                if (((MINY - poin[g][1]) >= -pogr) && g != MinH)
                {
                    chart1.Series[2].Points.Clear(); // Очистка линии
                    line2Y = poin[g][1]; // 2 точка по игрик
                    line2X = Convert.ToInt32(poin[g][0]); // 2 точка по икс                    
                    chart1.Series[2].XValueType = ChartValueType.Time; // Указывание типа координат по X
                    chart1.Series[2].Points.AddXY(Date[0].ToOADate(), line2Y); // 1 точка // можно использовать для начала line1X
                    chart1.Series[2].Points.AddXY(Date[tic].ToOADate(), line2Y); // 2 точка
                    chart1.Series[2].Color = Color.FromArgb(55, 0, 55); // задание цвета 
                }
            }
            //// сравнение точки в пределах данной погрешности
            if (MINY > (massYInetB[tic] + pogr)) 
            {
                chart1.Series[2].Points.Clear();
            }

            return MINY;
        }

        public double Resistance(List<List<double>> poin, int tic, double pogr, List<DateTime> Date, int h, double MAXY, double MAXX, double MaxH) ////Метод уровня сопротивления
        {
            int line2X;
            double line2Y;
            if (MAXY < poin[h][1]) 
            {
                MAXY = poin[h][1];
                MAXX = poin[h][0];
                MaxH = h;
            }

            for (int g = 0; g < poin.Count; g++)
            {
                if (((MAXY - poin[g][1]) <= pogr) && g != MaxH)
                {
                    chart1.Series[1].Points.Clear(); // Очистка линии
                    line2Y = poin[g][1]; // 2 точка по игрик
                    line2X = Convert.ToInt32(poin[g][0]); // 2 точка по икс

                    chart1.Series[1].XValueType = ChartValueType.Time; // Указывание типа координат по X
                    chart1.Series[1].Points.AddXY(Date[0].ToOADate(), line2Y); // 1 точка // можно использовать для начала line1X
                    chart1.Series[1].Points.AddXY(Date[tic].ToOADate(), line2Y); // 2 точка
                    chart1.Series[1].Color = Color.FromArgb(255, 0, 0); // задание цвета 
                }
            }

            if (MAXY < (massYInetB[tic] - pogr))
            {
                chart1.Series[1].Points.Clear();
            } // если за рамками погрешности
            return MAXY;
        }

        public void Window_Load(object sender, EventArgs e)
        { 
          t.Start(); 
          t.Interval = 1000; // время секунды
          t.Tick +=  new EventHandler(timer1_Tick); // прибавление времени       
        }

        public void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            double xS = x / 1920.0; // настройка под все  экраны
            double yS = y / 1080.0; // настройка под все  экраный

            if (e.Y > Convert.ToInt32(27 * yS * (WSrting.Y / fY)) && e.Y < Convert.ToInt32(629 * yS * (WSrting.Y / fY)) && e.X > Convert.ToInt32(110 * xS * (WSrting.X / fX)))
            {
                label_X.Location = new Point(Convert.ToInt32(110 * xS * (WSrting.X / fX)), e.Y); // перемещение линии по y
                label_Y.Location = new Point(e.X, Convert.ToInt32(30 * yS * (WSrting.Y / fY))); // перемещение линии по X
                lab_Cur.Location = new Point(Convert.ToInt32(1020 * xS * (WSrting.X / fX)), e.Y); // привязка значения 
                double YCur = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                double XCur = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X) / 0.00001157407;
                DateTime Date1 = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(XCur); // время в формате UNIX
                lab_Cur.Text = string.Concat(string.Concat(Math.Round(YCur, 5).ToString(), " , "), Date1.ToString("h:mm:ss.fff tt")); // перевод времени
            }
        } // вывод координат

        public void chart1_MouseWheel(object sender, MouseEventArgs e) //// метод недоработан
        {
            if (e.Delta / 120 > 0)
            {
                switch (Leaves)
                {
                    case 1: Zoom -= 5;
                    if (Zoom > 60) 
                    { 
                        ZoomT(Zoom); 
                    }
                        break;
                }
            }

            if (e.Delta / 120 < 0)
            {
                switch (Leaves)
                {
                    case 1: Zoom += 5; 
                    ZoomT(Zoom); 
                    break;
                }
            } // прокрутка вниз
        }

        private void timer1_Tick(object sender, EventArgs e)
        { 
            double dTime = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds -5; // Текущее время
            int NowTime;
            NowTime = Convert.ToInt32(dTime); // текущее время (работает)
            string value;
            value = "eurusd";

          if (inet == true)
          {
            var webReq1 = WebRequest.Create("http://currency-dred95.rhcloud.com/get_currency.php?time=" + NowTime + "&limit=" + 1 + "&sign=" + value); // запрос на сайт 
            WebResponse webRes1 = webReq1.GetResponse(); // получение ответа
            Stream st = webRes1.GetResponseStream(); // поток по которому получаем инфу
            StreamReader sr = new StreamReader(st); // прочитать поток
            string texts = sr.ReadToEnd(); // получение прочтенной записи
            Regex regex = new Regex(@"((\d{10,20})|(\d{1,20})\.(\d{1,4}))"); // регулярное выражение 
            MatchCollection M = regex.Matches(texts);
               
              if (tic == 0)
                {
                    poslchislo = massYInetA[massYInetB.Count - 1]; // Присвоение к последнему числу в записи
                }

                if (tic == 0)
                {
                    poslchislo1 = massYInetB[massYInetA.Count - 1]; // Присвоение к последнему числу в записи
                }

            if (M.Count > 0)
            {
              Buffer.Add(Convert.ToDouble(M[1].Value)); // добавить в лист значения        
              BufferS.Add(Convert.ToDouble(M[2].Value)); // добавить в лист значения    
              poslchislo = Convert.ToDouble(M[1].Value);
              poslchislo1 = Convert.ToDouble(M[2].Value);
            }
            else 
            {                              
                Buffer.Add(poslchislo); // Добавление в массив последнего числа из файла (Так как на сервере новых записей не найдено)
                BufferS.Add(poslchislo1); // Добавление в массив последнего числа из файла (Так как на сервере новых записей не найдено)
            } 

             }
           else
          {
              Buffer.Add(massYInetA[massYInetA.Count - 1]); // Добавление в массив последнего числа из файла (Так как на сервере новых записей не найдено)
              BufferS.Add(massYInetB[massYInetB.Count - 1]); // Добавление в массив последнего числа из файла (Так как на сервере новых записей не найдено)
          }

          DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(NowTime); // время в формате UNIX
          DTIME.Add(Date); // добавление времени в лист

          tic = Update(tic, DTIME); // функция по секунде
        } // работает

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Methods se = new Methods();
            BUY.Add(se.TradeBuy(true, Buffer, tic));  // Запомнить значение продажи          
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Methods se = new Methods();
            SELL.Add(se.TradeBuy(false, BufferS, tic));  // Запомнить значение продажи           
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void label_Y_Click(object sender, EventArgs e)
        {
        }

        private void label_X_Click(object sender, EventArgs e)
        {
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Tick -= new EventHandler(timer1_Tick); // прибавление тикового времени
        }
        
        private void lab_Cur_Click(object sender, EventArgs e)
        {
        }


        #region параметры уровней времени
        private void SecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Leaves = 1;
            area.AxisX.MajorGrid.Interval = 0.00001157407;
            Zoom = 60;
            Activ(sender);
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked; // убрать отметку 30 минутный уровень
            hourToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку часовой уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку недельный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку месячный уровень
        } // секундный уровень

        private void MinutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60;
            Zoom = 1800;
            Activ(sender);
            secondToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку секундный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked; // убрать отметку 30 минутный уровень
            hourToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку часовой уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку недельный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку месячный уровень
        } // 5 минутный уровень 

        private void MinutesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60 * 5;
            Zoom = 9000;
            Activ(sender);
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку минутный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку секундный уровень
            hourToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку часовой уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку недельный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку месячный уровень
        }

        private void HourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60 * 10;
            Zoom = 108000;
            Activ(sender);
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked; // убрать отметку 30 минутный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку секундный уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку недельный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку месячный уровень
        } // часовой уровень

        private void WeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60 * 70;
            Zoom = 1728000;
            Activ(sender);
            hourToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку часовой уровень
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked; // убрать отметку 30 минутный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку секундный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку месячный уровень
        } // недельный уровень

        private void DayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 1 / 24.0;
            Zoom = 432000;
            Activ(sender);
            hourToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку часовой уровень
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked; // убрать отметку 30 минутный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку секундный уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку месячный уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку недельный уровень
        } // дневной уровень

        private void MonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 1;
            Zoom = 8640000;
            Activ(sender);
            hourToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку часовой уровень
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked; // убрать отметку 30 минутный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку секундный уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку недельный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked; // убрать отметку дневной уровень
        } // месячный уровень
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {          
        }

        private void timeLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void OnClos(object sender, FormClosingEventArgs e)
        {
            Form1.WindowClosing = true;
        }

       private void reportToolStripMenuItem_Click(object sender, EventArgs e)
       {
       }

    private void CreateReportToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Report rep = new Report();
        rep.Size = new Size(650, 455); // задание размеров окна отчета
        rep.Show(); // Показать  окно отчета
    }

    private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
    {
        EURUSD fset = new EURUSD();
        fset.Show();
        fset.Size = new Size(580, 580); // задание размера формы 
    }

    private void ToCloseTheDealToolStripMenuItem_Click(object sender, EventArgs e)
    {
        CloseDeal f = new CloseDeal();
        f.Owner = this;
        f.Show(); // Показать форму
    }

    }
}
