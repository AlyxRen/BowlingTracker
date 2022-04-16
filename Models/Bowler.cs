using BowlingTracker.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * --------------------------------------------------------------------------------------------------------------
 * !     1    !      2    !     3   !    4   !    5    !    6    !    7    !     8    !     9    !     10       !
 * --------------------------------------------------------------------------------------------------------------
 * !          !           !         !        !         !         !         !          !          !              !
 * !  8 | /   !   5 | 4   !   9 | 0 !  X | _ !   X | _ !   5 | / !  5 | 3  !   6 | 3  !   9 | /  !   9 | / | X  !
 * !          !	          !         !        !         !         !         !          !          !              !
 * !      15  !	   24     !      33 !     58 !      78 !      93 !     101 !     110  !      129 !        149   !
 * --------------------------------------------------------------------------------------------------------------
 */

namespace BowlingTracker.Models
{
    internal class Bowler
    {
        /*
         * Tally is a Dict of the score of the game.
         * Each frame (1-10) is stored as an Array of integers
         * 0-9 is number of pins dropped,
         * 10 is used to indicate a spare ("/"), and
         * 11 is used to indicated a strike ("x" or "X")
         */

        // TODO: Look into validating the inputs to Frames at the Model level
        public Frame[] Frames { get; set; }

        public Bowler()
        {
            Frames = new Frame[]
            {
                new Frame(this, 1, 2),
                new Frame(this, 2, 2),
                new Frame(this, 3, 2),
                new Frame(this, 4, 2),
                new Frame(this, 5, 2),
                new Frame(this, 6, 2),
                new Frame(this, 7, 2),
                new Frame(this, 8, 2),
                new Frame(this, 9, 2),
                new Frame(this, 10, 3)
            };
        }

        public int ShotValue(int frame, int shot)
        {
            if (Frames[frame].Shots[shot] == ' ')
            {
                return 0;
            }
            if (Frames[frame].Shots[shot] == 'X' ||
                Frames[frame].Shots[shot] == 'x' ||
                Frames[frame].Shots[shot] == '/')
            {
                return 10;
            } else
            {
                return (int)Char.GetNumericValue((char)Frames[frame].Shots[shot]);
            }
        }
        public int NextShotValues(int frame, int shot, int numberOfShots)
        {
            int result = 0;
            int lastFrameValue = 0;

            while (numberOfShots > 0 && frame < 10)
            {
                // Calculate next shot to check.
                if (shot < 1 || (frame == 9 && shot < 2))
                {
                    shot++;
                } else
                {
                    shot = 0;
                    frame++;
                }
                if (frame == 10) { break; }
                if (Frames[frame].Shots[shot] != ' ')
                {
                    if (Frames[frame].Shots[shot] == '/')
                    {
                        result -= lastFrameValue;
                    }
                    lastFrameValue = ShotValue(frame, shot);
                    result += lastFrameValue;

                    numberOfShots--;
                }
            }

            return result;
        }
        public int FrameValue(int frame)
        {
            int result = 0;
            int frameLength = frame == 9 ? 3 : 2;

            for (int s = 0; s < frameLength; s++)
            {
                result += ShotValue(frame, s);
                if (frame != 9 && Frames[frame].Shots[s] != ' ')
                {
                    if (Frames[frame].Shots[s] == 'x' || Frames[frame].Shots[s] == 'X')
                    {
                        result += NextShotValues(frame, s, 2);
                    }
                    else if (Frames[frame].Shots[s] == '/')
                    {
                        result = 10;
                        result += NextShotValues(frame, s, 1);
                    }
                }

            }

            return result;
        }

        public int Score(int frame)
        {
            int score = 0;

            for (int f = 0; f < frame; f++)
            {
                score += FrameValue(f);
            }

            return score;
        }

        public void ResetScore()
        {
            foreach (var frame in Frames)
            {
                frame.Reset();
            }
        }
    }
}
