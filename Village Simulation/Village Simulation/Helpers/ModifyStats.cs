using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public class ModifyStats
    {

        public ModifyStats()
        {

        }

        public void redirectToModifier(Person person, KeyValuePair<string, float> modifier)
        {
            string keyModifier = modifier.Key;
            float valueModifier = modifier.Value;

            #region Basic Attributes
            if(keyModifier == "strength")
            {
                modifyStrength(person, valueModifier);
            }
            else if(keyModifier == "dexterity")
            {
                modifyDexterity(person, valueModifier);
            }
            else if (keyModifier == "constitution")
            {
                modifyConstitution(person, valueModifier);
            }
            else if (keyModifier == "intelligence")
            {
                modifyIntelligence(person, valueModifier);
            }
            else if (keyModifier == "wisdom")
            {
                modifyWisdom(person, valueModifier);
            }
            else if (keyModifier == "charisma")
            {
                modifyCharisma(person, valueModifier);
            }
            #endregion

            #region Vitals
            else if (keyModifier == "age")
            {
                modifyAge(person, valueModifier);
            }
            else if (keyModifier == "health")
            {
                modifyHealth(person, valueModifier);
            }
            else if (keyModifier == "stamina")
            {
                modifyStamina(person, valueModifier);
            }
            else if (keyModifier == "satiation")
            {
                modifySatiation(person, valueModifier);
            }
            else if (keyModifier == "hydration")
            {
                modifyHydration(person, valueModifier);
            }
            else if (keyModifier == "mood")
            {
                modifyMood(person, valueModifier);
            }
            else if (keyModifier == "stamina")
            {
                modifyStamina(person, valueModifier);
            }
            #endregion

        }

        public bool givenStatBlock(Person person, Dictionary<string, float> values)
        {
            bool containsMonetary = values.ContainsKey("wealth");

            if (containsMonetary)
            {
                bool transactionMade = false;
                transactionMade = modifyWealth(person, values["wealth"]);
                if (transactionMade)
                {
                    foreach (var item in values)
                    {
                        if (item.Key != "wealth")
                        {
                            redirectToModifier(person, item);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in values)
                {
                    redirectToModifier(person, item);
                }
            }
            return true;
        }


        #region Modify Basic Attributes
        public bool modifyStrength(Person person, float modifier)
        {
            if (person.Strength + (int)modifier <= 20 && person.Strength + (int)modifier >= 0)
            {
                person.Strength = person.Strength + (int)modifier < 0 ? 0 : 20;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool modifyDexterity(Person person, float modifier)
        {
            if (person.Dexterity + (int)modifier <= 20 && person.Dexterity + (int)modifier >= 0)
            {
                person.Dexterity += (int)modifier;
                return true;
            }
            else
            {
                person.Dexterity = person.Dexterity + (int)modifier < 0 ? 0 : 20;
                return false;
            }
        }

        public bool modifyConstitution(Person person, float modifier)
        {
            if (person.Constitution + (int)modifier <= 20 && person.Constitution + (int)modifier >= 0)
            {
                person.Constitution += (int)modifier;
                return true;
            }
            else
            {
                person.Constitution = person.Constitution + (int)modifier < 0 ? 0 : 20;
                return false;
            }
        }

        public bool modifyIntelligence(Person person, float modifier)
        {
            if (person.Intelligence + (int)modifier <= 20 && person.Intelligence + (int)modifier >= 0)
            {
                person.Intelligence += (int)modifier;
                return true;
            }
            else
            {
                person.Intelligence = person.Intelligence + (int)modifier < 0 ? 0 : 20;
                return false;
            }
        }
        public bool modifyWisdom(Person person, float modifier)
        {
            if (person.Wisdom + (int)modifier <= 20 && person.Wisdom + (int)modifier >= 0)
            {
                person.Wisdom += (int)modifier;
                return true;
            }
            else
            {
                person.Wisdom = person.Wisdom + (int)modifier < 0 ? 0 : 20;
                return false;
            }
        }



        public bool modifyCharisma(Person person, float modifier)
        {
            if (person.Charisma + (int)modifier <= 20 && person.Charisma + (int)modifier >= 0)
            {
                person.Charisma += (int)modifier;
                return true;
            }
            else
            {
                person.Charisma = person.Charisma + (int)modifier < 0 ? 0 : 20;
                return false;
            }
        }
        #endregion

        #region Vitals

        public bool modifyAge(Person person, float modifier)
        {
            int modifiedAge = person.Age[1] + (int)modifier;

            if (modifiedAge > 365)
            {
                person.Age[1] = 0;
                person.Age[0]++;
                if (person.Age[0] > 120)
                {
                    person.Age[0] = 120;
                    person.IsAlive = false;
                }
            }
            if (modifiedAge < 0)
            {
                person.Age[1] = 365;
                person.Age[0]--;
                if (person.Age[0] < 0)
                {
                    person.Age[0] = 0;
                    person.IsAlive = false;
                }

            }
            return true;
        }

        public bool modifyHealth(Person person, float modifier)
        {
            float modifiedHealth = person.Health + modifier;
            if (modifiedHealth <= 0)
            {
                person.Health = 0;
                person.IsAlive = false;
            }
            else
            {
                person.Health = modifiedHealth <= person.MaxHealth ? modifiedHealth : person.MaxHealth;
            }
            return true;
        }

        public bool modifyStamina(Person person, float modifier)
        {
            float modifiedStamina = person.Stamina + modifier;
            if (modifiedStamina <= 0)
            {
                modifyHealth(person, modifiedStamina);
                person.Stamina = 0;
            }
            else
            {
                person.Stamina = modifiedStamina <= person.MaxStamina ? modifiedStamina : person.MaxStamina;
            }
            return true;
        }

        public bool modifySatiation(Person person, float modifier)
        {
            float modifiedSatiation = person.Satiation + modifier;
            if (modifiedSatiation <= 0)
            {
                modifyStamina(person, modifiedSatiation);
                person.Satiation = 0;
            }
            else
            {
                person.Satiation = modifiedSatiation <= person.MaxSatiation ? modifiedSatiation : person.MaxSatiation;
            }
            return true;
        }

        public bool modifyHydration(Person person, float modifier)
        {
            float modifiedHydration = person.Hydration + modifier;
            if (modifiedHydration <= 0)
            {
                modifyStamina(person, modifiedHydration);
                person.Hydration = 0;
            }
            else
            {
                person.Hydration = modifiedHydration <= person.MaxHydration ? modifiedHydration : person.MaxHydration;
            }
            return true;
        }

        public bool modifyMood(Person person, float modifier)
        {
            float modifiedMood = person.Mood + modifier;
            if (modifiedMood <= 0)
            {
                modifySatiation(person, modifiedMood);
                person.Mood = 0;
            }
            else
            {
                person.Mood = modifiedMood <= person.MaxMood ? modifiedMood : person.MaxMood;
            }
            return true;
        }

        #endregion



        public bool modifyWealth(Person person, float modifier)
        {
            float modifiedWealth = person.Wealth + modifier;
            if (modifiedWealth < 0)
            {
                return false;
            }
            else
            {
                person.Wealth = modifiedWealth;
                return true;
            }

        }
    }
}
