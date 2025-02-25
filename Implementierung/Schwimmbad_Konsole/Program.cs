using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        while (true) 
        {
            Console.WriteLine("\nWillkommen zum Reservierungssystem für Gruppen!");
            Console.WriteLine("Optionen:");
            Console.WriteLine("=================================================");
            Console.WriteLine("1. Neue Reservierung erstellen");
            Console.WriteLine("=================================================");
            Console.WriteLine("2. Reservierungen anzeigen");
            Console.WriteLine("=================================================");
            Console.WriteLine("3. Reservierung suchen");
            Console.WriteLine("=================================================");
            Console.WriteLine("4. Programm beenden");
            Console.WriteLine("=================================================");
            Console.Write("Bitte wählen Sie eine Option (1/2/3/4): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                CreateReservation();
            }
            else if (choice == "2")
            {
                ShowReservations();
            }
            else if (choice == "3")
            {
                SearchReservation();
            }
            else if (choice == "4")
            {
                Console.WriteLine("Programm wird beendet. Auf Wiedersehen!");
                break; 
            }
            else
            {
                Console.WriteLine("Ungültige Auswahl. Bitte erneut versuchen.");
            }
        }
    }

    static void CreateReservation()
    {
        Console.WriteLine("\nNeue Reservierung");
        Console.Write("Name der Gruppe: ");
        string groupName = Console.ReadLine();

        Console.Write("Gruppengröße: ");
        if (!int.TryParse(Console.ReadLine(), out int groupSize))
        {
            Console.WriteLine("Ungültige Eingabe für die Gruppengröße. Reservierung abgebrochen.");
            return;
        }

        Console.Write("Reservierungsdatum (YYYY-MM-DD): ");
        string date = Console.ReadLine();

        Console.Write("Besondere Anforderungen: ");
        string requirements = Console.ReadLine();

        string reservation = $"{groupName},{groupSize},{date},{requirements}";

        try
        {
            File.AppendAllText("reservierungen.txt", reservation + Environment.NewLine);
            Console.WriteLine("Reservierung erfolgreich gespeichert!\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Speichern der Reservierung: {ex.Message}");
        }
    }

    static void ShowReservations()
    {
        Console.WriteLine("\nReservierungen:");

        if (File.Exists("reservierungen.txt"))
        {
            string[] reservations = File.ReadAllLines("reservierungen.txt");
            if (reservations.Length == 0)
            {
                Console.WriteLine("Keine Reservierungen gefunden.\n");
            }
            else
            {
                foreach (string reservation in reservations)
                {
                    string[] data = reservation.Split(',');
                    if (data.Length == 4)
                    {
                        Console.WriteLine($"Gruppe: {data[0]}\n Größe: {data[1]}\n Datum: {data[2]}\n Anforderungen: {data[3]}");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Keine Reservierungen gefunden.\n");
        }
    }

    static void SearchReservation()
    {
        Console.Write("\nBitte geben Sie den Gruppennamen ein, nach dem gesucht werden soll: ");
        string searchName = Console.ReadLine();

        if (File.Exists("reservierungen.txt"))
        {
            string[] reservations = File.ReadAllLines("reservierungen.txt");
            bool found = false;

            foreach (string reservation in reservations)
            {
                string[] data = reservation.Split(',');
                if (data.Length >= 1 && data[0].Equals(searchName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Gruppe: {data[0]}\n Größe: {data[1]}\n Datum: {data[2]}\n Anforderungen: {data[3]}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine($"Keine Reservierung für die Gruppe '{searchName}' gefunden.\n");
            }
        }
        else
        {
            Console.WriteLine("Keine Reservierungen gefunden. Die Datei existiert nicht.\n");
        }
    }
}
