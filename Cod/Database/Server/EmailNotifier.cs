/**************************************************************************
 *                                                                        *
 *  File:        EmailNotifier.cs                                         *
 *  Copyright:   (c) 2025, A. Marina                                      *
 *  E-mail:      marina.agavriloaei@tuiasi.ro                             *
 *  Description: Observator concret care trimite (simulează               *
 *               trimiterea de) notificări prin e-mail                    *
 *               către abonații întârziați.                               *
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
    /// Observator concret care trimite (simulează trimiterea de) notificări prin e-mail către abonații întârziati.
    /// </summary>
    public class EmailNotifier : INotificationObserver
    {
        /// <summary>
        /// Parcurge lista de abonați întârziati și trimite (simulează trimiterea) unei notificări prin e-mail fiecăruia.
        /// </summary>
        /// <param name="abonatiIntarziati">Lista de abonați în întârziere primită de la subiect</param>
        public void Notify(List<Database.Abonat> abonatiIntarziati)
        {
            foreach (var abonat in abonatiIntarziati)
            {
                Console.WriteLine($"[EMAIL] Trimitem notificare către {abonat.Nume} {abonat.Prenume} ({abonat.Email})");
            }
        }
    }
}
