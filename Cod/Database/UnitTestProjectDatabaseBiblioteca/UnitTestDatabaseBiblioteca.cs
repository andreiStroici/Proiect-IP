using Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTestProjectForDatabaseBiblioteca
{
    //insert user, login , insert client, cautare partiala , insert loan, get loaned books
    //return book
    [TestClass]
    public class UnitTestDatabaseBiblioteca
    {
        private Database.Database _database;

        [TestInitialize]
        public void Init()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\Server\bin\Debug\biblioteca.db"));
            string connString = $"Data Source={dbPath};Version=3;";
            _database = Database.Database.GetDatabase(connString);
            if (!File.Exists(dbPath))
            {
                throw new FileNotFoundException($"Database file not found: {dbPath}");
            }
            _database.Open();

        }

        [TestMethod]
        public void InsertUserSuccessfullyBibliotecar()
        {
            //in caz de fail userul exista deja in baza de date
            Utilizator testUser = new Utilizator("TestUser5", "parola", "bibliotecar");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertUser(testUser);
            string consoleOutput = output.ToString();
            Assert.IsTrue(result);
            StringAssert.Contains(consoleOutput, "Utilizator adaugat cu succes");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });


            _database.DeleteUser("TestUser5");
        }

        [TestMethod]
        public void InsertUserSuccessfullyAdministrator()
        {
            //administratorii nu pot fi stersi din cod deci userul doar exista deja
            Utilizator testUser = new Utilizator("TestUser8", "parola", "administrator");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertUser(testUser);
            string consoleOutput = output.ToString();
            Assert.IsTrue(result);
            StringAssert.Contains(consoleOutput, "Utilizator adaugat cu succes");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }
        [TestMethod]
        public void InsertUserRoleNotFound()
        {
            Utilizator testUser = new Utilizator("TestUser", "parola", "Inexisting role");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertUser(testUser);
            string consoleOutput = output.ToString();
            Assert.IsFalse(result);
            StringAssert.Contains(consoleOutput, "Rolul specificat nu exista in baza de date");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        [TestMethod]
        public void InsertUserSqlError()
        {
            Utilizator testUser = new Utilizator("TestUser", "parola", "bibliotecar");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertUser(testUser);
            string consoleOutput = output.ToString();
            Assert.IsFalse(result);
            StringAssert.Contains(consoleOutput, "Eroare la inserarea utilizatorului in baza de date: ");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }



        [TestMethod]
        public void InsertIsbnSuccsessfully()
        {
            //isbn-s for test -- 0000000000000, 0000000000007
            Carte testCarte = new Carte(6, "0000000000009", "Carte de test", "Autor de Test", "Gen de test", "Teste pentru toti");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertIsbn(testCarte);
            string consoleOutput = output.ToString();
            Assert.IsTrue(result);
            StringAssert.Contains(consoleOutput, "Noul isbn a fost adaugat cu succes.");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        [TestMethod]
        public void InsertIsbnAlreadyExisting()
        {
            Carte testCarte = new Carte(0, "0000000001", "Carte de test", "Autor de Test", "Gen de test", "Teste pentru toti");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertIsbn(testCarte);
            result = _database.InsertIsbn(testCarte);
            string consoleOutput = output.ToString();
            Assert.IsTrue(result);
            StringAssert.Contains(consoleOutput, "Isbn-ul exista deja");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

        }

        [TestMethod]
        public void InsertIsbnSqlError()
        {
            Carte testCarte = new Carte(0, "000000000X", null, "Autor de Test", "Gen de test", "Teste pentru toti");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertIsbn(testCarte);
            string consoleOutput = output.ToString();
            Assert.IsFalse(result);
            StringAssert.Contains(consoleOutput, "Eroare la inserarea in tabela Isbn: ");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        [TestMethod]
        public void InsertBookSuccessfully()
        {
            Carte testCarte = new Carte(0, "0000000000020", "Carte Test Insert", "Autor Insert", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            Assert.IsTrue(idCarte > 0);
            _database.DeleteBook(idCarte);
        }



        [TestMethod]
        public void InsertBookSqlError()
        {
            Carte testCarte = new Carte(0, "000000000X", null, "Autor Test", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            Assert.AreEqual(-1, idCarte);

        }

        [TestMethod]
        public void GetCartiByIsbnIsbnValid()
        {
            Carte testCarte = new Carte(0, "0000000000021", "Carte Valid ISBN", "Autor", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            var lista = _database.GetCartiByIsbn("0000000000021");
            Assert.IsTrue(lista.Count > 0);
            Assert.IsTrue(lista.Exists(c => c.Titlu == "Carte Valid ISBN"));
            _database.DeleteBook(idCarte);
        }

        [TestMethod]
        public void GetCartiByIsbnIsbnInvalid()
        {
            var lista = _database.GetCartiByIsbn("ISBNINEXISTENT");
            Assert.IsTrue(lista.Count == 0);
        }


        [TestMethod]
        public void IsCarteDisponibilaTrueCarte()
        {
            Carte testCarte = new Carte(0, "0000000000022", "Carte Disponibila", "Autor", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            var method = typeof(Database.Database).GetMethod("IsCartedisponibil", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool result = (bool)method.Invoke(_database, new object[] { idCarte });
            Assert.IsTrue(result);
            _database.DeleteBook(idCarte);
        }

        [TestMethod]
        public void IsCarteDisponibilaFalse()
        {
            
            Carte testCarte = new Carte(0, "0000000000023", "Carte Indisponibila", "Autor", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            Abonat abonat = new Abonat(0, "Indisponibil", "Abonat", "Adresa", "0711222333", "indisponibil@test.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0711222333");
            bool loanResult = _database.InsertLoan(abonatDb.IdAbonat, idCarte, "acasa");
            Assert.IsTrue(loanResult);
            var method = typeof(Database.Database).GetMethod("IsCartedisponibil", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool result = (bool)method.Invoke(_database, new object[] { idCarte });
            Assert.IsFalse(result);
            _database.ReturnBook(abonatDb.IdAbonat, idCarte);
            _database.DeleteBook(idCarte);
        }

        [TestMethod]
        public void IsCarteDisponibilaIdInvalid()
        {
            var method = typeof(Database.Database).GetMethod("IsCartedisponibil", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool result = (bool)method.Invoke(_database, new object[] { -12345 });
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void InsertLoanSuccessfully()
        {
            Carte testCarte = new Carte(0, "0000000000024", "Carte imprumutabila", "Autor", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            Abonat abonat = new Abonat(0, "NumeLoan", "PrenumeLoan", "Adresa", "0700123456", "loan@email.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700123456");
            bool result = _database.InsertLoan(abonatDb.IdAbonat, idCarte, "acasa");
            Assert.IsTrue(result);
            _database.ReturnBook(abonatDb.IdAbonat, idCarte);
            _database.DeleteBook(idCarte);
        }

        [TestMethod]
        public void InsertLoanCarteIndisponibila()
        {
            Carte testCarte = new Carte(0, "0000000000025", "Carte Indisp. Loan", "Autor", "Gen", "Editura", "indisponibil");
            int idCarte = _database.InsertBook(testCarte);
            Abonat abonat = new Abonat(0, "NumeLoan2", "PrenumeLoan2", "Adresa", "0700123457", "loan2@email.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700123457");
            bool result = _database.InsertLoan(abonatDb.IdAbonat, idCarte, "acasa");
            result = _database.InsertLoan(abonatDb.IdAbonat, idCarte, "acasa");
            Assert.IsFalse(result);
            _database.DeleteBook(idCarte);
        }


        [TestMethod]
        public void InsertLoanAbonatInexistent()
        {
            Carte testCarte = new Carte(0, "0000000000026", "Carte Loan Ab Inexist.", "Autor", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            bool result = _database.InsertLoan(-12345, idCarte, "acasa");
            Assert.IsFalse(result);
            _database.DeleteBook(idCarte);
        }

        [TestMethod]
        public void InsertLoanSqlError()
        {
            bool result = _database.InsertLoan(-1, -1, "acasa");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteUserSuccessfully()
        {
            Utilizator testUser = new Utilizator("TestUser2", "parola", "bibliotecar");
            bool result = _database.InsertUser(testUser);
            result = _database.DeleteUser(testUser.Nume);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteUserUserNotFound()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            Utilizator testUser = new Utilizator("User Not Found", "parola", "bibliotecar");
            bool result = _database.DeleteUser(testUser.Nume);
            string consoleOutput = output.ToString();
            Assert.IsFalse(result);
            StringAssert.Contains(consoleOutput, "Utilizatorul nu a fost gasit sau a fost deja sters");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }




        [TestMethod]
        public void DeleteUserSqlError()
        {
            Utilizator testUser = new Utilizator("TestAdminDelete", "parola", "administrator");
            _database.InsertUser(testUser);
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.DeleteUser(testUser.Nume);
            string consoleOutput = output.ToString();
            Assert.IsFalse(result);
            StringAssert.Contains(consoleOutput, "Utilizatorul nu a fost gasit sau a fost deja sters");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });


        }

        [TestMethod]
        public void DeteleBookSuccessfully()
        {
            Carte testCarte = new Carte(0, "0000000000027", "Carte De Sters", "Autor", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            bool result = _database.DeleteBook(idCarte);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteBookInvalidId()
        {
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.DeleteBook(-77777);
            string consoleOutput = output.ToString();
            Assert.IsFalse(result);
            StringAssert.Contains(consoleOutput, "Nu a fost gasita nicio carte cu acest id.");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        [TestMethod]
        public void GetAbonatByPhoneValidPhone()
        {
            Abonat abonat = new Abonat(0, "NumeValid", "PrenumeValid", "Adresa", "0700765432", "abonat@valid.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700765432");
            Assert.IsNotNull(abonatDb);
            Assert.AreEqual("NumeValid", abonatDb.Nume);
        }

        [TestMethod]
        public void GetAbonatByPhoneInvalidPhone()
        {
            var abonatDb = _database.GetAbonatByPhone("TELEFONINEXISTENT");
            Assert.IsNull(abonatDb);
        }

        [TestMethod]
        public void GetAbonatByPhoneSqlError()
        {
            var abonatDb = _database.GetAbonatByPhone(null);
            Assert.IsNull(abonatDb);
        }

        [TestMethod]
        public void UpdateStatusAbonatAbonatValidMesajValid()
        {
            Abonat abonat = new Abonat(0, "NumeUpdate", "PrenumeUpdate", "Adresa", "0700111111", "update@abonat.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700111111");
            bool result = _database.UpdateStatusAbonat(abonatDb.IdAbonat, "cu restrictii");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateStatusAbonatAbonatValidMesajInvalid()
        {
            Abonat abonat = new Abonat(0, "NumeUpdate2", "PrenumeUpdate2", "Adresa", "0700222222", "update2@abonat.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700222222");
            bool result = _database.UpdateStatusAbonat(abonatDb.IdAbonat, "statusInexistent");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateStatusAbonatAbonatInvalid()
        {
            bool result = _database.UpdateStatusAbonat(-88888, "cu restrictii");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UnrestrictAbonatAbonatValid()
        {
            Abonat abonat = new Abonat(0, "Unrestrict", "Abonat", "Adresa", "0700999888", "unrestrict@abonat.com", 5, "cu restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700999888");
            bool result = _database.UnRestrictAbonat(abonatDb.IdAbonat);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UnrestrictAbonatAbonatInvalid()
        {
            bool result = _database.UnRestrictAbonat(-99999);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BlocareAbonatAbonatValid()
        {
            Abonat abonat = new Abonat(0, "Blocare", "Abonat", "Adresa", "0700666777", "blocare@abonat.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700666777");
            bool result = _database.BlocareAbonat(abonatDb.IdAbonat);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BlocareAbonatAbonatInvalid()
        {
            bool result = _database.BlocareAbonat(-77777);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RestrictAbonatAbonatValid()
        {
            Abonat abonat = new Abonat(0, "Restrict", "Abonat", "Adresa", "0700333444", "restrict@abonat.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700333444");
            bool result = _database.RestrictAbonat(abonatDb.IdAbonat);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RestrictAbonatAbonatInvalid()
        {
            bool result = _database.RestrictAbonat(-33333);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LoginSuccessfully()
        {
            Utilizator testUser = new Utilizator("TestLogin", "parola", "bibliotecar");
            _database.InsertUser(testUser);
            bool result = _database.Login(testUser);
            Assert.IsTrue(result);
            _database.DeleteUser(testUser.Nume);
        }


        [TestMethod]
        public void LoginUnsuccessfully()
        {
            Utilizator testUser = new Utilizator("TestFailLogin", "gresit", "bibliotecar");
            bool result = _database.Login(testUser);
            Assert.IsFalse(result);
        }


        //get loaned books,cautare carti partial,is id client valid get status abonat, is blocked, is restrcitedd
        [TestCleanup]
        public void CleanUp()
        {
           _database.Close();
        }
    }
}
