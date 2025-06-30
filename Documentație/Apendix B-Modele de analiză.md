##### [<< Cuprins](Cuprins.md)
##### [< Apendix A: Glosar](Apendix%20A-Glosar.md)
## Apendix B: Modele de analiză
În această parte se va prezenta partea de  analiză și proiectare pentru realizarea aplicației. În această etapă s-au realizat o serie de diagrame care să descrie complet funcționalitățile și modul de procesare, care vor fi detaliate în cele ce urmează.   
Prima dată s-a realizat diagrama de cazuri de utilizare, sau „use case” după cum mai este cunoscută. Aceasta este ce acare descrie funcționalitățile principale ale aplicației și tipul de utilizatori care vor utiliza această aplicație. Astfel s-a realizat o listă de cazuri de utilizare compusă din:
- Gestionare abonați
- Gestionare împrumuturi
- Gestionare retururi
- Gestionare cărți
- Gestionare angajați
	Pentru aceste activități s-a constatat că sunt necesari doi actori distincți:
- Administrator
- Bibliotecar  
Diagrama de cazuri de utilizare este reprezentată în imaginea de mai jos:
![Diagrama de usecase](/Documentație/7%20Imagini/UseCaseDiagram.png)
În cele ce urmează se vor prezenta și descrie fiecărui actor.
1.	Administratorul  
	Acesta este cel care are o poziție administrativă în bibliotecă fiind responsabil pentru schema de personal al bibliotecii și de menținerea stocului de cărți disponibile din bibliotecă. De asemenea, deși el nu interacționează direct cu abonații bibliotecii, acesta poate realiza unele operații asupra tabelei cu angajați din baza de date, operație detaliată mai jos.
2.	Bibliotecatul  
	Acesta este un utilizator al aplicației propuse care se ocupă de interacțiunea cu abonații bibliotecii. Acesta este responsabil de realizarea împrumuturilor și retururilor, dar poate realiza și unele operații asupra bazei de date cu abonați.   
	Cazurile de utilizare sunt prezentate mai jos:
1.	Gestionarea abonaților  
		Gestionarea abonaților este diferită în funcție de ce actor interacționează cu ea. Funcțiile pe care le realizează fiecare actor asupra abonaților sunt distincte.  
		Administratorul este cel care se ocupă de monitorizarea statusului clienților bibliotecii. Pe baza acestui status se poate limita accesul al unele facilități ale bibliotecii (pentru utilizatorii restricționați se elimină dreptul de a împrumuta o carte acasă, în timp ce clienții blocați nu mai pot realiza niciun împrumut). Dar indiferent de statusul pe care îl au abonații nu se limitează opțiunea de a returna cărți.  
		În cazul bibliotecarului gestionarea abonaților are o altă semnificație: ce de a verifica dacă datele utilizatorului sunt valide sau dacă nu. De asemenea, considerând faptul că acesta interacționează direct cu abonații bibliotecii, acesta are posibilitatea de a adăuga abonați noi în baza de date.
2.	Gestionare împrumuturi  
		Gestionarea împrumuturilor este disponibilă numai bibliotecarului. Aceasta este compusă din mai multe operații care trebuie să se ruleze în ordinea specificată. Întâi se va prelua numărul de telefon pentru a ne asigura că există în baza de date o linie care să corespundă cu datele introduse spre a fi verificate. Dacă datele sunt valide, se vor activa meniurile în funcție de statusul pe care îl are abonatul. După ce autentificarea s-a realizat cu succes se va căuta cartea după titlu sau autor. În cele din urmă dacă abonatul decide să continue împrumutul atunci se va introduce în tabela „împrumuturi” cu o dată de retur distinctă în funcție de tipul împrumutului: dacă împrumutul se realizează doar în sala de lectură atunci data de retur coincide cu data curentă. În caz contrar data de retur va fi după 14 zile de la realizarea împrumutului.
3.	Gestionare retururi  
		Acest caz de utilizare descrie operația de retur a unei cărți. Această operație poate fi realizată numai de bibliotecar și care poate fi realizată de orice abonat, indiferent de statusul pe care îl are. Această operație este compusă din mai multe operații mai mici. Întâi se va căuta clientul în baza de date dup numărul de telefon, care este unic și nu se va repeta în baza de date. După ce datele au fost valide se va afișa și o listă de cărți împrumutate. Bibliotecarul va selecta o care din cele returnate și fiind marcate ca fiind împrumutate, iar după ce va selecta o carte pentru retur atunci se va apăsa bubonul de retur se va realiza efectiv returul cărții, marcat de inserarea în baza de date a informațiilor necesare retururilor.
