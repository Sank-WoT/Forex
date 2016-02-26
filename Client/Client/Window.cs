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
        double Size = 60;
        double unixtime;
        double shag = 1;//интервал для координаты X
        int dannoe = 0;
        List<double> massY = new List<double>();
        List<double> massYInetA = new List<double>();
        List<double> Times = new List<double>();
        List<double> massYInetB = new List<double>();
        List<DateTime> D = new List<DateTime>();
       int   tic = 0;
       public double Conect(string value, double time, double limit)
       {
           double poslTime = 0;
           System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");// преобразовение
          // WebProxy wp = new WebProxy("151.236.216.251", 10000); //задаем параметры прокси
           string pathDirectory = "C:\\Forex";//Путь к директории
           if (!Directory.Exists(pathDirectory))//Проверка  на существование директории
           {
               Directory.CreateDirectory(pathDirectory);// создание директории 
               MessageBox.Show("Директория создана путь : " + pathDirectory);// сообщение о создании директории
           }
           string pathFile = pathDirectory + "\\" + "eurusd" + ".txt";//Путь к файлу
           if (!File.Exists(pathFile))//проверка на существование файла
           {
               FileInfo writel = new FileInfo(pathFile);//получаем путь 
               StreamWriter l = writel.CreateText();//создаем текст
               l.WriteLine();//добавляем текст
               l.Close();//закрыть запись
               MessageBox.Show(" Файл создан путь: " + pathFile);// сообщение о создании файла
           } //Развертывание сервера в заранее известном каталоге 
           WebRequest webReq = WebRequest.Create("http://currency-dred95.rhcloud.com/get_currency.php?time=" + time + "&limit="+limit+"&sign=" + value);//запрос на сайт 
           //webReq.Proxy = wp;//меняем на прокси
           WebResponse webRes = webReq.GetResponse();//получение ответа
           Stream st = webRes.GetResponseStream();//поток по которому получаем инфу
           StreamReader sr = new StreamReader(st);//прочитать поток
           StreamReader sr1 = new StreamReader(st);//прочитать поток
          

           string response = sr.ReadToEnd(); // присвоение прочтенного к стринг   
           Regex regex = new Regex(@"((\d{10,20})|(\d{1,20})\.(\d{1,4}))");//регулярное выражение 
           Match m = regex.Match(response);

           for (int h = 0; h < Size; h++)
           { 
             response = m.Value;//Время
             Times.Add(Convert.ToDouble(response));
             m = m.NextMatch();
             response = m.Value;
             massYInetA.Add(Convert.ToDouble(response));
             m = m.NextMatch();
             response = m.Value;
             massYInetB.Add(Convert.ToDouble(response));
             m = m.NextMatch();
             massY.Add((massYInetA[h] + massYInetB[h]) / 2);
          
           }
         

           StreamReader r = new StreamReader(pathFile);
           string text = r.ReadToEnd();// получение прочтенной записи
           r.Close();//закрыть чтение 


           FileInfo write = new FileInfo(pathFile);//получаем путь 
           StreamWriter w = write.CreateText();//создаем текст
           text += "  " + response;// слияние прочитанного с вновь внесенным данные покупки 
           w.WriteLine(text);//добавляем текст
           w.Close();//закрыть запись
           poslTime = Times[9];
           return poslTime;
       }//Получить  данные  с собственного сайта
        //модифицировать для получения файлов
       
        
        public Window()
        {
            double poslTime = 0;
           string[] value = new string[1] {"eurusd" };
           poslTime = Conect(value[0], 0, Size);// соединение 
           Console.WriteLine(poslTime);//создан для дебага
           InitializeComponent(); 
           Graph();//функция построения 
           t.Start(); t.Interval = 1000;// время секунды
           t.Tick += new EventHandler(timer1_Tick);// прибавление времени
        }
      
        public void Graph()
        {  
            
            double d = massY.Count;   
            chart1 = new Chart();// Создание чарта
            chart1.Parent = this;
            ChartArea area = new ChartArea();// Создание области
            area.Name = "myGraph";
            area.AxisX.Minimum = 0;// область отображения минимум
          //area.AxisX.Maximum = Size;// область отображения максимум
            area.AxisX.MajorGrid.Interval = shag;
            this.chart1.Size = new System.Drawing.Size(1058, 684);
            chart1.ChartAreas.Add(area);// передача 
           
            Series series1 = new Series();
            series1.ChartArea = "myGraph";
            series1.XValueType = ChartValueType.DateTime;
            series1.ChartType = SeriesChartType.Line;
            series1.BorderWidth = 3;
            chart1.Series.Add(series1);
           
            Series series2 = new Series();
            series2.ChartArea = "myGraph";
            series2.ChartType = SeriesChartType.Line;
            series2.BorderWidth = 3;
            chart1.Series.Add(series2);
           
            Series series3 = new Series();
            series3.ChartArea = "myGraph";
            series3.ChartType = SeriesChartType.Line;
            series3.BorderWidth = 3;
            chart1.Series.Add(series3);
           
            Series series4 = new Series();
            series4.ChartArea = "myGraph";
            series4.ChartType = SeriesChartType.Line;
            series4.BorderWidth = 3;
            chart1.Series.Add(series4);

            
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm:ss";
           // chart1.ChartAreas[0].AxisX.IsStartedFromZero = true;
            //chart1.ChartAreas[0].AxisX.ScaleView.SizeType = DateTimeIntervalType.Seconds;
            //chart1.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            //chart1.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Seconds;
            //chart1.ChartAreas[0].AxisX.Interval = 0;

            chart1.ChartAreas[0].AxisY.Maximum = 1.12;//диапазон значений
            chart1.ChartAreas[0].AxisY.Minimum = 1.11;//диапазон значений

 
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;// изменение масштаба 
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true; // изменение масштаба 
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false; // изменение масштаба 
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chart1.ChartAreas[0].CursorY.IsUserEnabled = true;// изменение масштаба 
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = true; // изменение масштаба 
            chart1.ChartAreas[0].AxisY.ScaleView.Zoomable = true; // изменение масштаба 
            chart1.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
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

        private int Update(int tic, List<DateTime> Date)
        {
            List<List<double>> poin = new List<List<double>>();
            
            chart1.Series[0].XValueType = ChartValueType.Time;
            chart1.Series[0].Points.AddXY(Date[tic], massY[tic]);//точки для графика поискать информацию на этот счет
            if(checkBox1.Checked == true)
           {
            poin = Resistance(tic);//уровни      
            Resis(poin, tic, 0.0001) ;// рисуем уровни
           }
            chart1.Update();// обновление данныхr
            Console.WriteLine("tic=");
            Console.WriteLine(tic);
            Console.WriteLine("massY[tic]");
            Console.WriteLine(massY[tic]);
            tic++;
            return tic;
        }

        public void Resis(List<List<double>> poin, int tic, double pogr)
        {
            double[] line1X = new double[3];
            double[] line2X = new double[3];
            double[] line1Y = new double[3];
            double[] line2Y = new double[3];
            double[] urowen = new double[1];
            int p = 0;
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



                for (int g = 0; g < poin.Count; g++)
                {
                    if (((MAXY - poin[g][1]) <= pogr) && g != MaxH)
                    {
                    
                        chart1.Series[1].Points.Clear();
                        line1Y[1] = MAXY;//1 точка по игрик
                        line2Y[1] = poin[g][1];//2 точка по игрик
                        line1X[1] = MAXX;//1 точка по икс 
                        line2X[1] = poin[g][0];//2 точка по икс
                        Console.WriteLine("MAX=");
                        Console.WriteLine(MAXY);
                        Console.WriteLine("MIN=");
                        Console.WriteLine(massY[tic]);
                      //  chart1.Series[1].XValueType = ChartValueType.Time;
                        chart1.Series[1].Points.AddXY(line1X[1], line2Y[1]);// 1 точка
                        chart1.Series[1].Points.AddXY(tic, line2Y[1]);// 2 точка
                        chart1.Series[1].Color = Color.FromArgb(255, 0, 0);// задание цвета 
                    }

                    if (((MINY - poin[g][1]) >= -pogr) && g != MinH)
                    {
                        chart1.Series[2].Points.Clear();
                        line1Y[2] = MINY;//1 точка по игрик
                        line2Y[2] = poin[g][1];//2 точка по игрик
                        line1X[2] = MINX;//1 точка по икс 
                        line2X[2] = poin[g][0];//2 точка по икс
                      
                        Console.WriteLine("MIN=");
                        Console.WriteLine(MINY);
                        Console.WriteLine("massY=");
                        Console.WriteLine(massY[tic]);
                       
                        chart1.Series[2].Points.AddXY(line1X[2], line2Y[2]);// 1 точка
                        chart1.Series[2].Points.AddXY(tic, line2Y[2]);// 2 точка
                        chart1.Series[2].Color = Color.FromArgb(55, 0, 55);// задание цвета 
                    }         
                }
                if ((MAXY < (massY[tic] - pogr)))
               {
                chart1.Series[1].Points.Clear();
               }
                if ((MINY > (massY[tic] + pogr)))
               {
                   chart1.Series[2].Points.Clear();
               }
            }//сделать модификацию на удаление прошлых уровней l = 1  поддержки l = 2 сопротивления
        }

        public void Window_Load(object sender, EventArgs e)
        {
       
        }
        private void button1_Click(object sender, EventArgs e)
       {               
       }
        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            DateTime Date = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(Times[tic]);//время в формате UNIX
            D.Add(Date);
            Console.WriteLine(Date.ToString("M/dd/yyyy h:mm:ss.fff tt"));
            tic = Update(tic, D);// функция по секунде
        }

        private void button2_Click(object sender, EventArgs e)
        {      
            Size = 60;   
        }

        private void button6_Click(object sender, EventArgs e)
        {            
           Size = 300;
            shag = 5;
           // tic = Update(tic,);
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
         
        }
    }
}
//Проверить уровень поддержки на ее работоспособность