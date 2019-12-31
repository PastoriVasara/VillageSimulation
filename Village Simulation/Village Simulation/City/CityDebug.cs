using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public partial class City
    {
        public void printHousing()
        {
            int homeless = 0;
            List<string> lastNames = new List<string>();
            for (int i = 0; i < Houses.Count; i++)
            {
                for (int j = 0; j < Houses[i].Occupants.Count; j++)
                {
                    if (!(lastNames.Contains(Houses[i].Occupants[j].LastName)))
                    {
                        lastNames.Add(Houses[i].Occupants[j].LastName);
                        Houses[i].Occupants[j].upFamilyTree();
                    }
                    //Houses[i].Occupants[j].printStatArray();
                    if (Houses[i].Occupants[j].Home.HouseName == "Streets")
                    {
                        homeless++;
                    }
                }
            }
            Console.WriteLine("\nCity of {0} has {1} homeless persons and {2} houses", cityName, homeless, Houses.Count);
        }
        public void breakpointer()
        {
            citizens = citizens;
        }
    }
}
