﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public partial class House
    {
        public void addOccupant(Person newOccupant)
        {
            newOccupant.Home = this;
            occupants.Add(newOccupant);
        }
    }
}
