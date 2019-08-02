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
                var theDeductedTables = theCounter - 4;
                ++theCounter;

                if( theCounter == 2 )
                {
                    // get league name
                    theLeague.Name = theNode.SelectSingleNode( ".//tr//td[2]" ).InnerText;
                }

                if ( theDeductedTables < 0 )
                {
                    continue;
                }

                if ( theDeductedTables % 3 == 0 )
                {
                    // get team name
                    theTeamName = theNode.SelectSingleNode( ".//tr//td" ).InnerText;
                }

                if( theDeductedTables % 3 == 1 )
                {
                    var theTeam = new Team
                    {
                        Name = theTeamName
                    };

                    // get players 
                    var thePlayers = theNode.SelectNodes( ".//tr[position()>1]" );

                    foreach( var thePlayer in thePlayers )
                    {
                        var theColumns = thePlayer.SelectNodes( ".//td" );
                        var theNewPlayer = new Player();
                        theNewPlayer.FullName = theColumns.ElementAt(2).InnerText;
                        theNewPlayer.DateOfBirth = DateTime.Parse( theColumns.ElementAt(3).InnerText );

                        string theLivePZString = theColumns.ElementAt(6).InnerText.Trim();
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
        }
    }
}
