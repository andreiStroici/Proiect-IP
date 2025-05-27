using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
                            Console.WriteLine("Logout request received.");
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

                        case "registerSubscriber":
                            Console.WriteLine("Register subscriber request received");
                            if (parts.Length == 6)
                            {
                                string lastname = parts[1];
                                string firstname = parts[2];
                                string address = parts[3];
                                string phoneNumber = parts[4];
                                string email = parts[5];
                                bool success = registerSubscriber(lastname, firstname, address, phoneNumber, email);
                                Console.WriteLine($"Register subscriber attempt for: {lastname} {firstname}, Address: {address}, Phone: {phoneNumber}, Email: {email}");
                                writer.WriteLine(success ? "Subscriber Register successful" : "Subscriber Register failed");
                            }
                            else
                            {
                                Console.WriteLine("Invalid registerSubscriber format. Use: registerSubscriber|lastname|firstname|address|phoneNumber|email");
                                writer.WriteLine("Invalid registerSubscriber format. Use: registerSubscriber|lastname|firstname|address|phoneNumber|email");
                            }
                            Console.WriteLine("Finalizare inregistrare abonat");
                            break;
                        case "loginSubscriber":
                            string response = loginSubscriber(parts[1]);
                            writer.WriteLine(response);
                            break;
                        case "searchBooks":
                            Console.WriteLine("Search books request received.");
                            if (parts.Length == 3)
                            {
                                string title = parts[1];
                                string author = parts[2];
                                Console.WriteLine($"Searching attempt for books with Title: {title} and Author: {author}");
                                response = getBooks(title, author);

                                if (response != "No books found.")
                                {

                                    List<string> lines = response.Split('|').ToList();
                                    lines.RemoveAt(lines.Count - 1); // Remove the last empty line if exists1

                                    var books = lines.Select(line => line.Split('~')).Select(part => (part[0], part[1], part[2]));
                                    books = books.OrderBy(x => x.Item2)
                                        .ThenBy(x => x.Item3)
                                        .ThenBy(x => x.Item1);
                                    response = string.Join("|", books.Select(b => $"{b.Item1}~{b.Item2}~{b.Item3}"));
                                    Console.WriteLine("Books found: " + response);
                                    writer.WriteLine(response);
                                }
                                else
                                {
                                    Console.WriteLine("No books found.");
                                    writer.WriteLine("No books found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid searchBooks format. Use: searchBooks|title|author");
                                writer.WriteLine("Invalid searchBooks format. Use: searchBooks|title|author");
                            }
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
            if (response.Trim() == "Login successful.")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool registerSubscriber(string lastname, string firstname, string address, string phoneNumber, string email)
        {
            Console.WriteLine($"Register subscriber with info: Lastname: {lastname}, Firstname: {firstname}, Address: {address}, PhoneNumber: {phoneNumber}, Email: {email}");

            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "nume", lastname }, { "prenume", firstname }, { "adresa", address }, { "telefon", phoneNumber }, { "email", email } }
            };

            var obj = new
            {
                operation = "registerSubscriber",
                data = data
            };

            string json = JsonConvert.SerializeObject(obj);
            Console.WriteLine(json);
            sendMessage(json);
            string response = WaitForMessage();
            if (response.Trim() == "Subscriber Register successful.")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string loginSubscriber(string username)
        {
            Console.WriteLine($"Logging in subscriber: {username}");

            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "username", username } }
            };

            var obj = new
            {
                operation = "loginSubscriber",
                data = data
            };

            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            Console.WriteLine("Response from server: " + response);
            return response;
        }

        private string getBooks(string title, string author)
        {
            Console.WriteLine($"Searching for books with Title: {title} and Author: {author}");
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "title", title }, { "author", author } }
            };
            var obj = new
            {
                operation = "searchBooks",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            Console.WriteLine("Response from server: " + response);
            return response;
        }
    }
}
