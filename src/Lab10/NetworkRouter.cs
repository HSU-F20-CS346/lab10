using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab10
{
    /// <summary>
    /// Modeled on the forwarding tables presented in Chapter 20.
    /// </summary>
    public class NetworkRouter
    {
        public string Name { get; private set; }
        public ForwardingTable ForwardingTable { get; set; }

        /// <summary>
        /// Key represents the next router in the sequence.
        /// Value represents the cost to take the edge.  In RIP, the cost is always 1.
        /// For OSPF, the cost can vary and will influence the route taken.
        /// </summary>
        public Dictionary<NetworkRouter, int> DirectConnections { get; set; }

        public bool UpdateForwardingTable()
        {
            bool informationWasUpdated = false;
            foreach (var directConnectionKvp in DirectConnections)
            {
                var adjacentRouter = directConnectionKvp.Key;
                var weight = directConnectionKvp.Value;

                //ask adjacent router for its information
                foreach (var adjacentTableEntry in adjacentRouter.ForwardingTable.Entries.Values.ToList())
                {
                    //do we have a record of the cousin?
                    if (ForwardingTable.Entries.ContainsKey(adjacentTableEntry.Destination))
                    {
                        //is this new information better than what we already know?
                        var currentEntry = ForwardingTable.Entries[adjacentTableEntry.Destination];
                        var updatedCost = weight + adjacentTableEntry.Cost;
                        if (currentEntry.Cost > updatedCost)
                        {
                            //we had a bad entry, update
                            currentEntry.Cost = updatedCost;
                            currentEntry.NextRouter = adjacentRouter.Name;
                            informationWasUpdated = true;
                        }
                    }
                    else
                    {
                        //we just learned that we have a cousin, make new record
                        ForwardingTable.Entries.Add(adjacentTableEntry.Destination, new ForwardingTableEntry()
                        {
                            Cost = weight + adjacentTableEntry.Cost,
                            Destination = adjacentTableEntry.Destination,
                            NextRouter = adjacentRouter.Name
                        });
                        informationWasUpdated = true;
                    }
                }

            }

            return informationWasUpdated;
        }

        public NetworkRouter(string name)
        {
            ForwardingTable = new ForwardingTable();
            DirectConnections = new Dictionary<NetworkRouter, int>();
            Name = name;
            ForwardingTable.Entries.Add(
                name,
                new ForwardingTableEntry()
                {
                    Cost = 0,
                    Destination = name,
                    NextRouter = name
                }
            );
        }
    }
}
