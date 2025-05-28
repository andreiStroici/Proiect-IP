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
            Utilizator testUser = new Utilizator("TestUser", "parola", "bibliotecar");
            var output = new StringWriter();
            Console.SetOut(output);
            bool result = _database.InsertUser(testUser);
            string consoleOutput = output.ToString();
            Assert.IsTrue(result);
            StringAssert.Contains(consoleOutput, "Utilizator adaugat cu succes");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        [TestMethod]
        public void InsertUserSuccessfullyAdministrator()
        {
            Utilizator testUser = new Utilizator("TestUser", "parola", "administrator");
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
            Carte testCarte = new Carte(0,"0000000001","Carte de test", "Autor de Test", "Gen de test", "Teste pentru toti");
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

        }



        [TestMethod]
        public void InsertBookSqlError()
        {
            //de precizat daca in InsertLoan trebuie verificat si ca userul exista

        }

        [TestMethod]
        public void GetCartiByIsbnIsbnValid()
        {

        }

        [TestMethod]
        public void GetCartiByIsbnIsbnInvalid()
        {

        }


        [TestMethod]
        public void IsCarteDisponibilaTrue()
        {

        }

        [TestMethod]
        public void IsCarteDisponibilaFalse()
        {

        }

        [TestMethod]
        public void IsCarteDisponibilaIdInvalid()
        {

        }


        [TestMethod]
        public void InsertLoanSuccessfully()
        {

        }

        [TestMethod]
        public void InsertLoanCarteIndisponibila()
        {

        }


        [TestMethod]
        public void InsertLoanAbonatInexistent()
        {

        }

        [TestMethod]
        public void InsertLoanSqlError()
        {

        }

        [TestMethod]
        public void DeleteUserSuccessfully()
        {
            Utilizator testUser = new Utilizator("TestUser", "parola", "bibliotecar");
            bool result = _database.DeleteUser(testUser.Nume);
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
            StringAssert.Contains(consoleOutput, "Utilizatorul nu a fost gasit sau a fost deja sters.");
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }




        [TestMethod]
        public void DeleteUserSqlError()
        {
            //refacere metoda dupa adaugare imprumut pentru acest user


        }

        [TestMethod]
        public void DeteleBookSuccessfully()
        {

        }

        [TestMethod]
        public void DeleteBookInvalidId()
        {

        }

        [TestMethod]
        public void DeleteBookSqlError()
        {
            //cartea face parte dintr-un imprumut
        }

        [TestMethod]
        public void GetAbonatByPhoneValidPhone()
        {

        }

        [TestMethod]
        public void GetAbonatByPhoneInvalidPhone()
        {

        }

        [TestMethod]
        public void GetAbonatByPhoneSqlError()
        {
            //?
        }

        [TestMethod]
        public void UpdateStatusAbonatAbonatValidMesajValid()
        {

        }

        [TestMethod]
        public void UpdateStatusAbonatAbonatValidMesajInvalid()
        {

        }

        [TestMethod]
        public void UpdateStatusAbonatAbonatInvalid()
        {
            
        }

        [TestMethod]
        public void UnrestrictAbonatAbonatValid()
        {
            
        }

        [TestMethod]
        public void UnrestrictAbonatAbonatInvalid()
        {
            
        }

        [TestMethod]
        public void BlocareAbonatAbonatValid()
        {

        }

        [TestMethod]
        public void BlocareAbonatAbonatInvalid()
        {
            
        }

        [TestMethod]
        public void RestrictAbonatAbonatValid()
        {
            
        }

        [TestMethod]
        public void RestrictAbonatAbonatInvalid()
        {

        }

        [TestMethod]
        public void LoginSuccessfully()
        {
            //Utilizator    
        }


        [TestMethod]
        public void LoginUnsuccessfully()
        {

        }

        [TestCleanup]
        public void CleanUp()
        {


            /*
             * [TestCleanup]
            public void CleanUp()
            {
                // Golește toate tabelele
                string[] tabele = { "Utilizator", "Abonat", "Carte", "Imprumut", "Isbn", "Rol" };
                foreach (var tbl in tabele)
                {
                    try
                    {
                        using (var cmd = new SQLiteCommand($"DELETE FROM {tbl};", _database.Connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch { ignore dacă nu există tabela în testul respectiv 
                    }
                }
                _database.Close();
            }

            //de decuplat testele intre ele la final

          **/
           _database.Close();
        }
    }
}
