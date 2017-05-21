using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Client
{
    public abstract class ParserFabric
    {
        public abstract void Parse(StreamReader a);
    }
}