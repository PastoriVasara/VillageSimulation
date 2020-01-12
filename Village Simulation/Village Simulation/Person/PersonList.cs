using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    class PersonList
    {
        List<Person> citizens;
        Dictionary<string, List<Person>> families;

        public List<Person> Citizens { get => citizens; set => citizens = value; }
        public Dictionary<string, List<Person>> Families { get => families; set => families = value; }

        public PersonList()
        {
            Citizens = new List<Person>();
            Families = new Dictionary<string, List<Person>>();
        }

        public void addCitizen(Person newFamilyMember)
        {
            string lastname = newFamilyMember.LastName;
            if (Families.ContainsKey(lastname))
            {
                Families[lastname].Add(newFamilyMember);
            }
            else
            {
                List<Person> newFamily = new List<Person>();
                newFamily.Add(newFamilyMember);
                Families.Add(lastname, newFamily);
            }
        }
    }
}