4.	Gestionare cărți  
		Această ficționalitate este disponibilă numai  administratorului care este și responsabil de menținerea stocului de cărți. Astfel acest caz de utilizare are două funcționalități mari principale: ștergerea unei cărți și adăugarea unei cărți.
		Pentru ștergerea unei cărți se va introduce ISBN-ul unei cărți, se va întoarce o listă de cărți. Din această listă, administratorul poate să selecteze o carte și prin apăsarea unui buton de interfață se realizează procesul de ștergere.  
	Pentru a adăuga o carte este necesar completarea unui formular car conține datele necesare adăugării unei noi cărți. La apăsarea butoiului de adăugare de carte se va introduce în baza de date noua carte, dar nu după ce s-a realizat și o verificate asupra topurilor și datelor trimise.
5.	Gestionare angajați  
Această funcționalitate este disponibilă numai administratorului, și are și ea anumite limitări. Astfel, prin intermediul acestui caz de utilizare se permite o păstrare dinamică a fluxului de angajați din bibliotecă. Însă administratorul poarte doar să șteargă un bibliotecar, sau poate adăuga și un bibliotecar, dar și un administrator. Dacă se dorește ștergerea unui administrator este necesară intervenția noastră la aceste proiecte.  
După realizarea diagramei de cazuri de utilizare s-a realizat diagrama entitate-relație care descrie baza de date de tip SQLite folosită. Această diagramă este reprezentată în figura de mai jos. 
![Diagrama entitate relatie](/Documentație/7%20Imagini/ER_Diagram.jpg)
![Diagrama entiatate relatie](/Documentație/7%20Imagini/ER_Diagram1.jpg)

Baza de date a fost creată după următoarele reguli de business:
- O carte poate fi împrumutată dacă nu a fost împrumutată sau dacă toate datele de restituire au fost completate. 
- Dacă o carte nu este restituită în maxim 2 săptămâni de la data împrumutului se poate limita numărul maxim de împrumuturi ale acelui utilizator. 
- Numărul de telefon al unui abonat trebuie să fie unic. 
- Abonatul are minim 14 ani pentru a i se face un cont.(trebuie sa aibă buletin pentru adresa de reședință) 
- Data de restituire a fiecărui împrumut va fi ulterioară sau identică cu data de împrumut. 
- Pentru cărțile împrumutate în sala de lectură trebuie să fie aceiași dată de împrumut și de retur. 
- Data de retur nu poate fi o dată ulterioară datei calendaristice curente. 
- Data de împrumut este data curentă. 
- Numărul de telefon trebuie sa aibă 9 cifre și prefixul aferent acestuia. 
- Adresa de email trebuie să aibă un format valid de ex. nume_utilizator@domeniu.top-level_domain 
- Fiecare carte va fi identificată printr-un cod de inventar.(id_carte) 
- Numele, prenumele , adresa de domiciliu, numărul de telefon, id abonatului, emailul abonatului, numărul maxim de cărti care pot fi împrumutate de un abonat nu pot fi nule. 
- Un abonat poate împrumuta maxim numărul de cărți care îl are trecut pe cont.
- Numărul maxim de cărți pe care le poate împrumuta simultan un abonat este 5.
- ISBN este din 10 sau 13 cifre. 
- Numele și prenumele sunt formate doar din litere. 
- Administratorii și Bibliotecarii sunt identificați în funcție de numele de utilizator. 
- Este păstrat doar hash-ul parolei.(SHA-256).  

După s-a realizat diagrama de activități care are rolul de a descrie toate activitățile din interiorul sistemului. Astfel, aceasta încearcă să acopere toate activitățile pe care le are fiecare componentă a sistemului: clientului și serverul.

![Diagrama activitati client](/Documentație/7%20Imagini/ActivityDiagram.png)

Diagrama de activitate detaliază pașii parcurși de un utilizator de tip client în cadrul aplicației. Activitatea începe cu lansarea aplicației și continuă cu autentificarea în sistem. După autentificare, clientul poate accesa diverse funcționalități precum consultarea listei de cărți disponibile, filtrarea acestora, inițierea unui împrumut, returnarea unei cărți sau vizualizarea istoricului propriu.   

Diagrama include multiple condiții decizionale care ghidează ramificarea fluxului în funcție de acțiunile utilizatorului sau de starea contului (ex: dacă are sau nu cărți întârziate, dacă este restricționat, dacă o carte este disponibilă etc.).  

