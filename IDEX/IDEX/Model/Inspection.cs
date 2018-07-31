using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.Model
{
    class Inspection : BaseModel
    {
        public string Name { get; set; }
        public Scheme scheme { get; set; }
        public int SchemeId { get; set; }
    }
}
