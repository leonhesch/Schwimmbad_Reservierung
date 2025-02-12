using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gruppenreservierungen
{
    public class Reservation
    {
        public string GroupName { get; set; }
        public int GroupSize { get; set; }
        public string Date { get; set; }
        public string Requirements { get; set; }
    }

    public partial class MainWindow : Window
    {
        private const string FilePath = "reservierungen.txt";

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void BtnSaveReservation_Click(object sender, RoutedEventArgs e)
        {
           
        }
        
        private void BtnRefreshReservations_Click(object sender, RoutedEventArgs e)
        {
         
           
        }

    
        private void BtnSearchReservation_Click(object sender, RoutedEventArgs e)
        {
         
            
        }

    }
    }

