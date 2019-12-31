using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public partial class House
    {
        int capacity;
        string houseName;

        int foodAmount;
        double rent;
        List<Person> occupants;

        public House(Person firstOccupant)
        {
            Random rnd = firstOccupant.RollGenerator.Rnd;
            Occupants = new List<Person>();
            HouseName = firstOccupant.FirstName + "'s House";
            Capacity = rnd.Next(1, 15);
            firstOccupant.Home = this;
            rent = Capacity * rnd.Next(1, 21);
            FoodAmount = (int)((firstOccupant.Wealth/rent)*rnd.Next(0,30));
            Occupants.Add(firstOccupant);
        }

        public House(string location, int maxAmount, int food)
        {
            Occupants = new List<Person>();

            HouseName = location;
            Capacity = maxAmount;
            FoodAmount = food;
        }

        public int Capacity { get => capacity; set => capacity = value; }
        public string HouseName { get => houseName; set => houseName = value; }
        public int FoodAmount { get => foodAmount; set => foodAmount = value; }
        public List<Person> Occupants { get => occupants; set => occupants = value; }
        public double Rent { get => rent; set => rent = value; }
    }
}
