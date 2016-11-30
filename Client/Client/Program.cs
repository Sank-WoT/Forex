using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WString.ENG = true; // Задание базового языка
            WString.RUS = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            int y = SystemInformation.PrimaryMonitorSize.Height; // высота экрана
            int x = SystemInformation.PrimaryMonitorSize.Width; // ширина экрана 
            Application.Run(new MainForm(x,y));
        }
    }
    static class WString
    {
        public static int X { get; set; }
        public static int Y { get; set; }
        public static bool RUS;
        public static bool ENG;
        public static string VALUE;

    }// передача данных между формами

  public   static class Activ
    {
        public static bool Form1;
        public static bool Window;
        public static bool SWindow;
        public static bool Help;

    }// передача данных между формами

  public static class ReportTransit
  {
      public static List<double> SELL;
      public static List <double> BUY;
      public static List<List<double>> data = new List<List<double>>();
  }// передача данных между формами

    public static class InetConnect
    {
        public static bool Inet;     
    }// передача данных между формами

    public static class SpeedDraw
    {
        public static int Speed;
    }// передача данных между формами
}
