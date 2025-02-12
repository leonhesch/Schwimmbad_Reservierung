using System.Windows;

namespace Gruppenreservierungen
{
    public partial class MainWindow : Window
    {
        private DatabaseManager dbManager = new DatabaseManager();

        public MainWindow()
        {
            InitializeComponent();
            dbManager.InitializeDatabase();
        }

        private void BtnSaveReservation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reservieren-Funktion noch nicht implementiert.");
        }

        private void BtnRefreshReservations_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aktualisieren-Funktion noch nicht implementiert.");
        }

        private void BtnSearchReservation_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Such-Funktion noch nicht implementiert.");
        }
    }
}
