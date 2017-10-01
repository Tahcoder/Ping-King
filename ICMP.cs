using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TTS.PingKing {
    class ICMP {

        private string pingAddress;
        private int[] packetInfo = new int[6];

        /// <summary>
        /// Constructor assigns the IPv4 address to class field
        /// </summary>
        /// <param name="address">IPv4 address in string form</param>
        public ICMP(string address)
        {
            pingAddress = address;
            packetInfo[3] = 9999;
        }

        /// <summary>
        /// Displays results of individual ICMP pings
        /// </summary>
        /// <param name="reply">PingReply object containing ICMP results</param>
        public void DisplayReply(PingReply reply)
        {
            Console.WriteLine("Reply from address: {0} time={1}ms TTL={2} buffer={3}",
                reply.Address.ToString(), reply.RoundtripTime, reply.Options.Ttl, reply.Buffer.Length);
        }

        /// <summary>
        /// Displays statistical information about batch of ICMP pings
        /// </summary>
        /// <param name="pingData">Array with packet info from ICMP ping batch</param>
        public void DisplayStats()
        {
            Console.WriteLine("\nPing statistics for {0}", pingAddress);
            Console.WriteLine("\tPackets: Sent = {0}, Recieved = {1}, Lost = {2} ({3}% loss)", 
                packetInfo[0], packetInfo[1], packetInfo[2], (packetInfo[2] / packetInfo[0]) * 100);
            Console.WriteLine("Round Trip Times:");
            Console.WriteLine("\tMin = {0}ms, Max = {1}ms, Avg = {2}ms", packetInfo[3], packetInfo[4], packetInfo[5]);
        }

        /// <summary>
        /// Sends and listens for response from an ICMP ping then displays the results
        /// </summary>
        public void SendICMP()
        {
            Ping icmp = new Ping();
            PingOptions options = new PingOptions();
            PingReply reply;

            options.DontFragment = true;
            string buffer = "Winter is coming. - House Stark.";

            for (int i = 0; i < 4; i++)
            {
                packetInfo[0]++;
                try
                {
                    reply = icmp.Send(pingAddress, 3000, Encoding.ASCII.GetBytes(buffer), options);

                    Boolean isValid = ValidateReply(reply);

                    if (isValid)
                    {
                        packetInfo[1]++;
                        StatsTracker(reply);
                        DisplayReply(reply);
                    }
                } catch (PingException pingE)
                {
                    Console.WriteLine("\nINVALID USER INPUT\n");
                    Console.WriteLine(pingE.ToString());
                    packetInfo[2]++;
                    packetInfo[3] = 0;
                    break;
                }
            }

            packetInfo[5] = packetInfo[5] / packetInfo[0];
            DisplayStats();
        }

        /// <summary>
        /// Updates packetInfo with the data from the lastest PingReply
        /// </summary>
        /// <param name="reply">Latest PingReply</param>
        public void StatsTracker(PingReply reply)
        {
            if ((int)reply.RoundtripTime < packetInfo[3])
                packetInfo[3] = (int)reply.RoundtripTime;
            if ((int)reply.RoundtripTime > packetInfo[4])
                packetInfo[4] = (int)reply.RoundtripTime;
            packetInfo[5] += (int)reply.RoundtripTime;
        }

        /// <summary>
        /// Validates whether the lastest PingReply was successful and outputs to user accordingly
        /// </summary>
        /// <param name="reply">Latest PingReply</param>
        /// <returns></returns>
        public Boolean ValidateReply(PingReply reply)
        {
            if (reply == null)
            {
                packetInfo[2]++;
                Console.WriteLine("ICMP PingReply is null");
                return false;
            }
            else if (reply.Status == IPStatus.TimedOut)
            {
                packetInfo[2]++;
                StatsTracker(reply);
                Console.WriteLine("Ping timed out.");
                return false;
            }
            else if(reply.Status != IPStatus.Success) 
            {
                packetInfo[2]++;
                StatsTracker(reply);
                Console.WriteLine("Derp Derp. Something went wrong.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}