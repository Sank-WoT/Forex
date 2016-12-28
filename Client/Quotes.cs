using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    /// <summary>
    ///  Класс котировка
    /// </summary>
    public class Quotes
    {
        /// <summary>
        /// Лист UnixTime времени
        /// </summary>
        private List<double> _Buy;
        /// <summary>
        /// Лист DateTime времени 
        /// </summary>
        private List<DateTime> _TimeD;
        /// <summary>
        /// Цена покупки
        /// </summary>
        private List<int> _TimeU;
        /// <summary>
        /// Цена продажи
        /// </summary>
        private List<double> _Sell;
        public Quotes()
        {
        }
        /// <summary>
        /// Свойство UnixTime времени времени 
        /// </summary>
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
        /// <summary>
        /// Свойство Цена покупки
        /// </summary>
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
        /// <summary>
        /// Свойство DateTime времени 
        /// </summary>
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
        /// <summary>
        /// Свойство Цена продажи
        /// </summary>
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
    }
}