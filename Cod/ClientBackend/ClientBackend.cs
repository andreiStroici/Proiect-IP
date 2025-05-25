using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace ClientBackend
{
    public class ClientBackend
    {
        private TcpListener _port;
        private TcpClient _server;

        public ClientBackend()
        {
            _port = new TcpListener(System.Net.IPAddress.Any, 8081);
            string subnet = "192.168.137.";
            int port = 12345;

            for (int i = 1; i <= 254; i++)
            {
                string ip = subnet + i;
                try
                {
                    Console.WriteLine($"Trying to connect to {ip} on port {port}...");
                    _server = new TcpClient();
                    var task = _server.ConnectAsync(ip, port);
                    if (task.Wait(50))
                    {
                        Console.WriteLine($"Server found at IP: {ip}");
                        break;
                    }
                }
                catch
                {
                    // I cannot connect to this IP, continue searching
                }
            }
        }

        public void AcceptConnection()
        {
            _port.Start();
            while (true)
            {
                TcpClient client = _port.AcceptTcpClient();
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
                string username = "";
                while (true)
                {
                    string message = reader.ReadLine();
                    if (message == null)
                        break;

                    Console.WriteLine("Received: " + message + "\t" + username);
                    string[] parts = message.Split('|');
                    
                    switch (parts[0])
                    {
                        case "login":
                            Console.WriteLine("Login request received.");
                            if (parts.Length == 4)
                            {
                                string role = parts[1];
                                username = parts[2];
                                string password = parts[3];
                                bool success = login(username, password, role);
                                Console.WriteLine($"Login attempt for user: {username} with password: {password}");
                                writer.WriteLine(success ? "Login successful" : "Login failed");
                            }
                            else
                            {
                                Console.WriteLine("Invalid login format. Use: login|username|password");
                                writer.WriteLine("Invalid login format. Use: login|username|password");
                            }
                            Console.WriteLine("Finalizare logare");
                            break;

                    
                        case "logout":
                            var logoutData = new List<Dictionary<string, string>> {
                                new Dictionary<string, string> { { "username", username } }
                            };

                            var obj = new
                            {
                                operation = "logout",
                                data = logoutData
                            };
                            string json = JsonConvert.SerializeObject(obj);
                            sendMessage(json);
                            break;
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Client disconnected: " + ex.Message);
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client connection closed.");
            }
        }

        private void sendMessage(string message)
        {
            NetworkStream stream = _server.GetStream();
            StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };
            writer.WriteLine(message);
        }

        private string WaitForMessage()
        {
            NetworkStream stream = _server.GetStream();
            StreamReader reader = new StreamReader(stream, new UTF8Encoding(false));

            string message = reader.ReadLine();

            return message;
        }


        private bool login(string username, string password, string role)
        {
            // Implement your login logic here
            Console.WriteLine($"Logging in user: {username} with password: {password}");
            // For example, you might want to check the credentials against a database or an in-memory store
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "username", username }, { "password", password }, { "role", role }}
            };

            var obj = new
            {
                operation = "login",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            if (response.Trim() == "Login successful")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
