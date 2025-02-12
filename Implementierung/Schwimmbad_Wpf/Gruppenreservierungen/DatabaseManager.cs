using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Gruppenreservierungen
{
    public class DatabaseManager
    {
        public string ConnectionString { get; } = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=GruppenreservierungenDB;Integrated Security=True;";
        private string masterConnectionString { get; } = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";

        public void CreateDatabaseIfNotExists()
        {
            using (var connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();
                string checkDbQuery = @"
                    IF DB_ID('GruppenreservierungenDB') IS NULL
                    BEGIN
                        CREATE DATABASE GruppenreservierungenDB
                    END";
                using (var command = new SqlCommand(checkDbQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InitializeDatabase()
        {
            CreateDatabaseIfNotExists();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();


                string createGroupsTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Groups')
                    BEGIN
                        CREATE TABLE Groups (
                            GroupId INT IDENTITY(1,1) PRIMARY KEY,
                            GroupName NVARCHAR(100) NOT NULL,
                            GroupSize INT NOT NULL,
                            GroupType NVARCHAR(50) NOT NULL
                        )
                    END";
                using (var command = new SqlCommand(createGroupsTable, connection))
                {
                    command.ExecuteNonQuery();
                }


                string createReservationsTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Reservations')
                    BEGIN
                        CREATE TABLE Reservations (
                            ReservationId INT IDENTITY(1,1) PRIMARY KEY,
                            GroupId INT NOT NULL,
                            ReservationDate DATE NOT NULL,
                            CreatedAt DATETIME NOT NULL DEFAULT(GETDATE()),
                            CONSTRAINT FK_Reservations_Groups FOREIGN KEY (GroupId) REFERENCES Groups(GroupId)
                        )
                    END";
                using (var command = new SqlCommand(createReservationsTable, connection))
                {
                    command.ExecuteNonQuery();
                }


                string createRequirementsTable = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Requirements')
                    BEGIN
                        CREATE TABLE Requirements (
                            RequirementId INT IDENTITY(1,1) PRIMARY KEY,
                            ReservationId INT NOT NULL,
                            RequirementText NVARCHAR(MAX) NOT NULL,
                            CONSTRAINT FK_Requirements_Reservations FOREIGN KEY (ReservationId) REFERENCES Reservations(ReservationId)
                        )
                    END";
                using (var command = new SqlCommand(createRequirementsTable, connection))
                {
                    command.ExecuteNonQuery();
                }


                string countQuery = "SELECT COUNT(*) FROM Groups";
                using (var command = new SqlCommand(countQuery, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    if (count == 0)
                    {
                        InsertSampleData(connection);
                    }
                }
            }
        }

        private void InsertSampleData(SqlConnection connection)
        {
            var samples = new[]
            {
                new { GroupName = "Schulklasse A", GroupSize = 30, GroupType = "Schulklasse", ReservationDate = DateTime.Now.AddDays(1), RequirementText = "30 Schwimmreifen" },
                new { GroupName = "Verein B", GroupSize = 20, GroupType = "Verein", ReservationDate = DateTime.Now.AddDays(2), RequirementText = "Sauna-Aufenthalt" },
                new { GroupName = "Gruppe C", GroupSize = 15, GroupType = "Gruppe", ReservationDate = DateTime.Now.AddDays(3), RequirementText = "Wassersport" }
            };

            foreach (var sample in samples)
            {

                string groupInsertQuery = @"
                    INSERT INTO Groups (GroupName, GroupSize, GroupType)
                    VALUES (@GroupName, @GroupSize, @GroupType);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
                int groupId;
                using (var command = new SqlCommand(groupInsertQuery, connection))
                {
                    command.Parameters.AddWithValue("@GroupName", sample.GroupName);
                    command.Parameters.AddWithValue("@GroupSize", sample.GroupSize);
                    command.Parameters.AddWithValue("@GroupType", sample.GroupType);
                    groupId = (int)command.ExecuteScalar();
                }


                string reservationInsertQuery = @"
                    INSERT INTO Reservations (GroupId, ReservationDate)
                    VALUES (@GroupId, @ReservationDate);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";
                int reservationId;
                using (var command = new SqlCommand(reservationInsertQuery, connection))
                {
                    command.Parameters.AddWithValue("@GroupId", groupId);
                    command.Parameters.AddWithValue("@ReservationDate", sample.ReservationDate.ToString("yyyy-MM-dd"));
                    reservationId = (int)command.ExecuteScalar();
                }


                string requirementInsertQuery = @"
                    INSERT INTO Requirements (ReservationId, RequirementText)
                    VALUES (@ReservationId, @RequirementText);";
                using (var command = new SqlCommand(requirementInsertQuery, connection))
                {
                    command.Parameters.AddWithValue("@ReservationId", reservationId);
                    command.Parameters.AddWithValue("@RequirementText", sample.RequirementText);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
