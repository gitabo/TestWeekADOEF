using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using TestWeekADOEF.Lib;

namespace TestWeekADOEF
{
    public class DisconnectedModeClient
    {

        static IConfigurationRoot Config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
        static string ConnectionString = Config.GetConnectionString("AcademyG");


        private static SqlConnection connection;
        private static DataSet speseDs = new DataSet();
        private static SqlDataAdapter speseAdapter = new SqlDataAdapter();

        private static SqlCommand speseSelectCmd;
        private static SqlCommand speseDeleteCmd;

        static DisconnectedModeClient()
        {
            connection = new SqlConnection(ConnectionString);

            try
            {

                #region Select Command

                speseDs = new DataSet();
                speseAdapter = new SqlDataAdapter();

                speseSelectCmd = new SqlCommand("SELECT * FROM Spese ORDER BY DataSpesa DESC", connection);
                speseAdapter.SelectCommand = speseSelectCmd;

                #endregion

                #region Delete Command

                speseDeleteCmd = connection.CreateCommand();
                speseDeleteCmd.CommandType = System.Data.CommandType.Text;
                speseDeleteCmd.CommandText = "DELETE FROM Spese WHERE Id = @id";

                speseDeleteCmd.Parameters.Add(
                    new SqlParameter(
                        "@id",
                        SqlDbType.Int,
                        100,
                        "Id"
                    )
                );

                speseAdapter.DeleteCommand = speseDeleteCmd;

                #endregion

                speseAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                speseAdapter.Fill(speseDs, "Spese");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DisconnectedModeClient] Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

        }


        public static void ElencoSpese()
        {
            foreach (DataRow dataRow in speseDs.Tables["Spese"].Rows)
            {
                
                Console.WriteLine($"{ dataRow["Id"]} { dataRow["Descrizione"]} { dataRow["Utente"]} " +
                    $"Approvata: { dataRow["Approvato"]} Importo: { dataRow["Importo"]} {(DateTime)dataRow["DataSpesa"]}");
            }
        }


        public static void DeleteSpesa()
        {
            Console.WriteLine("---- Cancellare una spesa ----");


            string idValue = ConsoleHelpers.GetData("ID della spesa da cancellare");

            DataRow rowToBeDeleted = speseDs.Tables["Spese"].Rows.Find(int.Parse(idValue));
            // marco la riga come cancellata
            rowToBeDeleted?.Delete();

            // update db
            speseAdapter.Update(speseDs, "Spese");
            // refresh ds
            speseDs.Reset();
            speseAdapter.Fill(speseDs, "Tickets");

            Console.WriteLine("---- Premi un tasto ----");
            Console.ReadKey();
        }

    }
}
