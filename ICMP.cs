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
        /// Constructor assigns the IPv4 address to be send an ICMP ping to the pingAddress class field
        /// </summary>
        /// <param name="address">IPv4 address in string form</param>
        public ICMP(string address)
        {
            pingAddress = address;
            packetInfo[3] = 9999;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SendICMP()
        {
            Ping icmp = new Ping();
            PingOptions options = new PingOptions();
            PingReply reply;

            options.DontFragment = true;
            string buffer = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            for (int i = 0; i < 4; i++)
            {
                packetInfo[0]++;
                reply = icmp.Send(pingAddress, 120, Encoding.ASCII.GetBytes(buffer), options);

                if (reply == null)
                {
                    packetInfo[2]++;
                    Console.WriteLine("ICMP Ping Failed!");
                }
                else
                {
                    packetInfo[1]++;

                    if (reply.RoundtripTime < packetInfo[3])
                        packetInfo[3] = (int)reply.RoundtripTime;
                    if (reply.RoundtripTime > packetInfo[4])
                        packetInfo[4] = (int)reply.RoundtripTime;

                    packetInfo[5] += (int)reply.RoundtripTime;
                }

                DisplayReply(reply);
            }

            packetInfo[5] = packetInfo[5] / 4;
            DisplayStats(packetInfo);
        }

        public void DisplayReply(PingReply reply)
        {
            Console.WriteLine("Reply from address: {0} time={1}ms TTL={2} buffer={3}",
                reply.Address.ToString(), reply.RoundtripTime, reply.Options.Ttl, reply.Buffer.Length);
        }

        public void DisplayStats(int[] pingData)
        {
            /*
           int totalPackets = 0,
               packetsRecieved = 0,
               packetsLost = 0,
               minTrip = 9999,
               maxTrip = 0,
               averageTrip = 0;
           */
            Console.WriteLine("Ping statistics for {0}", pingAddress);
            Console.WriteLine("Packets: Sent = {0}, Recieved = {1}, Lost = {2} ({3}% loss)", packetInfo[0], packetInfo[1], packetInfo[2], (packetInfo[2] / packetInfo[0]) * 100);
            Console.WriteLine("Round Trip Times:");
            Console.WriteLine("Min = {0}ms, Max = {1}ms, Avg = {2}ms", packetInfo[3], packetInfo[4], packetInfo[5]);
        }
    }
}