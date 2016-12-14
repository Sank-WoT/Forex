using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{

    class ZoomS
    {
        // Стартовое время
        public int start;
        // Конечное время
        public int end;
        int n = 1;
        // Новый массив времени
        public List<int> NewZoom = new List<int>();
        public List<int> Zoom = new List<int>();
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
