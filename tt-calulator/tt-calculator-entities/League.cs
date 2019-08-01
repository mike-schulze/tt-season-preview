using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tt_calculator_entities
{
    public class League
    {
        public League()
        {
            Teams = new List<Team>();
        }

        public string Name { get; set; }

        public List<Team> Teams { get; }
    }
}
