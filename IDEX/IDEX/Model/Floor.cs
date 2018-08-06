using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.Model
{
    class Floor : Level
    {
        private int _levelType;

        public new int LevelType
        {
            get { return _levelType; }
            set { _levelType = 11; }
        }

        private Building _parent;

        public new Building Parent
        {
            get { return _parent; }
            set { _parent = null; }
        }

        private List<Room> _children;

        public new List<Room> Children
        {
            get { return _children; }
            set { _children = value; }
        }
    }
}
