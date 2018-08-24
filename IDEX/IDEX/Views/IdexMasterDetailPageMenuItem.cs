using System;

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