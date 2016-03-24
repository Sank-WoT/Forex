﻿using System;
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
    public delegate void CountDelegeate(object sender, EventArgs e);// работа с делегатами
   
    
    public partial class Window : Form
    {//  
        //Calsulations calculations = new Calsulations(); додумать
        ChartArea area = new ChartArea();// Создание области
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        int size = 30, Leaves = 0, Zoom = 0;
        double Zic = 0;
        double shag = 1, BUY = 0,SELL = 0;//интервал для координаты X
        int danoe = 0;
        int colvo = 0;
        List<double> massY = new List<double>();
        List<double> massYInetA = new List<double>();
        List<double> Times = new List<double>();
        List<double> massYInetB = new List<double>();
        List<DateTime> D = new List<DateTime>();
        List<double> sred = new List<double>();
        double poslTime = 0;// последнее время
        string[] value = new string[1] { "eurusd" };
        int tic = 0;
        int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; //высота экрана
        int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; //ширина экрана 
        double fX = 1366;//стандартный размер формы
        double fY = 757;//стандартный размер формы
        int cX = 1058;
        int cY = 684;
        List<int> PoinX = new List<int>();//данные точки

        

        public double Conect(string value, double poslTime, int limit)
        {
            Console.WriteLine(WSrting.ENG);
            string pathDirectory = Application.StartupPath;//Путь к директории
            if (!Directory.Exists(pathDirectory))//Проверка  на существование директории
            {
                Directory.CreateDirectory(pathDirectory);// создание директории 
                MessageBox.Show("Директория создана путь : " + pathDirectory);// сообщение о создании директории
            }
            string pathFile = pathDirectory + "\\" + value + ".txt";//Путь к файлу c котировками eurusd

            if (!File.Exists(pathFile))//проверка на существование файла
            {
                FileInfo writel = new FileInfo(pathFile);//получаем путь 
                StreamWriter l = writel.CreateText();//создаем текст
                l.Close();//закрыть запись
            } //Развертывание сервера в заранее известном каталоге 
            StreamReader r = new StreamReader(pathFile);
            string text = r.ReadToEnd();// получение прочтенной записи
            r.Close();//закрыть чтение   
            Regex regex1 = new Regex(@"(\d{10,20})");//регулярное выражение для поиска последнего времени в файле
            MatchCollection m1 = regex1.Matches(text);
            if (m1.Count != 0)
            {
                poslTime = Convert.ToDouble(m1[m1.Count - 1].Value);
                Console.WriteLine("poslTime");
                Console.WriteLine(poslTime);
            }//недопускает присвоение при значении прочтенного в файле меньше 1.


            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");// преобразовение
            // WebProxy wp = new WebProxy("151.236.216.251", 10000); //задаем параметры прокси

            WebRequest webReq = WebRequest.Create("http://currency-dred95.rhcloud.com/get_currency.php?time=" + poslTime + "&limit=" + limit + "&sign=" + value);//запрос на сайт 
            //webReq.Proxy = wp;//меняем на прокси
            WebResponse webRes = webReq.GetResponse();//получение ответа
            Stream st = webRes.GetResponseStream();//поток по которому получаем инфу
            StreamReader sr = new StreamReader(st);//прочитать поток

            StreamReader r1 = new StreamReader(pathFile);
            string text1 = r1.ReadToEnd();// получение прочтенной записи
            r1.Close();//закрыть чтение 
            string response = sr.ReadToEnd(); // присвоение прочтенного к стринг 
            text1 += response;// добавление в  файл новых значений
            FileInfo write = new FileInfo(pathFile);//получаем путь 
            StreamWriter w = write.CreateText();//создаем текст  
            w.WriteLine(text1);//добавляем текст 
            w.Close();//закрыть запись 

            Regex regex = new Regex(@"((\d{10,20})|(\d{1,20})\.(\d{1,4}))");//регулярное выражение 
            MatchCollection m = regex.Matches(text1);
            for (int h = 0; h < limit; h++)
            {
                response = m[colvo].Value;//Время
                colvo++;//порядковый номер даты в списке
                Times.Add(Convert.ToDouble(response));//Время в UNIX

                response = m[colvo].Value;//Время
                colvo++;//порядковый номер даты в списке
                massYInetB.Add(Convert.ToDouble(response));

                response = m[colvo].Value;//Время
                colvo++;//порядковый номер даты в списке
                massYInetA.Add(Convert.ToDouble(response));
            }
            return poslTime;
        }//Получить  данные  с собственного сайта
        //модифицировать для получения файлов





        public void Activ(object sender)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem.CheckState == CheckState.Checked)
            {
                menuItem.CheckState = CheckState.Unchecked;// Снять отметку
            }

            else if (menuItem.CheckState == CheckState.Unchecked)
            {
                menuItem.CheckState = CheckState.Checked;// поставить отметку
            }
        }


       public void CheckBox()
        {
            if (WSrting.ENG == true)
            {
                checkBox1.Text = "Levels suport and resistance";
                checkBox2.Text = "SMA";
                checkBox3.Text = "Line coordinates"; 
                label1.Text = "Methods";
                label2.Text = "Tools";
            }
            if (WSrting.RUS == true)
            {
                checkBox1.Text = "Уровни сопротивления и поддержки";
                checkBox2.Text = "Скользящая кривая";
                checkBox3.Text = "Координатные линии";
                label1.Text = "Meтоды";
                label2.Text = "Инструменты";
            }          
        }// перевод методов и инструментов
        

        public void tTip()
        {
            string TText;           
            toolTip1.AutoPopDelay = 3000;//
            toolTip1.InitialDelay = 1000;//
            toolTip1.ReshowDelay = 1000;//время сколько показывается надпись
            toolTip1.ShowAlways = true;

            toolTip2.AutoPopDelay = 3000;//
            toolTip2.InitialDelay = 1000;//
            toolTip2.ReshowDelay = 1000;//время сколько показывается надпись
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

            toolTip3.AutoPopDelay = 3000;//
            toolTip3.InitialDelay = 1000;//
            toolTip3.ReshowDelay = 1000;//время сколько показывается надпись
            toolTip3.ShowAlways = true;
        }


   
       public void Button()
        {
            if (WSrting.ENG == true)
            {
                button10.Text = "Sell";// текст клавиши покупки
                button9.Text = "Buy";// текст клавиши покупки
                button1.Text = "Value Buy";// текст клавиши покупки
                button7.Text = "Value Sell";// текст клавиши покупки
               
            }
            if (WSrting.RUS == true)
            {
                button9.Text = "Покупка";// текст клавиши покупки 
                button10.Text = "Продажа";// текст клавиши покупки 
                button1.Text = "Значение покупки";// текст клавиши покупки
                button7.Text = "Значение продажа";// текст клавиши покупки
            }
        }

        public void Menu()
        {
            if (WSrting.ENG == true)
            {
               timeLevelToolStripMenuItem.Text = "Time intervall";
               reportToolStripMenuItem.Text = "Report";
               createReportToolStripMenuItem.Text = "Creaty report";
               secondToolStripMenuItem.Text = "Second";
               minutesToolStripMenuItem.Text = "5 Minutes";
               minutesToolStripMenuItem1.Text = "30 Minutes";
               hourToolStripMenuItem.Text = "Hour";
               dayToolStripMenuItem.Text = "Day";
               weekToolStripMenuItem.Text = "Week";
               monthToolStripMenuItem.Text = "Month";
            }
            if (WSrting.RUS == true)
            {
               timeLevelToolStripMenuItem.Text = "Временные интервалы";
               reportToolStripMenuItem.Text = "Отчет";
               createReportToolStripMenuItem.Text = "Создать отчет";
               secondToolStripMenuItem.Text = "Секундный";
               minutesToolStripMenuItem.Text = "5 Минут";
               minutesToolStripMenuItem1.Text = "30 Минут";
               hourToolStripMenuItem.Text = "Час";
               dayToolStripMenuItem.Text = "День";
               weekToolStripMenuItem.Text= "Неделя";
               monthToolStripMenuItem.Text = "Месяц";
            }
        }



        public void MAXMIN(List<List<double>> poin, List<DateTime> Date)
        {
            if (poin.Count > 1)
            {
              
                if(danoe == 1)
                {
                   
                    for (int i = 1; i < poin.Count - 2; i++ )
                    {
                        if (((i) / 2 ) == 1)
                        {
                            chart1.Series[4].XValueType = ChartValueType.Time;
                            chart1.Series[4].Points.AddXY(Date[PoinX[i]], poin[i][1]);// 1 точка // можно использовать для начала line1X
                            chart1.Series[4].Color = Color.FromArgb(0, 0, 0);// задание цвета 
                        }
                        if (((i) / 2) == 0)
                        {
                            chart1.Series[5].XValueType = ChartValueType.Time;
                            chart1.Series[5].Points.AddXY(Date[PoinX[i]], poin[i][1]);// 1 точка // можно использовать для начала line1X
                            chart1.Series[5].Color = Color.FromArgb(0, 0, 0);// задание цвета 
                        }  
                    }
                        
                }

                if (danoe == -1)
                {
                    for (int i = 1; i < poin.Count - 2; i++)
                    {
                        if (((i) / 2) == 0)
                        { 
                            chart1.Series[4].XValueType = ChartValueType.Time;
                            chart1.Series[4].Points.AddXY(Date[PoinX[i]], poin[i][1]);// 1 точка // можно использовать для начала line1X
                            chart1.Series[4].Color = Color.FromArgb(0, 0, 0);// задание цвета 
                        }
                        if (((i) / 2) == 1)
                        {
                            chart1.Series[5].XValueType = ChartValueType.Time;
                            chart1.Series[5].Points.AddXY(Date[PoinX[i]], poin[i][1]);// 1 точка // можно использовать для начала line1X
                            chart1.Series[5].Color = Color.FromArgb(0, 0, 0);// задание цвета 
                        }
                    }
                }
            }
        }// додумать



        public List<List<double>> Resistance(int tic)
        {
            int Pervoe = 0;
            int trend = 0;//переменная тренда
            List<List<double>> poin = new List<List<double>>();//данные точки
            List<double> row = new List<double>();

            for (int i = 1; tic > i; i++)
            {
                if (trend != 1 && (massYInetB[i - 1] - massYInetB[i]) > 0)
                {
                    row = new List<double>();
                    trend = 1;//положительный тренд
                    row.Add(i - 1);//заполнение точек по икс    
                    row.Add(massYInetB[i - 1]);//заполнение точек по игрик  
                    if (Pervoe == 0)
                    {
                        danoe = -1;
                        Pervoe++;
                    }// узнаем первое значение минимум оно или максимум
                    poin.Add(row);
                }
                if (trend != -1 && (massYInetB[i - 1] - massYInetB[i]) < 0)
                {
                    if (Pervoe == 0)
                    {
                        danoe = 1;
                        Pervoe++;
                    }// узнаем первое значение минимум оно или максимум
                    row = new List<double>();
                    trend = -1;//положительный тренд
                    row.Add(i);//заполнение точек по икс    
                    PoinX.Add(i);
                    row.Add(massYInetB[i]);//заполнение точек по игрик  
                    poin.Add(row);
                }
            }
            return poin;
        }










        public  Window()
        {    
            double  fX = 1366;
            double fY = 757;
            double xS = (x / 1920.0);//настройка под все  экраны
            double yS = (y / 1080.0);//настройка под все  экраный
            for (int m = 0; m < 1; m++)
            {
                poslTime = Conect(value[0], poslTime, 30);// соединение 
            }
            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(OnClos);
            
            chart1 = new Chart();// Создание чарта
            chart1.Parent = this;
          
            area.Name = "myGraph";
            area.AxisX.MajorGrid.Interval = shag;// доработать интервал по координате X 1= 1 день
            chart1.Location = new Point(0,10);// размещение чарта
            this.chart1.Size = new System.Drawing.Size(Convert.ToInt32(cX * xS * (WSrting.X / fX)), Convert.ToInt32(cY * yS * (WSrting.X / fX)));//размеры чарта
            chart1.ChartAreas.Add(area);// передача 

            button9.Location = new Point(Convert.ToInt32(1100 * xS * (WSrting.X /fX )), Convert.ToInt32(15 * yS * ( WSrting.Y/fY )));// клавиша buy         
            button8.Location = new Point(Convert.ToInt32(1158 * xS * (WSrting.X / fX)), Convert.ToInt32(15 * yS * (WSrting.Y / fY)));// клавиша price     
            button10.Location = new Point(Convert.ToInt32(1274 * xS * (WSrting.X / fX)), Convert.ToInt32(15 * yS * (WSrting.Y / fY)));// клавиша sell   
            button1.Location = new Point(Convert.ToInt32(1100 * xS * (WSrting.X / fX)), Convert.ToInt32(60 * yS * (WSrting.Y / fY)));// клавиша value
            button7.Location = new Point(Convert.ToInt32(1216 * xS * (WSrting.X / fX)), Convert.ToInt32(60 * yS * (WSrting.Y / fY)));// клавиша value

            button9.Size = new Size(Convert.ToInt32(58 * xS * (WSrting.X / fX)), Convert.ToInt32(46 * yS * (WSrting.Y / fY)));
            button8.Size = new Size(Convert.ToInt32(118 * xS * (WSrting.X / fX)), Convert.ToInt32(46 * yS * (WSrting.Y / fY)));
            button10.Size = new Size(Convert.ToInt32(58 * xS * (WSrting.X / fX)), Convert.ToInt32(46 * yS * (WSrting.Y / fY)));
            button1.Size = new Size(Convert.ToInt32(117 * xS * (WSrting.X / fX)), Convert.ToInt32(46 * yS * (WSrting.Y / fY)));
            button7.Size = new Size(Convert.ToInt32(117 * xS * (WSrting.X / fX)), Convert.ToInt32(46 * yS * (WSrting.Y / fY)));

            label1.Location = new Point(Convert.ToInt32(1098 * xS * (WSrting.X / fX)), Convert.ToInt32(118 * yS * (WSrting.Y / fY)));
            label2.Location = new Point(Convert.ToInt32(1098 * xS * (WSrting.X / fX)), Convert.ToInt32(280 * yS * (WSrting.Y / fY)));

            checkBox1.Location = new Point(Convert.ToInt32(1101 * xS * (WSrting.X / fX)), Convert.ToInt32(149 * yS * (WSrting.Y / fY)));
            checkBox2.Location = new Point(Convert.ToInt32(1101 * xS * (WSrting.X / fX)), Convert.ToInt32(172 * yS * (WSrting.Y / fY)));
            checkBox3.Location = new Point(Convert.ToInt32(1101 * xS * (WSrting.X / fX)), Convert.ToInt32(305 * yS * (WSrting.Y / fY)));
            checkBox4.Location = new Point(Convert.ToInt32(1101 * xS * (WSrting.X / fX)), Convert.ToInt32(195 * yS * (WSrting.Y / fY)));

            Button();
        }
  

        public void Graph()
        {  

            Series series1 = new Series();
            series1.ChartArea = "myGraph";
            series1.XValueType = ChartValueType.DateTime;
            series1.ChartType = SeriesChartType.Line;
            series1.BorderWidth = 4;
            chart1.Series.Add(series1);// параметры главной линии

            Series series2 = new Series();
            series2.ChartArea = "myGraph";
            series2.XValueType = ChartValueType.DateTime;
            series2.ChartType = SeriesChartType.Line;
            series2.BorderWidth = 2;
            chart1.Series.Add(series2);// параметры  линии поддержки

            Series series3 = new Series();
            series3.ChartArea = "myGraph";
            series3.XValueType = ChartValueType.DateTime;
            series3.ChartType = SeriesChartType.Spline;
            series3.BorderWidth = 2;
            chart1.Series.Add(series3);// параметры  линии сопротивления

            Series series4 = new Series();
            series4.ChartArea = "myGraph";
            series4.XValueType = ChartValueType.DateTime;
            series4.ChartType = SeriesChartType.Line;
            series4.BorderWidth = 2;
            chart1.Series.Add(series4);// параметры  линии SMA

            Series series5 = new Series();
            series5.ChartArea = "myGraph";
            series5.XValueType = ChartValueType.DateTime;
            series5.ChartType = SeriesChartType.Line;
            series5.BorderWidth = 2;
            chart1.Series.Add(series5);// параметры  линии МАX

            Series series6 = new Series();
            series6.ChartArea = "myGraph";
            series6.XValueType = ChartValueType.DateTime;
            series6.ChartType = SeriesChartType.Line;
            series6.BorderWidth = 2;
            chart1.Series.Add(series6);// параметры  линии МIN


            chart1.ChartAreas[0].BackColor = Color.FromArgb(255, 255, 255);//цвет внутренней области
            chart1.BackColor = Color.FromArgb(255, 255, 255);//цвет внешней области

            chart1.ChartAreas[0].AxisX.LineColor = Color.FromArgb(0, 0, 0);// цвет нижней линии 
            chart1.ChartAreas[0].AxisX2.LineColor = Color.FromArgb(0, 0, 0);// цвет  линии 
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.FromArgb(0, 0, 0);// цвет надписей координат по X

            chart1.ChartAreas[0].AxisY.LineColor = Color.FromArgb(0, 0, 0);// цвет боковой  линии по Y
            chart1.ChartAreas[0].AxisY2.LineColor = Color.FromArgb(0, 0, 0);// цвет боковой  линии по Y
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.FromArgb(0, 0, 0);// цвет надписей координат по Y


            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;
            chart1.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            chart1.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Seconds;
            chart1.ChartAreas[0].AxisX.Interval = 0;


            chart1.ChartAreas[0].AxisY.Maximum = 1.12;//диапазон значений
            chart1.ChartAreas[0].AxisY.Minimum = 1.11;//диапазон значений


            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
        }

       

        public void setting()
        {
            double xS = (x / 1920.0);//настройка под все  экраны
            double yS = (y / 1080.0);//настройка под все  экраный
            if (checkBox3.Checked == true)
            {
                label_X.Visible = true;
                label_Y.Visible = true;
                lab_Cur.Visible = true;
                label_X.Size = new Size(Convert.ToInt32(980 * xS * (WSrting.X / fX)), 1);
                label_Y.Size = new Size(1, Convert.ToInt32(610* xS * (WSrting.Y / fY)));
                label_X.BackColor = Color.FromArgb(0, 0, 0);//цвет линии по X
                label_Y.BackColor = Color.FromArgb(0, 0, 0);//цвет линии по Y
                this.chart1.MouseMove += new MouseEventHandler(this.chart1_MouseMove);
            }//додумать и настроить появление 

            if (checkBox3.Checked == false)
            {
                label_X.Visible = false;// убрать отображение линии по X
                label_Y.Visible = false;// убрать отображение линии по Y
                lab_Cur.Visible = false;// убрать отображение значений
            }
        }


        public int Update(int tic, List<DateTime> Date)
        { 
            tTip();
            CheckBox();// переводчик 
            Button();// переводчик  кнопок
            Menu();//переводчик меню
            chart1.MouseWheel += new MouseEventHandler(this.chart1_MouseWheel);//событие вращения колесика
            chart1.Focus();// необходим фокус
            List<List<double>> poin = new List<List<double>>();
            setting();
            if (checkBox1.Checked == false)
            {
                   chart1.Series[2].Points.Clear();
                   chart1.Series[1].Points.Clear();
            }

           

            chart1.Series[0].XValueType = ChartValueType.Time;
            chart1.Series[0].Points.AddXY(Date[tic], massYInetB[tic]);//точки для графика 

            poin = Resistance(tic);//уровни
            
            if (checkBox4.Checked == true)
            {
            //    MAXMIN(poin, Date);
            }

            if (checkBox1.Checked == true)
            {
                Resis(poin, tic, 0.0001, Date);// рисуем уровни
            } 
            

            chart1.Update();// обновление данных

            int Z = 10;// Кол-во  точек берущихся в расчет
            SMA(tic, Date, Z);

            button8.Text = Convert.ToString(massYInetA[tic]); // вывод значений на кнопку  
            button1.Text = Convert.ToString(massYInetA[tic]); // вывод значений на кнопку  
            button7.Text = Convert.ToString(massYInetB[tic]); // вывод значений на кнопку  
            chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].XValue;// ограничение по X минимум 
            tic++;

            return tic;
        }// метод обновления данных

      public  void ZoomT(int ize, double Mine)
        {
            chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].XValue;// ограничение по X минимум 
            chart1.ChartAreas[0].AxisX.Maximum = chart1.Series[0].Points[0].XValue + ize * 0.00001157407;// ограничение по X максимум  под 60 секунд 0,00001157407
            //  chart1.ChartAreas[0].AxisX.ScaleView.Zoom(chart1.Series[0].Points[0].XValue, chart1.Series[0].Points[0].XValue + ize * 0.00001157407);
        }// функция необходимая для уровней рассмотренного времени


        public List<double> SMA(int tic, List<DateTime> Date, int Sglag)
        {
            double p = 0;
            if (tic >= Sglag)
            {
                for (int i = 0; i <= Sglag - 1; i++)
                {
                    p += massYInetB[tic - i];// складываем кол - во точек
                }
                sred.Add(p / Sglag);
            }
            if (checkBox2.Checked == true)
            {
                GraphSMA(tic, Date, Sglag);
            }
            if (checkBox2.Checked == false)
            {
                chart1.Series[3].Points.Clear();
            }
            return sred;
        }




        public void GraphSMA(int tic, List<DateTime> Date, int Sglag)
        {
            if (tic >= Sglag)
            {
                chart1.Series[3].Points.Clear();
                for (int h = 0; h < tic - Sglag; h++)
                {
                    chart1.Series[3].XValueType = ChartValueType.Time;
                    chart1.Series[3].Color = Color.FromArgb(255, 100, 100);// задание цвета
                    chart1.Series[3].Points.AddXY(Date[tic - h], sred[tic - Sglag - h]);
                }
            }
        }// рисует линию


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
                if (MAXY < poin[h][1])// cpoin[h][1] точка по игрик
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

                MAXY = Resistance(poin, tic, pogr, Date, h, MAXY, MAXX, MaxH);
                MINY = Support(poin, tic, pogr, Date, h, MINY, MINX, MinH);
            }
        }//Вызов 2х методов

        public double Support(List<List<double>> poin, int tic, double pogr, List<DateTime> Date, int h, double MINY, double MINX, double MinH)
        {
            int line2X;
            double line2Y;


            for (int g = 0; g < poin.Count; g++)
            {
                if (((MINY - poin[g][1]) >= -pogr) && g != MinH)
                {
                    chart1.Series[2].Points.Clear();
                    line2Y = poin[g][1];//2 точка по игрик
                    line2X = Convert.ToInt32(poin[g][0]);//2 точка по икс

                    chart1.Series[2].XValueType = ChartValueType.Time;
                    chart1.Series[2].Points.AddXY(Date[0], line2Y);// 1 точка // можно использовать для начала line1X
                    chart1.Series[2].Points.AddXY(Date[tic], line2Y);// 2 точка
                    chart1.Series[2].Color = Color.FromArgb(55, 0, 55);// задание цвета 
                }
            }
            if ((MINY > (massYInetB[tic] + pogr)))
            {
                chart1.Series[2].Points.Clear();
            }
            return MINY;
        }



        public double Resistance(List<List<double>> poin, int tic, double pogr, List<DateTime> Date, int h, double MAXY, double MAXX, double MaxH)
        {
            int line2X;
            double line2Y;
            if (MAXY < poin[h][1])// cpoin[h][1] точка по игрик
            {
                MAXY = poin[h][1];
                MAXX = poin[h][0];
                MaxH = h;
            }
            for (int g = 0; g < poin.Count; g++)
            {
                if (((MAXY - poin[g][1]) <= pogr) && g != MaxH)
                {

                    chart1.Series[1].Points.Clear();
                    line2Y = poin[g][1];//2 точка по игрик
                    line2X = Convert.ToInt32(poin[g][0]);//2 точка по икс

                    chart1.Series[1].XValueType = ChartValueType.Time;
                    chart1.Series[1].Points.AddXY(Date[0], line2Y);// 1 точка // можно использовать для начала line1X
                    chart1.Series[1].Points.AddXY(Date[tic], line2Y);// 2 точка
                    chart1.Series[1].Color = Color.FromArgb(255, 0, 0);// задание цвета 
                }
            }
            if ((MAXY < (massYInetB[tic] - pogr)))
            {
                chart1.Series[1].Points.Clear();
            }//если за рамками погрешности
            return MAXY;
        }


        public void Window_Load(object sender, EventArgs e)
        { 
            t.Start(); t.Interval = 1000;// время секунды
            t.Tick += new EventHandler(timer1_Tick);// прибавление времени 
          
          Graph();//функция построения 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(Times[tic]);//время в формате UNIX
            D.Add(Date);// добавление времени в лист
            tic = Update(tic, D);// функция по секунде
            if (((tic / 10) - Zic) == 1)
            {
                Zic++;
                poslTime = Conect(value[0], poslTime, size);// соединение  
            }
            Console.WriteLine(Date.ToString("M/dd/yyyy h:mm:ss.fff tt"));// дебаг           
        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void button9_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = Application.StartupPath + "/Music/test.wav"; ;
            player.Play();
            BUY = massYInetA[tic];//Запомнить значение покупки
            MessageBox.Show("Cовершена покупка по цене =  " + BUY);        
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = Application.StartupPath + "/Music/test.wav"; ;
            player.Play();
            SELL = massYInetB[tic];//Запомнить значение продажи
            MessageBox.Show("Cовершена продажа по цене =  " + SELL);
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

        void chart1_MouseMove(object sender, MouseEventArgs e)
        {

            double xS = (x / 1920.0);//настройка под все  экраны
            double yS = (y / 1080.0);//настройка под все  экраный
            if (e.Y > Convert.ToInt32(27 * yS * (WSrting.Y / fY)) && e.Y < Convert.ToInt32(629 * yS * (WSrting.Y / fY)) && e.X > Convert.ToInt32(110 * xS * (WSrting.X / fX)))
            {
                label_X.Location = new Point(Convert.ToInt32(110 * xS * (WSrting.X / fX)), (e.Y));//перемещение линии по y
                label_Y.Location = new Point((e.X), Convert.ToInt32(30 * yS * (WSrting.Y / fY)));//перемещение линии по X
                lab_Cur.Location = new Point(Convert.ToInt32(1020 * xS * (WSrting.X / fX)), (e.Y));//привязка значения 
                double YCur = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                double XCur = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X) / 0.00001157407;
                DateTime Date1 = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(XCur);//время в формате UNIX
                lab_Cur.Text = String.Concat(String.Concat(Math.Round(YCur, 5).ToString(), " , "), Date1.ToString("h:mm:ss.fff tt"));//перевод времени
            }

        }// вывод координат




        void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta / 120 > 0)
            {
                switch (Leaves)
                {
                    case 1: Zoom -= 5; if (Zoom > 60) { ZoomT(Zoom, chart1.Series[0].Points[0].XValue); } break;
                }
            }

            if (e.Delta / 120 < 0)
            {
                switch (Leaves)
                {
                    case 1: Zoom += 5; ZoomT(Zoom, chart1.Series[0].Points[0].XValue); break;
                }
            }// прокрутка вниз
        }





        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Tick -= new EventHandler(timer1_Tick);
        }
        
        private void lab_Cur_Click(object sender, EventArgs e)
        {

        }

        public void secondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Leaves = 1;

            area.AxisX.MajorGrid.Interval = 0.00001157407;

            ZoomT(60, chart1.Series[0].Points[0].XValue);
            Activ(sender);
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked;//убрать отметку 30 минутный уровень
            hourToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку часовой уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку недельный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку месячный уровень
        }// секундный уровень

        private void minutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 0.00001157407*60;
            Zoom = 300 * 6;
            ZoomT(Zoom, chart1.Series[0].Points[0].XValue);
            Activ(sender);
            secondToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку секундный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked;//убрать отметку 30 минутный уровень
            hourToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку часовой уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку недельный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку месячный уровень
        }// 5 минутный уровень 

        private void minutesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60*5;
            Zoom = 9000;
            ZoomT(Zoom, chart1.Series[0].Points[0].XValue);
            Activ(sender);
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку минутный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку секундный уровень
            hourToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку часовой уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку недельный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку месячный уровень
        }

        private void hourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60 * 10;
            Zoom = 108000;
            ZoomT(Zoom, chart1.Series[0].Points[0].XValue);
            Activ(sender);
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked;//убрать отметку 30 минутный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку секундный уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку недельный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку месячный уровень
        }//часовой уровень

        private void weekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 0.00001157407 * 60 * 70;
            Zoom = 1728000;
            ZoomT(1728000, chart1.Series[0].Points[0].XValue);
            Activ(sender);
            hourToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку часовой уровень
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked;//убрать отметку 30 минутный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку секундный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку дневной уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку месячный уровень
        }// недельный уровень

        private void dayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 1/24.0;
            Zoom = 432000;
            ZoomT(Zoom, chart1.Series[0].Points[0].XValue);
            Activ(sender);
            hourToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку часовой уровень
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked;//убрать отметку 30 минутный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку секундный уровень
            monthToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку месячный уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку недельный уровень
        }// дневной уровень

        private void monthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            area.AxisX.MajorGrid.Interval = 1;
            Zoom = 8640000;
            ZoomT(8640000, chart1.Series[0].Points[0].XValue);
            Activ(sender);
            hourToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку часовой уровень
            minutesToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку минутный уровень
            minutesToolStripMenuItem1.CheckState = CheckState.Unchecked;//убрать отметку 30 минутный уровень
            secondToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку секундный уровень
            weekToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку недельный уровень
            dayToolStripMenuItem.CheckState = CheckState.Unchecked;//убрать отметку дневной уровень
        }// месячный уровень

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

    private void createReportToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }// разобраться сделать на английском
    }
}
