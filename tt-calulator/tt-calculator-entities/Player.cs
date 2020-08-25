using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tt_calculator_entities
{
    public class Player
    {
        public Player()
        {
            IsActive = true;
        }

        public string FullName { get; set; }

        public int CurrentLivePZ { get; set; }

        public bool IsActive { get; set; }

        public override string ToString()
        {
            return FullName;
        }
    }
}
