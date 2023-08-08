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

            var theNodes = theDoc.DocumentNode.SelectNodes("//table");

            var theLeague = new League();

            var theCounter = 0;
            foreach( var theNode in theNodes )
            {
                ++theCounter;

                if( theCounter == 1 )
                {
                    // get league name
                    var theLeagueNode = theNode.SelectSingleNode( ".//tbody/tr/td[3]" );
                    theLeague.Name = theLeagueNode.InnerText;
                }

                if( theCounter >= 4 )
                {
                    var theTeam = theNode.SelectSingleNode(".//tbody/tr[2]/td[2]/table");

                    if ( theTeam == null )
                    {
                        continue;
                    }

                    var theNewTeam = new Team();
                    theNewTeam.Name = theTeam.SelectSingleNode(".//tbody/tr/td").InnerText; ;

                    var thePlayers = theTeam.SelectNodes(".//tbody/tr[2]/td/table/tbody/tr[position()>1]");
                    foreach( var thePlayer in thePlayers)
                    {
                        var theNewPlayer = new Player();
                        theNewPlayer.FullName = thePlayer.SelectSingleNode( ".//td[4]" ).InnerText;

                        var theLivePZNode = thePlayer.SelectSingleNode(".//td[11]");
                        if (theLivePZNode == null)
                        {
                            continue;
                        }
                        var theLivePZString = theLivePZNode.InnerText;
                        if (!String.IsNullOrEmpty(theLivePZString))
                        {
                            try
                            {
                                theNewPlayer.CurrentLivePZ = Int32.Parse(theLivePZString);
                            }
                            catch
                            {
                                theLivePZString = theLivePZString.Substring(1, theLivePZString.Length - 2);
                                theNewPlayer.CurrentLivePZ = Int16.Parse(theLivePZString);
                                theNewPlayer.IsActive = false;
                            }
                        }

                        if (theNewPlayer.CurrentLivePZ == 0)
                        {
                            if (theNewPlayer.FullName == "Dariusz Olechnik")
                            {
                                theNewPlayer.CurrentLivePZ = 1680;
                            }
                            if (theNewPlayer.FullName == "Ewelina Olechnik")
                            {
                                theNewPlayer.CurrentLivePZ = 1580;
                            }
                        }

                        theNewTeam.Players.Add( theNewPlayer );
                    }

                    theLeague.Teams.Add(theNewTeam);
                }

            }
            Simulator.SimulateSeason( theLeague );
            theLeague.PrintResults();
        }
    }
}
