namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
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

    /// <summary>
    /// main class EURUSD.
    /// </summary>
    public partial class Windowd : Form
    {
        Quotes quotations = new Quotes();
        Task tSMA, tMinMax, tInterval;
        public ChartArea area = new ChartArea(); // Создание области
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        public int Leaves = 0, Zoom = 100;
        public double shag = 1; // интервал для координаты X
        public int danoe = 0;
        public int colvo = 0;
        public string value;

        public double poslchislo;
        public double poslchislo1;
        public bool inet;
        public bool poluch = false;

        public List<Deal> BUY = new List<Deal>();
        public List<Deal> SELL = new List<Deal>();
        List<double> massY = new List<double>();
        List<double> massYInetBuy = new List<double>(); // лист значений отношения валюты по покупке из файла
        List<double> massYInetSell = new List<double>(); // лист значений отношения валюты по продаже из файла
        List<int> Times = new List<int>(); // лист времени
        public List<double> Buffer = new List<double>();
        public List<double> BufferS = new List<double>();

        public List<DateTime> DTIME = new List<DateTime>(); // буфферное время
        public List<DateTime> DINET = new List<DateTime>(); // время из файла

        public List<double> sred = new List<double>(); // Точки по X SMA
        public int tic = 0; // Переменная отслеживающая кол-во прошедших секунд с запуска формы
        private object sync = new object();
        private bool internetActionFinished = false;
        private bool internetInitialized = false;
        Internet IPair = new Internet();
        Methods cEURUSD = new Methods(); // Класс методов

        #region Параметры текущей формы
        int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; // высота экрана
        int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; // ширина экрана 
        int cX = 1058;
        int cY = 684;
        WorkFile rEURUSD = new WorkFile(); // вызов класса Обработки текста

        #endregion
        double fX = 1366; // форма
        double fY = 757; // форма
        List<int> PoinX = new List<int>(); // данные точки

        /// <summary>
        /// Cоздание окна валюты
        /// </summary>
        public Windowd(string Vvalue)
        {
            // получение строки через конструктор
            value = Vvalue;
            int ButtLocX = 1101;
            int ButtLocY = 35;
            int ButtonSize = 59;
            int shift = 12;
            SpeedDraw.Speed = 1;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            double xS = x / 1920.0; // настройка под все  экраны
            double yS = y / 1080.0; // настройка под все  экраны

            this.InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(OnClos);

            graphic = new Chart();
            graphic.Parent = this;
            this.Text = value;

            area.Name = "myGraph";
            area.AxisX.MajorGrid.Interval = shag; // доработать интервал по координате X 1= 1 день
            graphic.Location = new Point(0, 10); // размещение чарта
            this.graphic.Size = new System.Drawing.Size(Convert.ToInt32(cX * xS * (WString.X / fX)), Convert.ToInt32(cY * yS * (WString.X / fX))); // размеры чарта
            graphic.ChartAreas.Add(area); // передача 
            // замена присвоение точек через переопределение дисплея.
            #region Задание параметров кнопкам button9, button8, button10, button1, button7  
            buttonBuy.Location = Display.customizedPoint(1100, 27); // клавиша buy
            buttonPriceSell.Location = Display.customizedPoint(1157, 27); // клавиша value
            button9.Location = Display.customizedPoint(1274, 27); // клавиша  sell        
            price.Location = Display.customizedPoint(1100, 72); // клавиша price               
            buttonPriceBuy.Location = Display.customizedPoint(1216, 72); // клавиша value

            buttonBuy.rusLan = "Покупка";
            buttonBuy.engLan = "Buy";
            button9.rusLan = "Продажа";
            button9.engLan = "Sell";
            buttonPriceBuy.rusLan = "Цена покупки";
            buttonPriceBuy.engLan = "Value Buy";
            buttonPriceSell.rusLan = "Цена продажи";
            buttonPriceSell.engLan = "Value Sell";
            checkBoxLevelSupandResis.engLan = "Levels support and resistance";
            checkBoxSMA.engLan = "SMA";
            checkBoxLineCoord.engLan = "Line coordinates";
            checkBoxBinding.engLan = "Binding graphics";
            checkBoxLevelSupandResis.rusLan = "Уровни сопротивления и поддержки";
            checkBoxSMA.rusLan = "Скользящая кривая";
            checkBoxLineCoord.rusLan = "Координатные линии";
            checkBoxBinding.rusLan = "Привязка графика";

            tabControl.Location = Display.customizedPoint(ButtLocX, ButtLocY + 122);
            numericUpDown1.Value = 10;

            double SizeDisplayX = xS * (WString.X / fX);
            double SizeDisplayY = yS * (WString.Y / fY);
            tabControl.Size = new Size(Convert.ToInt32(250 * SizeDisplayX), Convert.ToInt32(500 * SizeDisplayY));

            // Проверка существования
            if(tabControl.TabPages[0] != null)
            {
                tabControl.TabPages[0].Text = "Methods";
            }

            if (tabControl.TabPages[1].Text != null)
            {
                tabControl.TabPages[1].Text = "Tools";
            } 

            button9.Size = new Size(Convert.ToInt32((ButtonSize + shift * 2 / 3.0) * SizeDisplayX), Convert.ToInt32(46 * SizeDisplayY));
            price.Size = new Size(Convert.ToInt32((ButtonSize + 60 + shift * 2 / 3.0) * SizeDisplayX), Convert.ToInt32(46 * SizeDisplayY));
            buttonBuy.Size = new Size(Convert.ToInt32((ButtonSize + shift) * SizeDisplayX), Convert.ToInt32(46 * SizeDisplayY));
            buttonPriceBuy.Size = new Size(Convert.ToInt32((ButtonSize + 60 + shift) * SizeDisplayX), Convert.ToInt32(46 * SizeDisplayY));
            buttonPriceSell.Size = new Size(Convert.ToInt32((ButtonSize + 60 + shift * 2 / 3.0) * SizeDisplayX), Convert.ToInt32(46 * SizeDisplayY));
            numericUpDown1.Size = new Size(Convert.ToInt32(117 * SizeDisplayX), Convert.ToInt32(20 * SizeDisplayY));
            #endregion
            checkBoxBinding.Checked = true; // Чекбокс отвечающий за привязку графика к середине включен
            Graph(); // Вызов метода объявления линий

            #region вызов Методов локализации формы
            tTip(); // локализация всплывающих подсказок
            Button(); // переводчик  кнопок
            Menu(); // переводчик меню       
            #endregion
        }


        /// <summary>
        /// Method create file 
        /// </summary>
        /// <param name="pathFile">String</param>   
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

        /// <summary>
        /// Метод перевода тултипов
        /// </summary>
        public void tTip()
       {
            if (WString.ENG == true)
            {
                toolTip1.SetToolTip(checkBoxLevelSupandResis, "Click to activate the display of support and resistance levels.");
                toolTip2.SetToolTip(checkBoxSMA, "Click to activate the displaying of the moving line.");
                toolTip3.SetToolTip(checkBoxLineCoord, "Press to activate the display lines value at the point.");               
            }

            if (WString.RUS == true)
            {
                toolTip1.SetToolTip(checkBoxLevelSupandResis, "Нажмите чтобы активировать отображение уровней поддержки и сопротивления.");
                toolTip2.SetToolTip(checkBoxSMA, "Нажмите чтобы активировать отображение уровней скользящей прямой");
                toolTip3.SetToolTip(checkBoxLineCoord, "Нажмите чтобы активировать отображение линий значение в точке.");                
            }

           
        }

        /// <summary>
        /// Метод перевода кнопок
        /// </summary>
        public void Button()
        {
            #region Локализация button10 button9 button1 button7
            string lang = "";
            if (WString.ENG == true)
            {
                lang = "eng";
            }
            if (WString.RUS == true)
            {
                lang = "rus";
            }
            buttonBuy.Translate(lang);
            button9.Translate(lang);// текст клавиши покупки
            buttonPriceSell.Translate(lang); // текст клавиши покупки
            buttonPriceBuy.Translate(lang);
            checkBoxLevelSupandResis.Translate(lang);
            checkBoxSMA.Translate(lang);
            checkBoxLineCoord.Translate(lang);
            checkBoxBinding.Translate(lang);
        }

        /// <summary>
        /// Метод перевода меню
        /// </summary>
        public void Menu()
        {
            if (WString.ENG == true)
            {
               timeLevelToolStripMenuItem.Text = "Time intervals";
               reportToolStripMenuItem.Text = "Report";              
               createReportToolStripMenuItem.Text = "Create a report";
               settingToolStripMenuItem.Text = "Settings";
               secondToolStripMenuItem.Text = "Second";
               minutesToolStripMenuItem.Text = "5 Minutes";
               minutesToolStripMenuItem1.Text = "30 Minutes";
               hourToolStripMenuItem.Text = "Hour";
               dayToolStripMenuItem.Text = "Day";
               weekToolStripMenuItem.Text = "Week";
               monthToolStripMenuItem.Text = "Month";
               toCloseTheDealToolStripMenuItem.Text = "Close a deal"; 
            }

            if (WString.RUS == true)
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

        /// <summary>
        /// Метод задания параметров линии
        /// </summary>
        /// <param name="ChartArea">Название чарта к которому привязана линия</param>
        /// <param name="XValueType">????</param>
        /// <param name="Line">тип линии</param>
        /// <param name="BorderWidth">толщина линии</param>
        public Series extSeries(string ChartArea, ChartValueType XValueType, SeriesChartType Line, int BorderWidth)
        {
            Series series = new Series();
            series.ChartArea = ChartArea;
            series.XValueType = XValueType;
            series.ChartType = Line;
            series.BorderWidth = BorderWidth;
            return series;
        }

        /// <summary>
        /// Метод задания параметров линии
        /// </summary>
        public void Graph()
        {  
            Series series1;
            series1 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series1); // параметры главной линии

            Series series2;
            series2 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series2); // параметры  линии поддержки

            Series series3;
            series3 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series3); // параметры  линии поддержки

            Series series4;
            series4 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series4); // параметры  линии поддержки// параметры  линии SMA

            Series series5;
            series5 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series5); // параметры  линии 

            Series series6;
            series6 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series6);  // параметры  линии МAX

            Series series7;
            series7 = extSeries("myGraph", ChartValueType.DateTime, SeriesChartType.Line, 2);
            graphic.Series.Add(series7);  // параметры  линии МIN

            graphic.ChartAreas[0].BackColor = Color.FromArgb(255, 255, 255); // цвет внутренней области
            graphic.BackColor = Color.FromArgb(255, 255, 255); // цвет внешней области

            graphic.ChartAreas[0].AxisX.LabelStyle.Format = "dd//hh:mm:ss tt"; // Установка отображения даты
            graphic.ChartAreas[0].AxisX.IsStartedFromZero = true;

            graphic.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Seconds;
            graphic.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            graphic.ChartAreas[0].AxisX.Interval = 0;
            graphic.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            graphic.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            graphic.ChartAreas[0].CursorX.IsUserEnabled = true;
            graphic.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            graphic.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Seconds;

            graphic.ChartAreas[0].AxisX.LineColor = Color.FromArgb(0, 0, 0); // цвет нижней линии 
            graphic.ChartAreas[0].AxisX2.LineColor = Color.FromArgb(0, 0, 0); // цвет  линии 
            graphic.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.FromArgb(0, 0, 0); // цвет надписей координат по X
            graphic.ChartAreas[0].AxisX.Title = "Date, Time";
            graphic.ChartAreas[0].AxisY.Title = "Attitude " + value;

            graphic.ChartAreas[0].AxisY.LineColor = Color.FromArgb(0, 0, 0); // цвет боковой  линии по Y
            graphic.ChartAreas[0].AxisY2.LineColor = Color.FromArgb(0, 0, 0); // цвет боковой  линии по Y
            graphic.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.FromArgb(0, 0, 0); // цвет надписей координат по Y

        } // График динамического создание линий

        /// <summary>
        /// Настройки
        /// </summary>
        public void Setting()
        {
            double xS = x / 1920.0; // настройка под все  экраны
            double yS = y / 1080.0; // настройка под все  экраный
            if (checkBoxLineCoord.Checked == true)
            {
                label_X.Visible = true;
                label_Y.Visible = true;
                lab_Cur.Visible = true;
                label_X.Size = new Size(Convert.ToInt32(860 * xS * (WString.X / fX)), 1);
                label_Y.Size = new Size(1, Convert.ToInt32(550 * xS * (WString.Y / fY)));
                label_X.BackColor = Color.FromArgb(0, 0, 0); // цвет линии по X
                label_Y.BackColor = Color.FromArgb(0, 0, 0); // цвет линии по Y
                this.graphic.MouseMove += new MouseEventHandler(this.chart1_MouseMove);
            }

            if (checkBoxLineCoord.Checked == false)
            {
                label_X.Visible = false; // убрать отображение линии по X
                label_Y.Visible = false; // убрать отображение линии по Y
                lab_Cur.Visible = false; // убрать отображение значений
            }
        }

        /// <summary>
        /// Метод задания параметров линии
        /// </summary>
        /// <param name="tic">время прошедшее с запуска</param>
        /// <param name="DateT">????</param>
        /// <param name="NowTime">Текущее время</param>
        /// <param name=" DINET">?????</param>
        public int Update(int tic, List<DateTime> DateT, double NowTime, List<DateTime> DINET)
        {
            #region вызов Методов локализации формы
            tTip(); // локализация всплывающих подсказок
            Button(); // переводчик  кнопок
            Menu(); // переводчик меню 
            #endregion

            graphic.MouseWheel += new MouseEventHandler(this.chart1_MouseWheel); // событие вращения колесика
            List<List<double>> poinl = new List<List<double>>(); // Точки изменения тренда
            Setting();

            if (checkBoxLevelSupandResis.Checked == false)
            {
                graphic.Series[2].Points.Clear();
                graphic.Series[1].Points.Clear(); // Очистка точек линий
            }
           
            graphic.Series[0].XValueType = ChartValueType.Time; // установление типа по икс время

            #region Склейка даннях из файла с буфером
            Splice mReqest = new Splice();
            quotations.TimeD = mReqest.MainTime(DINET, DateT, tic, 100); // Загрузка времени из файла
            quotations.Sell = mReqest.MainValue(Buffer, massYInetSell, tic, 100); // Загрузка значения из файла ###Сделать загрузку данных изменяемой tic


            #endregion

            graphic.Series[0].Points.Clear();       
            graphic.Series[4].Points.AddXY(DateT[0].ToOADate(), Buffer[0]); // создаем костыль для графика чарт
            
            for (int hl = 0; quotations.TimeD.Count - 1 >= hl; hl++)
            {
                graphic.Series[0].Points.AddXY(quotations.TimeD[hl].ToOADate(), quotations.Sell[hl]); //  Построение главного графика             
            }           
           
            graphic.Update(); // обновление данных 

            // Получение точек смены тренда
            tInterval = Task.Run(() =>
            {
                poinl = IntervalResistance(tic, Buffer, NowTime);  
            });  

            List<double> sreds = new List<double>();
            // Вызов метода для построения SMA 
            sreds = SMA(quotations.TimeD, (int)numericUpDown1.Value, quotations.Sell);
            // Точки миниму и максимума
            List<List<double>> listMinMax = new List<List<double>>();

            // Минимумы и максимумы
            tMinMax = Task.Run(() =>
            {
                listMinMax = MinMax(quotations.Sell, quotations.TimeD);
            });

            // Ожидаем завершение
            Task.WaitAll(tMinMax, tInterval);
            DrawSMA(quotations.TimeD, (int)numericUpDown1.Value, quotations.Sell, sreds); // рисовать скользящую кривую
            DrawMinMax(listMinMax); // рисовать минимум максимум

            if (checkBoxLevelSupandResis.Checked == true)
            {
                Resis(poinl, tic, 0.0001, DateT); // рисуем уровни
            } 

            price.Text = Convert.ToString(BufferS[tic]); // вывод значений на кнопку  по времени
            buttonPriceSell.Text = Convert.ToString(BufferS[tic]); // вывод значений на кнопку  по времени
            buttonPriceBuy.Text = Convert.ToString(Buffer[tic]); // вывод значений на кнопку  по времени
            ZoomT(Zoom, tic); // Вызов метода  регулирования уровней времени
            tic++; // Подсчет тикового времени
            return tic;
        } // метод обновления данных

        /// <summary>
        /// Изменение маштаба
        /// </summary>
        /// <param name="tic">время прошедшее с запуска</param>
        /// <param name="zoomChart">Текущее приближени</param>
        public void ZoomT(int zoomChart, int tic)
        {
          if (checkBoxBinding.Checked == true)
            {
            graphic.ChartAreas[0].AxisX.Minimum = graphic.Series[4].Points[0].XValue - (zoomChart * 0.00001157407) + (tic * 0.00001157407); // ограничение по X минимум   NowTime
            graphic.ChartAreas[0].AxisX.Maximum = graphic.Series[4].Points[0].XValue + (zoomChart * 0.00001157407) + (tic * 0.00001157407); // ограничение по X максимум  под 60 секунд 0,00001157407
            }       
        } // функция необходимая для уровней рассмотренного времени

        /// <summary>
        ///  Вычисление линии SMA
        /// </summary>
        /// <param name="Dat">время прошедшее с запуска</param>
        /// <param name="Sglag">Текущее приближени</param>
        /// <param name="Buff">Текущее приближени</param>
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
            return sreds;
        } ////  вычисление точки СМА исправлено под буфер и чтения с файла

        /// <summary>
        ///  Строит линию на графике
        /// </summary>
        /// <param name="Dat">время прошедшее с запуска</param>
        /// <param name="Sglag">Текущее приближени</param>
        /// <param name="Buff">Текущее приближени</param>
        /// <param name="sreds">Текущее приближени</param>
        public void DrawSMA(List<DateTime> Dat, int Sglag, List<double> Buff, List<double> sreds)
        {
            if (checkBoxSMA.Checked == true)
            {
                if (Buff.Count >= Sglag)
                {
                    graphic.Series[3].Points.Clear();

                    for (int h = 0; h <= sreds.Count - 1; h++)
                    {
                        graphic.Series[3].XValueType = ChartValueType.Time;
                        graphic.Series[3].Color = Color.FromArgb(255, 100, 100); // задание цвета
                        graphic.Series[3].Points.AddXY(Dat[Sglag - 1 + h].ToOADate(), sreds[h]); // добавление точек в линию
                    }
                }
            }

            if (checkBoxSMA.Checked == false)
            {
                graphic.Series[3].Points.Clear();
            }
        }

        /// <summary>
        ///  Построение линий сопротивлений
        /// </summary>
        /// <param name="tic">время прошедшее с запуска</param>
        /// <param name="Buff">???</param>
        /// <param name="NowTime">Текущее время</param>
        public List<List<double>> IntervalResistance(int tic, List<double> Buffer, double NowTime) //// подавать интервал
        {
            int Pervoe = 0;
            int trend = 0; // переменная тренда
            List<List<double>> poin = new List<List<double>>(); // данные точки
            List<double> koordPoint = new List<double>();

            for (int i = 1; tic > i; i++)
            {
                if (trend != 1 && (Buffer[i - 1] - Buffer[i]) > 0)
                {
                    koordPoint = new List<double>(); // Координата точки
                    trend = 1; // положительный тренд
                    koordPoint.Add(i - 1); // заполнение точек по икс    
                    koordPoint.Add(Buffer[i - 1]); // заполнение точек по игрик  
                  
                    if (Pervoe == 0)
                    {
                        danoe = -1;
                        Pervoe++;
                    } // узнаем первое значение минимум оно или максимум
                    poin.Add(koordPoint); // добавление координаты точки
                }

                if (trend != -1 && (Buffer[i - 1] - Buffer[i]) < 0)
                { 
                    koordPoint = new List<double>();  // Координата точки
                    trend = -1; // положительный тренд
                    koordPoint.Add(i-1); // заполнение точек по икс    
                    PoinX.Add(i-1);
                    koordPoint.Add(Buffer[i]-1); // заполнение точек по игрик  
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

        /// <summary>
        ///  Построение линий сопротивлений
        /// </summary>
        /// <param name="MainV">Лист Значений котировок</param>
        /// <param name="MainT">Лист значений времени</param>
        public List<List<double>> MinMax( List<double> MainV, List<DateTime>  MainT)
        {
            int Pervoe = 0;
            int trend = 0; // переменная тренда
            List<List<double>> poin = new List<List<double>>(); // данные точки
            List<double> koordPoint = new List<double>();

            for (int i = 1; MainV.Count - 1 > i; i++)
            {
                if (trend != 1 && (MainV[i - 1] - MainV[i]) > 0)
                {
                    koordPoint = new List<double>(); // Координата точки
                    trend = 1; // положительный тренд
                    koordPoint.Add(i - 1); // заполнение точек по икс    
                    koordPoint.Add(MainV[i - 1]); // заполнение точек по игрик  

                    if (Pervoe == 0)
                    {
                        danoe = -1;
                        Pervoe++;
                    } // узнаем первое значение минимум оно или максимум
                    poin.Add(koordPoint); // добавление координаты точки
                }

                if (trend != -1 && (MainV[i - 1] - MainV[i]) < 0)
                {
                    koordPoint = new List<double>();  // Координата точки
                    trend = -1; // положительный тренд
                    koordPoint.Add(i - 1); // заполнение точек по икс    
                    koordPoint.Add(MainV[i - 1]); // заполнение точек по игрик  
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

        /// <summary>
        ///  Построение линий минимумов и максимумов
        /// </summary>
        /// <param name="poin">Точки минимумов и максимов</param>
        public void DrawMinMax(List<List<double>> poin)
        {
            if (checkBox4.Checked == true)
            {
                graphic.Series[5].Color = Color.FromArgb(255, 100, 100); // задание цвета
                graphic.Series[5].Points.Clear();
                graphic.Series[6].Color = Color.FromArgb(255, 100, 100); // задание цвета
                graphic.Series[6].Points.Clear();
                for (int i = 0; poin.Count - 1 > i; i++)
                {
                    if ((i % 2) == 0)
                    {
                        graphic.Series[5].Points.AddXY(quotations.TimeD[(int)poin[i][0]], poin[i][1]); // добавление точек в линию
                    }
                    if ((i % 2) == 1)
                    {
                        graphic.Series[6].Points.AddXY(quotations.TimeD[(int)poin[i][0]], poin[i][1]); // добавление точек в линию
                    }

                }

            }
            if (checkBox4.Checked == false)
            {
                graphic.Series[5].Points.Clear();
                graphic.Series[6].Points.Clear();
            }
        }

        /// <summary>
        /// Формирование линий сопротивлений
        /// </summary>
        /// <param name="poin">Точки минимумов и максимов</param>
        /// <param name="tic">Прошедшее время с начала запуска</param>
        /// <param name="pogr">Некоторая погрешность в пределах которой формируется линия</param>
        /// <param name="Date">Время в формате DateTime</param>
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

        /// <summary>
        /// Формирование линий поддержки
        /// </summary>
        /// <param name="poin">Точки минимумов и максимов</param>
        /// <param name="tic">Прошедшее время с начала запуска</param>
        /// <param name="pogr">Некоторая погрешность в пределах которой формируется линия</param>
        /// <param name="h">???</param>
        /// <param name="MINY">???</param>
        /// <param name="MINX">???</param>
        /// <param name="MinH">???</param>
        public double Support(List<List<double>> poin, int tic, double pogr, List<DateTime> Date, int h, double MINY, double MINX, double MinH) ////Метод уровня поддержки
        {
            int line2X;
            double line2Y;

            for (int g = 0; g < poin.Count; g++)
            {
                //// g != MinH Не просматривать точку, которая уже входит в уровень поддержки
                if (((MINY - poin[g][1]) >= -pogr) && g != MinH)
                {
                    graphic.Series[2].Points.Clear(); // Очистка линии
                    line2Y = poin[g][1]; // 2 точка по игрик
                    line2X = Convert.ToInt32(poin[g][0]); // 2 точка по икс                    
                    graphic.Series[2].XValueType = ChartValueType.Time; // Указывание типа координат по X
                    graphic.Series[2].Points.AddXY(Date[0].ToOADate(), line2Y); // 1 точка // можно использовать для начала line1X
                    graphic.Series[2].Points.AddXY(Date[tic].ToOADate(), line2Y); // 2 точка
                    graphic.Series[2].Color = Color.FromArgb(55, 0, 55); // задание цвета 
                }
            }
            //// сравнение точки в пределах данной погрешности
            if (MINY > (massYInetBuy[tic] + pogr)) 
            {
                graphic.Series[2].Points.Clear();
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
                    graphic.Series[1].Points.Clear(); // Очистка линии
                    line2Y = poin[g][1]; // 2 точка по игрик
                    line2X = Convert.ToInt32(poin[g][0]); // 2 точка по икс

                    graphic.Series[1].XValueType = ChartValueType.Time; // Указывание типа координат по X
                    graphic.Series[1].Points.AddXY(Date[0].ToOADate(), line2Y); // 1 точка // можно использовать для начала line1X
                    graphic.Series[1].Points.AddXY(Date[tic].ToOADate(), line2Y); // 2 точка
                    graphic.Series[1].Color = Color.FromArgb(255, 0, 0); // задание цвета 
                }
            }

            if (MAXY < (massYInetBuy[tic] - pogr))
            {
                graphic.Series[1].Points.Clear();
            } // если за рамками погрешности
            return MAXY;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">???</param>
        public void Window_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Action internetConnectAction = () =>
            {
                lock (sync)
                {
                    internetActionFinished = true;
                }
            };

            new Thread(new ThreadStart(internetConnectAction)).Start();
            t.Start();
            t.Interval = 1000; // время секунды
            t.Tick += new EventHandler(timer1_Tick); // прибавление времени       
        }

        /// <summary>
        /// Наведение мыши и показание координат и значения валюты в данной точке
        /// </summary>
        /// <param name="sender">???</param>
        public void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            double xS = x / 1920.0; // настройка под все  экраны
            double yS = y / 1080.0; // настройка под все  экраный

            if (e.Y > Convert.ToInt32(27 * yS * (WString.Y / fY)) && e.Y < Convert.ToInt32(629 * yS * (WString.Y / fY)) && e.X > Convert.ToInt32(110 * xS * (WString.X / fX)))
            {
                label_X.Location = new Point(Convert.ToInt32(110 * xS * (WString.X / fX)), e.Y); // перемещение линии по y
                label_Y.Location = new Point(e.X, Convert.ToInt32(30 * yS * (WString.Y / fY))); // перемещение линии по X
                lab_Cur.Location = new Point(Convert.ToInt32(700 * xS * (WString.X / fX)), e.Y); // привязка значения 
                double YCur = graphic.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                double XCur = graphic.ChartAreas[0].AxisX.PixelPositionToValue(e.X) / 0.000011574074074074;
                DateTime Date1 = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(XCur); // время в формате UNIX
                lab_Cur.Text = string.Concat(string.Concat(Math.Round(YCur, 5).ToString(), " , "), Date1.ToString("h:mm:ss.fff tt")); // перевод времени
            }
        } // вывод координат подправить

        /// <summary>
        /// Вращение колесика
        /// </summary>
        /// <param name="sender">???</param>
        public void chart1_MouseWheel(object sender, MouseEventArgs e) //// метод недоработан
        {
            if (e.Delta / 120 > 0)
            {
                switch (Leaves)
                {
                    case 1: Zoom -= 5;
                    if (Zoom > 60) 
                    { 
                        ZoomT(Zoom, tic); 
                    }
                        break;
                }
            }

            if (e.Delta / 120 < 0)
            {
                switch (Leaves)
                {
                    case 1: Zoom += 5; 
                    ZoomT(Zoom, tic); 
                    break;
                }
            } // прокрутка вниз
        }

        /// <summary>
        /// Вращение колесика
        /// </summary>
        /// <param name="sender">объект колесико</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (sync)
            {
                if (!internetActionFinished)
                {
                    return;
                }
            }
            double NowTime = SecondConect(); // метод по секундного обновления    
            GraphY EURUSD = new GraphY();
            EURUSD.Y(graphic, Buffer[Buffer.Count - 1]);
            Cursor = Cursors.Default;

            DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(NowTime); // время в формате UNIX
            DTIME.Add(Date); // добавление времени в лист          
            tic = Update(tic, DTIME, NowTime, DINET); // функция по секунде
        }

        /// <summary>
        /// Метод посекундного конекта
        /// </summary>
        /// <param name="sender">объект колесико</param>
        public double SecondConect()
        {
            if (!internetInitialized)
            {
                internetInitialized = true;
                string pathDirectory = Application.StartupPath; // Путь к директории
                string pathFile = pathDirectory + "\\" + value + ".txt"; // Путь к файлу c котировками eurusd
                // Первое прочтение в переменные
                if (tic == 0)
                {
                    StreamReader r2 = new StreamReader(pathFile);
                    string textRead = r2.ReadToEnd(); // получение прочтенной записи
                    r2.Close(); // закрыть чтение  
                    rEURUSD.read(textRead, massYInetBuy, massYInetSell, Times); // функция обработки текста присвоение глобальным переменным

                    DINET = cEURUSD.ConvertD(Times); // Конвертируем время из  формата UNIX в DataTime
                } 
            }

            double dTime = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds - 15; // Текущее время
            int NowTime;
            // текущее время
            NowTime = Convert.ToInt32(dTime);

            //  посекундные запросы к сайту
            if (InetConnect.Inet == true)
            {
                InternetRequest Request = new InternetRequest(NowTime, 1, value); // Класс запросов
                MatchCollection M; // коллекция распарсенных данных
                M = Request.InternetData(); // присвоение результата запроса

                // Присвоение к последнему числу в записи для обработки данных.
                if (tic == 0)
                {
                    poslchislo = massYInetSell[massYInetSell.Count - 1]; 
                    poslchislo1 = massYInetBuy[massYInetBuy.Count - 1]; 
                }


                if (M.Count > 0)
                {
                    Buffer.Add(Convert.ToDouble(M[1].Value)); // добавить в лист значения покупки     
                    BufferS.Add(Convert.ToDouble(M[2].Value)); // добавить в лист значения продажи   
                    poslchislo = Convert.ToDouble(M[1].Value); // последнее число в покупке 
                    poslchislo1 = Convert.ToDouble(M[2].Value); // последнее число в продаже
                }

                // Добавление в массив последнего числа (Так как на сервере новых записей не найдено)
                else
                {
                    Buffer.Add(poslchislo);
                    BufferS.Add(poslchislo1); 
                }

            }
            // Добавление в массив последнего числа из файла (Так как нет связи с сервером )
            else
            {
                Buffer.Add(massYInetSell[massYInetSell.Count - 1]); 
                BufferS.Add(massYInetBuy[massYInetBuy.Count - 1]);
            }
            return NowTime;
        }

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
            Deal Sdelka = new Deal(true, BufferS, tic);
            SELL.Add(Sdelka);  // Запомнить значение продажи          
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            Methods se = new Methods();
            Deal Sdelka = new Deal(false, BufferS, tic);
            BUY.Add(Sdelka);  // Запомнить значение продажи           
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
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 5;
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
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60 * 5;
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
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60 * 5 * 5;
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
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60 * 10 * 5;
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
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60 * 70 * 5;
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
            area.AxisX.MajorGrid.Interval = 1 / 24.0 * 5;
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
            area.AxisX.MajorGrid.Interval = 1 * 5;
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
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void OnClos(object sender, FormClosingEventArgs e)
        {
            MainForm.WindowClosingEURUSD = true;
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ToCloseTheDealToolStripMenuItem_Click(object sender, EventArgs e)
    {
        CloseDeal f = new CloseDeal();
        f.Owner = this;
        f.Show(); // Показать форму
    }

    }
}
#endregion