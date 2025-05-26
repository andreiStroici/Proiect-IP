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
using System.Diagnostics.Contracts;

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
        volatile private Dictionary<string, string> connectedClients = new Dictionary<string, string>();

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
            string username = string.Empty;

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
                            username = receivedObj.data[0]["username"];
                            bool r = _database.Login(utilizator);
                            Console.WriteLine("r: " + r);
                            if (r)
                            {
                                if (connectedClients.ContainsKey(utilizator.Nume) == false)
                                {
                                    Console.WriteLine("Login successful.");
                                    writer.WriteLine("Login successful.");
                                    connectedClients[utilizator.Nume] = utilizator.Rol;
                                }
                                else
                                {
                                    writer.WriteLine("Login failed.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Login failed.");
                                writer.WriteLine("Login failed.");
                            }
                            break;
                        case "logout":
                            // Handle logout
                            if (receivedObj.data.Count > 0)
                            {
                                var firstKey = connectedClients.Keys.First();
                                bool removed = connectedClients.Remove(firstKey);
                                if (removed)
                                {

                                    Console.WriteLine("Logout successful.");
                                }
                                else
                                {
                                    Console.WriteLine("Logout failed.");
                                }
                            }
                            break;

                        case "registerSubscriber":
                            // Handle subscriber registration
                            Database.Abonat abonat = new Database.Abonat(
                                receivedObj.data[0]["nume"],
                                receivedObj.data[0]["prenume"],
                                receivedObj.data[0]["adresa"],
                                receivedObj.data[0]["telefon"],
                                receivedObj.data[0]["email"]
                            );

                            r = _database.InsertClient(abonat);
                            Console.WriteLine("r: " + r);
                            if (r)
                            {
                                Console.WriteLine("Subscriber Register successful.");
                                writer.WriteLine("Subscriber Register successful.");
                            }
                            else
                            {
                                Console.WriteLine("Subscriber registration failed.");
                                writer.WriteLine("Subscriber registration failed.");
                            }
                            break;
                        case "loginSubscriber":
                            abonat = _database.GetAbonatByPhone(receivedObj.data[0]["username"]);
                            if (abonat == null)
                            {
                                Console.WriteLine("Subscriber not found.");
                                writer.WriteLine("Subscriber Login failed");
                            }
                            else
                            {
                                Console.WriteLine("Subscriber found.");
                                writer.WriteLine($"Subscriber Login successful|{abonat.IdAbonat}|{abonat.Status}");
                            }
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client disconnected.");
                if (!string.IsNullOrEmpty(username) && connectedClients.ContainsKey(username))
                {
                    connectedClients.Remove(username);
                    Console.WriteLine($"Client {username} removed from connected clients.");
                }
            }
        }
    }
}
