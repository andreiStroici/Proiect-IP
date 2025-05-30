/*************************************************************************
 *                                                                       *
 *  File:        ClientBackend.cs                                        *
 *  Copyright:   (c) 2025, S. Andrei, A. Denisa                          *
 *                                                                       *
 *  Description: Aplicația care face legătura dintre interfață și Server.*
 *                                                                       *
 *                                                                       *
 *                                                                       *
 *  This code and information is provided "as is" without warranty of    *
 *  any kind, either expressed or implied, including but not limited     *
 *  to the implied warranties of merchantability or fitness for a        *
 *  particular purpose. You are free to use this source code in your     *
 *  applications as long as the original copyright notice is included.   *
 *                                                                       *      
 ************************************************************************ */

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;

namespace ClientBackend
{
    /// <summary>
    /// Această clasă va gestiona comunicarea dintre client (Interfața cu utilizatorul) și server.
    /// </summary>
    public class ClientBackend
    {
        private TcpListener _port;
        private TcpClient _server;

        /// <summary>
        /// Constructorul clasei ClientBackend. Caută adresa IP și portul pentru conectarea cu server-ul.
        /// </summary>
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
                    if (task.Wait(100))
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
        /// <summary>
        /// Acceptă conexiunea de la client și începe să asculte pentru mesaje.
        /// </summary>
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
        /// <summary>
        /// Această metodă va gestiona comunicarea cu clientul conectat.
        /// </summary>
        /// <param name="client"></param>
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
                        case "insertLoan":
                            Console.WriteLine("Insert loan request received.");
                            if (parts.Length == 4)
                            {
                                int subscriberId = int.Parse(parts[1]);
                                int bookId = int.Parse(parts[2]);
                                string selectedLocation = parts[3];

                                Console.WriteLine($"Insert loan attempt for Subscriber ID: {subscriberId}, Book ID: {bookId}, Location: {selectedLocation}");
                                bool success = insertLoan(subscriberId, bookId, selectedLocation);
                                writer.WriteLine(success ? "Inserted Loan successful." : "Inserted Loan failed.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid insertLoan format. Use: insertLoan|subscriberId|bookId|selectedLocation");
                                writer.WriteLine("Invalid insertLoan format. Use: insertLoan|subscriberId|bookId|selectedLocation");
                            }
                            break;
                        case "getLoans":
                            Console.WriteLine("Get loans request received.");
                            if(parts.Length == 2)
                            {
                                int subscriberId = int.Parse(parts[1]);
                                Console.WriteLine($"Get loans attempt for Subscriber ID: {subscriberId}");
                                response = getLoans(subscriberId);

                                if (response != "No loans found.")
                                {

                                    List<string> lines = response.Split('|').ToList();
                                    lines.RemoveAt(lines.Count - 1); // Remove the last empty line if exists1

                                    var books = lines.Select(line => line.Split('~')).Select(part => (part[0], part[1], part[2]));
                                    books = books.OrderBy(x => x.Item2)
                                        .ThenBy(x => x.Item3)
                                        .ThenBy(x => x.Item1);
                                    response = string.Join("|", books.Select(b => $"{b.Item1}~{b.Item2}~{b.Item3}"));
                                    Console.WriteLine("Loans found: " + response);
                                    writer.WriteLine(response);
                                }
                                else
                                {
                                    Console.WriteLine("No loans found.");
                                    writer.WriteLine("No loans found.");
                                }

                            }
                            else
                            {
                                Console.WriteLine("Invalid getLoans format. Use: getLoans|subscriberId");
                                writer.WriteLine("Invalid getLoans format. Use: getLoans|subscriberId");
                            }
                            break;
                        case "returnBook":
                            Console.WriteLine("Return book request received.");
                            if(parts.Length == 3)
                            {
                                int subscriberId = int.Parse(parts[1].Trim());
                                int bookId = int.Parse(parts[2].Trim());
                                Console.WriteLine($"Return book attempt for Subscriber ID: {subscriberId}, Book ID: {bookId}");
                                bool success = returnBook(subscriberId, bookId);
                                writer.WriteLine(success ? "Return successful." : "Return failed.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid returnBook format. Use: returnBook|subscriberId|bookId");
                                writer.WriteLine("Invalid returnBook format. Use: returnBook|subscriberId|bookId");
                            }
                            break;
                        case "getStatusClient":
                            Console.WriteLine("Get status client request received.");
                            if (parts.Length == 2)
                            {
                                int subscriberId = int.Parse(parts[1].Trim());
                                Console.WriteLine($"Get status client attempt for Subscriber ID: {subscriberId}");
                                response = getStatusClient(subscriberId);

                                writer.WriteLine(response);
                            }
                            else
                            {
                                Console.WriteLine("Invalid getStatusClient format. Use: getStatusClient|subscriberId");
                                writer.WriteLine("Invalid getStatusClient format. Use: getStatusClient|subscriberId");
                            }
                            break;
                        case "registerEmployee":
                            Console.WriteLine("Register employee request received.");
                            if (parts.Length == 4)
                            {
                                username = parts[1];
                                string password = parts[2];
                                string role = parts[3];
                                Console.WriteLine("Register employee attempt for Username: " + username + ", Password: " + password + ", Role: " + role);
                                bool success = register(username, password, role);
                                if (success)
                                {
                                    Console.WriteLine("Employee Register successful.");
                                    writer.WriteLine("Employee Register successful.");
                                }
                                else
                                {
                                    Console.WriteLine("Employee Register failed.");
                                    writer.WriteLine("Employee Register failed.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid registerEmployee format. Use: registerEmployee|username|password|role");
                                writer.WriteLine("Invalid registerEmployee format. Use: registerEmployee|username|password|role");
                            }
                            break;
                        case "deleteEmployee":
                            Console.WriteLine("Delete employee request received.");
                            if (parts.Length == 2)
                            {
                                username = parts[1];
                                Console.WriteLine("Delete employee attempt for Username: " + username);

                                bool success = deleteEmployee(username);

                                if(success)
                                {
                                    Console.WriteLine("Librarian deleted successful.");
                                    writer.WriteLine("Librarian deleted successful.");
                                }
                                else
                                {
                                    Console.WriteLine("Librarian deletion failed.");
                                    writer.WriteLine("Librarian deletion failed.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid deleteEmployee format. Use: deleteEmployee|username");
                                writer.WriteLine("Invalid deleteEmployee format. Use: deleteEmployee|username");
                            }
                            break;
                        case "addBook":
                            Console.WriteLine("Add book request received");
                            if (parts.Length == 6)
                            {
                                string ISBN = parts[1];
                                string title = parts[2];
                                string author = parts[3];
                                string publisher = parts[4];
                                string genre = parts[5];
                                Console.WriteLine($"Add book attempt for ISBN: {ISBN}, title: {title}, author: {author}, publisher: {publisher}, genre: {genre}");
                                bool success = addBook(ISBN, title, author, publisher, genre);
                                if (success)
                                {
                                    Console.WriteLine("Book added successful.");
                                    writer.WriteLine("Book added successful.");
                                }
                                else
                                {
                                    Console.WriteLine("Book addition failed.");
                                    writer.WriteLine("Book addition failed.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid addBook format. Use: addBook|isbn|title|author|publisher|genre");
                                writer.WriteLine("Invalid addBook format. Use: addBook|isbn|title|author|publisher|genre");
                            }
                            break;
                        case "searchBook":
                            Console.WriteLine("Search book request received.");
                            if (parts.Length == 2)
                            {
                                string isbn = parts[1];
                                
                                Console.WriteLine($"Searching attempt for book with ISBN: {isbn}");
                                response = searchBook(isbn);

                                if (response != "No books found with the given ISBN.")
                                {

                                    List<string> lines = response.Split('|').ToList();
                                    lines.RemoveAt(lines.Count - 1); // Remove the last empty line if exists1

                                    var books = lines.Select(line => line.Split('~')).Select(part => (part[0], part[1], part[2], part[3]));
                                    books = books.OrderBy(x => x.Item2)
                                        .ThenBy(x => x.Item3)
                                        .ThenBy(x => x.Item4)
                                        .ThenBy(x => x.Item1);
                                    response = string.Join("|", books.Select(b => $"{b.Item1}~{b.Item2}~{b.Item3}~{b.Item4}"));
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
                                Console.WriteLine("Invalid searchBook format. Use: searchBook|isbn");
                                writer.WriteLine("Invalid searchBook format. Use: searchBook|isbn");
                            }
                            break;
                        case "deleteBook":
                            Console.WriteLine("Delete book request received.");
                            if (parts.Length == 2)
                            {
                                int idBook = int.Parse(parts[1]);
                                Console.WriteLine($"Delete book attempt for ID: {idBook}");

                                bool success = deleteBook(idBook);
                                if (success)
                                {
                                    Console.WriteLine("Book deleted successful.");
                                    writer.WriteLine("Book deleted successful.");
                                }
                                else
                                {
                                    Console.WriteLine("Book deletion failed.");
                                    writer.WriteLine("Book deletion failed.");

                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid deleteBook format. Use: deleteBook|idBook");
                                writer.WriteLine("Invalid deleteBook format. Use: deleteBook|idBook");
                            }
                            break;
                        case "searchSubscribers":
                            Console.WriteLine("Search subscribers request received.");
                            response = searchSubscribers();
                            writer.WriteLine(response);
                            break;
                        case "updateStatus":
                            Console.WriteLine("Update status request received.");
                            if (parts.Length == 3)
                            {
                                int subscriberId = int.Parse(parts[1]);
                                string status = parts[2];

                                bool success = updateStatus(subscriberId, status);
                                if (success)
                                {
                                    Console.WriteLine("Status updated successful.");
                                    writer.WriteLine("Status updated successful.");
                                }
                                else
                                {
                                    Console.WriteLine("Status update failed.");
                                    writer.WriteLine("Status update failed.");
                                }

                            }
                            else
                            {
                                Console.WriteLine("Invalid updateStatus format. Use: updateStatus|subscriberId|status");
                                writer.WriteLine("Invalid updateStatus format. Use: updateStatus|subscriberId|status");
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client disconnected: " + ex.Message);
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client connection closed.");
            }
        }
        /// <summary>
        /// Trimite un mesaj către server.
        /// </summary>
        /// <param name="message"></param>
        private void sendMessage(string message)
        {
            NetworkStream stream = _server.GetStream();
            StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };
            writer.WriteLine(message);
        }
        /// <summary>
        /// Așteaptă un mesaj de la server și îl returnează.
        /// </summary>
        /// <returns></returns>
        private string WaitForMessage()
        {
            NetworkStream stream = _server.GetStream();
            StreamReader reader = new StreamReader(stream, new UTF8Encoding(false));

            string message = reader.ReadLine();

            return message;
        }
        /// <summary>
        /// Această metodă va gestiona logarea utilizatorului.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Această metodă va gestiona înregistrarea unui nou utilizator (angajat). 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private bool register(string username, string password, string role)
        {
            Console.WriteLine($"Registering user: {username} with password: {password} and role: {role}");
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "username", username }, { "password", password }, { "role", role } }
            };
            var obj = new
            {
                operation = "registerEmployee",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            if (response.Trim() == "Employee registered successful.")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Această metodă va înregistra un nou abonat (cititor) în sistem.
        /// </summary>
        /// <param name="lastname"></param>
        /// <param name="firstname"></param>
        /// <param name="address"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Această metodă va gestiona logarea unui abonat (cititor) în sistem.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Această metodă va căuta cărți în baza de date după titlu și/sau autor (se efectuează căutări parțiale).
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Această metodă va insera un nou împrumut în baza de date.
        /// </summary>
        /// <param name="subscriberId"></param>
        /// <param name="bookId"></param>
        /// <param name="selectedLocation"></param>
        /// <returns></returns>
        private bool insertLoan(int subscriberId, int bookId, string selectedLocation)
        {
            Console.WriteLine($"Inserting Loan with SubscriberId: {subscriberId}, bookId: {bookId} and selected location: {selectedLocation}");
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "subscriberId", subscriberId.ToString() }, { "bookId", bookId.ToString() }, { "selectedLocation", selectedLocation } }
            };
            var obj = new
            {
                operation = "insertLoan",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);

