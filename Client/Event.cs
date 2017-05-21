using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class Event : Information
    {
        public int IdEvent_Group;
        public int Time;
        public double FactNumber;
        public double ExpectNumber;
        public int idEvent;
        public string Pot;
        public string unitNumberst;
        public int idCountry;
        public EventGroup EventG;
        public Event() : base()
        {
        }
    }
}