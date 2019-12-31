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
            while(givenCity.Citizens.Count <= population)
            {
                Person founderPerson = new Person(peopleRoller,statModifier);
                givenCity.Citizens.Add(founderPerson);
                generateFamilyTree(founderPerson, givenCity, "default");
            }


            return true;
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
