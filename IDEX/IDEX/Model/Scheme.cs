using System.Collections.Generic;

namespace IDEX.Model
{
    public class Scheme : InitialModel
    {
        public Customer customer { get; set; }
        public int CustomerId { get; set; }
        public List<Inspection> Inspections { get; set; }

    }
}
