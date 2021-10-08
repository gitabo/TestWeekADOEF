using System;
using System.Collections.Generic;
using System.Text;

namespace TestWeekADOEF.Model
{
    public class Spesa
    {
        public int Id { get; set; }
        public DateTime DataSpesa { get; set; }
        public string Descrizione { get; set; }
        public string Utente { get; set; }
        public decimal Importo { get; set; }
        public bool Approvato { get; set; }
        public Category Category { get; set; }

    }
}
