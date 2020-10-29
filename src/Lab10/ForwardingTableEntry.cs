using System;
using System.Collections.Generic;
using System.Text;

namespace Lab10
{
    public class ForwardingTableEntry
    {
        public string Destination { get; set; }
        public string NextRouter { get; set; }
        public int Cost { get; set; }
    }
}
