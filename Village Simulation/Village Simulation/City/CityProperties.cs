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
        PersonList citizens;
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
            InitializedRoller = aRollGenerator;
            statModifier = aStatModifier;

            generator = new CityGeneration();
            Population = aPopulation;
            BuildingAmount = generator.rollBuildings(Population);
            HouseAmount = generator.rollHouses(Population);
            CityName = aCityName;
            houses = new List<House>();
            CitizenList = new PersonList();
            Buildings = new List<Building>();
        }

        public void generateCity()
        {

        }

        internal List<House> Houses { get => houses; set => houses = value; }
        public List<Building> Buildings { get => buildings; set => buildings = value; }
        public int Population { get => population; set => population = value; }
        public int BuildingAmount { get => buildingAmount; set => buildingAmount = value; }
        public int HouseAmount { get => houseAmount; set => houseAmount = value; }
        public string CityName { get => cityName; set => cityName = value; }
        public RollGeneration InitializedRoller { get => initializedRoller; set => initializedRoller = value; }
        internal PersonList CitizenList { get => citizens; set => citizens = value; }
    }
}
