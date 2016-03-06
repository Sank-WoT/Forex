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


namespace Client
{
    public partial class Window : Form
    {//  
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        int Size = 30, Leaves = 0, Zoom = 0;
        double Zic = 0;
        double shag = 1;//интервал для координаты X
        int dannoe = 0;
        int colvo = 0; 
        List<double> massY = new List<double>();
        List<double> massYInetA = new List<double>();
        List<double> Times = new List<double>();
        List<double> massYInetB = new List<double>();
        List<DateTime> D = new List<DateTime>();
        List<double> sred = new List<double>();   
        double poslTime = 0;// последнее время
        string[] value = new string[1] {"eurusd" };
        int tic = 0;
       public double Conect(string value, double poslTime, int limit)
       {  
           System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");// преобразовение
          // WebProxy wp = new WebProxy("151.236.216.251", 10000); //задаем параметры прокси

           WebRequest webReq = WebRequest.Create("http://currency-dred95.rhcloud.com/get_currency.php?time=" + poslTime + "&limit=" + limit + "&sign=" + value);//запрос на сайт 
           //webReq.Proxy = wp;//меняем на прокси
           WebResponse webRes = webReq.GetResponse();//получение ответа
           Stream st = webRes.GetResponseStream();//поток по которому получаем инфу
           StreamReader sr = new StreamReader(st);//прочитать поток
         
           
           string pathDirectory = "C:\\Forex";//Путь к директории
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
               MessageBox.Show(" Файл создан путь: " + pathFile);// сообщение о создании файла
           } //Развертывание сервера в заранее известном каталоге 
           StreamReader r = new StreamReader(pathFile);
           string text = r.ReadToEnd();// получение прочтенной записи
           r.Close();//закрыть чтение   
           string response = sr.ReadToEnd(); // присвоение прочтенного к стринг 
           text += response;
           FileInfo write = new FileInfo(pathFile);//получаем путь 
           StreamWriter w = write.CreateText();//создаем текст  
           w.WriteLine(text);//добавляем текст 
           w.Close();//закрыть запись 

           Regex regex = new Regex(@"((\d{10,20})|(\d{1,20})\.(\d{1,4}))");//регулярное выражение 
           Match m = regex.Match(response);
           for (int h = 0; h < limit; h++)
           { 
             response = m.Value;//Время
             Times.Add(Convert.ToDouble(response));//Время в UNIX
             m = m.NextMatch();
             response = m.Value;
             massYInetA.Add(Convert.ToDouble(response));
             m = m.NextMatch();
             response = m.Value;
             massYInetB.Add(Convert.ToDouble(response));
             m = m.NextMatch();
             massY.Add((massYInetA[colvo]));
             text += "  " + massYInetA[colvo];// слияние прочитанного с вновь внесенным данные покупки  так же необходимо сохранить даты      
             colvo++;
           }
           poslTime = Times[colvo - 1];
            Console.WriteLine(poslTime);//создан для дебага 
           return poslTime;
       }//Получить  данные  с собственного сайта
        //модифицировать для получения файлов
       
       public void tTip()
       {
           toolTip1.AutoPopDelay = 3000;//
           toolTip1.InitialDelay = 1000;//
           toolTip1.ReshowDelay = 1000;//время сколько показывается надпись
           toolTip1.ShowAlways = true;
           toolTip1.SetToolTip(this.checkBox1, "Нажмите чтобы активировать отображение уровней поддержки и сопротивления.");
           toolTip2.AutoPopDelay = 3000;//
           toolTip2.InitialDelay = 1000;//
           toolTip2.ReshowDelay = 1000;//время сколько показывается надпись
           toolTip2.ShowAlways = true;
           toolTip2.SetToolTip(this.checkBox2, "Нажмите чтобы активировать отображение уровней скользящей прямой.");
           toolTip3.AutoPopDelay = 3000;//
           toolTip3.InitialDelay = 1000;//
           toolTip3.ReshowDelay = 1000;//время сколько показывается надпись
           toolTip3.ShowAlways = true;
           toolTip3.SetToolTip(this.checkBox3, "Нажмите чтобы активировать отображение линий значение в точке."); 
       }
        
