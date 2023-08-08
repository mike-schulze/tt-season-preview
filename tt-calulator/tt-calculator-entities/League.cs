using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tt_calculator_entities
{
    public class League
    {
        private class TeamResult : IComparable<TeamResult>
        {
            public Team Team;
            public int FixturePointsFor;
            public int FixturePointsAgainst;
            public int Wins;
            public int Draws;
            public int Losses;
            public int SetsFor;
            public int SetsAgainst;

            public int CompareTo( TeamResult other )
            {
                TeamResult t1 = this;
                TeamResult t2 = other;

                if( t1.FixturePointsFor > t2.FixturePointsFor )
                {
                    return -1;
                }

                if( t1.FixturePointsFor < t2.FixturePointsFor )
                {
                    return 1;
                }

                if( t1.SetsFor - t1.SetsAgainst > t2.SetsFor - t2.SetsAgainst )
                {
                    return -1;
                }

                return 1;
            }

            public override string ToString()
            {
                return Team.Name + " " + FixturePointsFor + " Punkte";
            }
        }

        public League()
        {
            Teams = new List<Team>();
            Fixtures = new List<Fixture6>();
        }

        public void PrintResults()
        {
            var theResults = new Dictionary<Team, TeamResult>();
            foreach( var theTeam in Teams )
            {
                theResults.Add( theTeam, new TeamResult() { Team = theTeam } );
            }

            foreach( var theFixture in Fixtures )
            {
                var theTeam1Result = theResults[ theFixture.Team1 ];
                var theTeam2Result = theResults[ theFixture.Team2 ];

                theTeam1Result.FixturePointsFor += theFixture.Result.FixturePointsFor;
                theTeam1Result.FixturePointsAgainst += theFixture.Result.FixturePointsAgainst;

                theTeam2Result.FixturePointsFor += theFixture.Result.FixturePointsAgainst;
                theTeam2Result.FixturePointsAgainst += theFixture.Result.FixturePointsFor;

                if( theFixture.Result.Result == eResult.eHomeWin )
                {
                    ++theTeam1Result.Wins;
                    ++theTeam2Result.Losses;
                }
                else if ( theFixture.Result.Result == eResult.eAwayWin )
                {
                    ++theTeam1Result.Losses;
                    ++theTeam2Result.Wins;
                }
                else
                {
                    ++theTeam1Result.Draws;
                    ++theTeam2Result.Draws;
                }

                theTeam1Result.SetsFor += theFixture.Result.Points.Item1;
                theTeam1Result.SetsAgainst += theFixture.Result.Points.Item2;

                theTeam2Result.SetsFor += theFixture.Result.Points.Item2;
                theTeam2Result.SetsAgainst += theFixture.Result.Points.Item1;
            }

            var theStandings = new List<TeamResult>();
            foreach( var theItem in theResults )
            {
                theStandings.Add( theItem.Value );
            }
            theStandings.Sort();

            using( StreamWriter theFile = new StreamWriter( @"C:\Users\MikeS\Desktop\simulation.txt" ) )
            {
                int thePlace = 0;
                foreach( var theResult in theStandings )
                {
                    ++thePlace;
                    theFile.WriteLine( thePlace + "\t" + theResult.Team.Name + "\t" + 
                                       theResult.FixturePointsFor + ":" + theResult.FixturePointsAgainst + "\t" + 
                                       theResult.Wins + "/" + theResult.Draws + "/" + theResult.Losses  + "\t" + 
                                       theResult.SetsFor + ":" + theResult.SetsAgainst );
                }
                theFile.WriteLine();

                foreach( var theFixture in Fixtures )
                {
                    theFile.WriteLine( theFixture.Team1.Name + " - " + theFixture.Team2.Name + "\t" +
                                       theFixture.Result.Points.Item1 + ":" + theFixture.Result.Points.Item2 );
                }
            }
        }

        public string Name { get; set; }

        public List<Team> Teams { get; }
        public List<Fixture6> Fixtures { get; }
    }
}
