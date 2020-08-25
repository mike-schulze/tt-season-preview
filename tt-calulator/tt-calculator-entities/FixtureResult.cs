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

    public class FixtureResult
    {
        public FixtureResult( int aPointsTeam1, int aPointsTeam2 )
        {
            Points = new Tuple<int, int>( aPointsTeam1, aPointsTeam2 );
        }

        public eResult Result {
            get
            {
                if( Points.Item1 > Points.Item2 )
                {
                    return eResult.eHomeWin;
                }
                else if( Points.Item1 < Points.Item2 )
                {
                    return eResult.eAwayWin;
                }

                return eResult.eDraw;
            }
        }
        public Tuple<int, int> Points { get; private set; }

        public int FixturePointsFor
        {
            get
            {
                switch( Result )
                {
                    case eResult.eHomeWin:
                        return 2;
                    case eResult.eDraw:
                        return 1;
                    default:
                        return 0;
                }
            }
        }

        public int FixturePointsAgainst
        {
            get
            {
                return 2 - FixturePointsFor;
            }
        }

        public override string ToString()
        {
            return Points.Item1 + ":" + Points.Item2;
        }
    }
}
