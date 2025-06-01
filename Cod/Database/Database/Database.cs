/*************************************************************************
*                                                                       *
*  File:        Database.cs                                             *
*  Copyright:   (c) 2025, B. Andreea                                    *
*                                                                       *
*  Description: Gestioneaza conexiunea si operatiile CRUD               *
*               asupra tabelelor din baza de date                       *
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
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace Database
{
    public class Database
    {
        private static Database _database;
        private SQLiteConnection _connection;
        private string _filename;
        private static readonly object _staticLock = new object();

        /// <summary>
        /// constructorul clasei
        /// </summary>
        /// <param name="filename"></param>
        private Database(string filename = "Data Source=biblioteca.db;Version=3;")
        {
            _filename = filename;
            try
            {
                _connection = new SQLiteConnection(_filename);
                _connection.Open();
                Console.WriteLine("Conectarea la baza de date a fost realizata cu succes!");
                
            }
            catch (SQLiteException e)
            {
                Console.WriteLine("Eroare la coe=nectarea la baza de date: " + e.Message);
            }


        }

        /// <summary>
        /// getter pentru instanta statica privata
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Database GetDatabase(string filename = "Data Source=biblioteca.db;Version=3;")
        {
            if (_database == null)
            {
                _database = new Database(filename);
            }
            return _database;
        }


        /// <summary>
        /// insereaza o inregistrare in tabela Isbn verificand formatul isbn-ului si daca acesta nu exista deja in tabela
        /// </summary>
        /// <param name="carte"></param>
        /// <returns></returns>
        public bool InsertIsbn(Carte carte)
        {
            lock (_staticLock)
            {
                string query1 = "SELECT COUNT(*) FROM Isbn WHERE id_isbn = @id_isbn;";
                using (var cmd = new SQLiteCommand(query1, _connection))
                {
                    cmd.Parameters.AddWithValue("@id_isbn", carte.Isbn);
                    long count = (long)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        Console.WriteLine("Isbn-ul exista deja");
                        return true;
                    }
                }
                try
                {
                    string query2 = "INSERT INTO Isbn (id_isbn, titlu, autor, editura, gen) VALUES (@id_isbn, @titlu, @autor, @editura, @gen);";
                    using (var cmd = new SQLiteCommand(query2, _connection))
                    {
                        cmd.Parameters.AddWithValue("@id_isbn", carte.Isbn);
                        cmd.Parameters.AddWithValue("@titlu", carte.Titlu);
                        cmd.Parameters.AddWithValue("@autor", carte.Autor);
                        cmd.Parameters.AddWithValue("@editura", carte.Editura);
                        cmd.Parameters.AddWithValue("@gen", carte.Gen);

                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine("Noul isbn a fost adaugat cu succes.");
                    return true;
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la inserarea in tabela Isbn: " + ex.Message);
                    return false;
                }
            }

        }


        /// <summary>
        /// inserarea unei inregistrari in tabela Carte, apeland si InsertIsbn() si returnand id-ul acesteia din DB
        /// gestioneaza si inserarile concurente prin folosirea tranzactiilor
        /// </summary>
        /// <param name="carte"></param>
        /// <returns></returns>
        public int InsertBook(Carte carte)
        {
            lock (_staticLock)
            {
                if (!InsertIsbn(carte))
                    return -1;

                try
                {

                    using (var transaction = _connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                    {

                        string insertQuery = "INSERT INTO Carte (id_isbn, status) VALUES (@id_isbn, @status)";
                        using (var cmd = new SQLiteCommand(insertQuery, _connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@status", carte.StatusDisponibilitate);
                            cmd.Parameters.AddWithValue("@id_isbn", carte.Isbn);
                            cmd.ExecuteNonQuery();
                        }


                        string idQuery = "SELECT last_insert_rowid()";
                        using (var cmd = new SQLiteCommand(idQuery, _connection, transaction))
                        {
                            long id = (long)cmd.ExecuteScalar();
                            transaction.Commit();
                            Console.WriteLine("Carte adaugata cu succes.");
                            return (int)id;
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la adaugarea unei carti in baza de date: " + ex.Message);
                    return -1;
                }
            }
            
        }



        /// <summary>
        /// obtinerea listei de carti dupa isbn
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public List<Carte> GetCartiByIsbn(string isbn)
        {
            lock (_staticLock)
            {
                List<Carte> carti = new List<Carte>();

                string query = @"
                                SELECT 
                                    c.id_carte,
                                    i.id_isbn,
                                    i.titlu,
                                    i.autor,
                                    i.gen,
                                    i.editura,
                                    c.status
                                FROM Carte c
                                JOIN Isbn i ON c.id_isbn = i.id_isbn
                                WHERE i.id_isbn = @isbn
                                  AND c.status = 'disponibil'";


                try
                {
                    using (var cmd = new SQLiteCommand(query, _connection))
                    {
                        cmd.Parameters.AddWithValue("@isbn", isbn);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idCarte = reader.GetInt32(0);
                                string idIsbn = reader.GetString(1);
                                string titlu = reader.GetString(2);
                                string autor = reader.GetString(3);
                                string gen = reader.GetString(4);
                                string editura = reader.GetString(5);
                                string status = reader.GetString(6);

                                var carte = new Carte(idCarte, idIsbn, titlu, autor, gen, editura, status);
                                carti.Add(carte);
                            }
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la obtinerea cartilor prin ISBN: " + ex.Message);
                }

                return carti;
            }
        }




        /// <summary>
        /// stergerea unei carti din tabela Carte
        /// </summary>
        /// <param name="carte"></param>
        /// <returns></returns>
        public bool DeleteBook(int idCarte)
        {
            lock(_staticLock)
            {
                try
                {
                    string query = "DELETE FROM Carte WHERE id_carte = @idCarte AND status = 'disponibil'";
                    using (var cmd = new SQLiteCommand(query, _connection))
                    {
                        cmd.Parameters.AddWithValue("@idCarte", idCarte);

                        int rowsDeleted = cmd.ExecuteNonQuery();

                        if (rowsDeleted > 0)
                        {
                            Console.WriteLine("Cartea a fost stearsa.");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Nu a fost gasita nicio carte cu acest id.");
                            return false;
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la stergerea cartii: " + ex.Message);
                    return false;
                }
            }
            
        }




        /// <summary>
        /// insereaza o inregistrare in tabela Abonat
        /// </summary>
        /// <param name="abonat"></param>
        /// <returns></returns>
        public bool InsertClient(Abonat abonat)
        {
            lock(_staticLock)
            {
                try
                {

                    string query = "INSERT INTO Abonat (nume, prenume, adresa, telefon, email, limita, status) VALUES (@nume, @prenume, @adresa, @telefon, @email, @limita, @status)";
                    using (var cmd = new SQLiteCommand(query, _connection))
                    {
                        cmd.Parameters.AddWithValue("@nume", abonat.Nume);
                        cmd.Parameters.AddWithValue("@prenume", abonat.Prenume);
                        cmd.Parameters.AddWithValue("@adresa", abonat.Adresa);
                        cmd.Parameters.AddWithValue("@telefon", abonat.Telefon);
                        cmd.Parameters.AddWithValue("@email", abonat.Email);
                        cmd.Parameters.AddWithValue("@limita", abonat.LimitaCarti);
                        cmd.Parameters.AddWithValue("@status", abonat.Status);


                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la adaugarea unui abonat in baza de date: " + ex.Message);
                    return false;
                }
            }
            
        }



        /// <summary>
        /// obtinerea abonatului dupa telefon, arunca o exceptie in caz contrar
        /// </summary>
        /// <param name="telefon"></param>
        /// <returns></returns>
        public Abonat GetAbonatByPhone(string telefon)
        {
            lock (_staticLock)
            {
                string query = @"SELECT id_abonat, nume, prenume, adresa, telefon, email, limita, status
                         FROM Abonat WHERE telefon = @telefon";

                using (var cmd = new SQLiteCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@telefon", telefon);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int idAbonat = Convert.ToInt32(reader["id_abonat"]);
                            string nume = (string)reader["nume"];
                            string prenume = (string)reader["prenume"];
                            string adresa = (string)reader["adresa"];
                            string telefonDb = (string)reader["telefon"];
                            string email = (string)reader["email"];
                            int limita = Convert.ToInt32(reader["limita"]);
                            string status = (string)reader["status"];

                            Console.WriteLine("Abonat gasit cu succes dupa telefon.");
                            return new Abonat(idAbonat, nume, prenume, adresa, telefonDb, email, limita, status);
                        }
                        else
                        {
                            throw new ClientNotFoundException($"Nu a fost gasit niciun abonat cu telefonul: {telefon}");
                           
                        }
                    }
                }
            }
        }

        /// <summary>
        /// returneaza lista cu cartile nereturnate ale unui abonat
        /// </summary>
        /// <param name="telefon"></param>
        /// <returns></returns>
        public List<Carte> GetLoanedBooks(int idAbonat)
        {
            lock(_staticLock)
            {
                var books = new List<Carte>();



                string query = @"
                            SELECT Carte.id_carte, Isbn.id_isbn, Isbn.titlu, Isbn.autor, Isbn.gen, Isbn.editura, Carte.status
                            FROM Carte
                            JOIN Isbn ON Carte.id_isbn = Isbn.id_isbn
                            JOIN Imprumut ON Carte.id_carte = Imprumut.id_carte
                            WHERE Imprumut.data_restituire IS NULL AND Imprumut.id_abonat = @id_abonat
                        ";

                try
                {
                    using (var cmd = new SQLiteCommand(query, _connection))
                    {
                        cmd.Parameters.AddWithValue("@id_abonat", idAbonat);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var carte = new Carte(
                                    Convert.ToInt32(reader["id_carte"]),
                                    reader["id_isbn"].ToString(),
                                    reader["titlu"].ToString(),
                                    reader["autor"].ToString(),
                                    reader["gen"].ToString(),
                                    reader["editura"].ToString(),
                                    reader["status"].ToString()
                                );
                                books.Add(carte);
                            }
                        }
                    }

                    return books;
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la extragerea cartilor imprumutate: " + ex.Message);
                    return null;
                }
            }
            
        }



        /// <summary>
        /// realizeaza o cautare partiala a unei carti pe baza cuvintelor continute in titlu si in autor
        /// </summary>
        /// <param name="titluPartial"></param>
        /// <param name="autorPartial"></param>
        /// <returns></returns>
        public List<Carte> CautareCartiPartial(string titluPartial = "", string autorPartial = "")
        {
            lock (_staticLock)
            {
                try
                {

                    string query = @"SELECT Carte.id_carte, Isbn.id_isbn, Isbn.titlu, Isbn.autor, Isbn.gen, Isbn.editura, Carte.status
                                FROM Carte
                                JOIN Isbn ON Carte.id_isbn = Isbn.id_isbn
                                WHERE Isbn.titlu LIKE @cuvantTitlu 
                                  AND Isbn.autor LIKE @cuvantAutor
                                  AND Carte.status = 'disponibil'";

                    var listaCarti = new List<Carte>();
                    using (var cmd = new SQLiteCommand(query, _connection))
                    {
                        cmd.Parameters.AddWithValue("@cuvantTitlu", "%" + titluPartial + "%");
                        cmd.Parameters.AddWithValue("@cuvantAutor", "%" + autorPartial + "%");

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var carte = new Carte(
                                    Convert.ToInt32(reader["id_carte"]),
                                    reader["id_isbn"].ToString(),
                                    reader["titlu"].ToString(),
                                    reader["autor"].ToString(),
                                    reader["gen"].ToString(),
                                    reader["editura"].ToString(),
                                    "disponibil"
                                );
                                listaCarti.Add(carte);
                                

                            }
                        }
                    }

                    return listaCarti;
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare extragerea listei cu sugestii de carti: " + ex.Message);
                    return null;
                }
            }
           
        }




        /// <summary>
        /// verifica daca o carte este disponibil pentru imprumut
        /// </summary>
        /// <param name="idCarte"></param>
        /// <returns></returns>
        private bool IsCartedisponibil(int idCarte)
        {
            lock (_staticLock)
            {
                string query = @"
                        SELECT COUNT(*)
                        FROM Carte c
                        WHERE c.id_carte = @idCarte AND c.status = 'disponibil'";

                try
                {
                    using (var cmd = new SQLiteCommand(query, _connection))
                    {
                        cmd.Parameters.AddWithValue("@idCarte", idCarte);
                        var result = cmd.ExecuteScalar();
                        return result != null && Convert.ToInt32(result) > 0;
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la verificarea statusului cartii: " + ex.Message);
                    return false;
                }
            }
        }


        /// <summary>
        /// validare idClinet 
        /// </summary>
        /// <param name="idClient"></param>
        /// <returns></returns>
        private bool IsIdClientValid(int idClient)
        {
            lock (_staticLock)
            {
                string query = @"
                            SELECT COUNT(*) 
                            FROM Abonat c
                            WHERE c.id_abonat = @idClient";

                using (var cmd = new SQLiteCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@idClient", idClient);
                    var result = cmd.ExecuteScalar();
                    if (result != null && Convert.ToInt32(result) > 0)
                        return true;
                    else
                        return false;
                }
            }
            
        }

        /// <summary>
        /// obtinerea statusului abonatului pe baza id-ului
        /// </summary>
        /// <param name="idAbonat"></param>
        /// <returns></returns>
        public string GetStatusAbonat(int idAbonat)
        {
            lock (_staticLock)
            {
                string query = "SELECT status FROM Abonat WHERE id_abonat = @idAbonat";
                using (var cmd = new SQLiteCommand(query, _connection))
                {
                    cmd.Parameters.AddWithValue("@idAbonat", idAbonat);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return result.ToString().ToLower(); // status = 'blocat', 'cu restrictii', 'fara restrictii'
                    }
                    else
                    {
                        return null;
                    }

                }
            }
           
        }


        /// <summary>
        /// verificarea daca un abonat este blocat
        /// </summary>
        /// <param name="idAbonat"></param>
        /// <returns></returns>
        public bool IsBlocked(int idAbonat)
        {
            return GetStatusAbonat(idAbonat) == "blocat";
        }
        /// <summary>
        /// verificarea daca un abonat este restrictionat
        /// </summary>
        /// <param name="idAbonat"></param>
        /// <returns></returns>
        public bool IsRestricted(int idAbonat)
        {
            return GetStatusAbonat(idAbonat) == "cu restrictii";
        }


        /// <summary>
        /// insereaza o inregistrare in tabela Imprumut
        /// </summary>
        /// <param name="abonat"></param>
        /// <param name="carte"></param>
        /// <returns></returns>
        public bool InsertLoan(int idAbonat, int idCarte, string locatie)
        {
            lock(_staticLock)
            {
                if (!IsCartedisponibil(idCarte))
                {
                    Console.WriteLine("Cartea nu este disponibila pentru imprumut.");
                    return false;
                }

                if (!IsIdClientValid(idAbonat))
                {
                    Console.WriteLine("Id ul abonatului nu este valid");
                    return false;
                }

                using (var transaction = _connection.BeginTransaction())
                {
                    try
                    {
                        DateTime deadline = locatie.ToLower() == "acasa"
                            ? DateTime.Now.AddDays(14)
                            : DateTime.Now;

                        string insertQuery = "INSERT INTO Imprumut (id_abonat, id_carte, deadline) " +
                                             "VALUES (@idAbonat, @idCarte, @deadline)";
                        using (var cmdInsert = new SQLiteCommand(insertQuery, _connection))
                        {
                            cmdInsert.Parameters.AddWithValue("@idAbonat", idAbonat);
                            cmdInsert.Parameters.AddWithValue("@idCarte", idCarte);
                            cmdInsert.Parameters.AddWithValue("@deadline", deadline.ToString("yyyy-MM-dd"));
                            cmdInsert.ExecuteNonQuery();
                        }


                        string updateCarte = "UPDATE Carte SET status = 'indisponibil' WHERE id_carte = @idCarte";
                        using (var cmdCarte = new SQLiteCommand(updateCarte, _connection))
                        {
                            cmdCarte.Parameters.AddWithValue("@idCarte", idCarte);
                            cmdCarte.ExecuteNonQuery();
                        }

                        string updateAbonat = "UPDATE Abonat SET limita = limita - 1 WHERE id_abonat = @idAbonat";
                        using (var cmdAbonat = new SQLiteCommand(updateAbonat, _connection))
                        {
                            cmdAbonat.Parameters.AddWithValue("@idAbonat", idAbonat);
                            cmdAbonat.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine("Eroare la inregistrarea imprumutului: " + ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            
        }



        /// <summary>
        /// returnarea unei carti de catre un abonat (verificare inatrziere si actualizare data_restituire)
        /// </summary>
        /// <param name="carte"></param>
        /// <param name="abonat"></param>
        /// <returns></returns>
        public bool ReturnBook(int idAbonat, int idCarte)
        {
            lock(_staticLock)
            {
                using (var transaction = _connection.BeginTransaction())
                {
                    try
                    {

                        string selectImprumut = @"
                                SELECT id_imprumut, deadline 
                                FROM Imprumut 
                                WHERE id_abonat = @idAbonat AND id_carte = @idCarte ";

                        int idImprumut = -1;
                        DateTime deadline;
                        using (var cmdSelect = new SQLiteCommand(selectImprumut, _connection))
                        {
                            cmdSelect.Parameters.AddWithValue("@idAbonat", idAbonat);
                            cmdSelect.Parameters.AddWithValue("@idCarte", idCarte);

                            using (var reader = cmdSelect.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    idImprumut = Convert.ToInt32(reader["id_imprumut"]);
                                    deadline = DateTime.Parse(reader["deadline"].ToString()).Date;
                                    Console.WriteLine("deadline: " + deadline);
                                    Console.WriteLine("DateTime.Now: " + DateTime.Now.Date);
                                    if (DateTime.Now.Date > deadline)
                                    {
                                        Console.WriteLine("Cartea a fost returnata cu intarziere.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cartea a fost returnata la timp.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Nu exista un imprumut activ pentru aceasta carte.");
                                    transaction.Rollback();
                                    return false;
                                }
                            }
                        }

                        string updateImprumut = @"
                                            UPDATE Imprumut 
                                            SET data_restituire = DATE('now') 
                                            WHERE id_imprumut = @idImprumut";

                        using (var cmdUpdate = new SQLiteCommand(updateImprumut, _connection))
                        {
                            cmdUpdate.Parameters.AddWithValue("@idImprumut", idImprumut);
                            cmdUpdate.ExecuteNonQuery();
                        }


                        string updateCarte = "UPDATE Carte SET status = 'disponibil' WHERE id_carte = @idCarte";
                        using (var cmd3 = new SQLiteCommand(updateCarte, _connection))
                        {

                            cmd3.Parameters.AddWithValue("@idCarte", idCarte);
                            cmd3.ExecuteNonQuery();
                        }



                        string updateLimita = "UPDATE Abonat SET limita = limita + 1 WHERE id_abonat = @idAbonat";
                        using (var cmd4 = new SQLiteCommand(updateLimita, _connection))
                        {
                            cmd4.Parameters.AddWithValue("@idAbonat", idAbonat);
                            cmd4.ExecuteNonQuery();
                        }


                        transaction.Commit();
                        return true;
                    }
                    catch (SQLiteException ex)
                    {
                        Console.WriteLine("Eroare la returnarea cartii: " + ex.Message);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
            
        }

        /// <summary>
        /// returneaza lista de abonati cu probleme (abonati cu inprumuturi active intarziate si/sau cei restrictionati)
        /// </summary>
        /// <returns></returns>
        public List<Abonat> CautareIntarziati()
        {
            lock (_staticLock)
            {
                var lista = new List<Abonat>();

                string query = @"
                            SELECT 
                                a.id_abonat, a.nume, a.prenume, a.adresa, a.telefon, a.email, a.limita, a.status,
                                MIN(i.deadline) AS cel_mai_vechi_deadline
                            FROM Abonat a
                            LEFT JOIN Imprumut i ON i.id_abonat = a.id_abonat AND i.data_restituire IS NULL
                            GROUP BY a.id_abonat
                            HAVING a.status = 'cu restrictii' 
                                OR MIN(i.deadline) < date(@azi)
                            ";

                try
                {
                    using (var cmd = new SQLiteCommand(query, _connection))
                    {
                        cmd.Parameters.AddWithValue("@azi", DateTime.Today);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int zileIntarziere = 0;

                                if (!reader.IsDBNull(reader.GetOrdinal("cel_mai_vechi_deadline")))
                                {
                                    DateTime deadline = Convert.ToDateTime(reader["cel_mai_vechi_deadline"]);

                                    if (deadline < DateTime.Today)
                                    {
                                        zileIntarziere = (DateTime.Today - deadline).Days;
                                    }
                                }

                                var abonat = new Abonat(
                                    Convert.ToInt32(reader["id_abonat"]),
                                    reader["nume"].ToString(),
                                    reader["prenume"].ToString(),
                                    reader["adresa"].ToString(),
                                    reader["telefon"].ToString(),
                                    reader["email"].ToString(),
                                    zileIntarziere,
                                    Convert.ToInt32(reader["limita"]),
                                    reader["status"].ToString()
                                );

                                lista.Add(abonat);
                            }
                        }
                    }

                    return lista;
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la cautarea abonatilor intarziati sau cu restrictii: " + ex.Message);
                    return null;
                }
            }
           
        }

        /// <summary>
        /// returneaza o lista cu abonatii care au imprumuturi active intarziate
        /// </summary>
        /// <returns></returns>
        public List<Abonat> CautareDoarIntarziati()
        {
            lock (_staticLock)
            {
                var lista = new List<Abonat>();

                string query = @"
                    SELECT 
                        a.id_abonat, a.nume, a.prenume, a.adresa, a.telefon, a.email, a.limita, a.status,
                        MIN(i.deadline) AS cel_mai_vechi_deadline
                    FROM Abonat a
                    LEFT JOIN Imprumut i ON i.id_abonat = a.id_abonat AND i.data_restituire IS NULL
                    GROUP BY a.id_abonat
                    HAVING MIN(i.deadline) < date(@azi)
                    ";

                try
                {
                    using (var cmd = new SQLiteCommand(query, _connection))
                    {
                        cmd.Parameters.AddWithValue("@azi", DateTime.Today);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DateTime deadline = Convert.ToDateTime(reader["cel_mai_vechi_deadline"]);
                                int zilePanaLaDeadline = (DateTime.Today - deadline).Days;

                                var abonat = new Abonat(
                                    Convert.ToInt32(reader["id_abonat"]),
                                    reader["nume"].ToString(),
                                    reader["prenume"].ToString(),
                                    reader["adresa"].ToString(),
                                    reader["telefon"].ToString(),
                                    reader["email"].ToString(),
                                    zilePanaLaDeadline,
                                    Convert.ToInt32(reader["limita"]),
                                    reader["status"].ToString()
                                );

                                lista.Add(abonat);
                            }
                        }
                    }

                    return lista;
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la cautarea abonatilor cu deadline viitor: " + ex.Message);
                    return null;
                }
            }
        }




        /// <summary>
        /// actualizeaza statusul unui abonat blocat/cu rescrictii/fara restrictii
        /// </summary>
        /// <param name="abonat"></param>
        /// <param name="mesaj"></param>
        /// <returns></returns>
        public bool UpdateStatusAbonat(int idAbonat, string mesaj)
        {
            lock(_staticLock)
            {
                try
                {

                    string updateStatus = "UPDATE Abonat SET status = @status WHERE id_abonat = @idAbonat";
                    using (var cmd = new SQLiteCommand(updateStatus, _connection))
                    {
                        cmd.Parameters.AddWithValue("@status", mesaj.ToLower());
                        cmd.Parameters.AddWithValue("@idAbonat", idAbonat);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Statusul abonatului a fost modificat cu succes");
                        }
                        return rowsAffected > 0;
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la actualizarea statusului abonatului: " + ex.Message);
                    return false;
                }
            }
           
        }


        /// <summary>
        /// elimina restrictiile unui abonat
        /// </summary>
        /// <param name="abonat"></param>
        /// <returns></returns>
        public bool UnRestrictAbonat(int idAbonat)
        {
            return UpdateStatusAbonat(idAbonat, "fara restrictii");
        }

        /// <summary>
        /// blocheaza un abonat
        /// </summary>
        /// <param name="abonat"></param>
        /// <returns></returns>
        public bool BlocareAbonat(int idAbonat)
        {
            return UpdateStatusAbonat(idAbonat, "blocat");
        }

        /// <summary>
        /// aplica restrictii unui abonat
        /// </summary>
        /// <param name="abonat"></param>
        /// <returns></returns>
        public bool RestrictAbonat(int idAbonat)
        {
            return UpdateStatusAbonat(idAbonat, "cu restrictii");
        }


        /// <summary>
        /// metoda care verifica daca utilizatorul exista in baza de date, in caz contrat arunca o exceptie
        /// </summary>
        /// <param name="utilizator"></param>
        /// <returns></returns>
        public bool Login(Utilizator utilizator)
        {
            lock (_staticLock)
            {
                string query = @"
                SELECT COUNT(*)
                FROM Utilizator U
                JOIN Rol R ON U.id_rol = R.id_rol
                WHERE U.nume_user = @nume_user
                  AND U.hash_parola = @hash_parola
                  AND R.nume_rol = @nume_rol;
                                        ";

              

                using (var command = new SQLiteCommand(query, _connection))
                {

                    command.Parameters.AddWithValue("@nume_user", utilizator.Nume);
                    command.Parameters.AddWithValue("@hash_parola", utilizator.HashParola);
                    command.Parameters.AddWithValue("@nume_rol", utilizator.Rol);

                    long count = (long)command.ExecuteScalar();
                   

                    if (count == 1)
                    {
                        Console.WriteLine("Utilizator autentificat cu succes");
                        return true;
                    }
                    else
                    {
                       throw new InvalidUserDataException("Datele de autentificare sunt invalide pentru utilizatorul: " + utilizator.Nume);
                     
                    }
                }
            }
        }

        

        /// <summary>
        /// insereaza un utilizator in baza de date verificand rolul acestuia pentru a corespunde cu rolurile disponibile
        /// </summary>
        /// <param name="utilizator"></param>
        public bool InsertUser(Utilizator utilizator)
        {
            lock(_staticLock)
            {
                string getRoleIdQuery = "SELECT id_rol FROM Rol WHERE nume_rol = @rol";
                int idRol;

                using (var cmdRol = new SQLiteCommand(getRoleIdQuery, _connection))
                {
                    cmdRol.Parameters.AddWithValue("@rol", utilizator.Rol);
                    object result = cmdRol.ExecuteScalar();

                    if (result == null)
                    {
                        Console.WriteLine("Rolul specificat nu exista in baza de date");
                        return false;
                    }


                    idRol = Convert.ToInt32(result);
                }


                try
                {
                    string insertQuery = @"
                                INSERT INTO Utilizator (nume_user, hash_parola, id_rol)
                                VALUES (@nume, @parola, @id_rol);";

                    using (var cmdInsert = new SQLiteCommand(insertQuery, _connection))
                    {
                        cmdInsert.Parameters.AddWithValue("@nume", utilizator.Nume);
                        cmdInsert.Parameters.AddWithValue("@parola", utilizator.HashParola);
                        cmdInsert.Parameters.AddWithValue("@id_rol", idRol);

                        cmdInsert.ExecuteNonQuery();
                    }
                    Console.WriteLine("Utilizator adaugat cu succes");
                    return true;
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("Eroare la inserarea utilizatorului in baza de date: " + e.Message);
                    return false;
                }
            }
               
               
                
                
            
        }

        /// <summary>
        /// stergerea unui angajat pe baza numelui
        /// </summary>
        /// <param name="nume"></param>
        /// <returns></returns>
        public bool DeleteUser(string nume)
        {
            lock (_staticLock)
            {
                string query = @"
                            DELETE FROM Utilizator 
                            WHERE nume_user = @nume
                              AND EXISTS (
                                  SELECT 1 FROM Rol 
                                  WHERE Rol.id_rol = Utilizator.id_rol 
                                    AND Rol.nume_rol <> 'administrator'
                              );";
                try
                {
                    using (var cmd = new SQLiteCommand(query, _connection))
                    {
                        cmd.Parameters.AddWithValue("@nume", nume);

                        int affected = cmd.ExecuteNonQuery();

                        if (affected == 0)
                        {
                            Console.WriteLine("Utilizatorul nu a fost gasit sau a fost deja sters sau este administrator.");
                            return false;
                        }
                        else
                        {
                            Console.WriteLine("Stergere cu succes");
                            return true;
                        }
                    }
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Eroare la stergerea utilizatorului: " + ex.Message);
                    return false;
                }
            }
           
        }




        /// <summary>
        /// inchidere conexiunii cu baza de date
        /// </summary>
        public void Close()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }

        /// <summary>
        /// deschide conexiunea cu baza de date
        /// </summary>
        public void Open()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
                _connection.Open();
        }

    }
}
