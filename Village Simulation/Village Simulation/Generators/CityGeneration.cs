using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    class CityGeneration
    {
        int population;
        int houses;
        int buildings;
        Random rnd;

        public CityGeneration(int aPopulation)
        {
            rnd = new Random();
            Population = aPopulation;
            Houses = rollHouses();
            Buildings = rollBuildings();

        }

        public int Population { get => population; set => population = value; }
        public int Houses { get => houses; set => houses = value; }
        public int Buildings { get => buildings; set => buildings = value; }

        int rollBuildings()
        {
            int buildingAmount = 0;
            buildingAmount = Population / 10;
            //buildingAmount = Population / rnd.Next(50, 201);
            return buildingAmount;
        }
        int rollHouses()
        {
            int houseAmount = 0;
            houseAmount = Population / rnd.Next(5,50);
            return houseAmount;
        }
        public Person generateFamilyTree(Person givenPerson, City givenCity, string relationship)
        {
            givenCity.Citizens.Add(givenPerson);
            Person so = null;
            List<Person> children = new List<Person>();
            Person father = null;
            Person mother = null;

            if (relationship != "child")
            {
                if (givenPerson.Father == null && givenPerson.generateFather())
                {
                    father = new Person(givenPerson, "father");
                    mother = new Person(givenPerson, "mother");
                    //generateFamilyTree(new Person(givenPerson, "father"), givenCity, "father");
                }
            }

            if (givenPerson.SignificantOther == null && givenPerson.generateSO())
            {
                so = new Person(givenPerson, "so");
            }
            while (relationship != "so" && givenPerson.childGeneration())
            {
                children.Add(new Person(givenPerson, "child"));
            }

            if (so != null)
            {
                generateFamilyTree(so, givenCity, "so");
            }

            if (relationship != "child")
            {
                if (father != null)
                {
                    generateFamilyTree(father, givenCity, "father");
                }
            }


            if(children.Count > 0)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    generateFamilyTree(children[i], givenCity, "child");
                }
            }
            return givenPerson;

        }
    }
}
