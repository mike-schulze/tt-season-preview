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
            Matches.Add(new Match(Team1.Players[0], Team2.Players[0]));
            Matches[0].Simulate();
            Result = new FixtureResult(7, 7);
        }

        public readonly Team Team1;
        public readonly Team Team2;
        public readonly List<Match> Matches;
        public FixtureResult Result;
    }
}
