using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public partial class Building
    {
        int visitorCapacity;
        int workerCapacity;
        KeyValuePair<string, double>[] eventList;
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

        public Building()
        {
            workers = new List<Person>();
            visitors = new List<Person>();

            salary = 2;
            workerAmount = 0;
        }

        
    }
}
