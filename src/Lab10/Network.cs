using System;
using System.Collections.Generic;
using System.Text;

namespace Lab10
{
    public class Network
    {
        public Dictionary<string, NetworkRouter> Routers { get; set; }

        public Network()
        {
            Routers = new Dictionary<string, NetworkRouter>();
        }

        /// <summary>
        /// Generates routing tables for each NetworkRouter in the Network. 
        /// </summary>
        public void GenerateRoutingTables()
        {

        }

        /// <summary>
        /// Generates a path of routers that a packet will follow.  E.g. the path from R3 to R8 would be R3, R1, R6, R8
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sink"></param>
        /// <returns></returns>
        public List<string> GeneratePath(string source, string sink)
        {
            List<string> path = new List<string>();
            return path;
        }

        /// <summary>
        /// Generates a sample network for us to test against
        /// </summary>
        /// <returns></returns>
        public static Network GenerateSampleNetwork()
        {
            Network network = new Network();
            network.Routers.Add("R1", new NetworkRouter());
            network.Routers.Add("R2", new NetworkRouter());
            network.Routers.Add("R3", new NetworkRouter());
            network.Routers.Add("R4", new NetworkRouter());
            network.Routers.Add("R5", new NetworkRouter());
            network.Routers.Add("R6", new NetworkRouter());
            network.Routers.Add("R7", new NetworkRouter());
            network.Routers.Add("R8", new NetworkRouter());

            network.Routers["R1"].Name = "R1";
            network.Routers["R1"].DirectConnections.Add(network.Routers["R2"], 1);
            network.Routers["R1"].DirectConnections.Add(network.Routers["R3"], 1);
            network.Routers["R1"].DirectConnections.Add(network.Routers["R4"], 1);
            network.Routers["R1"].DirectConnections.Add(network.Routers["R5"], 1);
            network.Routers["R1"].DirectConnections.Add(network.Routers["R6"], 1);

            network.Routers["R2"].Name = "R2";
            network.Routers["R2"].DirectConnections.Add(network.Routers["R1"], 1);

            network.Routers["R3"].Name = "R3";
            network.Routers["R3"].DirectConnections.Add(network.Routers["R1"], 1);

            network.Routers["R4"].Name = "R4";
            network.Routers["R4"].DirectConnections.Add(network.Routers["R1"], 1);

            network.Routers["R5"].Name = "R5";
            network.Routers["R5"].DirectConnections.Add(network.Routers["R1"], 1);

            network.Routers["R6"].Name = "R6";
            network.Routers["R6"].DirectConnections.Add(network.Routers["R1"], 1);
            network.Routers["R6"].DirectConnections.Add(network.Routers["R7"], 1);
            network.Routers["R6"].DirectConnections.Add(network.Routers["R8"], 1);

            network.Routers["R7"].Name = "R7";
            network.Routers["R7"].DirectConnections.Add(network.Routers["R6"], 1);

            network.Routers["R8"].Name = "R8";
            network.Routers["R8"].DirectConnections.Add(network.Routers["R8"], 1);

            return network;
        }
    }
}
