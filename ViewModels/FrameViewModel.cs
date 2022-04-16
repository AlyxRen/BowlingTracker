using BowlingTracker.Models;
using BowlingTracker.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingTracker.ViewModels
{
    internal class FrameViewModel : ViewModelBase
    {
        private readonly Frame _frame;
        private readonly BowlerViewModel _parent;

        public string FrameNumber => $"Frame {_frame.FrameSet.ToString()}";
        public string FirstShot
        {
            get
            {
                return _frame.FirstShot;
            }
            set
            {
                try
                {
                    _frame.FirstShot = value;
                }
                catch (ArgumentOutOfRangeException e)
                { _frame.FirstShot = ""; }
                NotifyPropertyChanged(nameof(this.FirstShot));
                NotifyPropertyChanged(nameof(this.SecondShot));
                NotifyPropertyChanged(nameof(this.ThirdShot));

                _parent.UpdateScores();
            }
        }
        public bool FirstShotFocus
        {
            get { return _frame.FrameSet == 1? true : false; }
        }
        public string SecondShot
        {
            get
            {
                return _frame.SecondShot;
            }
            set
            {
                try
                {
                    _frame.SecondShot = value;
                }
                catch (ArgumentOutOfRangeException e)
                { _frame.SecondShot = ""; }
                NotifyPropertyChanged(nameof(this.FirstShot));
                NotifyPropertyChanged(nameof(this.SecondShot));
                NotifyPropertyChanged(nameof(this.ThirdShot));
                _parent.UpdateScores();
            }
        }
        public bool SecondShotFocus
        {
            get { return false; }
        }
        public string ThirdShot
        {
            get
            {
                return _frame.ThirdShot;
            }
            set
            {
                try
                {
                    _frame.ThirdShot = value;
                } catch (ArgumentOutOfRangeException e)
                { _frame.ThirdShot = ""; }
                NotifyPropertyChanged(nameof(this.FirstShot));
                NotifyPropertyChanged(nameof(this.SecondShot));
                NotifyPropertyChanged(nameof(this.ThirdShot));
                _parent.UpdateScores();
            }
        }
        public bool ThirdShotFocus
        {
            get { return false; }
        }
        public bool IsLastFrame
        {
            get
            {
                return _frame.FrameSet == 10 ? true : false;
            }
        }
        public int Score
            { get { return _frame.Score; } }

        public string FocusTarget
        {
            get { return ""; }
        }

        public FrameViewModel(Frame frame, BowlerViewModel parent)
        {
            _frame = frame;
            _parent = parent;
        }
        public void UpdateScore()
        {
            NotifyPropertyChanged(nameof(this.Score));
        }
        public void Reset()
        {
            FirstShot = "";
            SecondShot = "";
            if (IsLastFrame)
            { ThirdShot = ""; }

            NotifyPropertyChanged(nameof(this.Score));
        }
    }
}
