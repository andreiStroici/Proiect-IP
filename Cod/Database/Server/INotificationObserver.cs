/**************************************************************************
 *                                                                        *
 *  File:        INotificationObserver.cs                                 *
 *  Copyright:   (c) 2025, A. Marina                                      *
 *  E-mail:      marina.agavriloaei@tuiasi.ro                             *
 *  Description: Interfața pentru observatorii care trebuie sa primească  *
 *               notificări despre abonații întârziați.                   *
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
    /// Interfață pentru observatorii care trebuie să primească notificări despre abonații întârziati.
    /// </summary>
    public interface INotificationObserver
    {
        /// <summary>
        /// Este apelată de subiect (NotificationSubject) de fiecare dată când există date noi despre abonații întârziati.
        /// </summary>
        /// <param name="abonatiIntarziati">Lista de abonați care sunt în întârziere</param>
        void Notify(List<Database.Abonat> abonatiIntarziati);
    }
}
