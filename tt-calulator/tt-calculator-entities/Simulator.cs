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

        public static void SimulateSeason( League aLeague )
        {
            List<Team> theTeams = aLeague.Teams;

            var theFixtures = new List<Fixture>();
            for( int i=0; i<aLeague.Teams.Count; ++i )
            {
                for( int j=1; j<aLeague.Teams.Count; ++j )
                {
                    theFixtures.Add(new Fixture(theTeams[i], theTeams[j]));
                    theFixtures.Add(new Fixture(theTeams[j], theTeams[i]));
                }
            }

            foreach( var theFixture in theFixtures)
            {
                theFixture.Simulate();
            }
        }
    }
}
