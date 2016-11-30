using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class Quotes : Interface
    {
        private List<int>  _Time;
        private List<double> _Buy;
        private List<DateTime> _TimeD;
        private List<double> _Sell;
        public Quotes()
        {
        }

        public List<int> Time
        {
            get
            {
                return _Time;
                throw new System.NotImplementedException();
            }

            set
            {
                _Time = value;
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