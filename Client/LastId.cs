using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
   public  class LastId
    {
        private  string commanda;

        public  string Last(string nameElement, string nameTable)
        {
            commanda = $"SELECT MAX({nameElement}) FROM dbo.{nameTable};";
            Console.WriteLine(commanda);
            return commanda;
        }
    }
}
