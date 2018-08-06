using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.Model
{
    class CustomerBuilding : Level 
    {
        private int _levelType;

        public new int LevelType
        {
            get { return _levelType; }
            set { _levelType = 5; }
        }

        private Level _parent;

        public new Level Parent
        {
            get { return _parent; }
            set { _parent = null; }
        }

        private List<Building> _children;

        public new List<Building> Children
        {
            get { return _children;  }
            set { _children = value; }
        }
        
    }
}
