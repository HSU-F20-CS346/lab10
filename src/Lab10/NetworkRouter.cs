using System;
using System.Collections.Generic;
using System.Text;

namespace Lab10
{
    /// <summary>
    /// Modeled on the forwarding tables presented in Chapter 20.
    /// </summary>
    public class NetworkRouter
    {
        public string Name { get; set; }
        public ForwardingTable ForwardingTable { get; set; }

        /// <summary>
        /// Key represents the next router in the sequence.
        /// Value represents the cost to take the edge.  In RIP, the cost is always 1.
        /// For OSPF, the cost can vary and will influence the route taken.
        /// </summary>
        public Dictionary<NetworkRouter, int> DirectConnections { get; set; }

        public NetworkRouter()
        {
            ForwardingTable = new ForwardingTable();
            DirectConnections = new Dictionary<NetworkRouter, int>();
        }
    }
}
