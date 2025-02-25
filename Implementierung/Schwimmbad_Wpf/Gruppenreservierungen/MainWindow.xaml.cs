﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.SqlClient;

namespace Gruppenreservierungen
{
    public partial class MainWindow : Window
    {
        private DatabaseManager dbManager = new DatabaseManager();

        public MainWindow()
        {
            InitializeComponent();
            dbManager.InitializeDatabase();
            UpdateBlackoutDatesFromDatabase();
        }

        private void dpReservationDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpReservationDate.SelectedDate.HasValue)
            {
                DateTime selected = dpReservationDate.SelectedDate.Value.Date;
                foreach (CalendarDateRange range in dpReservationDate.BlackoutDates)
                {
                    if (selected >= range.Start && selected <= range.End)
                    {
                        MessageBox.Show("Dieser Tag ist bereits reserviert. Bitte wählen Sie einen anderen Termin.");
                        dpReservationDate.SelectedDate = null;
                        break;
                    }
                }
            }
        }

        private List<DateTime> GetReservedDatesFromDatabase()
        {
            List<DateTime> reservedDates = new List<DateTime>();

            using (var connection = new SqlConnection(dbManager.ConnectionString))
            {
                connection.Open();
                string query = "SELECT ReservationDate FROM Reservations";
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reservedDates.Add(reader.GetDateTime(0).Date);
                    }
                }
            }
            return reservedDates;
        }

        private void UpdateBlackoutDatesFromDatabase()
        {
            List<DateTime> reservedDates = GetReservedDatesFromDatabase();
            dpReservationDate.BlackoutDates.Clear();
            foreach (DateTime date in reservedDates)
            {
                dpReservationDate.BlackoutDates.Add(new CalendarDateRange(date));
            }
        }

        private void BtnSaveReservation_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGroupName.Text))
            {
                MessageBox.Show("Bitte geben Sie einen Gruppennamen ein.");
                return;
            }
            if (!int.TryParse(txtGroupSize.Text, out int groupSize))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Gruppengröße ein.");
                return;
            }
            if (!dpReservationDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Bitte wählen Sie ein Reservierungsdatum aus.");
                return;
            }
            DateTime reservationDate = dpReservationDate.SelectedDate.Value;
            string requirements = txtRequirements.Text;

            try
            {
                dbManager.SaveReservation(txtGroupName.Text, groupSize, reservationDate, requirements);

                MessageBox.Show("Reservierung erfolgreich gespeichert.");

                txtGroupName.Clear();
                txtGroupSize.Clear();
                dpReservationDate.SelectedDate = null;
                txtRequirements.Clear();

                UpdateBlackoutDatesFromDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Speichern der Reservierung: " + ex.Message);
            }
        }

        private void BtnRefreshReservations_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aktualisieren-Funktion ist noch nicht implementiert.");
        }

        private void BtnSearchReservation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Such-Funktion ist noch nicht implementiert.");
        }
    }
}
