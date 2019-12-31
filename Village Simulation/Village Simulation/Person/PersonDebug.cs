using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public partial class Person
    {

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
                FirstName, LastName, Age[0], Age[1], race, wealth, Strength, Constitution, Dexterity, Wisdom, Charisma, Intelligence);
            Console.WriteLine("### Housing ### \n" +
                "House Name: {0}\n" +
                "House Capacity: {1}\n" +
                "House Food: {2}\n" +
                "House Rent: {3}\n" +
                "### End of Housing ### \n" +
                "-------------------------\n", home.HouseName, home.Capacity, home.FoodAmount, home.Rent);
        }

        public void printEventLog()
        {
            string separator = "#####################################";


            Console.WriteLine("\n{0}\n", separator);
            Console.WriteLine("Events of: {0} {1}", firstName, lastName);
            for (int i = 0; i < actionLog.Count; i++)
            {
                Console.WriteLine(actionLog[i]);
            }
            Console.WriteLine("\n{0}\n", separator);
        }

        #region Print Family Tree
        public void upFamilyTree()
        {
            if (Father == null && Mother == null)
            {
                Console.WriteLine("Family Name: " + lastName + "\n");
                downFamilyTree("-");
            }
            else
            {
                if (Father != null)
                {
                    Father.upFamilyTree();
                }
                else if (Mother != null)
                {
                    upFamilyTree();
                }

            }
        }

        public void downFamilyTree(string level)
        {
            string response = level + firstName + "\n";

            response += significantOther == null ? "" : level + significantOther.firstName;
            response += "\n";
            Console.WriteLine(response);
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].downFamilyTree(level += "-");
            }

        }
        #endregion
    }
}
