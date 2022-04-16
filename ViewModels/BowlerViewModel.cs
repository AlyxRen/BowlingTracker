using BowlingTracker.Models;
using BowlingTracker.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingTracker.ViewModels
{
    internal class BowlerViewModel : ViewModelBase
    {
        private readonly Bowler _bowler;
        private readonly ObservableCollection<FrameViewModel> _frames;

        public IEnumerable<FrameViewModel> Frames => _frames;
        public int Score
        { get {  return _bowler.Score(10); } }

        public BowlerViewModel(Bowler bowler)
        {
            _bowler = bowler;
            _frames = new ObservableCollection<FrameViewModel>();
            foreach (Frame frame in _bowler.Frames)
            {
                _frames.Add(new FrameViewModel(frame, this));
            }
        }

        public void UpdateScores()
        {
            foreach(FrameViewModel f in Frames)
            {
                f.UpdateScore();
            }
        }
        public void Reset()
        {
            foreach (var frame in Frames)
            {
                frame.Reset();
            }
            _bowler.ResetScore();
            NotifyPropertyChanged("Bowlers");
        }
    }
}
