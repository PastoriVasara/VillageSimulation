using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public partial class House
    {
        public bool addOccupant(Person newOccupant)
        {
            newOccupant.Home = this;
            if(family.Count < capacity)
            {
                family.Add(newOccupant);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
