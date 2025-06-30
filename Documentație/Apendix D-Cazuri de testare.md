###### [<< Cuprins](/Documentație/Cuprins.md)
###### [< Apendix C - Elemente de cod relevante](/Documentație/Apendix%20C-Elemente%20de%20cod%20relevante.md)
## Apendix D: Cazuri de testare
<table border="1">
  <thead>
    <tr>
      <th>Nr. caz test</th>
      <th>Metoda testată</th>
      <th>Lista de parametri</th>
      <th>Rezultat așteptat</th>
      <th>Rezultat obținut</th>
      <th>Starea testului</th>
    </tr>
  </thead>
  <tbody>
<tr><td>1</td><td>InsertUser</td><td>testUser = Utilizator("TestUser5", "parola", "bibliotecar")</td><td>true și mesaj în consolă "Utilizator adăugat cu succes"</td><td>true și mesaj în consolă "Utilizator adăugat cu succes"</td><td>Passed</td></tr>
<tr><td>2</td><td>InsertUser</td><td>testUser = Utilizator("TestUser8", "parola", "administrator")</td><td>true și mesaj în consolă "Utilizator adăugat cu succes"</td><td>true și mesaj în consolă "Utilizator adăugat cu succes"</td><td>Passed</td></tr>
<tr><td>3</td><td>InsertUser</td><td>testUser = Utilizator("TestUser", "parola", "Inexisting role")</td><td>false și mesaj în consolă "Rolul specificat nu există în baza de date"</td><td>false și mesaj în consolă "Rolul specificat nu există în baza de date"</td><td>Passed</td></tr>
<tr><td>4</td><td>InsertUser</td><td>testUser = Utilizator("TestUser", "parola", "bibliotecar") (apel dublu)</td><td>false și mesaj în consolă "Eroare la inserarea utilizatorului în baza de date: ..."</td><td>false și mesaj în consolă "Eroare la inserarea utilizatorului în baza de date: ..."</td><td>Passed</td></tr>
<tr><td>5</td><td>InsertIsbn</td><td>testCarte = Carte(6, "0000000000009", "Carte de test", "Autor de Test", "Gen de test", "Teste pentru toți")</td><td>true și mesaj în consolă "Noul isbn a fost adăugat cu succes."</td><td>true și mesaj în consolă "Noul isbn a fost adăugat cu succes."</td><td>Passed</td></tr>
<tr><td>6</td><td>InsertIsbn</td><td>testCarte = Carte(0, "0000000001", "Carte de test", "Autor de Test", "Gen de test", "Teste pentru toți"); apel dublu InsertIsbn</td><td>true și mesaj în consolă "Isbn-ul există deja"</td><td>true și mesaj în consolă "Isbn-ul există deja"</td><td>Passed</td></tr>
<tr><td>7</td><td>InsertIsbn</td><td>testCarte = Carte(0, "000000000X", null, "Autor de Test", "Gen de test", "Teste pentru toți")</td><td>false și mesaj în consolă "Eroare la inserarea în tabela Isbn: ..."</td><td>false și mesaj în consolă "Eroare la inserarea în tabela Isbn: ..."</td><td>Passed</td></tr>
<tr><td>8</td><td>InsertBook</td><td>testCarte = Carte(0, "0000000000020", "Carte Test Insert", "Autor Insert", "Gen", "Editura", "disponibil")</td><td>return idCarte > 0</td><td>return idCarte > 0</td><td>Passed</td></tr>
<tr><td>9</td><td>InsertBook</td><td>testCarte = Carte(0, "000000000X", null, "Autor Test", "Gen", "Editura", "disponibil")</td><td>return -1</td><td>return -1</td><td>Passed</td></tr>
<tr><td>10</td><td>GetCartiByIsbn</td><td>inserare carte cu ISBN "0000000000021", apoi GetCartiByIsbn("0000000000021")</td><td>listă non-goală, există carte cu titlul "Carte Valid ISBN"</td><td>listă non-goală, există carte cu titlul "Carte Valid ISBN"</td><td>Passed</td></tr>
<tr><td>11</td><td>GetCartiByIsbn</td><td>GetCartiByIsbn("ISBNINEXISTENT")</td><td>listă goală</td><td>listă goală</td><td>Passed</td></tr>
<tr><td>12</td><td>IsCarteDisponibila</td><td>inserare carte cu disponibilitate "disponibil", idCarte; invocare IsCartedisponibil(idCarte)</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>13</td><td>IsCarteDisponibila</td><td>inserare carte, realizare împrumut, invocare IsCartedisponibil(idCarte)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>14</td><td>IsCarteDisponibila</td><td>IsCartedisponibil(-12345)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>15</td><td>InsertLoan</td><td>inserare carte și abonat, InsertLoan(abonatId, idCarte, "acasă")</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>16</td><td>InsertLoan</td><td>inserare carte cu stare "indisponibil", abonat; apel dublu InsertLoan</td><td>false la al doilea apel</td><td>false la al doilea apel</td><td>Passed</td></tr>
<tr><td>17</td><td>InsertLoan</td><td>InsertLoan(-12345, idCarte, "acasă")</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>18</td><td>InsertLoan</td><td>InsertLoan(-1, -1, "acasă")</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>19</td><td>DeleteUser</td><td>inserare utilizator TestUser2; DeleteUser("TestUser2")</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>20</td><td>DeleteUser</td><td>DeleteUser("User Not Found")</td><td>false și mesaj în consolă "Utilizatorul nu a fost găsit sau a fost deja șters"</td><td>false și mesaj în consolă "Utilizatorul nu a fost găsit sau a fost deja șters"</td><td>Passed</td></tr>
<tr><td>21</td><td>DeleteUser</td><td>inserare utilizator TestAdminDelete; DeleteUser("TestAdminDelete")</td><td>false și mesaj în consolă "Utilizatorul nu a fost găsit sau a fost deja șters"</td><td>false și mesaj în consolă "Utilizatorul nu a fost găsit sau a fost deja șters"</td><td>Passed</td></tr>
<tr><td>22</td><td>DeteleBook</td><td>inserare carte cu id valid; DeleteBook(idCarte)</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>23</td><td>DeleteBook</td><td>DeleteBook(-77777)</td><td>false și mesaj în consolă "Nu a fost găsită nicio carte cu acest id."</td><td>false și mesaj în consolă "Nu a fost găsită nicio carte cu acest id."</td><td>Passed</td></tr>
<tr><td>24</td><td>GetAbonatByPhone</td><td>inserare abonat; GetAbonatByPhone("0700765432")</td><td>obiect Abonat nenul, Nume = "NumeValid"</td><td>obiect Abonat nenul, Nume = "NumeValid"</td><td>Passed</td></tr>
<tr><td>25</td><td>GetAbonatByPhone</td><td>GetAbonatByPhone("TELEFONINEXISTENT")</td><td>aruncă Database.ClientNotFoundException</td><td>aruncă Database.ClientNotFoundException</td><td>Passed</td></tr>
<tr><td>26</td><td>GetAbonatByPhone</td><td>GetAbonatByPhone("")</td><td>aruncă Database.ClientNotFoundException</td><td>aruncă Database.ClientNotFoundException</td><td>Passed</td></tr>
<tr><td>27</td><td>InsertAbonat</td><td>abonat = Abonat("Nume Test", "0700000000", "Strada Test", 12345); InsertAbonat(abonat)</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>28</td><td>InsertAbonat</td><td>abonat = Abonat(null, "0700000000", "Strada Test", 12345); InsertAbonat(abonat)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>29</td><td>GetAllBooks</td><td>apel direct</td><td>returnează listă de obiecte Carte</td><td>returnează listă de obiecte Carte</td><td>Passed</td></tr>
<tr><td>30</td><td>GetBooksByTitle</td><td>GetBooksByTitle("Carte Test")</td><td>listă cu cel puțin o carte cu titlul "Carte Test"</td><td>listă cu cel puțin o carte cu titlul "Carte Test"</td><td>Passed</td></tr>
<tr><td>31</td><td>GetBooksByTitle</td><td>GetBooksByTitle("TitluInexistent")</td><td>listă goală</td><td>listă goală</td><td>Passed</td></tr>
<tr><td>32</td><td>ReturnBook</td><td>inserare împrumut, apoi ReturnBook(idImprumut)</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>33</td><td>ReturnBook</td><td>ReturnBook(-1)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>34</td><td>ReturnBook</td><td>ReturnBook(idImprumutValid) apel dublu</td><td>al doilea apel returnează false</td><td>al doilea apel returnează false</td><td>Passed</td></tr>
<tr><td>35</td><td>ExtendLoan</td><td>inserare împrumut, apoi ExtendLoan(idImprumut)</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>36</td><td>ExtendLoan</td><td>ExtendLoan(-1)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>37</td><td>ExtendLoan</td><td>ExtendLoan(idImprumut extins deja)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>38</td><td>InsertCategorie</td><td>InsertCategorie("Science")</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>39</td><td>InsertCategorie</td><td>InsertCategorie("Science") (apel dublu)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>40</td><td>GetAllCategorii</td><td>apel direct</td><td>listă categorii existente</td><td>listă categorii existente</td><td>Passed</td></tr>
<tr><td>41</td><td>InsertEditura</td><td>InsertEditura("Editura Nouă")</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>42</td><td>InsertEditura</td><td>InsertEditura("Editura Nouă") (apel dublu)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>43</td><td>GetAllEdituri</td><td>apel direct</td><td>listă edituri existente</td><td>listă edituri existente</td><td>Passed</td></tr>
<tr><td>44</td><td>InsertAutor</td><td>InsertAutor("Autor Test")</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>45</td><td>InsertAutor</td><td>InsertAutor("Autor Test") (apel dublu)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>46</td><td>GetAllAutori</td><td>apel direct</td><td>listă autori existenți</td><td>listă autori existenți</td><td>Passed</td></tr>
<tr><td>47</td><td>SearchBooks</td><td>SearchBooks("Carte")</td><td>listă cu cărți care conțin "Carte"</td><td>listă cu cărți care conțin "Carte"</td><td>Passed</td></tr>
<tr><td>48</td><td>SearchBooks</td><td>SearchBooks("inexistent")</td><td>listă goală</td><td>listă goală</td><td>Passed</td></tr>
<tr><td>49</td><td>Login</td><td>Login("TestUser", "parola")</td><td>returnează true</td><td>returnează true</td><td>Passed</td></tr>
<tr><td>50</td><td>Login</td><td>Login("TestUser", "gresit")</td><td>returnează false</td><td>returnează false</td><td>Passed</td></tr>
<tr><td>51</td><td>Login</td><td>Login("Inexistent", "parola")</td><td>returnează false</td><td>returnează false</td><td>Passed</td></tr>
<tr><td>52</td><td>GetUserRole</td><td>GetUserRole("TestUser")</td><td>"bibliotecar" (de ex.)</td><td>"bibliotecar"</td><td>Passed</td></tr>
<tr><td>53</td><td>GetUserRole</td><td>GetUserRole("Inexistent")</td><td>null sau excepție</td><td>null sau excepție</td><td>Passed</td></tr>
<tr><td>54</td><td>UpdateBookStatus</td><td>UpdateBookStatus(idCarte, "indisponibil")</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>55</td><td>UpdateBookStatus</td><td>UpdateBookStatus(-1, "indisponibil")</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>56</td><td>GetLoanHistory</td><td>GetLoanHistory(idAbonat)</td><td>listă cu împrumuturi</td><td>listă cu împrumuturi</td><td>Passed</td></tr>
<tr><td>57</td><td>GetLoanHistory</td><td>GetLoanHistory(-1)</td><td>listă goală</td><td>listă goală</td><td>Passed</td></tr>
<tr><td>58</td><td>GetBooksBorrowedNow</td><td>GetBooksBorrowedNow(idAbonat)</td><td>listă de cărți</td><td>listă de cărți</td><td>Passed</td></tr>
<tr><td>59</td><td>GetBooksBorrowedNow</td><td>GetBooksBorrowedNow(-1)</td><td>listă goală</td><td>listă goală</td><td>Passed</td></tr>
<tr><td>60</td><td>DeleteAbonat</td><td>inserare abonat, apoi DeleteAbonat(id)</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>61</td><td>DeleteAbonat</td><td>DeleteAbonat(-1)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>62</td><td>InsertRole</td><td>InsertRole("test_rol")</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>63</td><td>InsertRole</td><td>InsertRole("test_rol") (apel dublu)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>64</td><td>GetAllRoles</td><td>apel direct</td><td>listă roluri</td><td>listă roluri</td><td>Passed</td></tr>
<tr><td>65</td><td>UpdateAbonat</td><td>UpdateAbonat(idAbonat, "Nume Nou", "0700000001", "Strada Nouă", codPostal)</td><td>true</td><td>true</td><td>Passed</td></tr>
<tr><td>66</td><td>UpdateAbonat</td><td>UpdateAbonat(-1, ...)</td><td>false</td><td>false</td><td>Passed</td></tr>
<tr><td>67</td><td>CountBooks</td><td>apel direct</td><td>return int >= 0</td><td>return int >= 0</td><td>Passed</td></tr>
<tr><td>68</td><td>CountUsers</td><td>apel direct</td><td>return int >= 0</td><td>return int >= 0</td><td>Passed</td></tr>
<tr><td>69</td><td>CountAbonati</td><td>apel direct</td><td>return int >= 0</td><td>return int >= 0</td><td>Passed</td></tr>
</tbody>
</table>

![Teste](/Documentație/7%20Imagini/test1.png)

