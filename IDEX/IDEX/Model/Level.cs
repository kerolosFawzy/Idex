using CustomControls;
using ReactiveUI;
using System.Collections.Generic;


namespace IDEX.Model
{
    public class Level : InitialModel
    {
        public int UserId { get; set; }
        public string DoorNumber { get; set; }
        public double Area { get; set; }
        public int LevelType { get; set; }
        public int OwnerId { get; set; }
        public int ControlStatus { get; set; }
        public Level Parent { get; set; }
        public List<Level> Children { get; set; }

        public Insta800InseptionResult insta800InseptionResult { get; set; }
        public List<HygieneInsepectionResult> HygieneInsepectionResults { get; set; }

        #region to can bind in view using overview viewModel
        //Note you can put all this in anthor model and use the second method 
        private int _finished;
        public int Finished
        {
            get => _finished;
            set => this.RaiseAndSetIfChanged(ref _finished, value);
        }

        private int _childernCount;
        public int ChildernCount
        {
            get => _childernCount;
            set => this.RaiseAndSetIfChanged(ref _childernCount, value);
        }
        private bool _completed = false;

        public bool Completed
        {
            get { return _completed; }
            set { _completed = value; }
        }

        private IList<Segment> _segments = new List<Segment>();

        public IList<Segment> Segments
        {
            get => _segments;
            set => this.RaiseAndSetIfChanged(ref _segments, value);
        }

        private string _listViewMode;

        public string ListViewMode
        {
            get => _listViewMode;
            set => this.RaiseAndSetIfChanged(ref _listViewMode, value);
        }

        private string _listViewModeValue;

        public string ListViewModeValue
        {
            get => _listViewModeValue;
            set => this.RaiseAndSetIfChanged(ref _listViewModeValue, value);
        }

 
        #endregion
    }
}