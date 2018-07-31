using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.Model
{
    class Scheme : BaseModel
    {
        public string SchemeName { get; set; }
        public Customer customer { get; set; }
        public int CustomerId { get; set; }
        public List<Inspection> Inspections { get; set; }
    }
}
