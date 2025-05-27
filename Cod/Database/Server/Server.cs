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
                        case "searchBooks":
                            Console.WriteLine($"Cautare carti");
                            List<Carte> books = _database.CautareCartiPartial(receivedObj.data[0]["title"], receivedObj.data[0]["author"]);
                            Console.WriteLine($"Found {books.Count} books.");
                            if (books.Count == 0)
                            {
                                Console.WriteLine("No books found.");
                                writer.WriteLine("No books found.");
                            }
                            else
                            {
                                string response = "";
                                foreach (var item in books)
                                {
                                    response = item.IdCarte + "~" + item.Titlu + "~" + item.Autor + "|";
                                }
                                Console.WriteLine(response);
                                writer.WriteLine(response);
                            }
                            break;
                        case "insertLoan":
                            Console.WriteLine($"Inserting loan");
                            Console.WriteLine($"{receivedObj.data[0]["subscriberId"]}\t" +
                                $"{receivedObj.data[0]["bookId"]}" +
                                $"{receivedObj.data[0]["selectedLocation"]}");
                            int idAbonat = int.Parse(receivedObj.data[0]["subscriberId"].Trim());
                            int idCarte = int.Parse(receivedObj.data[0]["bookId"].Trim());
                            string selectedLocation = receivedObj.data[0]["selectedLocation"].Trim();
                            bool insertResult = _database.InsertLoan(idAbonat, idCarte, selectedLocation);
                            if (insertResult)
                            {
                                Console.WriteLine("Loan inserted successfully.");
                                writer.WriteLine("Inserted Loan successful.");
                            }
                            else
                            {
                                Console.WriteLine("Loan insertion failed.");
                                writer.WriteLine("Inserted Loan failed.");
                            }
                            break;
                        case "getLoans":
                            Console.WriteLine($"Getting loans for subscriber {receivedObj.data[0]["subscriberId"]}");
                            int subscriberId = int.Parse(receivedObj.data[0]["subscriberId"].Trim());
                            List<Carte> loans = _database.GetLoanedBooks(subscriberId);
                            if (loans.Count == 0)
                            {
                                Console.WriteLine("No loans found for this subscriber.");
                                writer.WriteLine("No loans found.");
                            }
                            else
                            {
                                string response = "";
                                foreach (var item in loans)
                                {
                                    response += $"{item.IdCarte}~{item.Titlu}~{item.Autor}|";
                                }
                                Console.WriteLine($"Loans found: {response}");
                                writer.WriteLine(response);
                            }
                            break;
                        case "returnBook":
                            Console.WriteLine($"{receivedObj.data[0]["subscriberId"]}\t" +
                                $"{receivedObj.data[0]["bookId"]}");
                            int subscriberIdReturn = int.Parse(receivedObj.data[0]["subscriberId"].Trim());
                            int bookIdReturn = int.Parse(receivedObj.data[0]["bookId"].Trim());
                            bool returnResult = _database.ReturnBook(subscriberIdReturn, bookIdReturn);
                            if (returnResult) 
                            {
                                Console.WriteLine("Book returned successful.");
                                writer.WriteLine("Book returned successful.");
                            }
                            else
                            {
                                Console.WriteLine("Book return failed.");
                                writer.WriteLine("Book return failed.");
                            }
                            break;
                        case "getStatusClient":
                            Console.WriteLine($"{receivedObj.data[0]["subcriberId"]}");
                            int subscriberIdStatus = int.Parse(receivedObj.data[0]["subcriberId"].Trim());
                            string abonatStatus = _database.GetStatusAbonat(subscriberIdStatus);
                            if (abonatStatus != null) 
                            {
                                Console.WriteLine("Status found: " + abonatStatus);
                                writer.WriteLine($"Status found: {abonatStatus}");
                            }
                            else
                            {
                                Console.WriteLine("Status not found.");
                                writer.WriteLine("Status not found.");
                            }
                            break;
                        case "registerEmployee":
                            Console.WriteLine($"Registering employee:{receivedObj.data[0]["username"]}\t" +
                                $"{receivedObj.data[0]["password"]}\t" +
                                $"{receivedObj.data[0]["role"]}");
                            bool validate = _database.InsertUser(new Utilizator(receivedObj.data[0]["username"],
                                receivedObj.data[0]["password"], receivedObj.data[0]["role"]));
                            if (validate) 
                            {
                                writer.WriteLine("Employee registered successful.");
                                Console.WriteLine("Employee registered successful.");
                            }
                            else
                            {
                                writer.WriteLine("Employee registration failed.");
                                Console.WriteLine("Employee registration failed.");
                            }
                            break;
                        case "deleteLibrarian":
                            Console.WriteLine($"Deleting librarian: {receivedObj.data[0]["username"]}");
                            string usernameToDelete = receivedObj.data[0]["username"];
                            bool deleteResult = _database.DeleteUser(usernameToDelete);
                            if (deleteResult) 
                            {
                                writer.WriteLine("Librarian deleted successful.");
                                Console.WriteLine("Librarian deleted successful.");
                            }
                            else
                            {
                                writer.WriteLine("Librarian deletion failed.");
                                Console.WriteLine("Librarian deletion failed.");
                            }
                            break;
                        case "addBook":
                            string title = receivedObj.data[0]["title"].Trim();
                            string author = receivedObj.data[0]["author"].Trim();
                            string publisher = receivedObj.data[0]["publisher"].Trim();
                            string genre = receivedObj.data[0]["genre"].Trim();
                            string isbn = receivedObj.data[0]["isbn"].Trim();
                            int addBookResult = _database.InsertBook(new Carte(isbn, title, author,
                                genre, publisher));
                            if (addBookResult != -1)
                            {
                                Console.WriteLine("Book added successful.");
                                writer.WriteLine("Book added successful.");
                            }
                            else
                            {
                                Console.WriteLine("Book addition failed.");
                                writer.WriteLine("Book addition failed.");
                            }
                            break;
                        case "searchBook":
                            Console.WriteLine("Searching book: " + receivedObj.data[0]["isbn"]);
                            List<Carte> bookSearch = _database.GetCartiByIsbn(receivedObj.data[0]["isbn"]);
                            if (bookSearch.Count == 0)
                            {
                                Console.WriteLine("No books found with the given ISBN.");
                                writer.WriteLine("No books found with the given ISBN.");
                            }
                            else
                            {
                                string responseSearch = "";
                                foreach (var item in bookSearch)
                                {
                                    responseSearch += $"{item.IdCarte}~{item.Titlu}~{item.Autor}~{item.Editura}|";
                                }
                                writer.WriteLine(responseSearch);
                            }
                            break;
                        case "deleteBook":
                            Console.WriteLine($"Deleting book: {receivedObj.data[0]["idBook"]}");
                            int bookIdToDelete = int.Parse(receivedObj.data[0]["idBook"].Trim());
                            bool deleteBookResult = _database.DeleteBook(bookIdToDelete);

                            if (deleteBookResult)
                            {
                                writer.WriteLine("Book deleted successful.");
                                Console.WriteLine("Book deleted successful.");
                            }
                            else
                            {
                                writer.WriteLine("Book deletion failed.");
                                Console.WriteLine("Book deletion failed.");
                            }
                            break;
                        case "searchAbonat":
                            Console.WriteLine("Searching subscriber: " + receivedObj.data[0]["phone"]);
                            //Abonat abonati = _database.GetAbonatByPhone(receivedObj.data[0]["phone"]);
                            //if (abonati != null) 
                            //{
                            //    Console.WriteLine($"Subscriber found: {abonati.IdAbonat} {abonati.Nume} {abonati.Prenume}");
                            //    writer.WriteLine($"{abonati.IdAbonat}~{abonati.Nume}~{abonati.Prenume}~{abonati.Adresa}~{abonati.Telefon}~{abonati.Email}");
                            //}
                            //else
                            //{
                            //    Console.WriteLine("No subscriber found with the given phone number.");
                            //    writer.WriteLine("No subscriber found with the given phone number.");
                            //}
                            break;
                        case "":
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
