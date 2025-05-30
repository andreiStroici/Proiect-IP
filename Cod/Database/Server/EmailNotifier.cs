using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class EmailNotifier : INotificationObserver
    {
        public void Notify(List<Database.Abonat> abonatiIntarziati)
        {
            foreach (var abonat in abonatiIntarziati)
            {
                Console.WriteLine($"[EMAIL] Trimitem notificare către {abonat.Nume} {abonat.Prenume} ({abonat.Email})");
            }
        }
    }
}
