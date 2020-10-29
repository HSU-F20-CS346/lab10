using System;
using System.Collections.Generic;
using System.Text;

namespace Lab10
{
    public class ForwardingTable
    {

        /// <summary>
        /// Key: Name of Router
        /// Value: routing specifics for the given router
        /// </summary>
        public Dictionary<string, ForwardingTableEntry> Entries { get; set; }
        public ForwardingTable()
        {
            Entries = new Dictionary<string, ForwardingTableEntry>();
        }
    }
}
