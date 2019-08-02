using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tt_calculator_entities
{
    public enum eResult
    {
        eHomeWin,
        eAwayWin,
        eDraw
    }

    public class MatchResult
    {
        public MatchResult( int aPointsTeam1, int aPointsTeam2 )
        {
            Points = new Tuple<int, int>( aPointsTeam1, aPointsTeam2 );
            Result = eResult.eDraw;
            if ( Points.Item1 > Points.Item2 )
            {
                Result = eResult.eHomeWin;
            }
            else if( Points.Item2 < aPointsTeam2 )
            {
                Result = eResult.eAwayWin;
            }
        }

        public eResult Result { get; private set; }
        public Tuple<int, int> Points { get; private set; }
    }
}
