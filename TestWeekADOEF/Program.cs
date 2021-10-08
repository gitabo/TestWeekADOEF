using System;
using System.Collections.Generic;
using TestWeekADOEF.Lib;

namespace TestWeekADOEF
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("=== Test Week ADOEF ===");

            #region Main loop

            bool quit = false;
            do
            {
                string command = ConsoleHelpers.BuildMenu("Main Menu",
                    new List<string> {
                        "[ 1 ] - Aggiungi spesa",
                        "[ 2 ] - Approva spesa",
                        "[ 3 ] - Cancella spesa",
                        "[ 4 ] - Modifica spesa",
                        "[ 5 ] - Elenco spese approvate",
                        "[ 6 ] - Elenco spese di un utente",
                        "[ 7 ] - Numero spese per categoria",
                        "[ 8 ] - Elenco spese (ADO.NET)",
                        "[ 9 ] - Cancella spesa (ADO.NET)",
                        "[ q ] - QUIT"
                    });

                switch (command)
                {
                    case "1":
                        // aggiungi spesa
                        GestioneSpeseEFClient.AggiungiSpesa();
                        break;
                    case "2":
                        // approva spesa
                        GestioneSpeseEFClient.ApprovaCancellaModificaSpesa(0);
                        break;
                    case "3":
                        // cancella spesa
                        GestioneSpeseEFClient.ApprovaCancellaModificaSpesa(1);
                        break;
                    case "4":
                        // modifica spesa
                        GestioneSpeseEFClient.ApprovaCancellaModificaSpesa(2);
                        break;
                    case "5":
                        // elenco spese approvate
                        GestioneSpeseEFClient.SpeseApprovate();
                        break;
                    case "6":
                        // elenco spese per utente
                        GestioneSpeseEFClient.SpesePerUtente();
                        break;
                    case "7":
                        // numero spese per categoria
                        GestioneSpeseEFClient.TotaleSpesePerCategoria();
                        break;
                    case "8":
                        // Elenco spese ADO.NET
                        DisconnectedModeClient.ElencoSpese();
                        break;
                    case "9":
                        // Elenco spese ADO.NET
                        DisconnectedModeClient.DeleteSpesa();
                        break;
                    case "q":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Comando sconosciuto.");
                        break;
                }

            } while (!quit);

            #endregion
        }
    }
}
