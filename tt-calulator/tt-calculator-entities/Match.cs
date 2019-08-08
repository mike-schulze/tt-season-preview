﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tt_calculator_entities
{
    public class Match
    {
        public enum eMatchType
        {
            eSingles,
            eDoubles
        }

        public Match( Player aPlayerA, Player aPlayerB )
        {
            Type = eMatchType.eSingles;
            Players = new List<Player>();
            Players.Add(aPlayerA);
            Players.Add(aPlayerB);
            SetResults = new List<int>();
        }

        public Match( Player aPlayerA1, Player aPlayerA2, Player aPlayerB1, Player aPlayerB2 )
        {
            Type = eMatchType.eDoubles;
            Players = new List<Player>();
            Players.Add(aPlayerA1);
            Players.Add(aPlayerA2);
            Players.Add(aPlayerB1);
            Players.Add(aPlayerB2);
            SetResults = new List<int>();
        }

        public void Simulate()
        {
            int theLpz1, theLpz2;

            if( Type == eMatchType.eDoubles )
            {
                theLpz1 = ( Players[0].CurrentLivePZ + Players[1].CurrentLivePZ ) / 2;
                theLpz2 = ( Players[2].CurrentLivePZ + Players[3].CurrentLivePZ ) / 2;
            }
            else
            {
                theLpz1 = Players[0].CurrentLivePZ;
                theLpz2 = Players[1].CurrentLivePZ;
            }

            double theWinPercentage1 = 1 / ( 1 + Math.Pow( 10, ( (double)theLpz2 - (double)theLpz1 ) / 150 ) );
        }

        public readonly List<Player> Players;
        public readonly eMatchType Type;
        public List<int> SetResults;
    }
}