        public Window()
        {
            
            int y = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; //высота экрана
            int x = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width; //ширина экрана  
            double xS = (x / 1920.0);//настройка под все  экраны
            double yS = (y / 1080.0);//настройка под все  экраны
            for (int m = 0; m < 1; m++ )
            { 
            poslTime = Conect(value[0], poslTime, 30);// соединение 
            }
           InitializeComponent(); 
                   
           Graph();//функция построения 
            t.Start(); t.Interval = 1000;// время секунды
            t.Tick += new EventHandler(timer1_Tick);// прибавление времени 
            tTip();
        }
      
        public void Graph()
        {  
            
            double d = massY.Count;   
            chart1 = new Chart();// Создание чарта
            chart1.Parent = this;
            ChartArea area = new ChartArea();// Создание области
            area.Name = "myGraph";
            area.AxisX.MajorGrid.Interval = shag;// доработать интервал по координате X 1= 1 день
            this.chart1.Size = new System.Drawing.Size(1058, 684);//размеры чарта
            chart1.ChartAreas.Add(area);// передача 
           
            Series series1 = new Series();
            series1.ChartArea = "myGraph";
            series1.XValueType = ChartValueType.DateTime;
            series1.ChartType = SeriesChartType.Line;
            series1.BorderWidth = 4;
            chart1.Series.Add(series1);
           
            Series series2 = new Series();
            series2.ChartArea = "myGraph";
            series2.XValueType = ChartValueType.DateTime;
            series2.ChartType = SeriesChartType.Line;
            series2.BorderWidth = 2;
            chart1.Series.Add(series2);
           
            Series series3 = new Series();
            series3.ChartArea = "myGraph";
            series3.XValueType = ChartValueType.DateTime;
            series3.ChartType = SeriesChartType.Spline;
            series3.BorderWidth = 2;
            chart1.Series.Add(series3);
           
            Series series4 = new Series();
            series4.ChartArea = "myGraph";
            series4.XValueType = ChartValueType.DateTime;
            series4.ChartType = SeriesChartType.Line;
            series4.BorderWidth = 2;
            chart1.Series.Add(series4);

            Series series5 = new Series();
            series5.ChartArea = "myGraph";
            series5.XValueType = ChartValueType.DateTime;
            series5.ChartType = SeriesChartType.Line;
            series5.BorderWidth = 2;
            chart1.Series.Add(series5);


            chart1.ChartAreas[0].BackColor = Color.FromArgb(255,255,255);//цвет внутренней области
            chart1.BackColor = Color.FromArgb(255,255,255);//цвет внешней области

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

 
                //chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0,30);
                chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
                chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
        }
            

        public int vp (int h)
        {
            int p = 0;
            if (dannoe == -1)
            {
                if (h % 2 == 0)
                {
                    p = 1;// уровень поддержки
                }
                if (h % 2 == 1)
                {
                    p = 2;// уровень сопротивления
                }
            }
            if (dannoe == 1)
            {
                if (h % 2 == 1)
                {
                    p = 1;// уровень поддержки
                }
                if (h % 2 == 0)
                {
                    p = 2;// уровень сопротивления
                }
            }
            return p;
        }

        public List<List<double>> Resistance(int tic)
        {
            int Pervoe = 0;
            int trend = 0;//переменная тренда
            List<List<double>> poin = new List<List<double>>();//данные точки
            List<double> row = new List<double>();
        
            for(int i = 1;tic>i;i++)
            {              
            if(trend != 1 && (massY[i-1]- massY[i]) > 0)
            {
                row = new List<double>();
                trend = 1;//положительный тренд
                row.Add(i-1);//заполнение точек по икс    
                row.Add(massY[i-1]);//заполнение точек по игрик  
                if(Pervoe == 0)
                {
                    dannoe = - 1;
                    Pervoe++;
                }// узнаем первое значение минимум оно или максимум
                poin.Add(row);   
            }
            if (trend != -1 && (massY[i-1] - massY[i]) < 0)
            {
                if (Pervoe == 0)
                {
                    dannoe = 1;
                    Pervoe++;
                }// узнаем первое значение минимум оно или максимум
                row = new List<double>();
                trend = -1;//положительный тренд
                row.Add(i);//заполнение точек по икс    
                row.Add(massY[i]);//заполнение точек по игрик  
                poin.Add(row);
            }
            }
            return  poin;
        }

