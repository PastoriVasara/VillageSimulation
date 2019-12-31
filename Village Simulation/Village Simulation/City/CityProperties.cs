using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public partial class City
    {
        List<House> houses;
        List<Person> citizens;
        List<Building> buildings;
        CityGeneration generator;
        RollGeneration initializedRoller;
        ModifyStats statModifier;

        string cityName;
        int population;
        int buildingAmount;
        int houseAmount;

        public City(string aCityName, int aPopulation, RollGeneration aRollGenerator, ModifyStats aStatModifier)
        {
            initializedRoller = aRollGenerator;
            statModifier = aStatModifier;

            generator = new CityGeneration();
            population = aPopulation;
            buildingAmount = generator.rollBuildings(population);
            houseAmount = generator.rollHouses(population);
            cityName = aCityName;
            houses = new List<House>();
            citizens = new List<Person>();
            Buildings = new List<Building>();
        }

        public void generateCity()
        {

        }

        public List<Person> Citizens { get => citizens; set => citizens = value; }
        internal List<House> Houses { get => houses; set => houses = value; }
        public List<Building> Buildings { get => buildings; set => buildings = value; }
    }
}
