using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public class Person
    {

        List<string> actionLog;
        // Main Stats.
        float stamina;
        float maxStamina;
        float health;
        float maxHealth;
        float hunger;
        float maxHunger;
        float thirst;
        float maxThirst;
        float mood;
        float maxMood;

        bool isAlive;

        bool working;
        int nextShift;
        int workAmount;
        int workContract;

        bool sleeping;
        int sleepAmount;
        

        // Basic information
        string firstName;
        string lastName;
        string race;
        int[] age = new int[2];
        double wealth;
        Person father;
        Person mother;
        Person significantOther;
        Building workPlace;

        List<Person> children;


        House home;

        // Attributes
        int strength;
        int constitution;
        int dexterity;
        int wisdom;
        int charisma;
        int intelligence;

        RollGeneration rollGenerator;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public double Wealth { get => wealth; set => wealth = value; }
        internal House Home { get => home; set => home = value; }
        public RollGeneration RollGenerator { get => rollGenerator; set => rollGenerator = value; }
        public Person Father { get => father; set => father = value; }
        public Person Mother { get => mother; set => mother = value; }
        public Person SignificantOther { get => significantOther; set => significantOther = value; }
        public List<Person> Children { get => children; set => children = value; }
        public int WorkAmount { get => workAmount; set => workAmount = value; }
        public int WorkContract { get => workContract; set => workContract = value; }
        internal Building WorkPlace { get => workPlace; set => workPlace = value; }

        public Person(RollGeneration initialized)
        {
            Children = new List<Person>();
            Father = null;
            Mother = null;
            Home = null;
            RollGenerator = initialized;
            generatePerson();
        }

        //relationship of this compared to familymember
        public Person(Person familyMember, string relationship)
        {
            Children = new List<Person>();
            Father = null;
            Mother = null;
            Home = familyMember.home;
            RollGenerator = familyMember.rollGenerator;
            generatePerson();
            lastName = familyMember.LastName;
            race = familyMember.race;


            if(relationship == "so")
            {
                SignificantOther = familyMember;
                familyMember.SignificantOther = this;
                age[0] = familyMember.age[0] + rollGenerator.Rnd.Next(-10, 10);
            }
            else if(relationship == "father")
            {
                Children.Add(familyMember);
                familyMember.Father = this;
                familyMember.Mother = this.significantOther;
                age[0] = familyMember.age[0] + 18;
            }
            else if(relationship == "child")
            {
                familyMember.Children.Add(this);
                Father = familyMember;
                age[0] = familyMember.age[0] - 18;
                if(familyMember.SignificantOther != null)
                {
                    familyMember.significantOther.Children.Add(this);
                    Mother = familyMember.SignificantOther;
                }
            }
        }

        private void generateEssentials()
        {
            stamina = constitution;
            maxStamina = stamina;

            health = (int)Math.Ceiling(age[0]*1.0/18) * rollGenerator.Rnd.Next(1,7);
            maxHealth = health;
            hunger = constitution * 5;
            maxHunger = hunger;
            thirst = hunger;
            maxThirst = thirst;
            mood = charisma * 5;
            maxMood = mood;
            WorkAmount = 0;
            WorkContract = 0;
            WorkPlace = null;
            workAmount = 0;
            nextShift = 0;
            working = false;
            sleeping = false;
            sleepAmount = 0;
            actionLog = new List<string>();
        }

        #region ModifyEssentials
        public void modifyHealth(float modifier)
        {
            if(health+modifier > maxHealth)
            {
                health = maxHealth;
            }
            else if(health + modifier <= 0)
            {
                health = 0;
                isAlive = false;
                addToEventLog("the bitch fucking died");
            }
            else
            {
                health =+ modifier;
            }
        }
        public bool modifyMoney(float modifier)
        {
            if(wealth+modifier < 0)
            {
                addToEventLog("couldn't afford to purchase food!");
                return false;
            }
            else
            {
                wealth += modifier;
                return true;
            }
        }
        public bool modifyStamina(float modifier)
        {
            if (stamina + modifier < 0)
            {
                addToEventLog("took damage from exhaustion!");
                stamina = 0;
                modifyHealth(-1);
                return false;
            }
            else
            {
                stamina += modifier;
                return true;
            }
        }

        public void modifyHunger(float modifier)
        {
            if(hunger+modifier > maxHunger)
            {
                hunger = maxHunger;
            }
            else if(hunger + modifier <= 0)
            {
                hunger = 0;
                modifyHealth(-1);
                addToEventLog("took damage from hunger!");
            }
            else
            {
                hunger += modifier;
            }
        }
        public void modifyThirst(float modifier)
        {
            if (thirst + modifier > maxThirst)
            {
                thirst = maxThirst;
            }
            else if (thirst + modifier <= 0)
            {
                thirst = 0;
                modifyHealth(-1);
                addToEventLog("took damage from hunger!");
            }
            else
            {
                thirst += modifier;
            }
        }
        public void modifyMood(float modifier)
        {
            if (mood + modifier > maxMood)
            {
                mood = maxMood;
            }
            else if (mood + modifier <= 0)
            {
                modifyHunger(mood + modifier);
                mood = 0;
                
            }
            else
            {
                mood += modifier;
            }
        }
        #endregion

        private void generateStats()
        {
            int[] statArray = RollGenerator.rollStats();
            strength = statArray[0];
            constitution = statArray[1];
            dexterity = statArray[2];
            wisdom = statArray[3];
            charisma = statArray[4];
            intelligence = statArray[5];
        }
        private void generateBasicInformation()
        {
            string[] fullName = RollGenerator.rollName();
            FirstName = fullName[0];
            LastName = fullName[1];
            race = fullName[2];
            age = RollGenerator.rollAge();
            wealth = RollGenerator.generateWealth(intelligence);
            
        }

        private void generatePerson()
        {
            generateStats();
            generateBasicInformation();
            generateEssentials();
        }
        bool parentGeneration()
        {
            int maxAge = 120;
            double propOfParents = (maxAge - 18 + age[0]) / maxAge;

            return rollGenerator.Rnd.NextDouble() > propOfParents;
        }
        public bool childGeneration()
        {
            int childAmount = Children.Count;
            int years = age[0];
            if(years > 18)
            {
                double propOfChild = 0.1 * childAmount + 0.15;
                return rollGenerator.Rnd.NextDouble() > propOfChild;
            }
            else
            {
                return false;
            }
            
        }
        public bool generateSO()
        {
            int years = age[0];
            if(years > 16)
            {
                return rollGenerator.Rnd.NextDouble() > 0.5;
            }
            else
            {
                return false;
            }
        }
        public bool generateFather()
        {
            return parentGeneration();
        }
        public bool generateMother()
        {
            return parentGeneration();
        }
        public void printStatArray()
        {
            Console.WriteLine("\n-------------------------\n" +
                "First Name: {0} \n" +
                "Last Name: {1} \n" +
                "Age: {2} Years {3} Days \n" +
                "Race: {4} \n" +
                "Wealth: {5} \n" +
                "Strength: {6} \n" +
                "Constitution: {7} \n" +
                "Dexterity: {8} \n" +
                "Wisdom: {9} \n" +
                "Charisma: {10} \n" +
                "Intelligence: {11} \n", 
                FirstName,LastName,age[0],age[1],race,wealth,strength, constitution, dexterity, wisdom, charisma, intelligence);
            Console.WriteLine("### Housing ### \n" +
                "House Name: {0}\n" +
                "House Capacity: {1}\n" +
                "House Food: {2}\n" +
                "House Rent: {3}\n" +
                "### End of Housing ### \n" +
                "-------------------------\n",home.HouseName,home.Capacity,home.FoodAmount,home.Rent);
        }
        // DebugTest
        public int getYear()
        {
            return age[0];
        }
        public void upFamilyTree()
        {
            if (Father == null && Mother == null)
            {
                Console.WriteLine("Family Name: " + lastName +"\n");
                downFamilyTree("-");
            }
            else
            {
                if(Father != null)
                {
                    Father.upFamilyTree();
                }
                else if(Mother != null)
                {
                    upFamilyTree();
                }
                
            }
        }
        public void downFamilyTree(string level)
        {
            string response = level+firstName + "\n";

            response += significantOther == null ? "" : level +significantOther.firstName;
            response += "\n";
            Console.WriteLine(response);
            for(int i = 0; i < Children.Count; i++)
            {
                Children[i].downFamilyTree(level += "-");
            }

        }

        public void advanceTime(int oClock)
        {
            bool madeAnAction = false;

            if(workAmount < WorkContract && WorkContract != 0 && !sleeping && nextShift == 0)
            {
                if(oClock < WorkPlace.ClosingHour && oClock > WorkPlace.OpeningHour)
                {
                    madeAnAction = WorkPlace.work(this);
                    working = madeAnAction;

                }
            }
            while (!madeAnAction)
            {
                if(stamina < maxStamina * 0.2 || sleeping)
                {
                    if (sleepAmount < 8 && (workPlace == null || (oClock < workPlace.OpeningHour) || oClock > workPlace.ClosingHour))
                    {
                        if (!sleeping)
                        {
                            addToEventLog("went to sleep!");
                        }
                        sleeping = true;
                        madeAnAction = sleep();
                        sleepAmount++;
                    }
                    else
                    {
                        if (sleeping)
                        {
                            addToEventLog("woke up!");
                        }
                        sleeping = false;
                        sleepAmount = 0;
                    }
                }
                else if (!sleeping)
                {
                    madeAnAction = purchaseFood();
                    madeAnAction = !madeAnAction ? (madeAnAction = hunger < maxHunger * 0.2 ? eat() : false) : true;
                }

                if (!madeAnAction)
                {
                    madeAnAction = idle();
                }

            }
            if (!working)
            {
                nextShift = nextShift - 1 > 0 ? nextShift-- : nextShift = 0;
            }
        }

        public bool eat()
        {
            if(home.FoodAmount - 1 > 0)
            {
                addToEventLog("ate some food!");
                home.FoodAmount -= 1;
                modifyHunger(+2);
                modifyThirst(+2);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool sleep()
        {
            modifyHunger(-1);
            modifyThirst(-1);
            return modifyStamina(+2);
        }

        public bool idle()
        {
            addToEventLog("idled around!");
            modifyStamina(-2);
            return true;
        }

        public bool purchaseFood()
        {
            if(home.FoodAmount == 0)
            {
                addToEventLog("purchased some new food!");
                bool canAfford = modifyMoney(-2);
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
            bool eligibleForWork = age[0] >= 18;

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
            actionLog.Add(firstName+" "+ lastName +" did: " +newEvent);
        }

        public void printEventLog()
        {
            string separator = "#####################################";


            Console.WriteLine("\n{0}\n",separator);
            Console.WriteLine("Events of: {0} {1}", firstName, lastName);
            for(int i = 0; i < actionLog.Count; i++)
            {
                Console.WriteLine(actionLog[i]);
            }
            Console.WriteLine("\n{0}\n", separator);
        }

    }
}
