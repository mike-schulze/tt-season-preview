using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tt_calculator_entities
{
    public class Fixture
    {
        public Fixture(Team aTeam1, Team aTeam2)
        {
            Team1 = aTeam1;
            Team2 = aTeam2;
            Matches = new List<Match>();
            Result = null;
        }

        public void Simulate()
        {
            Matches.Add( new Match( Team1.Players[ 0 ], Team1.Players[ 1 ], Team2.Players[ 0 ], Team2.Players[ 1 ] ) );
            Matches.Add( new Match( Team1.Players[ 2 ], Team1.Players[ 3 ], Team2.Players[ 2 ], Team2.Players[ 3 ] ) );

            Matches.Add( new Match( Team1.Players[ 0 ], Team2.Players[ 1 ] ) );
            Matches.Add( new Match( Team1.Players[ 1 ], Team2.Players[ 0 ] ) );
            Matches.Add( new Match( Team1.Players[ 2 ], Team2.Players[ 3 ] ) );
            Matches.Add( new Match( Team1.Players[ 3 ], Team2.Players[ 2 ] ) );

            Matches.Add( new Match( Team1.Players[ 0 ], Team2.Players[ 0 ] ) );
            Matches.Add( new Match( Team1.Players[ 1 ], Team2.Players[ 1 ] ) );
            Matches.Add( new Match( Team1.Players[ 2 ], Team2.Players[ 2 ] ) );
            Matches.Add( new Match( Team1.Players[ 3 ], Team2.Players[ 3 ] ) );

            Matches.Add( new Match( Team1.Players[ 0 ], Team2.Players[ 2 ] ) );
            Matches.Add( new Match( Team1.Players[ 1 ], Team2.Players[ 3 ] ) );
            Matches.Add( new Match( Team1.Players[ 2 ], Team2.Players[ 0 ] ) );
            Matches.Add( new Match( Team1.Players[ 3 ], Team2.Players[ 1 ] ) );

            int thePointsHome = 0;
            int thePointsAway = 0;
            foreach( var theMatch in Matches )
            {
                theMatch.Simulate();
                thePointsAway += theMatch.FixturePointsAway;
                thePointsHome += theMatch.FixturePointsHome;
            }

            Result = new FixtureResult( thePointsHome, thePointsAway );
        }

        public readonly Team Team1;
        public readonly Team Team2;
        public readonly List<Match> Matches;
        public FixtureResult Result;

        public override string ToString()
        {
            return Team1.Name + " - " + Team2.Name;
        }
    }
}
