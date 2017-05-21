namespace Client
{
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

    class InsertEvent : Insert
    {
        public InsertEvent(string Value, List<Event> per, SqlConnection con) : base()
        {
            int Index = 0;
            // создание сущноcти строка запрос           
            InsertSplit.InsertSplit Request800 = new InsertSplit.InsertSplit(con);
            Converter Convert = new Converter();
            List<List<string>> Data = new List<List<string>>();
            // конвертируем все данные в стринг
            Data = Convert.toStringEvent(per);
            while (Index < (Data[0].Count - 1))
            {
                // запрос на добавление
                Request800.request(Value, Data, ref Index, 800);
                //  присвоение 
                CompleteInsert = Index;
                Console.WriteLine("Выполнено добавлений" + CompleteInsert);
            }
        }
    }
}
