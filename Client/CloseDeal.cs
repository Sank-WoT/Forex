using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class CloseDeal
    {
        private double openDeal;
        private double closeDeal;
        private double profit;
        public CloseDeal(string chislo, List<double> BufferB, List<double> BufferS)
        {
            if (chislo.Contains("  покупка") == true || chislo.Contains("       buy") == true)
            {
                double p = Convert.ToDouble(chislo.Remove(chislo.Length - 9, 9));
               this. profit = Math.Round((BufferS[BufferS.Count - 1] - p), 4);
            }

            if (chislo.Contains("  продано") == true || chislo.Contains("      sell") == true)
            {
                double p = Convert.ToDouble(chislo.Remove(chislo.Length - 9, 9));
                this.profit = Math.Round((p - BufferB[BufferB.Count - 1]), 4);
            }
        }

        public double getProfit()
        {
            return profit;
        }
    }
}
