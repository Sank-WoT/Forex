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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    static class WSrting
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

  public delegate void MyDelegate();
}
