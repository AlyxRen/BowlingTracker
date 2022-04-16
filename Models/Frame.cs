using BowlingTracker.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace BowlingTracker.Models
{
    internal class Frame
    {
        public Bowler Parent;
        public int FrameSet;
        public int ShotCount;
        public char[] Shots { get; set; }
        public string FirstShot
        {
            get
            {
                if (Shots[0] == ' ')
                {
                    return "";
                } else
                {
                    return Shots[0].ToString();
                }
            }
            set
            {
                Shots[0] = _validateInput(value);
                _UpdateFirstAndSecondShot();
                _UpdateSecondAndThirdShot();
            }
        }
        public string SecondShot
        {
            get
            {
                if (Shots[1] == ' ')
                {
                    return "";
                } else
                {
                    return Shots[1].ToString();
                }
            }
            set
            {
                
                Shots[1] = _validateInput(value);
                _UpdateFirstAndSecondShot();
                _UpdateSecondAndThirdShot();
            }
        }
        public string ThirdShot
        {
            get
            {
                if (ShotCount < 3)
                {
                    return "";
                } else if(Shots[2] == ' ')
                {
                    return "";
                }
                else
                {
                    return Shots[2].ToString();
                }
            }
            set
            {
                if (Shots.Length > 1)
                {
                    Shots[2] = _validateInput(value);
                    _UpdateFirstAndSecondShot();
                    _UpdateSecondAndThirdShot();
                }
            }
        }

        public int Score
        {
            get { return Parent.Score(FrameSet); }
        }

        public Frame(Bowler parent, int frameSet, int shotCount)
        {
            Parent = parent;
            FrameSet = frameSet;
            ShotCount = shotCount;

            Shots = new char[shotCount];
            Reset();
        }

        private char _validateInput(string input)
        {
            char c;
            if (input.Length == 0)
            { return ' '; }
            else if (input.Length > 1)
            { throw new ArgumentOutOfRangeException("Input may only be a single character"); }
            c = (char)input[0];
            if (c != ' ' &&
                c != 'x' && c != 'X' &&
                c != '/' &&
                Char.GetNumericValue(c) == -1.0)
            { throw new ArgumentOutOfRangeException("Input must be 0-9, x, X, or /"); }
            return c;
        }
        private char _calculateSpare(char currentShot, char previousShot)
        {
            if ( (Char.GetNumericValue(currentShot) + Char.GetNumericValue(previousShot)) == 10)
            {
                return '/';
            }
            return currentShot;
        }
        private void _UpdateFirstAndSecondShot()
        {
            if (FrameSet != 10 && (Shots[0] == 'x' || Shots[0] == 'X'))
            {
                Shots[1] = ' ';
            } else if (FrameSet != 10 && (Shots[1] == 'x' || Shots[1] == 'X'))
            {
                Shots[0] = Shots[1];
                Shots[1] = ' ';
            } else 
            {
                Shots[1] = _calculateSpare(Shots[1], Shots[0]);
            }
        }
        private void _UpdateSecondAndThirdShot()
        {
            if (FrameSet != 10)
            { return; }
            if ( Shots[0] != 'x' && Shots[0] != 'X' && Shots[1] != '/')
            {
                Shots[2] = ' ';
            }
            else
            {
                Shots[2] = _calculateSpare(Shots[2], Shots[1]);
            }
        }

        public void Reset()
        {
            for (int i = 0; i < ShotCount; i++)
            {
                Shots[i] = ' ';
            }
        }
    }
}