       public void setting()
        {
            if(checkBox3.Checked == true)
            {
            label_X.Visible = true;
            label_Y.Visible = true;
            lab_Cur.Visible = true;
            label_X.Size = new Size(958, 1);
            label_Y.Size = new Size(1, 595);
            label_X.BackColor = Color.FromArgb(0, 0, 0);//цвет линии по X
            label_Y.BackColor = Color.FromArgb(0, 0, 0);//цвет линии по Y
            this.chart1.MouseMove += new MouseEventHandler(this.chart1_MouseMove);
            }//додумать и настроить появление




          if(checkBox3.Checked == false)
            {
                label_X.Visible = false;
                label_Y.Visible = false;
            }
        }


        private int Update(int tic, List<DateTime> Date)
        {         
         chart1.MouseWheel += new MouseEventHandler(this.chart1_MouseWheel);
         chart1.Focus();// необходим фокус
            List<List<double>> poin = new List<List<double>>();
            setting();
            if (checkBox1.Checked == false)
            {
                chart1.Series[2].Points.Clear();
                chart1.Series[1].Points.Clear();
            }


            chart1.Series[0].XValueType = ChartValueType.Time;
            chart1.Series[0].Points.AddXY(Date[tic], massY[tic]);//точки для графика 

            poin = Resistance(tic);//уровни

            if(checkBox1.Checked == true)
           {       
            Resis(poin, tic, 0.0001, Date) ;// рисуем уровни
           }

            chart1.Update();// обновление данных
            Console.WriteLine("tic=");
            Console.WriteLine(tic);
            Console.WriteLine("massY[tic]");
            Console.WriteLine(massY[tic]);

            int Z = 10;// Кол-во  точек берущихся в расчет
            SMA(tic, Date, Z);
            
            button8.Text = Convert.ToString(massY[tic]); // вывод значений на кнопку  
            chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].XValue;// ограничение по X минимум 
            tic++;
            
