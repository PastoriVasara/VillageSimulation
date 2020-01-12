using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    class CityGeneration
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());

        public CityGeneration()
        {
            
        }


        public int rollBuildings(int Population)
        {
            int buildingAmount = 0;
            buildingAmount = Population / 10;
            return buildingAmount;
        }
        public int rollHouses(int Population)
        {
            int houseAmount = 0;
            houseAmount = Population / 5;
            return houseAmount;
        }
        public bool generatePopulation (int population, City givenCity, RollGeneration peopleRoller, ModifyStats statModifier)
        {
            PersonList citizenlist = givenCity.CitizenList;
            while (citizenlist.Citizens.Count <= population)
            {

                Person founderPerson = new Person(peopleRoller,statModifier);
                citizenlist.Citizens.Add(founderPerson);
                generateFamilyTree(founderPerson, givenCity, "default",0);
            }
            return true;
        }

        public bool generateHousing (int houseCount, City givenCity)
        {
            for(int i = 0; i < houseCount; i++)
            {
                House newHouse = new House(givenCity.InitializedRoller.Rnd);
                givenCity.Houses.Add(newHouse);
            }
            return true;
        }

        public bool populateHousing(List<House> houses, List<Person> people)
        {

            return true;
        }


        public Person generateFamilyTree(Person givenPerson, City givenCity, string relationship, int level)
        {
            givenCity.CitizenList.Citizens.Add(givenPerson);
            Person so = null;
            List<Person> children = new List<Person>();
            Person father = null;
            Person mother = null;

            if (relationship != "child")
            {
                if (givenPerson.Father == null && givenPerson.generateFather())
                {
                    father = new Person(givenPerson, "father", level + 1);
                    mother = new Person(givenPerson, "mother", level + 1);
                }
            }
            if (givenPerson.SignificantOther == null && givenPerson.generateSO())
            {
                so = new Person(givenPerson, "so",level);
            }
            while (relationship != "so" && givenPerson.childGeneration())
            {
                children.Add(new Person(givenPerson, "child",level-1));
            }

            if (so != null)
            {
                generateFamilyTree(so, givenCity, "so",level);
            }

            if (relationship != "child")
            {
                if (father != null)
                {
                    generateFamilyTree(father, givenCity, "father",level+1);
                }
            }

            if(children.Count > 0)
            {
                for (int i = 0; i < children.Count; i++)
                {
                    generateFamilyTree(children[i], givenCity, "child",level-1);
                }
            }
            return givenPerson;

        }
    }
}
