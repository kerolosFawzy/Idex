using CustomController;
using System.Collections.Generic;


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

        #region to can bind in view using overview viewModel
        private int _finished;
        public int Finished
        {
            get { return _finished; }
            set { _finished = value;
                RaisePropertyChanged();
            }
        }

        private int _childernCount;
        public int ChildernCount
        {
            get { return _childernCount; }
            set
            {
                _childernCount =value;
                RaisePropertyChanged();
            }
        }

        private IList<Segment> _segments = new List<Segment>();

        public IList<Segment> Segments
        {
            get { return _segments; }
            set
            {
                _segments = value;
                RaisePropertyChanged();
            }
        }

        private string _listViewMode;

        public string ListViewMode
        {
            get { return _listViewMode; }
            set { _listViewMode = value; }
        }

        private string _listViewModeValue;

        public string ListViewModeValue
        {
            get { return _listViewModeValue; }
            set { _listViewModeValue = value; }
        }

 
        #endregion
    }
}