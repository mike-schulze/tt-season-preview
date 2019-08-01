using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tt_calulator_cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var theDoc = new HtmlDocument();
            theDoc.Load( @"C:\Users\MikeS\Desktop\sample.html" );
            var theNodes = theDoc.DocumentNode.SelectNodes( "//table" );

            var theCounter = 0;
            string theTeamName;
            string theLeagueName;
            foreach( var theNode in theNodes )
            {
                var theDeductedTables = theCounter - 4;
                ++theCounter;

                if( theCounter == 2 )
                {
                    // get league name
                    theLeagueName = theNode.SelectSingleNode( ".//tr//td[2]" ).InnerText;
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
                    // get players 
                    var thePlayers = theNode.SelectNodes( ".//tr[position()>1]" );

                    foreach( var thePlayer in thePlayers )
                    {
                        var theColumns = thePlayer.SelectNodes( ".//td" );
                        var theName = theColumns.ElementAt(2).InnerText;
                        var theDoB = theColumns.ElementAt(3).InnerText;
                        var theLivePZ = theColumns.ElementAt(6).InnerText;
                    }
                }
                
            }
        }
    }
}