            string response = WaitForMessage();
            if (response.Trim() == "Inserted Loan successful.")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Această metodă va obține toate împrumuturile pentru un anumit abonat (cititor) din baza de date.
        /// </summary>
        /// <param name="subscriberId"></param>
        /// <returns></returns>
        private string getLoans(int subscriberId)
        {
            Console.WriteLine($"Getting loans for SubscriberId: {subscriberId}");
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "subscriberId", subscriberId.ToString() } }
            };
            var obj = new
            {
                operation = "getLoans",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);

            string response = WaitForMessage();
            Console.WriteLine("Response from server: " + response);
            return response;
        }
        /// <summary>
        /// Această metodă va returna o carte împrumutată de către un abonat (cititor) în baza de date.
        /// </summary>
        /// <param name="subscriberId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        private bool returnBook(int subscriberId, int bookId)
        {
            Console.WriteLine($"Returning book with SubscriberId: {subscriberId}, BookId: {bookId}");
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "subscriberId", subscriberId.ToString() }, { "bookId", bookId.ToString() } }
            };
            var obj = new
            {
                operation = "returnBook",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            if (response.Trim() == "Book returned successful.")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Această metodă va obține statusul unui abonat (cititor) din baza de date.
        /// </summary>
        /// <param name="subscriberId"></param>
        /// <returns></returns>
        private string getStatusClient(int subscriberId)
        {
            Console.WriteLine($"Getting status for SubscriberId: {subscriberId}");
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "subscriberId", subscriberId.ToString() } }
            };
            var obj = new
            {
                operation = "getStatusClient",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            Console.WriteLine("Response from server: " + response);
            return response;
        }
        /// <summary>
        /// Această metodă va șterge un angajat (bibliotecar) din baza de date.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private bool deleteEmployee(string username)
        {
            Console.WriteLine("Deleting employee with Username: " + username);
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "username", username } }
            };

            var obj = new
            {
                operation = "deleteLibrarian",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            Console.WriteLine("Response from server: " + response);
            if (response.Trim() == "Librarian deleted successful.")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Această metodă va adăuga o nouă carte în baza de date.
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="publisher"></param>
        /// <param name="genre"></param>
        /// <returns></returns>
        private bool addBook(string isbn, string title, string author, string publisher, string genre)
        {
            Console.WriteLine("Adding book with ISBN: " + isbn + ", Title: " + title + ", Author: " + author + ", Publisher: " + publisher + ", Genre: " + genre);
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "isbn", isbn }, { "title", title }, { "author", author }, { "publisher", publisher }, { "genre", genre } }
            };
            var obj = new
            {
                operation = "addBook",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            if (response == "Book added successful.")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Această metodă va căuta o carte în baza de date după ISBN.
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        private string searchBook(string isbn)
        {
            Console.WriteLine("Searching book with ISBN: " + isbn);
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "isbn", isbn } }
            };
            var obj = new
            {
                operation = "searchBook",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            Console.WriteLine("Response from server: " + response);
            return response;
        }
        /// <summary>
        /// Această metodă va șterge o carte din baza de date după ID-ul acesteia.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool deleteBook(int id)
        {
            Console.WriteLine("Deleting book with ID: " + id);
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "idBook", id.ToString() } }
            };
            var obj = new
            {
                operation = "deleteBook",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            if(response == "Book deleted successful.")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Această metodă va căuta toți abonații (cititorii) din baza de date.
        /// </summary>
        /// <returns></returns>
        private string searchSubscribers()
        {
            Console.WriteLine("Searching subscribers");
            var data = new List<Dictionary<string, string>>();
            var obj = new
            {
                operation = "searchSubscribers",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            Console.WriteLine("Response from server: " + response);
            return response;
        }
        /// <summary>
        /// Această metodă va actualiza statusul unui abonat (cititor) în baza de date.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private bool updateStatus(int id, string status)
        {
            Console.WriteLine($"Updating status for SubscriberId: {id} to Status: {status}");
            var data = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "subscriberId", id.ToString() }, { "status", status } }
            };
            var obj = new
            {
                operation = "updateStatus",
                data = data
            };
            string json = JsonConvert.SerializeObject(obj);
            sendMessage(json);
            string response = WaitForMessage();
            if (response.Trim() == "Status updated successful.")
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