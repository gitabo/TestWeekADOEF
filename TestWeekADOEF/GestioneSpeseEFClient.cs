using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestWeekADOEF.EF;
using TestWeekADOEF.Lib;
using TestWeekADOEF.Model;

namespace TestWeekADOEF
{
    public class GestioneSpeseEFClient
    {

        public static void AggiungiSpesa()
        {

            using GestioneSpeseContext ctx = new GestioneSpeseContext();

            Console.WriteLine("---- Inserire una nuova spesa ----");

            string descrizione = ConsoleHelpers.GetData("Descrizione");
            string utente = ConsoleHelpers.GetData("Utente");
            string approvatoStr = ConsoleHelpers.GetData("Approvata (s/n)");
            bool approvato = false;
            if (approvatoStr.Equals("s"))
                approvato = true;
            string categoriaStr = ConsoleHelpers.GetData("Categoria spesa: 1 (Viaggio), 2 (Cibo), 3 (Albergo)");
            int.TryParse(categoriaStr, out int categoria);
            Category c = ctx.Categories.Find(categoria);
            string importoStr = ConsoleHelpers.GetData("Importo");
            decimal.TryParse(importoStr, out decimal importo);
            DateTime dataSpesa = DateTime.Now;

            Spesa spesa = new Spesa()
            {
                Descrizione = descrizione,
                Utente = utente,
                Approvato = approvato,
                DataSpesa = dataSpesa,
                Importo = importo,
                Category = c
            };

            ctx.Spese.Add(spesa);
            ctx.SaveChanges();

            Console.WriteLine("---- Premi un tasto ----");
            Console.ReadKey();

        }


        public static void ApprovaCancellaModificaSpesa(int flag)
        {
            using GestioneSpeseContext ctx = new GestioneSpeseContext();

            if(flag == 0)
                Console.WriteLine("---- Approva spesa ----");
            else if (flag == 1 )
                Console.WriteLine("---- Cancella spesa ----");
            else
                Console.WriteLine("---- Modifica spesa ----");

            foreach (Spesa s in ctx.Spese.Include(c => c.Category))
                Console.WriteLine($"[{s.Id}] {s.Descrizione}  Utente: {s.Utente} Importo: " +
                    $"{s.Importo} Approvata: {s.Approvato} {s.DataSpesa.ToShortDateString()}" +
                    $" Categoria: {s.Category.Categoria}");
            
            string idStr = ConsoleHelpers.GetData("ID Spesa");
            int.TryParse(idStr, out int id);

            Spesa spesa = ctx.Spese.Find(id);

            if (flag == 0)
                spesa.Approvato = true;
            else if (flag == 1)
                ctx.Remove(spesa);
            else
            {
                string descrizione = ConsoleHelpers.GetData("Descrizione");
                string utente = ConsoleHelpers.GetData("Utente");
                string categoriaStr = ConsoleHelpers.GetData("Categoria spesa: 1 (Viaggio), 2 (Cibo), 3 (Albergo)");
                int.TryParse(categoriaStr, out int categoria);
                Category c = ctx.Categories.Find(categoria);
                string approvatoStr = ConsoleHelpers.GetData("Approvata (s/n)");
                bool approvato = false;
                if (approvatoStr.Equals("s"))
                    approvato = true;
                string importoStr = ConsoleHelpers.GetData("Importo");
                decimal.TryParse(importoStr, out decimal importo);

                spesa.Descrizione = descrizione;
                spesa.Utente = utente;
                spesa.Approvato = approvato;
                spesa.Importo = importo;
                spesa.Category = c;

            }

            ctx.SaveChanges();

            Console.WriteLine("---- Premi un tasto ----");
            Console.ReadKey();

        }

        public static void SpeseApprovate()
        {
            using GestioneSpeseContext ctx = new GestioneSpeseContext();

            Console.WriteLine("---- Spese approvate ----");

            var spese = ctx.Spese.Include(c => c.Category).Where(s => s.Approvato == true);
            foreach (Spesa s in spese)
                Console.WriteLine($"[{s.Id}] {s.Descrizione}  Utente: {s.Utente} Importo: " +
                    $"{s.Importo} Approvata: {s.Approvato} {s.DataSpesa.ToShortDateString()}" +
                    $" Categoria: {s.Category.Categoria}");

        }

        public static void SpesePerUtente()
        {
            using GestioneSpeseContext ctx = new GestioneSpeseContext();
            
            string utente = ConsoleHelpers.GetData("Utente");
            Console.WriteLine("---- Spese di " + utente + " ----");

            var spese = ctx.Spese.Include(c => c.Category).Where(s => s.Utente == utente);
            foreach (Spesa s in spese)
                Console.WriteLine($"[{s.Id}] {s.Descrizione} {s.Utente} Categoria: {s.Category.Categoria}");

        }


        public static void TotaleSpesePerCategoria()
        {

            using GestioneSpeseContext ctx = new GestioneSpeseContext();

            Console.WriteLine("---- Totale spese per categoria ----");

            var results = ctx.Spese.Include(c => c.Category).GroupBy(
                s => new { s.Category.Id, s.Category.Categoria },
                (key, grp) => new
                {
                    CategoryID = key.Id,
                    Categoria = key.Categoria,
                    NumSpesePerCategoria = grp.Count()
                }
                );

            foreach (var s in results)
            {
                Console.WriteLine($"[{s.CategoryID}] {s.Categoria} Numero spese: {s.NumSpesePerCategoria}");
            }
        }
      

    }
}
