using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tt_calculator_entities
{
    public static class Simulator
    {
        public static void SimulateSeason( League aLeague )
        {
            List<Team> theTeams = aLeague.Teams;

            for( int i=0; i<theTeams.Count - 1; ++i )
            {
                for( int j=i+1; j<theTeams.Count; ++j )
                {
                    aLeague.Fixtures.Add(new Fixture6(theTeams[i], theTeams[j]));
                    aLeague.Fixtures.Add(new Fixture6(theTeams[j], theTeams[i]));
                }
            }

            foreach( var theFixture in aLeague.Fixtures)
            {
                theFixture.Simulate();
            }
        }
    }
}
