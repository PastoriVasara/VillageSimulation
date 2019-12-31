using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public partial class Building
    {
        public bool work(Person person)
        {
            if (!workers.Contains(person))
            {
                workers.Add(person);
                person.addToEventLog("went to work");
            }
            person.WorkAmount += 1;
            person.StatModifier.modifyStamina(person,-1);
            if (person.WorkAmount >= workHours)
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
            if (workerAmount < workerCapacity)
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
