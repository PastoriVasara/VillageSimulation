using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Village_Simulation
{
     public class RollGeneration
    {
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        List<string> beginningPart;
        List<string> middlePart;
        List<string> endPart;
        List<string> surNames;

        // 10 Count
        string[] raceTemplate = new string[] { "Human", "Elf", "Dwarf", "Half-Elf", "Gnome",  "Halfling", "Tiefling", "Goblin", "Minotaur", "Dragonborn" };
        string[] races;

        public Random Rnd { get => rnd; set => rnd = value; }

        public RollGeneration()
        {
            beginningPart = new List<string>();
            middlePart = new List<string>();
            endPart = new List<string>();
            surNames = new List<string>();
            races = new string[100];
            initializeLists();
        }

        public float generateWealth(int intelligence)
        {
            float wealth = 0;

            wealth = Rnd.Next(0, 100);
            wealth = (10 / (float)intelligence) * wealth;
            return wealth;
        }

        private void initializeLists()
        {
            string rootPath = "D:/Ohjelmointi/VillageSimulation/data/";
            using (var reader = new StreamReader(rootPath+"FirstNames.csv"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    beginningPart.Add(values.Length > 0 ? values[0] : "");
                    middlePart.Add(values.Length > 1 ? values[1] : "");
                    endPart.Add(values.Length > 2 ? values[2] : "");
                }
            }
            using (var reader = new StreamReader(rootPath + "surnames.txt"))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    surNames.Add(line);
                }
            }
            int[] limits = { 40, 50, 60, 70, 75, 80, 85, 90, 96, 99 };
            for(int i = 0; i < races.Length; i++)
            {
                if(i <= limits[0])
                {
                    races[i] = raceTemplate[0];
                }
                if(i <= limits[1] && i > limits[0])
                {
                    races[i] = raceTemplate[1];
                }
                if (i <= limits[2] && i > limits[1])
                {
                    races[i] = raceTemplate[2];
                }
                if (i <= limits[3] && i > limits[2])
                {
                    races[i] = raceTemplate[3];
                }
                if (i <= limits[4] && i > limits[3])
                {
                    races[i] = raceTemplate[4];
                }
                if (i <= limits[5] && i > limits[4])
                {
                    races[i] = raceTemplate[5];
                }
                if (i <= limits[6] && i > limits[5])
                {
                    races[i] = raceTemplate[6];
                }
                if (i <= limits[7] && i > limits[6])
                {
                    races[i] = raceTemplate[7];
                }
                if (i <= limits[8] && i > limits[7])
                {
                    races[i] = raceTemplate[8];
                }
                if (i > limits[8])
                {
                    races[i] = raceTemplate[9];
                }
            }

        }

        public int[] rollAge()
        {
            int[] age = new int[2];
            double spot = Rnd.NextDouble();
            double[] limits = { 0.03, 0.3,0.4,0.6,0.85,0.9};
            int limitSpot = 0;
            for(int i = 0; i < limits.Length; i++)
            {
                if(spot <= limits[i])
                {
                    limitSpot = i;
                    break;
                }
            }
            int[][] ranges = new int[7][];
            ranges[0] = new int[2] { 1, 13 };
            ranges[1] = new int[2] { 13, 20 };
            ranges[2] = new int[2] { 20, 25 };
            ranges[3] = new int[2] { 25, 35 };
            ranges[4] = new int[2] { 35, 50 };
            ranges[5] = new int[2] { 50, 70 };
            ranges[6] = new int[2] { 70, 100 };

            age[0] = Rnd.Next(ranges[limitSpot][0], ranges[limitSpot][1]);
            age[1] = Rnd.Next(0, 365);

            return age;
        }

        public int[] rollStats()
        {
            int[] statArray = new int[6];
            for(int i = 0; i < statArray.Length; i++)
            {
                int[] dropLowest = new int[4];
                int sum = 0;
                for (int j = 0; j < dropLowest.Length; j++)
                {
                    dropLowest[j] = Rnd.Next(1, 7);
                    sum += dropLowest[j];
                }
                Array.Sort(dropLowest, new Comparison<int>(
                 (i1, i2) => i2.CompareTo(i1)));

                sum -= dropLowest[dropLowest.Length - 1];

                statArray[i] = sum;
            }
            return statArray;
        }

        public string[] rollName()
        {
      
            string[] fullName = new string[3];
            fullName[0] = beginningPart[Rnd.Next(0, 100)] + middlePart[Rnd.Next(0, 100)] + endPart[Rnd.Next(0, 100)];
            fullName[1] = surNames[Rnd.Next(0, surNames.Count)];
            fullName[2] = races[Rnd.Next(0, races.Length)];
            return fullName;
        }
    }


}
