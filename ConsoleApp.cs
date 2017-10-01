using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS.PingKing {
    class PingKingConsoleApp {

        public static string inputIP;
        public static ICMP icmp;

        public static void Main() {
            Console.WriteLine("PingKing Console App (Development)");
            UserPrompt();
        }

        public static void UserPrompt()
        {
            int again = 0;
            string input;

            do
            {
                Console.Write("\nEnter IP Address to ping: ");
                inputIP = Console.ReadLine();
                icmp = new ICMP(inputIP);
                icmp.SendICMP();

                Console.Write("Run again (y/n)? ");
                input = Console.ReadLine();
                if (input == "n" || input == "N")
                    again = 1;
            } while (again == 0);
        }
    }
}