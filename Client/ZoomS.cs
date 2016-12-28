using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// Класс для работы с масштабоом графика
    /// </summary>
    class ZoomS
    {
        /// <summary>
        ///  Стартовое время
        /// </summary>
        public int start;
        /// <summary>
        ///  конечное время
        /// </summary>
        public int end;
        int n = 1;
        /// <summary>
        ///  Новый лист времени
        /// </summary>
        public List<int> NewZoom = new List<int>();
        public List<int> Zoom = new List<int>();
        /// <summary>
        ///  Измененение маштаба
        /// </summary>
        public ZoomS(List<int> a, int Period)
        {
            // получаем первое число периода
            start = a[0] - (a[0] % Period);
            // получаем последнее число периода
            end = start + Period;
            // где n номер цикла 
            while(start + Period * (n - 1) < a[a.Count - 1])
            {
                Zoom = a.FindAll(x => x >= start + Period * (n - 1) && x <= start + Period * n);
                if(0 != Zoom.Count )
                {
                    NewZoom.Add(Zoom.Max());
                    n++;
                    Console.WriteLine("Max Period = " + Zoom.Max());
                }
            }
           
        }
    }
}
