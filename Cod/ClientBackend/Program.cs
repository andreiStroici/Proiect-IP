using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientBackend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client Backend is running...");
            // Here you can initialize and start your client backend logic
            // For example, you might want to create an instance of ClientBackend and call AcceptConnection
            ClientBackend clientBackend = new ClientBackend();
            clientBackend.AcceptConnection();
        }
    }
}
