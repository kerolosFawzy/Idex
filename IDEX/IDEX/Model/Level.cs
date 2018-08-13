using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using static CustomController.CirclePieChart;

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
        // to know how many childern are Finished
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
                _childernCount = Children.Count();
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
    }
}