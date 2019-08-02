using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tt_calculator_entities
{
    public static class Simulator
    {
        private struct Setup
        {
            public Setup( Team aTeam )
            {
                TeamName = aTeam.Name;
                Doubles = new List<int>();
                PlayerLivePZs = new List<int>();

                foreach( var thePlayer in aTeam.Players )
                {
                    if( thePlayer.IsActive )
                    {
                        PlayerLivePZs.Add( thePlayer.CurrentLivePZ );
                        if( PlayerLivePZs.Count >= 4 )
                        {
                            break;
                        }
                    }
                }

                Doubles.Add( ( PlayerLivePZs[0] + PlayerLivePZs[1] ) / 2 );
                Doubles.Add( ( PlayerLivePZs[2] + PlayerLivePZs[3] ) / 2 );
            }

            public readonly string TeamName;
            public readonly List<int> Doubles;
            public readonly List<int> PlayerLivePZs;
        }

        private struct Match
        {
            public Match( Setup aTeam1, Setup aTeam2 )
            {
                Team1 = aTeam1;
                Team2 = aTeam2;
                Result = null;
            }

            public void Simulate()
            {
                Result = new MatchResult( 7, 7 );
            }

            public readonly Setup Team1;
            public readonly Setup Team2;
            public MatchResult Result;
        }

        public static void SimulateSeason( League aLeague )
        {
            var theTeams = new List<Setup>();
            foreach( var theTeam in aLeague.Teams )
            {
                theTeams.Add( new Setup( theTeam ) );
            }

            var theMatches = new List<Match>();
            for( int i=0; i<aLeague.Teams.Count; ++i )
            {
                for( int j=0; j<aLeague.Teams.Count; ++j )
                {
                    theMatches.Add(new Match(theTeams[i], theTeams[j]));
                    theMatches.Add(new Match(theTeams[j], theTeams[i]));
                }
            }

            foreach( var theMatch in theMatches )
            {
                theMatch.Simulate();
            }
        }
    }
}
