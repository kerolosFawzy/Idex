using IDEX.Model;
using System.Collections.Generic;

namespace IDEX.ViewModel
{
    //this for make sure data save onDisAppearing in ViewModel 
    public class AdditionalRequirementsData : BaseModel
    {
        public string SmallRangeValue { get; set; }
        public double LargeRangeValue { get; set; }
        public string SelectedItem { get; set; }
        public List<AdditionalRequirementsCheckBox> checkBoxesList { get; set; }
            = new List<AdditionalRequirementsCheckBox>();

    }
}
