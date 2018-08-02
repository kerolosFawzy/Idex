﻿using System.Collections.Generic;
using System.ComponentModel;

namespace IDEX.Model
{
    class Scheme : BaseModel , INotifyPropertyChanged
    {
        public Customer customer { get; set; }
        public int CustomerId { get; set; }
        public List<Inspection> Inspections { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}