
namespace IDEX.Model
{
    public class HygieneInsepectionResult : BaseModel
    {
        public string CategoryName { get; set; }
        public int EasyDust { get; set; }
        public int HardDust { get; set; }
        public int EasyWast { get; set; }
        public int HardWast { get; set; }
        public int HardHumBio { get; set; }

    }
}
