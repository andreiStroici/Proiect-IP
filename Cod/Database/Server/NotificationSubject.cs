/**************************************************************************
 *                                                                        *
 *  File:        NotificationSubject.cs                                   *
 *  Copyright:   (c) 2025, A. Marina                                      *
 *  E-mail:      marina.agavriloaei@tuiasi.ro                             *
 *  Description: Clasa Subiect (Observable) care gestionează o            *
 *               colecție de observatori și transmite starea              *
 *               (lista de abonați întârziați) către ei.                  *
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// Clasă Subiect (Observable) care gestionează o colecție de observatori și transmite starea (lista de abonați întârziati) către ei.
    /// </summary>
    public class NotificationSubject
    {
        private List<INotificationObserver> _observers = new List<INotificationObserver>();
        private List<Database.Abonat> _abonatiIntarziati;

        // <summary>
        /// Înregistrează un observator care dorește să primească notificări despre abonații întârziati.
        /// </summary>
        /// <param name="observer">Instanța unui obiect care implementează INotificationObserver</param>
        public void AddObserver(INotificationObserver observer)
        {
            _observers.Add(observer);
        }

        /// <summary>
        /// Actualizează starea internă cu lista curentă de abonați întârziati.
        /// </summary>
        /// <param name="lista">Lista de abonați care au întârziere la împrumuturi</param>
        public void SetAbonatiIntarziati(List<Database.Abonat> lista)
        {
            _abonatiIntarziati = lista;
        }

        /// <summary>
        /// Dezabonează un observator astfel încât să nu mai primească notificări.
        /// </summary>
        /// <param name="observer">Observatorul de eliminat din lista de înregistrări</param>
        public void RemoveObserver(INotificationObserver observer)
        {
            _observers.Remove(observer);
        }
        /// <summary>
        /// Notifică toți observatorii înregistrați, apelând metoda Notify(...) cu lista actualizată de abonați întârziati.
        /// </summary>
        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Notify(_abonatiIntarziati);
            }
        }
    }
}
