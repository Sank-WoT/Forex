using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    ///  Класс минимум максимум
    /// </summary>

    public class MinMax
    {
        /// <summary>
        ///  Свойство для выдачи точек минимума и максимума +
        /// </summary>
        List<List<double>> _poin = new List<List<double>>();
        public List<List<double>> Svoistvo_poin
        {
            get
            {
                return _poin;
            }
        }

        List<double> _koordPoint = new List<double>();

        /// <summary>
        ///  Свойство для выдачи одной точки +
        /// </summary>
        public List<double> Svoistvo_koordPoint
        {
            get
            {
                return _koordPoint;
            }
        }

        /// <summary>
        ///  Метод для добавления новых значений котировок +
        /// </summary>
        /// <param name="MainV">лист значений</param>
        /// <param name="_koordPoint">лист времени</param>
        /// <param name="_koordPoint">начальное значение</param>
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
                    // узнаем первое значение минимум оно или максимум
                    if (Pervoe == 0)
                    {
                        danoe = -1;
                        Pervoe++;
                    } 
                    // добавление координаты точки
                    _poin.Add(_koordPoint); 
                }

                if (trend != -1 && (MainV[i - 1] - MainV[i]) < 0)
                {
                    // Координата точки
                    _koordPoint = new List<double>();
                    // положительный тренд
                    trend = -1;
                    // заполнение точек по икс 
                    _koordPoint.Add(i - 1);
                    // заполнение точек по игрик  
                    _koordPoint.Add(MainV[i - 1]);
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
    }
}
