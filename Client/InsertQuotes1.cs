namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    class insertQuotes : Insert
    {
        public insertQuotes(string Value, Quotes per, SqlConnection con) : base()
        {
            int Index = 0;
            // длина 
            Console.WriteLine("Длина  " + per.Sell.Count);
            // дополнительный сдвиг
            // создание сущноcти строка запрос           
            InsertSplit.InsertSplit Request800 = new InsertSplit.InsertSplit(con);
            Converter Convert = new Converter();
            List<List<string>> Data = new List<List<string>>();
            // конвертируем все данные в стринг
            Data = Convert.toStringQuotes(per.TimeU, per.Buy, per.Sell);
            while (Index < (per.Sell.Count - 1))
            {
                // запрос на добавление
                Request800.request(Value, Data, ref Index, 800);
                //  присвоение 
                CompleteInsert = Index;
            }
        }
    }
}
