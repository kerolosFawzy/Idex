using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDEX.View
{

    public class IdexMasterDetailPageMenuItem
    {
        public IdexMasterDetailPageMenuItem()
        {
            TargetType = typeof(IdexMasterDetailPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}