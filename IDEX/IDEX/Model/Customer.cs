using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.Model
{
    class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Scheme> Schemes { get; set; }
    }
}
