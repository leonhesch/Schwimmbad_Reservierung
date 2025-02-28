Titel: Reservierungssystem f�r Gruppen

Autor: Leon Hesch

---

Kurze Beschreibung

Dieses Programm erm�glicht die Verwaltung von Reservierungen f�r gr��ere Gruppen, zum Beispiel Schulklassen oder Vereine. Es speichert Informationen wie Gruppennamen, Gruppengr��e, Reservierungsdatum und besondere Anforderungen in einer Datenbank und bietet folgende Hauptfunktionen:

- Neue Reservierungen erstellen  
- Reservierungen anzeigen und aktualisieren  
- Reservierungen nach Gruppennamen durchsuchen  

---

Nutzung des Programms

1. Start des Programms
   - Lade das Repository herunter oder klone es.  
   - Stelle sicher, dass die Datenbankverbindung (LocalDB) funktioniert.  
   - �ffne die Solution in Visual Studio und f�hre das Projekt aus.

2. Neue Reservierung anlegen  
   - Start in dem Tab �Neue Reservierung�.  
   - F�lle die Felder f�r den Gruppennamen, die Gruppengr��e, das Reservierungsdatum und ggf. besondere Anforderungen aus.  
   - Klicke auf �Reservierung speichern�. Das Programm speichert die Daten in der Datenbank und leert die Eingabefelder.

3. Reservierungen anzeigen  
   - Gehe in den Tab �Reservierungen anzeigen�.  
   - Klicke auf �Aktualisieren�, um alle gespeicherten Reservierungen zu laden.  
   - Die Daten erscheinen im DataGrid.

4. Reservierungen suchen  
   - Wechsle in den Tab �Reservierung suchen�.  
   - Gib im Feld �Gruppenname� den Suchbegriff (oder Teilbegriffe) ein.  
   - Klicke auf �Suchen�. Die gefundenen Reservierungen werden darunter im Textblock angezeigt.

---

![Reservierung erstellen](Dokumentation/Screenshots/Neue_Reservierung.png)
![Reservierung anzeigen](Dokumentation/Screenshots/Reservierung_Anzeigen.png)
![Reservierung suchen](Dokumentation/Screenshots/Reservierung_Suchen.png)

