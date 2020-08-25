using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tt_calculator_entities
{
    public class Team
    {
        public Team()
        {
            Players = new List<Player>();
        }

        public string Name { get; set; }

        public List<Player> Players { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
