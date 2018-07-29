using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.Model
{
    class Scheme
    {
        public int ID { get; set; }
        public string SchemeName { get; set; }
        public Customer customer { get; set; }
        public int CustomerId { get; set; }
        public List<Inspection> Inspections { get; set; }
    }
}
