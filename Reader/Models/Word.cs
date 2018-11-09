using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reader.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Spelling { get; set; }
        public string Meaning { get; set; }
        public string Text { get; set; }
    }
}
