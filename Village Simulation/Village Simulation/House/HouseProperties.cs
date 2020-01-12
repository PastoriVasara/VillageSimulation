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
        List<Person> family;
        Random rnd;

        public House(Random aRnd, KeyValuePair<string,List<Person>> aFamily)
        {
            rnd = aRnd;
            Family =aFamily.Value;
            Capacity = Family.Count;
            rent = Capacity * rnd.Next(1, 21);
        }

        public House(string location, int maxAmount, int food)
        {
            Family = new List<Person>();
            HouseName = location;
            Capacity = maxAmount;
            FoodAmount = food;
        }

        public int Capacity { get => capacity; set => capacity = value; }
        public string HouseName { get => houseName; set => houseName = value; }
        public int FoodAmount { get => foodAmount; set => foodAmount = value; }
        public double Rent { get => rent; set => rent = value; }
        public List<Person> Family { get => family; set => family = value; }
    }
}
