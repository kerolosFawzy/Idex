using System;
using System.Collections.Generic;
using System.Text;

namespace IDEX.Model
{
    class Room : Level
    {
        private int _levelType;

        public new int LevelType
        {
            get { return _levelType; }
            set { _levelType = 14; }
        }

        private Floor _parent;

        public new Floor Parent
        {
            get { return _parent; }
            set { _parent = null; }
        }

        private List<Level> _children;

        public new List<Level> Children
        {
            get { return _children; }
            set { _children = null; }
        }
    }
}
