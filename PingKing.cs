using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS.PingKing
{
    class PingKing
    {
        public static void Main()
        {
            Console.WriteLine("Ping King Console App (Development)");
            UserPrompt();
        }

        /// <summary>
        /// Prompts the user for UI input
        /// </summary>
        public static void UserPrompt()
        {
            string input;

            do
            {
                Console.WriteLine("Select an option:\n1) ICMP\n2) HTTP\n3) Exit");
                Console.Write(">> ");
                input = Console.ReadLine();

                ValidateUserInput(input);
            } while (input != "42");
        }

        /// <summary>
        /// Validates UI input
        /// </summary>
        /// <param name="input">A string containing user's input from the UI</param>
        public static void ValidateUserInput(string input)
        {
            string targetAddress;

            if (input == "1")
            {
                Console.Write("\nEnter IP address or domain to ping: ");
                targetAddress = Console.ReadLine();
                ICMP icmp = new ICMP(targetAddress);
                icmp.SendICMP();
            } else if (input == "2")
            {
                Console.Write("\nEnter HTTP or HTTPS address to query: ");
                targetAddress = Console.ReadLine();
                HTTP http = new HTTP(targetAddress);
                Task httpTask = http.SendHTTPAsync();
            } else if (input == "3")
            {
                Environment.Exit(0);
            }else
            {
                Console.WriteLine("\nInvalid input. Try again.\n");
                UserPrompt();
            }                
        }
    }
}