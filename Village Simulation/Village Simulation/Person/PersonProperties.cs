using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public partial class Person
    {

        List<string> actionLog;
        // Main Stats.
        float stamina;
        float maxStamina;
        float health;
        float maxHealth;
        float satiation;
        float maxSatiation;
        float hydration;
        float maxHydration;
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
        int familyLevel;
        string firstName;
        string lastName;
        string race;
        int[] age = new int[2];
        float wealth;
        Person father;
        Person mother;
        Person significantOther;
        Building workPlace;
        ModifyStats statModifier;

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
        public float Wealth { get => wealth; set => wealth = value; }
        internal House Home { get => home; set => home = value; }
        public RollGeneration RollGenerator { get => rollGenerator; set => rollGenerator = value; }
        public Person Father { get => father; set => father = value; }
        public Person Mother { get => mother; set => mother = value; }
        public Person SignificantOther { get => significantOther; set => significantOther = value; }
        public List<Person> Children { get => children; set => children = value; }
        public int WorkAmount { get => workAmount; set => workAmount = value; }
        public int WorkContract { get => workContract; set => workContract = value; }
        internal Building WorkPlace { get => workPlace; set => workPlace = value; }
        public float Stamina { get => stamina; set => stamina = value; }
        public float MaxStamina { get => maxStamina; set => maxStamina = value; }
        public float Health { get => health; set => health = value; }
        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float Satiation { get => satiation; set => satiation = value; }
        public float MaxSatiation { get => maxSatiation; set => maxSatiation = value; }
        public float Hydration { get => hydration; set => hydration = value; }
        public float MaxHydration { get => maxHydration; set => maxHydration = value; }
        public float Mood { get => mood; set => mood = value; }
        public float MaxMood { get => maxMood; set => maxMood = value; }
        public int Strength { get => strength; set => strength = value; }
        public int Constitution { get => constitution; set => constitution = value; }
        public int Dexterity { get => dexterity; set => dexterity = value; }
        public int Wisdom { get => wisdom; set => wisdom = value; }
        public int Charisma { get => charisma; set => charisma = value; }
        public int Intelligence { get => intelligence; set => intelligence = value; }
        public bool IsAlive { get => isAlive; set => isAlive = value; }
        public int[] Age { get => age; set => age = value; }
        public ModifyStats StatModifier { get => statModifier; set => statModifier = value; }

        public Person(RollGeneration rollInitialized,ModifyStats statInitialized)
        {
            Children = new List<Person>();
            Father = null;
            Mother = null;
            Home = null;
            familyLevel = 0;
            statModifier = statInitialized;
            RollGenerator = rollInitialized;
            generatePerson();
        }

        //relationship of this compared to familymember
        public Person(Person familyMember, string relationship,int level)
        {
            familyLevel = level;
            statModifier = familyMember.StatModifier;
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
                Age[0] = familyMember.Age[0] + rollGenerator.Rnd.Next(-10, 10);
            }
            else if(relationship == "father")
            {
                Children.Add(familyMember);
                familyMember.Father = this;
                familyMember.Mother = this.significantOther;
                Age[0] = familyMember.Age[0] + 18;
            }
            else if(relationship == "child")
            {
                familyMember.Children.Add(this);
                Father = familyMember;
                Age[0] = familyMember.Age[0] - 18;
                if(familyMember.SignificantOther != null)
                {
                    familyMember.significantOther.Children.Add(this);
                    Mother = familyMember.SignificantOther;
                }
            }
        }

        private void generateEssentials()
        {
            Stamina = Constitution;
            MaxStamina = Stamina;

            Health = (int)Math.Ceiling(Age[0]*1.0/18) * rollGenerator.Rnd.Next(1,7);
            MaxHealth = Health;
            Satiation = Constitution * 5;
            MaxSatiation = Satiation;
            Hydration = Satiation;
            MaxHydration = Hydration;
            Mood = Charisma * 5;
            MaxMood = Mood;
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


        private void generateStats()
        {
            int[] statArray = RollGenerator.rollStats();
            Strength = statArray[0];
            Constitution = statArray[1];
            Dexterity = statArray[2];
            Wisdom = statArray[3];
            Charisma = statArray[4];
            Intelligence = statArray[5];
        }
        private void generateBasicInformation()
        {
            string[] fullName = RollGenerator.rollName();
            FirstName = fullName[0];
            LastName = fullName[1];
            race = fullName[2];
            Age = RollGenerator.rollAge();
            wealth = RollGenerator.generateWealth(Intelligence);
            
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
            double propOfParents = (maxAge - 18 + Age[0]) / maxAge;

            return rollGenerator.Rnd.NextDouble() > propOfParents;
        }
        public bool childGeneration()
        {
            int childAmount = Children.Count;
            int years = Age[0];
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
            int years = Age[0];
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
    }
}
