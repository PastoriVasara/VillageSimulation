using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public partial class Person
    {
        public void advanceTime(int oClock)
        {
            bool madeAnAction = false;

            if (workAmount < WorkContract && WorkContract != 0 && !sleeping && nextShift == 0)
            {
                if (oClock < WorkPlace.ClosingHour && oClock > WorkPlace.OpeningHour)
                {
                    madeAnAction = WorkPlace.work(this);
                    working = madeAnAction;
                }
            }
            while (!madeAnAction)
            {

            }

        }


        public bool eat()
        {
            if (home.FoodAmount - 1 > 0)
            {
                addToEventLog("ate some food!");
                home.FoodAmount -= 1;
                StatModifier.modifySatiation(this,+2);
                StatModifier.modifyHydration(this, +2);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool sleep()
        {
            StatModifier.modifySatiation(this, -1);
            StatModifier.modifyHydration(this, -1);
            return StatModifier.modifyStamina(this, +2);
        }

        public bool idle()
        {
            addToEventLog("idled around!");
            StatModifier.modifyStamina(this, -2);
            return true;
        }

        public bool purchaseFood()
        {
            if (home.FoodAmount == 0)
            {
                addToEventLog("purchased some new food!");
                bool canAfford = StatModifier.modifyWealth(this,-2);
                if (canAfford)
                {
                    home.FoodAmount += 2;
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }


        public bool applyForWork(Building workplace)
        {
            bool eligibleForWork = Age[0] >= 18;

            if (eligibleForWork)
            {
                return workplace.applyForJob(this);
            }
            else
            {
                return true;
            }
        }

        public void addToEventLog(string newEvent)
        {
            actionLog.Add(firstName + " " + lastName + " did: " + newEvent);
        }
    }
}
