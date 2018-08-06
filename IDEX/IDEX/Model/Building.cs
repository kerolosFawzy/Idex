using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.Model
{
    class Building : Level 
    {
        private int _levelType;

        public new int LevelType
        {
            get { return _levelType; }
            set { _levelType = 8; }
        }

        private CustomerBuilding _parent;

        public new CustomerBuilding Parent
        {
            get { return _parent; }
            set { _parent = null; }
        }

        private List<Floor> _children;

        public new List<Floor> Children
        {
            get { return _children; }
            set { _children = value; }
        }
    }
}
