using AgroMarketRD.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroMarketRD.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get Guid
            Console.WriteLine($"Guid >> {Guid.NewGuid().ToString()}");  // Input key para cryptohelper
            Console.WriteLine($"Otro Guid >> {Guid.NewGuid().ToString()}"); // SALT KEY para cryptohelper

            Console.Write("Password en plain text >> ");

            string _input = Console.ReadLine();

            Console.WriteLine($"Cifrada >> {CryptoHelper.Encrypt(_input)}");

            Console.ReadLine();
        }
    }
}
