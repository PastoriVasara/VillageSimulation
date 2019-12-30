using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Village_Simulation
{
    class Program
    {
        static void jsonit(City city)
        {
            string json = JsonConvert.SerializeObject(city, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            System.IO.File.WriteAllText(@"D:\Ohjelmointi\VillageSimulation\data\people.json", json);
        }

        static void Main(string[] args)
        {

            RollGeneration initializedRoller = new RollGeneration();
            CityGeneration flintGenerator = new CityGeneration(100);
            City flint = new City("Flint");
            while (flint.Citizens.Count < 100)
            {
                Person firstPerson = new Person(initializedRoller);
                flintGenerator.generateFamilyTree(firstPerson, flint, "default");
            }

            for (int i = 0; i < flintGenerator.Houses; i++)
            {
                bool foundOccupant = false;
                Person newOccupant = new Person(initializedRoller);
                while (!foundOccupant)
                {
                    int randomCitizen = initializedRoller.Rnd.Next(0, flint.Citizens.Count);
                    if (flint.Citizens[randomCitizen].Home == null)
                    {
                        newOccupant = flint.Citizens[randomCitizen];
                        foundOccupant = true;
                    }
                }
                House newHouse = new House(newOccupant);
                flint.Houses.Add(newHouse);
            }
            House homeless = new House("Streets", 10000, 0);
            for (int i = 0; i < flint.Citizens.Count; i++)
            {
                if (flint.Citizens[i].Home == null)
                {
                    homeless.addOccupant(flint.Citizens[i]);
                }
            }
            string[] types = { "Work", "Leisure" };
            string[] places = { "Docks", "Bar", "Library" };
            List<Building> buildings = new List<Building>();
            for(int i = 0; i < flintGenerator.Buildings; i++)
            {
                int type = initializedRoller.Rnd.Next(0, 2);
                int place = type == 0 ? 0 : initializedRoller.Rnd.Next(1, 3);
                BuildingType newType = new BuildingType(types[type], places[place]);
                Building newBuilding = new Building(newType);
                buildings.Add(newBuilding);
            }
            flint.Houses.Add(homeless);
            for (int i = 0; i < flint.Citizens.Count; i++)
            {
                int building = 0;
                while(building < buildings.Count && flint.Citizens[i].applyForWork(buildings[building]))
                {
                    building++;
                }
            }
            int time = 0;
            while(time <= 24)
            { 
                for(int i = 0; i < flint.Citizens.Count; i++)
                {
                    flint.Citizens[i].advanceTime(time);
                }
                time++;
            }
            for(int i = 0; i < flint.Citizens.Count; i++)
            {
               flint.Citizens[i].printEventLog();
            }
            //flint.printHousing();
            //flint.breakpointer();
            //jsonit(flint);
            Console.Read();
        }
    }
}
