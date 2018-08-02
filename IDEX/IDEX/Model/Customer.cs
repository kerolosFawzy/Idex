using CustomController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace IDEX.Model
{
    class Customer : BaseModel
    {
        public List<Scheme> Schemes { get; set; }

    }
}
