Testfall 1

ID: T01  
Beschreibung: Erstellen einer neuen Reservierung mit gültigen Eingaben.

Vorbedingungen:  
- Das Programm ist gestartet und das Fenster „Neue Reservierung“ ist geöffnet.  

Test-Schritte:
1. Im Feld „Name der Gruppe“ wird „Schulklasse 7b“ eingetragen.  
2. Im Feld „Gruppengröße“ wird „23“ eingetragen.  
3. Im DatePicker „Reservierungsdatum“ wird das Datum 01.03.2025 ausgewählt.
4. Im Feld „Besondere Anforderungen“ wird „Schwimmflügel“ eingetragen.  
5. Auf den Button „Reservierung speichern“ klicken.

Erwartetes Resultat:
- Eine Meldung „Reservierung erfolgreich gespeichert.“ wird angezeigt.  
- Die Eingabefelder werden geleert.  
- In der Datenbank im Programm ist nun eine neue Reservierung für „Schulklasse 7b“ vorhanden.

-----------------------------------------------------------------------------------------------------------------------------

Testfall 2

ID: T02  
Beschreibung: Suche nach einer bestehenden Reservierung über den Gruppennamen.

Vorbedingungen: 
- Das Programm ist gestartet.  
- Im Tab „Reservierung suchen“ ist mindestens eine Reservierung in der Datenbank, deren Gruppenname „Schulklasse“ enthält.

Test-Schritte:
1. Wechsle in den Tab „Reservierung suchen“.  
2. Gib im Feld „Gruppenname“ den Text „Schulklasse“ ein.  
3. Klicke auf den Button „Suchen“.  

Erwartetes Resultat:
- Im Textfeld unterhalb der Schaltfläche „Suchen“ werden alle gefundenen Reservierungen gelistet, deren Gruppenname „Schulklasse“ enthält.  
- Jede Reservierung zeigt den Gruppennamen, die Gruppengröße, das Datum und die Anforderungen.