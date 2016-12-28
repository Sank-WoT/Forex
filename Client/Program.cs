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
            // стартовые значения
            WString.Langue.Add("RUS", false);
            WString.Langue.Add("ENG", true);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // высота экрана
            int y = SystemInformation.PrimaryMonitorSize.Height;
            // ширина экрана 
            int x = SystemInformation.PrimaryMonitorSize.Width;
            Application.Run(new MainForm(x,y));
        }
    }
    static class WString
    {
        public static int X { get; set; }
        public static int Y { get; set; }
        public static Dictionary<string, bool> Langue = new Dictionary<string, bool>(5);

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