            return tic;
        }// метод обновления данных

        void ZoomT(int ize, double Mine)
        {
           chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].XValue;// ограничение по X минимум 
           chart1.ChartAreas[0].AxisX.Maximum = chart1.Series[0].Points[0].XValue + ize*0.00001157407;// ограничение по X максимум  под 60 секунд 0,00001157407
          //  chart1.ChartAreas[0].AxisX.ScaleView.Zoom(chart1.Series[0].Points[0].XValue, chart1.Series[0].Points[0].XValue + ize * 0.00001157407);
        }// функция необходимая для уровней рассмотренного времени


        public List<double> SMA(int tic, List<DateTime> Date, int Sglag)
        {
            double  p = 0;
            if (tic >= Sglag)
            {
            for (int i = 0; i <= Sglag - 1; i++)
            {
                p += massY[tic - i];// складываем кол - во точек
            }
            sred.Add(p / Sglag);
            }
            if(checkBox2.Checked == true)
            {
                GraphSMA(tic, Date, Sglag);
            } 
            if(checkBox2.Checked == false)
            {
                chart1.Series[3].Points.Clear();
            }    
            return sred;  
        }//метод скользящей кривой //сделать код  идеальнee




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
                    Console.WriteLine("sred");
                    Console.WriteLine(sred[tic - Sglag]);// дебаг скользящей кривой
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
            int line1X;
            int line2X;
            double line1Y;
            double line2Y;


            for (int g = 0; g < poin.Count; g++)
            {
                if (((MINY - poin[g][1]) >= -pogr) && g != MinH)
                {
                    chart1.Series[2].Points.Clear();
                    line1Y = MINY;//1 точка по игрик
                    line2Y = poin[g][1];//2 точка по игрик
                    line1X = Convert.ToInt32(MINX);//1 точка по икс 
                    line2X = Convert.ToInt32(poin[g][0]);//2 точка по икс

                    chart1.Series[2].XValueType = ChartValueType.Time;
                    chart1.Series[2].Points.AddXY(Date[0], line2Y);// 1 точка // можно использовать для начала line1X
                    chart1.Series[2].Points.AddXY(Date[tic], line2Y);// 2 точка
                    chart1.Series[2].Color = Color.FromArgb(55, 0, 55);// задание цвета 
                }
            }
            if ((MINY > (massY[tic] + pogr)))
            {
                chart1.Series[2].Points.Clear();
            }
            return MINY;
        }



        public double Resistance(List<List<double>> poin, int tic, double pogr, List<DateTime> Date, int h, double MAXY, double MAXX, double MaxH)
        {
            int line1X;
            int line2X;
            double line1Y;
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
                    line1Y = MAXY;//1 точка по игрик
                    line2Y = poin[g][1];//2 точка по игрик
                    line1X = Convert.ToInt32(MAXX);//1 точка по икс 
                    line2X = Convert.ToInt32(poin[g][0]);//2 точка по икс

                    chart1.Series[1].XValueType = ChartValueType.Time;
                    chart1.Series[1].Points.AddXY(Date[0], line2Y);// 1 точка // можно использовать для начала line1X
                    chart1.Series[1].Points.AddXY(Date[tic], line2Y);// 2 точка
                    chart1.Series[1].Color = Color.FromArgb(255, 0, 0);// задание цвета 
                }
            }
            if ((MAXY < (massY[tic] - pogr)))
            {
                chart1.Series[1].Points.Clear();
            }//если за рамками погрешности
            return MAXY;
        }


        public void Window_Load(object sender, EventArgs e)
        {
       
        }

        private void timer1_Tick(object sender, EventArgs e)
        {              
            DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(Times[tic]);//время в формате UNIX
            D.Add(Date);// добавление времени в лист
            tic = Update(tic, D);// функция по секунд 
            if (((tic / 10) - Zic) == 1)
            {
                Zic++;   
               poslTime = Conect(value[0], poslTime, Size);// соединение  
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

        }

        private void button10_Click(object sender, EventArgs e)
        {

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
            label_X.Location = new Point(100, (e.Y));//перемещение линии по y
            label_Y.Location = new Point((e.X), 30);//перемещение линии по X
            lab_Cur.Location = new Point(1020, (e.Y));//привязка значения 
            double YCur = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);    
            double XCur = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X)/0.00001157407;// понять  как вывести дату
            DateTime Date1 = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(XCur);//время в формате UNIX
            lab_Cur.Text = String.Concat(String.Concat(Math.Round(YCur, 5).ToString(), " , "), Date1.ToString("h:mm:ss.fff tt"));//перевод времени
        }// вывод координат


        void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta/120 > 0)
            {
                switch (Leaves)
                {
                    case 1: Zoom -= 5; if (Zoom > 80) { ZoomT(Zoom, chart1.Series[0].Points[0].XValue); } break;
                }     
            }
         
           if (e.Delta/120 < 0)
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

        private void button3_Click(object sender, EventArgs e)
        {
            Zoom = 432000;
            ZoomT(Zoom, chart1.Series[0].Points[0].XValue);
        }// дневной уровень

        private void button12_Click(object sender, EventArgs e)
        {
            Zoom = 9000;
            ZoomT(Zoom, chart1.Series[0].Points[0].XValue);
        }// 30 минутный уровень

        private void button11_Click(object sender, EventArgs e)
        {
            Zoom = 108000;
            ZoomT(Zoom, chart1.Series[0].Points[0].XValue);
        }//часовой уровень

        private void button5_Click(object sender, EventArgs e)
        {
            Zoom = 1728000;
            ZoomT(1728000, chart1.Series[0].Points[0].XValue);
        }// недельный уровень

        private void button4_Click(object sender, EventArgs e)
        {
            Zoom = 8640000;
            ZoomT(8640000, chart1.Series[0].Points[0].XValue);
        }// месячный уровень
       private void button2_Click(object sender, EventArgs e)
        {
            Leaves = 1;
            ZoomT(60, chart1.Series[0].Points[0].XValue);
        }// секундный уровень

        private void button6_Click(object sender, EventArgs e)
        {
            Zoom = 300 * 6;
            ZoomT(Zoom, chart1.Series[0].Points[0].XValue);
        }// 5 минутный уровень 

        private void lab_Cur_Click(object sender, EventArgs e)
        {

        }
    }
}