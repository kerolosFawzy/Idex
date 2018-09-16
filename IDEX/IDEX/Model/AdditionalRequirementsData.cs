using IDEX.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.ViewModel
{
    public class AdditionalRequirementsData : BaseViewModel
    {
        public string SelectedItem { get; set; }
        public List<AdditionalRequirementsCheckBox> checkBoxesList { get; set; } 
            = new List<AdditionalRequirementsCheckBox>();    }
}
