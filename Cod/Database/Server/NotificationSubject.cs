using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class NotificationSubject
    {
        private List<INotificationObserver> _observers = new List<INotificationObserver>();
        private List<Database.Abonat> _abonatiIntarziati;
        public void AddObserver(INotificationObserver observer)
        {
            _observers.Add(observer);
        }


        public void SetAbonatiIntarziati(List<Database.Abonat> lista)
        {
            _abonatiIntarziati = lista;
        }
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Notify(_abonatiIntarziati);
            }
        }
    }
}
