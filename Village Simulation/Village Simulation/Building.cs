using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public class Building
    {
        BuildingType type;
        int visitorCapacity;
        int workerCapacity;
 
        string name;

        float salary;
        int[] requirements;
        int workDifficulty;
        int openingHour;
        int closingHour;
        int workHours;
        int workerAmount;

        List<Person> workers;
        List<Person> visitors;

        public int OpeningHour { get => openingHour; set => openingHour = value; }
        public int ClosingHour { get => closingHour; set => closingHour = value; }
        public BuildingType Type { get => type; set => type = value; }

        public Building(BuildingType aType)
        {
            workers = new List<Person>();
            visitors = new List<Person>();
            Type = aType;
            visitorCapacity = Type.VisitorCapacity;
            workerCapacity = Type.WorkerCapacity;
            OpeningHour = Type.OpeningHour;
            ClosingHour = Type.ClosingHour;
            workHours = Type.WorkHours;
            salary = 2;
            workerAmount = 0;
        }

        public bool work(Person person)
        {
            if (!workers.Contains(person))
            {
                workers.Add(person);
                person.addToEventLog("went to work");
            }
            person.WorkAmount += 1;
            person.modifyStamina(-1);
            if(person.WorkAmount >= workHours)
            {
                person.Wealth += salary;
                workers.Remove(person);
                person.addToEventLog("left the work");
            }
            return true;
        }

        public bool visit(Person person)
        {
            if (!visitors.Contains(person))
            {
                if (visitors.Count + 1 < visitorCapacity)
                {
                    visitors.Add(person);
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return true;
            }
            
        }

        public bool interact(Person person)
        {
            return true;
        }

        public bool applyForJob(Person person)
        {
            if(workerAmount < workerCapacity)
            {
                person.WorkContract = workHours;
                person.WorkPlace = this;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
