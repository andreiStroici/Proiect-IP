/**************************************************************************
 *                                                                        *
 *  File:        UnitTestDatabaseBiblioteca.cs                            *
 *  Copyright:   (c) 2025, A. Marina                                      *
 *  E-mail:      marina.agavriloaei@tuiasi.ro                             *
 *  Description: Conține toate testele pentru clasa Database              *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/


using Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace UnitTestProjectForDatabaseBiblioteca
{
    /// <summary>
    /// Clasa care implementeaza testele pentru operatiile asupra bazei de date
    /// </summary>
     [TestClass]
    public class UnitTestDatabaseBiblioteca
    {
        private Database.Database _database;

        /// <summary>
        /// Se execută înainte de fiecare test pentru a inițializa conexiunea la baza de date.
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.GetFullPath(Path.Combine(basePath, @"ClearDatabase/biblioteca.db"));
            string connString = $"Data Source={dbPath};Version=3;";
            _database = Database.Database.GetDatabase(connString);
            if (!File.Exists(dbPath))
            {
                throw new FileNotFoundException($"Database file not found: {dbPath}");
            }
            _database.Open();

        }

        /// <summary>
        /// Verifică inserarea cu succes a unui utilizator cu rolul "bibliotecar".
        /// </summary>
        [TestMethod]
        public void InsertUserSuccessfullyBibliotecar()
        {
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

        /// <summary>
        /// Verifică inserarea cu succes a unui utilizator cu rolul "administrator".
        /// </summary>
        [TestMethod]
        public void InsertUserSuccessfullyAdministrator()
        {

            Utilizator testUser = new Utilizator("TestUser8", "parola", "administrator");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertUser(testUser);
            string consoleOutput = output.ToString();
            Assert.IsTrue(result);
            StringAssert.Contains(consoleOutput, "Utilizator adaugat cu succes");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        /// <summary>
        /// Verifică scenariul în care rolul specificat nu există și inserarea trebuie să eșueze.
        /// </summary>
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

        /// <summary>
        /// Verifică gestionarea unei erori SQL atunci când se încearcă inserarea a doi utilizatori cu aceiași cheie unică.
        /// </summary>
        [TestMethod]
        public void InsertUserSqlError()
        {
            Utilizator testUser = new Utilizator("TestUser", "parola", "bibliotecar");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertUser(testUser);
            result = _database.InsertUser(testUser);
            string consoleOutput = output.ToString();
            Assert.IsFalse(result);
            StringAssert.Contains(consoleOutput, "Eroare la inserarea utilizatorului in baza de date: ");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }


        /// <summary>
        /// Verifică inserarea corectă a unui ISBN în tabelă.
        /// </summary>
        [TestMethod]
        public void InsertIsbnSuccsessfully()
        {

            Carte testCarte = new Carte(6, "0000000000009", "Carte de test", "Autor de Test", "Gen de test", "Teste pentru toti");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertIsbn(testCarte);
            string consoleOutput = output.ToString();
            Assert.IsTrue(result);
            StringAssert.Contains(consoleOutput, "Noul isbn a fost adaugat cu succes.");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        /// <summary>
        /// Verifică mesajul de atenționare atunci când se încearcă inserarea unui ISBN deja existent.
        /// </summary>
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

        /// <summary>
        /// Verifică gestionarea unei erori SQL când se încearcă inserarea unui ISBN cu date invalide.
        /// </summary>
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

        /// <summary>
        /// Verifică inserarea cu succes a unei cărți.
        /// </summary>
        [TestMethod]
        public void InsertBookSuccessfully()
        {
            Carte testCarte = new Carte(0, "0000000000020", "Carte Test Insert", "Autor Insert", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            Assert.IsTrue(idCarte > 0);
            _database.DeleteBook(idCarte);
        }


        /// <summary>
        /// Verifică gestionarea unei erori SQL la inserarea unei cărți cu date invalide.
        /// </summary>
        [TestMethod]
        public void InsertBookSqlError()
        {
            Carte testCarte = new Carte(0, "000000000X", null, "Autor Test", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            Assert.AreEqual(-1, idCarte);

        }

        /// <summary>
        /// Verifică obținerea cărților după ISBN atunci când ISBN-ul este valid.
        /// </summary>
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

        /// <summary>
        /// Verifică că metoda <c>GetCartiByIsbn</c> returnează o listă goală atunci când ISBN-ul este invalid.
        /// </summary>
        [TestMethod]
        public void GetCartiByIsbnIsbnInvalid()
        {
            var lista = _database.GetCartiByIsbn("ISBNINEXISTENT");
            Assert.IsTrue(lista.Count == 0);
        }

        /// <summary>
        /// Verifică că o carte care nu are împrumut activ este considerată disponibilă.
        /// </summary>

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


        /// <summary>
        /// Verifică că <c>IsCartedisponibil</c> returnează <c>false</c> când cartea este împrumutată.
        /// </summary>
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

        /// <summary>
        /// Verifică comportamentul <c>IsCartedisponibil</c> când se trimite un ID invalid.
        /// </summary>
        [TestMethod]
        public void IsCarteDisponibilaIdInvalid()
        {
            var method = typeof(Database.Database).GetMethod("IsCartedisponibil", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            bool result = (bool)method.Invoke(_database, new object[] { -12345 });
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifică inserarea cu succes a unui împrumut.
        /// </summary>
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

        /// <summary>
        /// Verifică eșecul inserării de împrumut atunci când cartea este deja indisponibilă.
        /// </summary>
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

        /// <summary>
        /// Verifică eșecul inserării de împrumut atunci când abonatul nu există.
        /// </summary>
        [TestMethod]
        public void InsertLoanAbonatInexistent()
        {
            Carte testCarte = new Carte(0, "0000000000026", "Carte Loan Ab Inexist.", "Autor", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            bool result = _database.InsertLoan(-12345, idCarte, "acasa");
            Assert.IsFalse(result);
            _database.DeleteBook(idCarte);
        }

        /// <summary>
        /// Verifică gestiunea unei erori SQL la inserarea unui împrumut cu date invalide.
        /// </summary>
        [TestMethod]
        public void InsertLoanSqlError()
        {
            bool result = _database.InsertLoan(-1, -1, "acasa");
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifică ștergerea cu succes a unui utilizator.
        /// </summary>
        [TestMethod]
        public void DeleteUserSuccessfully()
        {
            Utilizator testUser = new Utilizator("TestUser2", "parola", "bibliotecar");
            bool result = _database.InsertUser(testUser);
            result = _database.DeleteUser(testUser.Nume);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifică mesajul de eroare când se încearcă ștergerea unui utilizator inexistent.
        /// </summary>
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


        /// <summary>
        /// Verifică gestionarea unei erori SQL la ștergerea unui utilizator (de exemplu, când încercăm să ștergem un administrator).
        /// </summary>

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

        /// <summary>
        /// Verifică ștergerea cu succes a unei cărți.
        /// </summary>
        [TestMethod]
        public void DeteleBookSuccessfully()
        {
            Carte testCarte = new Carte(0, "0000000000027", "Carte De Sters", "Autor", "Gen", "Editura", "disponibil");
            int idCarte = _database.InsertBook(testCarte);
            bool result = _database.DeleteBook(idCarte);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifică mesajul de eroare când se încearcă ștergerea unei cărți cu ID invalid.
        /// </summary>
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

        /// <summary>
        /// Verifică obținerea unui abonat după numărul de telefon atunci când telefonul există în baza de date.
        /// </summary>
        [TestMethod]
        public void GetAbonatByPhoneValidPhone()
        {
            Abonat abonat = new Abonat(0, "NumeValid", "PrenumeValid", "Adresa", "0700765432", "abonat@valid.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700765432");
            Assert.IsNotNull(abonatDb);
            Assert.AreEqual("NumeValid", abonatDb.Nume);
        }

        /// <summary>
        /// Se așteaptă o excepție de tip <c>ClientNotFoundException</c> atunci când telefonul nu există.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Database.ClientNotFoundException))]
        public void GetAbonatByPhoneInvalidPhone()
        {
            var abonatDb = _database.GetAbonatByPhone("TELEFONINEXISTENT");
        }

        /// <summary>
        /// Verifică actualizarea stării unui abonat când abonatul există și mesajul de stare este valid.
        /// </summary>
        [TestMethod]
        public void UpdateStatusAbonatAbonatValidMesajValid()
        {
            Abonat abonat = new Abonat(0, "NumeUpdate", "PrenumeUpdate", "Adresa", "0700111111", "update@abonat.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700111111");
            bool result = _database.UpdateStatusAbonat(abonatDb.IdAbonat, "cu restrictii");
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifică că <c>UpdateStatusAbonat</c> returnează <c>false</c> când mesajul de stare nu există în baza de date.
        /// </summary>
        [TestMethod]
        public void UpdateStatusAbonatAbonatValidMesajInvalid()
        {
            Abonat abonat = new Abonat(0, "NumeUpdate2", "PrenumeUpdate2", "Adresa", "0700222222", "update2@abonat.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700222222");
            bool result = _database.UpdateStatusAbonat(abonatDb.IdAbonat, "statusInexistent");
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifică că <c>UpdateStatusAbonat</c> returnează <c>false</c> când ID-ul abonatului este invalid.
        /// </summary>
        [TestMethod]
        public void UpdateStatusAbonatAbonatInvalid()
        {
            bool result = _database.UpdateStatusAbonat(-88888, "cu restrictii");
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Verifică deblocarea (eliminarea restricțiilor) unui abonat existent.
        /// </summary>
        [TestMethod]
        public void UnrestrictAbonatAbonatValid()
        {
            Abonat abonat = new Abonat(0, "Unrestrict", "Abonat", "Adresa", "0700999888", "unrestrict@abonat.com", 5, "cu restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700999888");
            bool result = _database.UnRestrictAbonat(abonatDb.IdAbonat);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifică că <c>UnRestrictAbonat</c> returnează <c>false</c> când ID-ul abonatului este invalid.
        /// </summary>

        [TestMethod]
        public void UnrestrictAbonatAbonatInvalid()
        {
            bool result = _database.UnRestrictAbonat(-99999);
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Verifică blocarea unui abonat existent.
        /// </summary>
        [TestMethod]
        public void BlocareAbonatAbonatValid()
        {
            Abonat abonat = new Abonat(0, "Blocare", "Abonat", "Adresa", "0700666777", "blocare@abonat.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700666777");
            bool result = _database.BlocareAbonat(abonatDb.IdAbonat);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifică că <c>BlocareAbonat</c> returnează <c>false</c> când ID-ul abonatului este invalid.
        /// </summary>
        [TestMethod]
        public void BlocareAbonatAbonatInvalid()
        {
            bool result = _database.BlocareAbonat(-77777);
            Assert.IsFalse(result);
        }
        /// <summary>
        /// Verifică restricționarea unui abonat existent.
        /// </summary>
        [TestMethod]
        public void RestrictAbonatAbonatValid()
        {
            Abonat abonat = new Abonat(0, "Restrict", "Abonat", "Adresa", "0700333444", "restrict@abonat.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var abonatDb = _database.GetAbonatByPhone("0700333444");
            bool result = _database.RestrictAbonat(abonatDb.IdAbonat);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Verifică că <c>RestrictAbonat</c> returnează <c>false</c> când ID-ul abonatului este invalid.
        /// </summary>
        [TestMethod]
        public void RestrictAbonatAbonatInvalid()
        {
            bool result = _database.RestrictAbonat(-33333);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifică autentificarea cu succes a unui utilizator corespunzător în baza de date.
        /// </summary>
        [TestMethod]
        public void LoginSuccessfully()
        {
            Utilizator testUser = new Utilizator("TestLogin", "parola", "bibliotecar");
            _database.InsertUser(testUser);
            bool result = _database.Login(testUser);
            Assert.IsTrue(result);
            _database.DeleteUser(testUser.Nume);
        }

        /// <summary>
        /// Se așteaptă <c>InvalidUserDataException</c> când datele de autentificare sunt incorecte.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Database.InvalidUserDataException))]
        public void LoginUnsuccessfully()
        {
            Utilizator testUser = new Utilizator("TestFailLogin", "gresit", "bibliotecar");
            bool result = _database.Login(testUser);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifică inserarea cu succes a unui client (abonat).
        /// </summary>
        [TestMethod]
        public void InsertClientSuccessfully()
        {
            var abonat = new Abonat(0, "ClientTest", "PrenumeTest", "AdresaTest", "0722001234", "client@test.com", 3, "fara restrictii");
            bool result = _database.InsertClient(abonat);
            Assert.IsTrue(result);
            var fetched = _database.GetAbonatByPhone("0722001234");
            Assert.IsNotNull(fetched);
            Assert.AreEqual("ClientTest", fetched.Nume);
        }

        /// <summary>
        /// Verifică gestionarea unei erori SQL la inserarea unui client cu nume null.
        /// </summary>
        [TestMethod]
        public void InsertClientSqlErrorNullName()
        {
            var abonat = new Abonat(0, null, "Prenume", "Adresa", "0722002345", "error@test.com", 2, "fara restrictii");
            var output = new StringWriter();
            Console.SetOut(output);

            bool result = _database.InsertClient(abonat);
            string consoleOutput = output.ToString();
            Assert.IsFalse(result);
            StringAssert.Contains(consoleOutput, "Eroare la adaugarea unui abonat in baza de date");

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        /// <summary>
        /// Verifică lista de cărți împrumutate după efectuarea unui împrumut și returnarea cărții.
        /// </summary>

        [TestMethod]
        public void GetLoanedBooksAfterLoanReturnsBook()
        {
            var carte = new Carte(0, "0000000000300", "LoanedTitle", "AutorLoan", "GenLoan", "EdituraLoan", "disponibil");
            int idCarte = _database.InsertBook(carte);
            Assert.IsTrue(idCarte > 0);
            var abonat = new Abonat(0, "LoanClient", "PrenumeLC", "AdresaLC", "0722004567", "loan@test.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var fetchedClient = _database.GetAbonatByPhone("0722004567");
            bool loanResult = _database.InsertLoan(fetchedClient.IdAbonat, idCarte, "acasa");
            Assert.IsTrue(loanResult);
            List<Carte> lista = _database.GetLoanedBooks(fetchedClient.IdAbonat);
            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.Exists(c => c.IdCarte == idCarte && c.Titlu == "LoanedTitle"));
            bool returnResult = _database.ReturnBook(fetchedClient.IdAbonat, idCarte);
            Assert.IsTrue(returnResult);
            _database.DeleteBook(idCarte);
        }


        /// <summary>
        /// Verifică că <c>CautareCartiPartial</c> returnează doar cărțile care se potrivesc parțial cu titlul și autorul specificat.
        /// </summary>
        [TestMethod]
        public void CautareCartiPartialReturnsMatchingBooks()
        {
            var c1 = new Carte(0, "0000000000301", "AlphaTitle", "AuthorMatch", "GenA", "EditA", "disponibil");
            int id1 = _database.InsertBook(c1);
            Assert.IsTrue(id1 > 0);
            var c2 = new Carte(0, "0000000000302", "BetaTitle", "AuthorMatch", "GenB", "EditB", "disponibil");
            int id2 = _database.InsertBook(c2);
            Assert.IsTrue(id2 > 0);
            var c3 = new Carte(0, "0000000000303", "GammaTitle", "OtherAuthor", "GenC", "EditC", "disponibil");
            int id3 = _database.InsertBook(c3);
            Assert.IsTrue(id3 > 0);
            List<Carte> lista = _database.CautareCartiPartial("Alpha", "Author");
            Assert.IsNotNull(lista);
            Assert.AreEqual(1, lista.Count);
            Assert.AreEqual(id1, lista[0].IdCarte);
            _database.DeleteBook(id1);
            _database.DeleteBook(id2);
            _database.DeleteBook(id3);
        }

        /// <summary>
        /// Verifică că <c>CautareCartiPartial</c> returnează o listă goală atunci când nu există rezultate.
        /// </summary>

        [TestMethod]
        public void CautareCartiPartialNoMatchesReturnsEmptyList()
        {
            List<Carte> lista = _database.CautareCartiPartial("Nonexistent", "Nobody");
            Assert.IsNotNull(lista);
            Assert.AreEqual(0, lista.Count);
        }


        /// <summary>
        /// Verifică obținerea stării unui abonat atunci când ID-ul este valid.
        /// </summary>
        [TestMethod]
        public void GetStatusAbonatValidReturnsCorrectStatus()
        {
            var abonat = new Abonat(0, "StatusClient", "PrenumeS", "AdresaS", "0722005678", "status@test.com", 5, "cu restrictii");
            _database.InsertClient(abonat);
            var fetched = _database.GetAbonatByPhone("0722005678");

            string status = _database.GetStatusAbonat(fetched.IdAbonat);
            Assert.IsNotNull(status);
            Assert.AreEqual("cu restrictii", status);
        }

        /// <summary>
        /// Verifică că <c>GetStatusAbonat</c> returnează <c>null</c> când ID-ul abonatului este invalid.
        /// </summary>
        [TestMethod]
        public void GetStatusAbonatInvalidIdReturnsNull()
        {
            string status = _database.GetStatusAbonat(-9999);
            Assert.IsNull(status);
        }


        /// <summary>
        /// Verifică că <c>ReturnBook</c> returnează <c>false</c> când nu există împrumut activ.
        /// </summary>
        [TestMethod]
        public void ReturnBookNoActiveLoanReturnsFalse()
        {
            bool result = _database.ReturnBook(-1, -1);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Verifică că <c>ReturnBook</c> returnează <c>true</c> după ce împrumutul a fost efectuat și apoi returnat.
        /// </summary>
        [TestMethod]
        public void ReturnBookAfterLoanReturnsTrue()
        {
            var carte = new Carte(0, "0000000000304", "ReturnTest", "AutorR", "GenR", "EditR", "disponibil");
            int idCarte = _database.InsertBook(carte);
            Assert.IsTrue(idCarte > 0);
            var abonat = new Abonat(0, "ReturnClient", "PrenumeR", "AdresaR", "0722006789", "return@test.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            var fetched = _database.GetAbonatByPhone("0722006789");
            bool loanResult = _database.InsertLoan(fetched.IdAbonat, idCarte, "acasa");
            Assert.IsTrue(loanResult);

            bool returnResult = _database.ReturnBook(fetched.IdAbonat, idCarte);
            Assert.IsTrue(returnResult);
            _database.DeleteBook(idCarte);
        }

        /// <summary>
        /// Verifică că un abonat cu restricții este inclus în lista de întârziati.
        /// </summary>
        [TestMethod]
        public void CautareIntarziatiRestrictedClientIncluded()
        { 
            var abonat = new Abonat(0, "RestrictedClient", "PrenumeRI", "AdresaRI", "0722007890", "restricted@test.com", 5, "cu restrictii");
            _database.InsertClient(abonat);
            var fetched = _database.GetAbonatByPhone("0722007890");
            List<Abonat> lista = _database.CautareIntarziati();
            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.Exists(a => a.IdAbonat == fetched.IdAbonat));
        }



        /// <summary>
        /// Verifică că lista de întârziati nu include abonați fără întârziere.
        /// </summary>
        [TestMethod]
        public void CautareIntarziatiNoIssuesReturnsEmptyList()
        {
            var abonat = new Abonat(0, "NormalClient", "PrenumeN", "AdresaN", "0722008901", "normal@test.com", 5, "fara restrictii");
            _database.InsertClient(abonat);
            List<Abonat> lista = _database.CautareIntarziati();
            Assert.IsNotNull(lista);
            Assert.IsFalse(lista.Exists(a => a.Telefon == "0722008901"));
        }

        /// <summary>
        /// Verifică că <c>CautareDoarIntarziati</c> returnează o listă goală când nu există întârziere.
        /// </summary>
        [TestMethod]
        public void CautareDoarIntarziatiNoOverdueReturnsEmptyList()
        {
            var abonat = new Abonat(0, "RestrictedOnly", "PrenumeRO", "AdresaRO", "0722009012", "restonly@test.com", 5, "cu restrictii");
            _database.InsertClient(abonat);
            List<Abonat> lista = _database.CautareDoarIntarziati();
            Assert.IsNotNull(lista);
            Assert.AreEqual(0, lista.Count);
        }


        /// <summary>
        /// Închide conexiunea la baza de date după fiecare test.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
           _database.Close();
        }
    }
}
