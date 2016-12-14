// проблемы с оптимизацией 
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
        delegate void MouseMo(object sender, MouseEventArgs e);
        public int loadPoint = 0;
        // создать объект сделка
        Quotes quotations = new Quotes();
        Task  tMinMax, tInterval;

        public ChartArea area = new ChartArea(); // Создание области
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        public int Leaves = 0, Zoom = 100;
        public double shag = 1; // интервал для координаты X
        public int danoe = 0;
        public int colvo = 0;
        public string value;

        public double poslchisloSell;
        public double poslchisloBuy;
        public bool inet;
        public bool poluch = false;

        public List<Deal> BUY = new List<Deal>();
        public List<Deal> SELL = new List<Deal>();
        List<double> massY = new List<double>();
        List<double> massYInetBuy = new List<double>(); // лист значений отношения валюты по покупке из файла
        List<double> massYInetSell = new List<double>(); // лист значений отношения валюты по продаже из файла
        List<int> Times = new List<int>(); // лист времени
        public List<double> BufferB = new List<double>();
        public List<double> BufferS = new List<double>();

        // буфферное время
        public List<DateTime> DBUF = new List<DateTime>();
        // время из интернета
        public List<DateTime> DINET = new List<DateTime>();

        public List<double> sred = new List<double>(); // Точки по X SMA
        public int tic = 0; // Переменная отслеживающая кол-во прошедших секунд с запуска формы
        private object sync = new object();
        private bool internetActionFinished = false;
        private bool internetInitialized = false;
        Internet IPair = new Internet();
        Methods cEURUSD = new Methods(); // Класс методов
        #region Параметры текущей формы
        // высота экрана
        int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height;
         // ширина экрана 
        int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
        // ширина чарта
        int chatSizeX = 1058;
        // высота чарта
        int chatSizeY = 684;
        // создание объекта обработки текста.
        WorkFile rEURUSD = new WorkFile(); 

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
            // настройка под все  экраны
            double xS = x / 1920.0;
            // настройка под все  экраны
            double yS = y / 1080.0;

            this.InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(OnClos);

            graphic = new Chart();
            graphic.Parent = this;
            this.Text = value;

            area.Name = "myGraph";
            // доработать интервал по координате X 1= 1 день
            area.AxisX.MajorGrid.Interval = shag;
            // размещение чарта
            graphic.Location = new Point(0, 10);
            this.graphic.Size = new System.Drawing.Size(Convert.ToInt32(chatSizeX * xS * (WString.X / fX)), Convert.ToInt32(chatSizeY * yS * (WString.X / fX))); // размеры чарта
            // передача
            graphic.ChartAreas.Add(area);
            // замена присвоение точек через переопределение дисплея.
            #region Задание параметров кнопкам button9, button8, button10, button1, button7  
            buttonBuy.Location = Display.customizedPoint(1100, 27); // клавиша buy
            buttonPriceSell.Location = Display.customizedPoint(1157, 27); // клавиша value
            buttonSell.Location = Display.customizedPoint(1274, 27); // клавиша  sell        
            price.Location = Display.customizedPoint(1216, 72); // клавиша price               
            buttonPriceBuy.Location = Display.customizedPoint(1100, 72); // клавиша value

            buttonBuy.rusLan = "Покупка";
            buttonBuy.engLan = "Buy";
            buttonSell.rusLan = "Продажа";
            buttonSell.engLan = "Sell";
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

            buttonSell.Size = new Size(Convert.ToInt32((ButtonSize + shift * 2 / 3.0) * SizeDisplayX), Convert.ToInt32(46 * SizeDisplayY));
            price.Size = new Size(Convert.ToInt32((ButtonSize + 60 + shift * 2 / 3.0) * SizeDisplayX), Convert.ToInt32(46 * SizeDisplayY));
            buttonBuy.Size = new Size(Convert.ToInt32((ButtonSize + shift) * SizeDisplayX), Convert.ToInt32(46 * SizeDisplayY));
            buttonPriceBuy.Size = new Size(Convert.ToInt32((ButtonSize + 60 + shift) * SizeDisplayX), Convert.ToInt32(46 * SizeDisplayY));
            buttonPriceSell.Size = new Size(Convert.ToInt32((ButtonSize + 60 + shift * 2 / 3.0) * SizeDisplayX), Convert.ToInt32(46 * SizeDisplayY));
            numericUpDown1.Size = new Size(Convert.ToInt32(117 * SizeDisplayX), Convert.ToInt32(20 * SizeDisplayY));
            #endregion
            // Чекбокс отвечающий за привязку графика к середине включен
            checkBoxBinding.Checked = true;
            // Вызов метода объявления линий
            ChartQ S = new ChartQ();
            graphic = S.Quote(graphic, value);

            #region вызов Методов локализации формы
            // локализация всплывающих подсказок
            tTip();
            // переводчик  кнопок
            Button();
            // переводчик меню  
            Menu();       
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
                // Снять отметку
                menuItem.CheckState = CheckState.Unchecked;
            }
            else if (menuItem.CheckState == CheckState.Unchecked)
            {
                // поставить отметку
                menuItem.CheckState = CheckState.Checked;
            }
        }

        /// <summary>
        /// Метод перевода тултипов
        /// </summary>
        public void tTip()
       {
            if ( true == WString.Langue["ENG"])
            {
                toolTip1.SetToolTip(checkBoxLevelSupandResis, "Click to activate the display of support and resistance levels.");
                toolTip2.SetToolTip(checkBoxSMA, "Click to activate the displaying of the moving line.");
                toolTip3.SetToolTip(checkBoxLineCoord, "Press to activate the display lines value at the point.");               
            }

            if ( true == WString.Langue["RUS"])
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
            if (true == WString.Langue["ENG"])
            {
                lang = "eng";
            }
            if ( true == WString.Langue["RUS"])
            {
                lang = "rus";
            }
            buttonBuy.Translate(lang);
            // текст клавиши покупки
            buttonSell.Translate(lang);
            // текст клавиши продажи
            buttonPriceSell.Translate(lang);
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

            if ( true == WString.Langue["ENG"])
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

            if (true == WString.Langue["RUS"])
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
        /// Метод задания параметров линии и  обновления данных
        /// </summary>
        /// <param name="tic">время прошедшее с запуска</param>
        /// <param name="DateT">????</param>
        /// <param name="NowTime">Текущее время</param>
        /// <param name=" DINET">Время в результате запуска</param>
        public int Update(int tic, List<DateTime> DateT, double NowTime, List<DateTime> DINET)
        {
            #region вызов Методов локализации формы
            tTip(); // локализация всплывающих подсказок
            Button(); // переводчик  кнопок
            Menu(); // переводчик меню 
            #endregion

            graphic.MouseWheel += new MouseEventHandler(this.chart1_MouseWheel); // событие вращения колесика
            List<List<double>> poinl = new List<List<double>>(); // Точки изменения тренда
            LineCoord L = new LineCoord(x, y, fX, fY, lab_Cur, label_Y, label_X, ref graphic);
            L.Show(checkBoxLineCoord);

            if (checkBoxLevelSupandResis.Checked == false)
            {
                graphic.Series[2].Points.Clear();
                // Очистка точек линий
                graphic.Series[1].Points.Clear();
            }

            #region Склейка даннях из файла с буфером
            // все изменения с временными интервалами и тому подобным должны производиться через БД!!!
            // Загрузка времени из файла
            if(1 == tic || 0 == tic)
            {
                Splice mReqest = new Splice();
                graphic.Series[0].Points.Clear();
                quotations.TimeD = mReqest.MainTime(DINET, DateT, tic, 10000);
                // Загрузка значения из файла 
                quotations.Sell = mReqest.MainValue(BufferB, massYInetSell, tic, 10000);
                for (int hl = 0; quotations.TimeD.Count - 1 >= hl; hl++)
                {
                        graphic.Series[0].Points.AddXY(quotations.TimeD[hl].ToOADate() , quotations.Sell[hl]);
                }

            }
            else
            {
                quotations.TimeD.Add(DateT[DateT.Count - 1]);
                quotations.Sell.Add(massYInetSell[massYInetSell.Count - 1]);
            }

            graphic.Series[0].Points.AddXY(DateT[DateT.Count - 1].ToOADate(), massYInetSell[massYInetSell.Count - 1]);

            // запускаем в другом потоке вычисление нового графика zoom
            /* zooming = Task.Run(() =>
             {
                 // запускаем в другом потоке
                 ZoomS z = new ZoomS(cEURUSD.ConvertU(quotations.TimeD), 60);
             });
             */

            #endregion
            // создаем костыль для графика чарт
            graphic.Series[4].Points.AddXY(DateT[0].ToOADate(), BufferB[0]);

            // обновление данных графика
            graphic.Update();

            // Получение точек смены тренда
            tInterval = Task.Run(() =>
            {
                poinl = IntervalResistance(tic, BufferB, NowTime);  
            });
            // рисуем уровни
            if (true == checkBoxLevelSupandResis.Checked)
            {
                Resis(poinl, tic, 0.0001, DateT); 
            }

            // проблема с 100к необходимо оптимизировать
            if (true == checkBoxSMA.Checked)
            {
                // Определяем объеrnf 
                ClassSMA Sred = new ClassSMA();
                // Добавление точек
                Sred.Add((int)numericUpDown1.Value, quotations.Sell);
                // рисовать скользящую кривую
                DrawSMA(quotations.TimeD, (int)numericUpDown1.Value, quotations.Sell, Sred.GetSred());
            }
            if( true == checkBox4.Checked)
            {
                MinMax LineMinMax = new MinMax();
                List<List<double>> listMinMax = new List<List<double>>();
                // Минимумы и максимумы
                tMinMax = Task.Run(() =>
                {
                    LineMinMax.AddMinMax(quotations.Sell, quotations.TimeD, danoe);
                });
                // Точки миниму и максимума
                Task.WaitAll(tMinMax, tInterval);
                // рисовать минимум максимум
                DrawMinMax(LineMinMax.GetMinMax());
            }
           

            price.Text = Convert.ToString(BufferS[tic]); // вывод значений на кнопку  по времени
            buttonPriceSell.Text = Convert.ToString(BufferS[tic]); // вывод значений на кнопку  по времени
            buttonPriceBuy.Text = Convert.ToString(BufferB[tic]); // вывод значений на кнопку  по времени
            ZoomT(Zoom, tic); // Вызов метода  регулирования уровней времени
            tic++; // Подсчет тикового времени
            return tic;
        }

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
        }

        /// <summary>
        ///  Строит линию на графике
        /// </summary>
        /// <param name="Dat">время прошедшее с запуска</param>
        /// <param name="Sglag">Текущее приближени</param>
        /// <param name="Buff">Текущее приближени</param>
        /// <param name="sreds">Текущее приближени</param>
        public void DrawSMA(List<DateTime> Dat, int Sglag, List<double> Buff, List<double> sreds)
        {
            if (true == checkBoxSMA.Checked)
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
        }

        /// <summary>
        ///  Построение линий минимумов и максимумов
        /// </summary>
        /// <param name="poin">Точки минимумов и максимов</param>
        public void DrawMinMax(List<List<double>> poin)
        {
            if (checkBox4.Checked == true)
            {
                // задание цвета линии Max
                iniLineMinMax(graphic, 5);

                // задание цвета линии Min
                iniLineMinMax(graphic, 6);

                for (int i = 0; poin.Count - 1 > i; i++)
                {
                    if ((i % 2) == 0)
                    {
                        // добавление точек в линию
                        graphic.Series[5].Points.AddXY(quotations.TimeD[(int)poin[i][0]], poin[i][1]); 
                    }
                    if ((i % 2) == 1)
                    {
                        // добавление точек в линию
                        graphic.Series[6].Points.AddXY(quotations.TimeD[(int)poin[i][0]], poin[i][1]);
                    }
                }

            }
        }

        /// <summary>
        /// Задание цвета линии
        /// </summary>
        /// <param name="graphic">Точки минимумов и максимов</param>
        /// <param name="index">Прошедшее время с начала запуска</param>
        public void iniLineMinMax(Chart graphic, int index)
        {
            graphic.Series[index].Color = Color.FromArgb(255, 100, 100);
            graphic.Series[index].Points.Clear();
        }


        /// <summary>
        ///  Подавать интервал
        /// </summary>
        /// <param name="tic">время прошедшее с запуска</param>
        /// <param name="Buff">???</param>
        /// <param name="NowTime">Текущее время</param>
        public List<List<double>> IntervalResistance(int tic, List<double> Buffer, double NowTime)
        {
            int Pervoe = 0;
            // переменная тренда
            int trend = 0;
            // данные точки
            List<List<double>> poin = new List<List<double>>();
            List<double> koordPoint = new List<double>();

            for (int i = 1; tic > i; i++)
            {
                if (trend != 1 && (Buffer[i - 1] - Buffer[i]) > 0)
                {
                    // Координата точки
                    koordPoint = new List<double>();
                    // положительный тренд
                    trend = 1;
                    // заполнение точек по икс  
                    koordPoint.Add(i - 1);
                    // заполнение точек по игрик
                    koordPoint.Add(Buffer[i - 1]);
                    // узнаем первое значение минимум оно или максимум
                    if (Pervoe == 0)
                    {
                        danoe = -1;
                        Pervoe++;
                    }
                    // добавление координаты точки
                    poin.Add(koordPoint);
                }

                if (trend != -1 && (Buffer[i - 1] - Buffer[i]) < 0)
                {
                    // Координата точки
                    koordPoint = new List<double>();
                    // положительный тренд
                    trend = -1;
                    // заполнение точек по икс
                    koordPoint.Add(i - 1);  
                    PoinX.Add(i - 1);
                    // заполнение точек по игрик 
                    koordPoint.Add(Buffer[i] - 1);
                    // узнаем первое значение минимум оно или максимум
                    if (Pervoe == 0)
                    {
                        danoe = 1;
                        Pervoe++;
                    }
                    // добавление координаты точки
                    poin.Add(koordPoint);
                }
            }
            return poin;
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
                    prisvoenie(h, MAXY, poin, MAXX, MaxH);
                }
                if (MINY > poin[h][1])
                {
                    prisvoenie(h, MINY, poin, MINX, MinH);
                }
                // Нахождение точки максимума удовлетворяющему условию разброса и повторения более 2 раз
                MAXY = Resistance(poin, tic, pogr, Date, h, MAXY, MAXX, MaxH);
                // Нахождение точки минимума удовлетворяющему условию разброса и повторения более 2 раз
                MINY = Support(poin, tic, pogr, Date, h, MINY, MINX, MinH);
            }
        } // Вызов 2х методов для рисования и получения значений MAXY, MINY по которым происходит построение линий

        /// <summary>
        /// Получение точек путем сравнивания предыдущих точек
        /// </summary>
        /// <param name="h">Точки минимумов и максимов</param>
        /// <param name="Y">Прошедшее время с начала запуска</param>
        /// <param name="poin">Некоторая погрешность в пределах которой формируется линия</param>
        /// <param name="X">Время в формате DateTime</param>
        public void prisvoenie(int h, double Y, List<List<double>> poin, double X, double Mh)
        {
                Y = poin[h][1];
                X = poin[h][0];
                Mh = h;
        }

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
            // метод по секундного обновления. Узнаем сколько необходимо добавить секунд для преобразования в DTIME   
            double NowTime = SecondConect() + 16 * 3600;  
            GraphY EURUSD = new GraphY();
            EURUSD.Y(graphic, BufferB[BufferB.Count - 1]);
            // рнуть к дефолтному состоянию
            Cursor = Cursors.Default;
            // время в формате из DateTime в UnixTime ТЕКУЩЕЕЕ ВРЕМЯ
            DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(NowTime);
            // часовой пояс
            DBUF.Add(Date); // добавление времени в лист  
            //         
                tic = Update(tic, DBUF, NowTime, DINET); // функция по секунде
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
                    // получение прочтенной записи
                    string textRead = r2.ReadToEnd();
                    // закрыть чтение
                    r2.Close();
                    // функция обработки текста присвоение глобальным переменным
                    Times = rEURUSD.read(textRead, massYInetBuy, massYInetSell);
                     // Конвертируем время из  формата UNIX в DataTime
                    DINET = cEURUSD.ConvertD(Times);
                } 
            }

            double dTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds - 15; // Текущее время
            int NowTime;
            // текущее время
            NowTime = Convert.ToInt32(dTime);

            //  посекундные запросы к сайту
            if (InetConnect.Inet == true)
            { 
                // Класс запросов
                InternetRequest Request = new InternetRequest(NowTime, 1, value);
                 // коллекция распарсенных данных
                MatchCollection M;
                // присвоение результата запроса
                M = Request.InternetData();

                // Присвоение к последнему числу в записи для обработки данных.
                if (tic == 0)
                {
                    poslchisloSell = massYInetSell[massYInetSell.Count - 1]; 
                    poslchisloBuy = massYInetBuy[massYInetBuy.Count - 1]; 
                }


                if (M.Count > 0)
                {
                    BufferB.Add(Convert.ToDouble(M[1].Value)); // добавить в лист значения покупки     
                    BufferS.Add(Convert.ToDouble(M[2].Value)); // добавить в лист значения продажи   
                    poslchisloSell = Convert.ToDouble(M[1].Value); // последнее число в покупке 
                    poslchisloBuy = Convert.ToDouble(M[2].Value); // последнее число в продаже
                }

                // Добавление в массив последнего числа (Так как на сервере новых записей не найдено)
                else
                {
                    BufferB.Add(poslchisloSell);
                    BufferS.Add(poslchisloBuy); 
                }

            }
            // Добавление в массив последнего числа из файла (Так как нет связи с сервером )
            else
            {
                BufferB.Add(massYInetSell[massYInetSell.Count - 1]); 
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

        private void Sell_Click(object sender, EventArgs e)
        {
            Methods se = new Methods();
            Deal Sdelka = new Deal(true, BufferS, tic);
            // Запомнить значение продажи  
            SELL.Add(Sdelka);        
        }

        private void Buy_Click(object sender, EventArgs e)
        {
            Methods se = new Methods();
            Deal Sdelka = new Deal(false, BufferB, tic);
            // Запомнить значение покупки  
            BUY.Add(Sdelka);      
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
            // прибавление тикового времени
            t.Tick -= new EventHandler(timer1_Tick);
        }
        
        private void lab_Cur_Click(object sender, EventArgs e)
        {
        }


        #region параметры уровней времени
        /// <summary>
        /// секундный уровень
        /// </summary>
        /// <param name="sender">объект меню</param>
        private void SecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Leaves = 1;
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 5;
            Zoom = 60;
            // активировать заметку
            Activ(sender);
            // убрать отметку минутный уровень
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked;
            // убрать отметку 30 минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked;
            // убрать отметку часовой уровень
            hourToolStripMenuItem.CheckState = CheckState.Unchecked;
            // убрать отметку недельный уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked;
            // убрать отметку дневной уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked;
            // убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked;
        }

        /// <summary>
        /// минутный уровень
        /// </summary>
        /// <param name="sender">объект меню</param>
        private void MinutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60 * 5;
            Zoom = 1800;
            Activ(sender);
            // убрать отметку секундный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked;
            // убрать отметку 30 минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked;
            // убрать отметку часовой уровень
            hourToolStripMenuItem.CheckState = CheckState.Unchecked;
            // убрать отметку недельный уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked;
            // убрать отметку дневной уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked;
            // убрать отметку месячный уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked;
        } // 5 минутный уровень 

        /// <summary>
        /// минутный уровень
        /// </summary>
        /// <param name="sender">объект меню</param>
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

        /// <summary>
        /// часовый уровень
        /// </summary>
        /// <param name="sender">объект меню</param>
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
        }

        /// <summary>
        /// недельный уровень
        /// </summary>
        /// <param name="sender">объект меню</param>
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
        }

        /// <summary>
        /// дневной уровень
        /// </summary>
        /// <param name="sender">объект меню</param>
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
        }

        /// <summary>
        /// дмесячный уровень
        /// </summary>
        /// <param name="sender">объект меню</param>
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
        }
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
        }


        private void checkBoxSMA_Checked(object sender, EventArgs e)
        {
            // стирает линию при неактивности чекбокс
            if (checkBoxSMA.Checked == false)
            {
                graphic.Series[3].Points.Clear();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {          
        }

        private void checkBox4_Check(object sender, EventArgs e)
        {
            if (checkBox4.Checked == false)
            {
                graphic.Series[5].Points.Clear();
                graphic.Series[6].Points.Clear();
            }
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
        // задание размеров окна отчета
        rep.Size = new Size(650, 455);
        rep.Show(); // Показать  окно отчета
    }

    private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
    {
        EURUSD fset = new EURUSD();
        fset.Show();
        // задание размера формы 
        fset.Size = new Size(580, 580); 
    }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Форма для закрытия сделок
        /// </summary>
        /// <param name="sender">объект меню</param>
        private void ToCloseTheDealToolStripMenuItem_Click(object sender, EventArgs e)
    {
        CloseDeal DealClose = new CloseDeal();
        DealClose.Owner = this;
        // Показать форму
        DealClose.Show();
    }

    }
}
#endregion