Sunt reprezentate și acțiunile administrative automatizate, precum afișarea mesajelor de eroare, restricționarea împrumuturilor sau actualizarea bazei de date cu noile stări ale împrumuturilor.  

Activitatea se încheie fie prin deconectarea utilizatorului, fie prin închiderea aplicației. Diagrama reflectă complexitatea și interactivitatea funcționalităților disponibile pentru client, precum și condițiile de validare aplicate de sistem pentru menținerea coerenței datelor și a regulilor de funcționare.  


Pentru că aplicația noastră se bazează pe o  paradigmă client server, atunci a fost realizată și o diagramă de activități care descrie serverul, inserată mai jos.
![Diagrama activitati server](/Documentație/7%20Imagini/ActivityDiagramServer.png)

Diagrama descrie pașii parcurși de server pentru procesarea unei cereri. Operațiile sunt grupate pe baza operațiilor CRUD realizate asupra bazei de date. Acestea sunt reprezentate într-o manieră generală care deoarece toate operațiile care se realizează sunt: inserare, ștergere, update și ștergere. Operație de notificare este realizată de un șablon de proiectare de tip Observator, care este notificat când se găsește un client problematic. Parcurgerea bazei de date se realizează o dată 24 de ore.  

Pentru a facilita înțelegerea modului în care funcționează aplicația s-au realizat 3 diagrame: una pentru bibliotecar, una pentru administrator și una pentru server.  

Diagrama pentru bibliotecar este reprezentată mai jos.
![Diagrama secventa bibliotecar](/Documentație/7%20Imagini/SequenceDiagramBibliotecar.png)

În această diagramă se ilustrează toate entitățile active care interacționează cu bibliotecarul, de la cele software cum ar fi gestionarea bazei de date până la clientul care interacționează cu bibliotecarul.   

După ce a fost realizată diagrama de secvențe pentru bibliotecar a fost realizată diagrama de secvențe pentru administrator care este prezentată mai jos:
![Diagrama de secventa administrator](/Documentație/7%20Imagini/SequenceDiagramAdministrator.png)

După ce s-au realizat aceste diagrame, care oferă o imagine amplă și complexă asupra sistemului s-a realizat diagrama de clase, care descrie care sunt entitățile încapsulate sub formă de obiecte care au fost implementate.
![Diagrama de clase](/Documentație/7%20Imagini/Class_Diagram.jpg)

În cadrul acestei diagrame sunt acoperite componentele aplicației: serverul și clientul care la rândul său este compus din 2 componente: interfața și un backend care realizează comunicarea cu serverul și realizează unele operații asupra răspunsului serverului.
![Diagrama UML pentru Observer](/Documentație/7%20Imagini/Observer.png)
Interfaţa Observer
- Definim INotificationObserver cu o metodă Notify(List<Abonat> abonatiIntarziati).
- Orice clasă care vrea să „asculte” modificările implementează această interfaţă.

Clasa Subject (NotificationSubject)
- Păstrează o listă privată de observatori (_observers).
- Are un câmp intern cu starea de transmis (_abonatiIntarziati).
- Metoda AddObserver(...) permite oricărei instanţe de INotificationObserver să se înregistreze.
- Când datele se schimbă, codul apelează SetAbonatiIntarziati(...) pentru a actualiza lista de abonaţi întârziati, apoi NotifyObservers().
- NotifyObservers() parcurge fiecare observator şi îi apelează Notify(...), transmţând lista actualizată.

Observer concret (EmailNotifier)
- De exemplu, EmailNotifier implementează INotificationObserver.
- În Notify(...), iterează prin lista de abonaţi întârziati şi trimite (sau afişează în consolă) mesaje de notificare.
- Subiectul nu ştie cum trimite observatorul notificarea; doar îi cheamă metoda Notify(...).

Fluxul general
    La pornirea aplicaţiei, creăm un NotificationSubject şi îi adăugăm observator:
```C#
var subject = new NotificationSubject();
        	subject.AddObserver(new EmailNotifier());
   Când detectăm abonaţi întârziati (de ex. o dată pe zi), construim List<Abonat>
``` 
şi facem:
```C#
subject.SetAbonatiIntarziati(lateList);
        	subject.NotifyObservers();
Acest apel declanşează automat Notify(...)
```
în EmailNotifier, fără ca NotificationSubject să cunoască detalii despre cum anume e trimisă notificarea.





