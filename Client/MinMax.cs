using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class MinMax
    {
        List<List<double>> _poin = new List<List<double>>();
        List<double> _koordPoint = new List<double>();
        public void AddMinMax(List<double> MainV, List<DateTime> MainT, int danoe)
        {
            int Pervoe = 0;
            // переменная тренда
            int trend = 0;
            // данные точки
            for (int i = 1; MainV.Count - 1 > i; i++)
            {
                if (trend != 1 && (MainV[i - 1] - MainV[i]) > 0)
                {
                    // Координата точки
                    _koordPoint = new List<double>();
                    // положительный тренд
                    trend = 1;
                    // заполнение точек по икс
                    _koordPoint.Add(i - 1);
                    _koordPoint.Add(MainV[i - 1]); // заполнение точек по игрик  

                    if (Pervoe == 0)
                    {
                        danoe = -1;
                        Pervoe++;
                    } // узнаем первое значение минимум оно или максимум
                    _poin.Add(_koordPoint); // добавление координаты точки
                }

                if (trend != -1 && (MainV[i - 1] - MainV[i]) < 0)
                {
                    _koordPoint = new List<double>();  // Координата точки
                    trend = -1; // положительный тренд
                    _koordPoint.Add(i - 1); // заполнение точек по икс    
                    _koordPoint.Add(MainV[i - 1]); // заполнение точек по игрик  
                    // узнаем первое значение минимум оно или максимум
                    if (Pervoe == 0)
                    {
                        danoe = 1;
                        Pervoe++;
                    }
                    // добавление координаты точки
                    _poin.Add(_koordPoint);
                }
            }
        }
        public List<List<double>> GetMinMax()
        {
            return _poin;
        }
    }
}
