using System;
using System.Collections.Generic;
using System.Text;

namespace YTL
{
    class Param
    {
        public string Type { get; }
        public string Filter { get; }
            
        public Param(string Type, string Filter)
        {
            this.Type = Type;
            this.Filter = Filter;
        }
    }
}
