## Appendix C: Elemente de cod relevante
După cu s-a menționat mai sus clientul este compus din 2 componente: interfața și  backend-ul. Pentru a crea o experiență plăcută s-a ales să se pornească backend-ul automat în momentul în care se lansează în execuție interfața în mod automat, fără intervenția utilizatorului. Codul care realizează această operație este ilustrat mai jos:
```C#
	string relativePath = @"..\..\..\ClientBackend\bin\Debug\ClientBackend.exe";
            string workerPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), relativePath));
            bool showConsole = true;

            var startInfo = new ProcessStartInfo
            {
                FileName = workerPath,
                UseShellExecute = false,
                CreateNoWindow = !showConsole,
                WindowStyle = showConsole ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden
            };
 Process proc = Process.Start(startInfo);
```

Conectarea la server se realizează prin intermediul unei conexiuni TCP. Astfel, clientul trimite o cerere de conectare la server, iar serverul o acceptă.
Serverul acceptă o conexiune în mod blocant, iar pentru fiecare client se va genera un fir de execuție separat conform codului de mai jos:
```C#
while (_isRunning)
            {
                TcpClient client = _listener.AcceptTcpClient();
                Console.WriteLine("Client connected.");
                Thread thread = new Thread(() => HandleClient(client));
                thread.Start();
            } 
```

Clientul se conectează la server prin intermediul deschiderii unei conexiuni TCP. Deoarece adresa IP a serverului nu e cunoscută de către client, acesta parcurge o listă de adrese IP în căutarea serverului (operație realizată în constructor). Verificarea se realizează prin încercări, așteptând la fiecare încercare un timp de 50 de milisecunde. Din acest motiv interfața pornește mai târziu de la momentul în care se lansează în execuție programul. Acest proces este ilustrat în codul de mai jos:
```C#
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
```
###### [Apendix D: Cazuri de testare >](/Documentație/Apendix%20D-Cazuri%20de%20testare.md)