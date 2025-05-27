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
        public void InsertUserError()
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
        public void DeleteUserSuccessfully()
        {
            Utilizator testUser = new Utilizator("TestUser", "parola", "bibliotecar");
            bool result=_database.DeleteUser(testUser.Nume);
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
        public void DeleteUserError()
        {
            //refacere metoda dupa adaugare imprumut pentru acest user


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
           _database.Close();
        }
    }
}
