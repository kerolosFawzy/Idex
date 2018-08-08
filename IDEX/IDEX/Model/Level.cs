using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.Model
{
    class Level : BaseModel
    {
        public int UserId { get; set; }
        public string DoorNumber { get; set; }
        public double Area { get; set; }
        public int LevelType { get; set; }
        public int OwnerId { get; set; }
        public int ControlStatus { get; set; }
        public Level Parent { get; set; }
        public List<Level> Children { get; set; }
    }
}