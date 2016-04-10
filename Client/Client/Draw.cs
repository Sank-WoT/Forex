using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
   public class Draw
    {
       public List<double> MainValue(List<double> Buffer, List<double> File, int tic)
       {
           List<double> Value = new List<double>();
           for(int h = 0 ; tic >= h; h++)
           {
               Value.Add(File[File.Count - 1 - tic + h]);
           }

           for (int h = 0; tic >= h; h++)
           {
               Value.Add(Buffer[h]);
           }
           return Value;
       }
       public List<DateTime> MainTime(List<DateTime> Ftime, List<DateTime> ITime, int tic)
       {
           List<DateTime> Value = new List<DateTime>();
           for (int h = 0; tic >= h; h++)
           {
               Value.Add(Ftime[Ftime.Count - 1 - tic + h]);
           }

           for (int h = 0; tic >= h; h++)
           {
               Value.Add(ITime[h]);
           }
           return Value;
       }
    }
}// Додумать по поводу выхода индекса за пределы листа
