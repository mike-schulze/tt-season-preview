using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tt_calculator_entities;

namespace tt_calulator_cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var theDoc = new HtmlDocument();
            theDoc.Load( @"C:\Users\MikeS\Desktop\sample.html", Encoding.UTF8 );
            var theNodes = theDoc.DocumentNode.SelectNodes( "//table" );

            var theLeague = new League();

            var theCounter = 0;
            string theTeamName = "";
            foreach( var theNode in theNodes )
            {
                var theDeductedTables = theCounter - 3;
                ++theCounter;

                if( theCounter == 1 )
                {
                    // get league name
                    var theLeagueNode = theNode.SelectSingleNode( ".//tbody//tr//td[3]" );
                    theLeague.Name = theLeagueNode.InnerText;
                }

                if ( theDeductedTables < 0 )
                {
                    continue;
                }

                if ( theDeductedTables % 2 == 0 )
                {
                    // get team name
                    theTeamName = theNode.SelectSingleNode( ".//tbody//tr//td" ).InnerText;
                    if( theTeamName.Substring(0,8) == "Anmelden")
                    {
                        break;
                    }
                }

                if( theDeductedTables % 2 == 1 )
                {
                    var theTeam = new Team
                    {
                        Name = theTeamName
                    };

                    // get players 
                    var thePlayers = theNode.SelectNodes( ".//tbody//tr[position()>1]" );

                    foreach( var thePlayer in thePlayers )
                    {
                        var theNewPlayer = new Player();
                        theNewPlayer.FullName = thePlayer.SelectSingleNode( ".//td[4]" ).InnerText;

                        var theLivePZNode = thePlayer.SelectSingleNode( ".//td[12]" );
                        if( theLivePZNode == null )
                        {
                            continue;
                        }
                        var theLivePZString = theLivePZNode.InnerText;
                        if( !String.IsNullOrEmpty( theLivePZString ) )
                        {
                            try
                            {
                                theNewPlayer.CurrentLivePZ = Int32.Parse( theLivePZString );
                            }
                            catch
                            {
                                theLivePZString = theLivePZString.Substring(1, theLivePZString.Length - 2);
                                theNewPlayer.CurrentLivePZ = Int16.Parse(theLivePZString);
                                theNewPlayer.IsActive = false;
                            }
                        }

                        theTeam.Players.Add( theNewPlayer );
                    }

                    theLeague.Teams.Add( theTeam );
                }
            }
            Simulator.SimulateSeason( theLeague );
            theLeague.PrintResults();
        }
    }
}
