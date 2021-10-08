using System;
using System.Collections.Generic;
using System.Text;

namespace TestWeekADOEF.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public IList<Spesa> Spese { get; set; }
    }
}
