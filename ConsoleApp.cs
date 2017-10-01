using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS.PingKing {
    class PingKingConsoleApp {
        static void Main() {

            string inputIP;
            ICMP icmp;

            Console.WriteLine("PingKing Console App (Development)");
            Console.WriteLine("Enter IP Address to ping:");
            inputIP = Console.ReadLine();

            icmp = new ICMP(inputIP);
            icmp.SendICMP();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}