using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Database;

namespace Server
{
    public class Message
    {
        public string operation { get; set; }
        public List<Dictionary<string, string>> data { get; set; }
    }

    public class Server
    {
        private TcpListener _listener;
        private bool _isRunning;

        private readonly IPAddress _ipAddress;
        private readonly int _port;
        private Database.Database _database;

        public Server(IPAddress ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
            _listener = new TcpListener(_ipAddress, _port);
            _database = Database.Database.GetDatabase();
        }

        public void Start()
        {
            _listener.Start();
            _isRunning = true;
            Console.WriteLine($"Server started on {_ipAddress}:{_port}");

            while (_isRunning)
            {
                TcpClient client = _listener.AcceptTcpClient();
                Console.WriteLine("Client connected.");
                Thread thread = new Thread(() => HandleClient(client));
                thread.Start();
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            try
            {
                while (true)
                {
                    string message = reader.ReadLine();
                    if (message == null)
                        break;
                    Console.WriteLine($"Received: {message}");
                    Message receivedObj = JsonConvert.DeserializeObject<Message>(message);
                    switch (receivedObj.operation)
                    {
                        case "login":
                            // Handle login
                            Database.Utilizator utilizator = new Database.Utilizator(
                                receivedObj.data[0]["username"], receivedObj.data[0]["password"],
                                receivedObj.data[0]["role"]);
                            
                            bool r = _database.Login(utilizator);
                            Console.WriteLine("r: " + r);
                            if (r)
                            {
                                Console.WriteLine("Login successful.");
                                writer.WriteLine(JsonConvert.SerializeObject(new { status = "success" }));
                            }
                            else
                            {
                                Console.WriteLine("Login failed.");
                                writer.WriteLine(JsonConvert.SerializeObject(new { status = "failed" }));
                            }
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.StackTrace}\n{ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client disconnected.");
            }
        }
    }
}
