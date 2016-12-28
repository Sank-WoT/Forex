using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    ///  Вычисление линии SMA +
    /// </summary>
   public class ClassSMA
    {
        /// <summary>
        ///  Шаг сглаживания линии SMA +
        /// </summary>
        private int _Sglag;
        /// <summary>
        ///  Точки SMA
        /// </summary>
        private List<double> _Point = new List<double>();
        public void Add(int Sglag, List<double> point)
        {
            double p = 0;
            if (point.Count >= Sglag)
            {
                for (int j = 0; j <= (point.Count - Sglag); j++)
                {
                    for (int i = 0; i <= Sglag - 1; i++)
                    {
                        // складываем кол - во точек
                        p += point[i + j];
                    }
                    // Добавление в лист средних значений по Sglag точкам 
                    _Point.Add(p / Sglag);
                    p = 0;
                }
            }
        }

        /// <summary>
        ///  Выдача точек SMA +
        /// </summary>
        public List<double> GetSred()
        {
            return _Point;
        }
    }
}
