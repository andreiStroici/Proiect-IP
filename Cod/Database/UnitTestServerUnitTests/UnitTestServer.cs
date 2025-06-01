/**************************************************************************
 *                                                                        *
 *  File:        UnitTestServer.cs                                        *
 *  Copyright:   (c) 2025, A. Marina                                      *
 *  E-mail:      marina.agavriloaei@tuiasi.ro                             *
 *  Description: Conține toate testele pentru clasa Server                *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Server;
using System.Reflection;

namespace UnitTestServerUnitTests
{
    /// <summary>
    /// Clasa care definește testele unitare pentru Server.Server.
    /// </summary>
    [TestClass]
    public class UnitTestServer
    {
        private Server.Server _server;
        private Thread _serverThread;
        private const int Port = 12345;
        private Database.Database _db;

        /// <summary>
        /// Se execută înainte de fiecare test pentru a inițializa baza de date și a porni serverul.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.GetFullPath(Path.Combine(basePath, @"ClearDatabase/biblioteca.db"));
            if (!File.Exists(dbPath))
                throw new FileNotFoundException($"Database file not found: {dbPath}");
            string connString = $"Data Source={dbPath};Version=3;";

            typeof(Database.Database)
                .GetField("_database", BindingFlags.Static | BindingFlags.NonPublic)
                ?.SetValue(null, null);

            _db = Database.Database.GetDatabase(connString);

            var existingUser = new Utilizator("existingUser", "correctHash", "bibliotecar");
            _db.InsertUser(existingUser);

            var existingSub = new Abonat("Test", "Subscriber", "Some Address", "0700765432", "sub@example.com");
            _db.InsertClient(existingSub);

            _db.InsertBook(new Carte("ISBN_ALPHA", "AlphaTitle", "AuthorMatch", "Gen", "Publisher"));

            _server = new Server.Server(IPAddress.Any, Port, connString);
            _serverThread = new Thread(() => _server.Start());
            _serverThread.IsBackground = true;
            _serverThread.Start();
            Thread.Sleep(200);
        }

        // <summary>
        /// Se execută după fiecare test pentru a opri serverul și a curăța datele introduse.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            var isRunningField = typeof(Server.Server)
                                .GetField("_isRunning", BindingFlags.NonPublic | BindingFlags.Instance);
            isRunningField.SetValue(_server, false);

            var listenerField = typeof(Server.Server)
                                .GetField("_listener", BindingFlags.NonPublic | BindingFlags.Instance);
            var listener = (TcpListener)listenerField.GetValue(_server);
            listener.Stop();

            try
            {
                using (var dummy = new TcpClient("127.0.0.1", Port))
                using (var w = new StreamWriter(dummy.GetStream()) { AutoFlush = true })
                    w.WriteLine();
            }
            catch { }

            Thread.Sleep(200);
            if (_serverThread.IsAlive)
                _serverThread.Abort();

            _db.DeleteUser("existingUser");
            _db.Close();
        }


        /// <summary>
        /// Trimite un mesaj JSON către server și returnează răspunsul pe o singură linie.
        /// </summary>
        /// <param name="msg">Obiectul Message de trimis.</param>
        /// <returns>Răspunsul serverului.</returns>
        private string SendSingle(Message msg)
        {
            using (var client = new TcpClient("127.0.0.1", Port))
            using (var reader = new StreamReader(client.GetStream(), Encoding.UTF8))
            using (var writer = new StreamWriter(client.GetStream(), Encoding.UTF8) { AutoFlush = true })
            {
                writer.WriteLine(JsonConvert.SerializeObject(msg));
                return reader.ReadLine();
            }
        }

        /// <summary>
        /// Trimite două cereri de autentificare consecutive și returnează ambele răspunsuri.
        /// </summary>
        /// <param name="msg">Obiectul Message de tip login.</param>
        /// <returns>Tuplu cu răspunsurile primului și celui de-al doilea mesaj.</returns>
        private (string, string) SendTwoLoginAttempts(Message msg)
        {
            var client = new TcpClient("127.0.0.1", Port);
            var stream = client.GetStream();
            var reader = new StreamReader(stream, Encoding.UTF8);
            var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            writer.WriteLine(JsonConvert.SerializeObject(msg));
            string r1 = reader.ReadLine();

            writer.WriteLine(JsonConvert.SerializeObject(msg));
            string r2 = reader.ReadLine();

            client.Close();
            return (r1, r2);
        }

        /// <summary>
        /// Verifică autentificarea cu credențiale valide și așteaptă răspunsul "Login successful.".
        /// </summary>
        [TestMethod]
        public void LoginValidCredentialsReturnsSuccess()
        {
            var msg = new Message
            {
                operation = "login",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["username"] = "existingUser",
                        ["password"] = "correctHash",
                        ["role"]     = "bibliotecar"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Login successful.");
        }

        /// <summary>
        /// Verifică autentificarea cu credențiale invalide și așteaptă "Login failed. Invalid data.".
        /// </summary>
        [TestMethod]
        public void LoginInvalidCredentialsReturnsFailInvalidData()
        {
            var msg = new Message
            {
                operation = "login",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["username"] = "nonexistent",
                        ["password"] = "wrong",
                        ["role"]     = "bibliotecar"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Login failed. Invalid data.");
        }

        /// <summary>
        /// Trimite două cereri de login pentru același utilizator și așteaptă eșecul celei de-a doua încercări.
        /// </summary>
        [TestMethod]
        public void LoginDuplicateAttemptReturnsFailOnSecond()
        {
            var msg = new Message
            {
                operation = "login",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["username"] = "existingUser",
                        ["password"] = "correctHash",
                        ["role"]     = "bibliotecar"
                    }
                }
            };
            var (r1, r2) = SendTwoLoginAttempts(msg);
            StringAssert.Contains(r1, "Login successful.");
            StringAssert.Contains(r2, "Login failed.");
        }

        /// <summary>
        /// Înregistrează un abonat cu un număr de telefon unic și verifică răspunsul de succes.
        /// </summary>
        [TestMethod]
        public void RegisterSubscriberNewPhoneReturnsSuccess()
        {
            string uniquePhone = "099900" + DateTime.Now.Ticks.ToString().Substring(10);
            var msg = new Message
            {
                operation = "registerSubscriber",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["nume"]    = "TestSub",
                        ["prenume"] = "User",
                        ["adresa"]  = "Some Address",
                        ["telefon"] = uniquePhone,
                        ["email"]   = "testsub@example.com"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Subscriber Register successful.");
        }

        /// <summary>
        /// Încearcă înregistrarea unui abonat cu un număr de telefon deja existent și așteaptă eșecul.
        /// </summary>
        [TestMethod]
        public void RegisterSubscriberDuplicatePhoneReturnsFail()
        {
            string phone = "0700111222";
            _db.InsertClient(new Abonat("Dummy", "Exists", "Addr", phone, "dup@example.com"));

            var msg = new Message
            {
                operation = "registerSubscriber",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["nume"]    = "Already",
                        ["prenume"] = "Exists",
                        ["adresa"]  = "Any",
                        ["telefon"] = phone,
                        ["email"]   = "dup@example.com"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Subscriber registration failed.");
        }

        /// <summary>
        /// Verifică autentificarea abonatului existent prin telefon și așteaptă datele returnate.
        /// </summary>
        [TestMethod]
        public void LoginSubscriberExistingPhoneReturnsSuccessData()
        {
            string phone = "0700765432";
            var msg = new Message
            {
                operation = "loginSubscriber",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["username"] = phone
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Subscriber Login successful|");
        }

        /// <summary>
        /// Încearcă autentificarea abonatului cu telefon inexistent și așteaptă eșecul.
        /// </summary>
        [TestMethod]
        public void LoginSubscriberInvalidPhoneReturnsFail()
        {
            var msg = new Message
            {
                operation = "loginSubscriber",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["username"] = "0000000000"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Subscriber Login failed");
        }
        /// <summary>
        /// Caută cărți care nu există și așteaptă "No books found." ca răspuns.
        /// </summary>
        [TestMethod]
        public void SearchBooksNoMatchReturnsNoBooks()
        {
            var msg = new Message
            {
                operation = "searchBooks",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["title"]  = "ZZZNonexistentTitle",
                        ["author"] = "Nobody"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "No books found.");
        }

        /// <summary>
        /// Caută cărți care există parțial în baza de date și așteaptă o listă de rezultate.
        /// </summary>
        [TestMethod]
        public void SearchBooksMatchExistsReturnsList()
        {
            var msg = new Message
            {
                operation = "searchBooks",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["title"]  = "Alpha",
                        ["author"] = "AuthorMatch"
                    }
                }
            };
            string response = SendSingle(msg);
            Assert.IsTrue(response.Contains("~"));
        }

        /// <summary>
        /// Încearcă inserarea unui împrumut cu ID-uri invalide și așteaptă eșecul.
        /// </summary>
        [TestMethod]
        public void InsertLoanInvalidIdsReturnsFail()
        {
            var msg = new Message
            {
                operation = "insertLoan",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["subscriberId"]     = "-1",
                        ["bookId"]           = "-1",
                        ["selectedLocation"] = "acasa"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Inserted Loan failed.");
        }

        /// <summary>
        /// Creează o carte și un abonat temporar, apoi înserează împrumut și verifică succesul.
        /// </summary>
        [TestMethod]
        public void InsertLoanValidReturnsSuccess()
        {
            var addBookMsg = new Message
            {
                operation = "addBook",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["isbn"]      = "TEMPISBN123",
                        ["title"]     = "TempTitle",
                        ["author"]    = "TempAuthor",
                        ["genre"]     = "Temp",
                        ["publisher"] = "TempPub"
                    }
                }
            };
            string addBookResp = SendSingle(addBookMsg);
            StringAssert.Contains(addBookResp, "Book added successful.");

            string tempPhone = "099900" + DateTime.Now.Ticks.ToString().Substring(10);
            var regMsg = new Message
            {
                operation = "registerSubscriber",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["nume"]    = "LoanSub",
                        ["prenume"] = "Temp",
                        ["adresa"]  = "Addr",
                        ["telefon"] = tempPhone,
                        ["email"]   = "loan@example.com"
                    }
                }
            };
            string regResp = SendSingle(regMsg);
            StringAssert.Contains(regResp, "Subscriber Register successful.");

            var searchBookMsg = new Message
            {
                operation = "searchBook",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["isbn"] = "TEMPISBN123"
                    }
                }
            };
            string sbResp = SendSingle(searchBookMsg);
            int bookId = int.Parse(sbResp.Split('~')[0]);

            var loginSubMsg = new Message
            {
                operation = "loginSubscriber",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["username"] = tempPhone
                    }
                }
            };
            string lsResp = SendSingle(loginSubMsg);
            int subscriberId = int.Parse(lsResp.Split('|')[1]);

            var loanMsg = new Message
            {
                operation = "insertLoan",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["subscriberId"]     = subscriberId.ToString(),
                        ["bookId"]           = bookId.ToString(),
                        ["selectedLocation"] = "acasa"
                    }
                }
            };
            string loanResp = SendSingle(loanMsg);
            StringAssert.Contains(loanResp, "Inserted Loan successful.");
        }

        /// <summary>
        /// Solicită împrumuturi pentru un abonat inexistent și așteaptă "No loans found.".
        /// </summary>

        [TestMethod]
        public void GetLoansNoActiveReturnsNoLoans()
        {
            var msg = new Message
            {
                operation = "getLoans",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["subscriberId"] = "-1"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "No loans found.");
        }

        /// <summary>
        /// Încearcă returnarea unei cărți pentru un împrumut inexistent și așteaptă eșecul.
        /// </summary>
        [TestMethod]
        public void ReturnBookNoActiveReturnsFail()
        {
            var msg = new Message
            {
                operation = "returnBook",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["subscriberId"] = "-1",
                        ["bookId"]       = "-1"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Book return failed.");
        }

        /// <summary>
        /// Solicită statusul unui abonat cu ID invalid și așteaptă "Status not found.".
        /// </summary>
        [TestMethod]
        public void GetStatusClientInvalidIdReturnsNotFound()
        {
            var msg = new Message
            {
                operation = "getStatusClient",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["subcriberId"] = "-1"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Status not found.");
        }

        /// <summary>
        /// Încearcă înregistrarea unui angajat cu rol inexistent și așteaptă eșecul.
        /// </summary>
        [TestMethod]
        public void RegisterEmployeeInvalidRoleReturnsFail()
        {
            var msg = new Message
            {
                operation = "registerEmployee",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["username"] = "tempEmp",
                        ["password"] = "pw",
                        ["role"]     = "nonexistentRole"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Employee registration failed.");
        }

        /// <summary>
        /// Încearcă ștergerea unui bibliotecar inexistent și așteaptă eșecul.
        /// </summary>
        [TestMethod]
        public void DeleteLibrarianInvalidUserReturnsFail()
        {
            var msg = new Message
            {
                operation = "deleteLibrarian",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["username"] = "noSuchUser"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Librarian deletion failed.");
        }

        /// <summary>
        /// Trimite date invalide pentru adăugarea unei cărți și așteaptă răspuns nul.
        /// </summary>
        [TestMethod]
        public void AddBookInvalidDataReturnsNullResponse()
        {
            var msg = new Message
            {
                operation = "addBook",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["isbn"]      = "INVALIDISBN",
                        ["title"]     = null,
                        ["author"]    = "A",
                        ["genre"]     = "G",
                        ["publisher"] = "P"
                    }
                }
            };
            string response = SendSingle(msg);
            Assert.IsNull(response);
        }

        /// <summary>
        /// Caută carte după ISBN inexistent și așteaptă "No books found with the given ISBN.".
        /// </summary>
        [TestMethod]
        public void SearchBookNoMatchReturnsNoBooks()
        {
            var msg = new Message
            {
                operation = "searchBook",

                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["isbn"] = "0000000000"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "No books found with the given ISBN.");
        }

        /// <summary>
        /// Încearcă ștergerea unei cărți cu ID invalid și așteaptă eșecul.
        /// </summary>
        [TestMethod]
        public void DeleteBookInvalidIdReturnsFail()
        {
            var msg = new Message
            {
                operation = "deleteBook",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["idBook"] = "-9999"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Book deletion failed.");
        }

        /// <summary>
        /// Solicită cautarea abonaților cu restricții/întârziere când nu există astfel de abonați și așteaptă mesajul de eșec.
        /// </summary>
        [TestMethod]
        public void SearchSubscribersNoneMatchReturnsNoSubscribers()
        {
            var msg = new Message
            {
                operation = "searchSubscribers",
                data = new List<Dictionary<string, string>>()
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "No subscribers found with restrictions or blocked.");
        }

        /// <summary>
        /// Încearcă actualizarea statusului unui abonat inexistent și așteaptă eșecul.
        /// </summary>
        [TestMethod]
        public void UpdateStatusInvalidSubscriberReturnsFail()
        {
            var msg = new Message
            {
                operation = "updateStatus",
                data = new List<Dictionary<string, string>>
                {
                    new Dictionary<string, string>
                    {
                        ["subscriberId"] = "-1",
                        ["status"]       = "cu restrictii"
                    }
                }
            };
            string response = SendSingle(msg);
            StringAssert.Contains(response, "Status update failed.");
        }
    }
}
