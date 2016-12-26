using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class IntervalResistance
    {
        public int danoe = 0;
        public List<List<double>> poin = new List<List<double>>();


        /// <summary>
        ///  Подавать интервал +
        /// </summary>
        /// <param name="tic">время прошедшее с запуска</param>
        /// <param name="Buff">???</param>
        /// <param name="NowTime">Текущее время</param>
        public IntervalResistance(int tic, List<double> Buffer, double NowTime)
        {     
            int Pervoe = 0;
            // переменная тренда
            int trend = 0;
            // данные точки
            List<double> koordPoint = new List<double>();
            List<int> PoinX = new List<int>();
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
        }
    }
}
