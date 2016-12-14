using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    /// <summary>
    ///  Класс котировка
    /// </summary>
    /// <param name="_TimeU;">Лист UnixTime времени </param>
    /// <param name="_TimeD;">Лист DateTime времени </param>
    /// <param name="_Buy">Цена покупки</param>
    /// <param name="_Sell">Цена продажи</param>
    public class Quotes : Interface
    {
        private List<double> _Buy;
        private List<DateTime> _TimeD;
        private List<int> _TimeU;
        private List<double> _Sell;
        public Quotes()
        {
        }

        public List<int> TimeU
        {
            get
            {
                return _TimeU;
                throw new System.NotImplementedException();
            }

            set
            {
                _TimeU = value;
            }
        }

        public List<double> Buy
        {
            get
            {
                return _Buy;
                throw new System.NotImplementedException();
            }

            set
            {
                _Buy = value;
            }
        }

        public List<DateTime> TimeD
        {
            get
            {
                return _TimeD;
                throw new System.NotImplementedException();
            }

            set
            {
               _TimeD = value;
            }
        }

        public List<double> Sell
        {
            get
            {
                return _Sell;
                throw new System.NotImplementedException();
            }

            set
            {
                _Sell = value;
            }
        }

        public void getData()
        {
            Console.WriteLine("");
            throw new System.NotImplementedException();
        }

        public void setData()
        {
            Console.WriteLine("");
            throw new System.NotImplementedException();
        }
    }
}