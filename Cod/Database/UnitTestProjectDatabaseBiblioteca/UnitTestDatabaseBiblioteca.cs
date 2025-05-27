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
        }
        [TestMethod]
        public void InsertUserRoleNotFound()
        {

        }

        [TestMethod]
        public void InsertUserError()
        {

        }


        [TestMethod]
        public void DeleteUserSuccessfully()
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
    }
}
