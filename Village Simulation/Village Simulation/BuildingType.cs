using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Simulation
{
    public class BuildingType
    {
        //Leisure, Work
        string type;
        Random rnd = new Random(Guid.NewGuid().GetHashCode());
        //Bar, Library, Docks
        string place;
        string name;
        int visitorCapacity;
        int workerCapacity;
        int openingHour;
        int closingHour;
        int workHours;

        public string Place { get => place; set => place = value; }
        public string Name { get => name; set => name = value; }
        public int VisitorCapacity { get => visitorCapacity; set => visitorCapacity = value; }
        public int WorkerCapacity { get => workerCapacity; set => workerCapacity = value; }
        public int OpeningHour { get => openingHour; set => openingHour = value; }
        public int ClosingHour { get => closingHour; set => closingHour = value; }
        public int WorkHours { get => workHours; set => workHours = value; }

        public BuildingType(string aType, string aPlace)
        {
            name = aPlace;
            type = aType;
            Place = aPlace;
            workHours = 8;
            generateCapacity();
        }
        void generateCapacity()
        {
            if(type == "Work")
            {
                VisitorCapacity = 0;
                WorkerCapacity = rnd.Next(5, 25);
            }
            else
            {
                WorkerCapacity = rnd.Next(10, 30);
                VisitorCapacity = rnd.Next(15, 50);
            }
            if(Place == "Docks")
            {
                Name = "Docks";
                OpeningHour = 6;
                ClosingHour = 18;
            }
            else if (Place == "Library")
            {
                Name = "Library";
                OpeningHour = 8;
                ClosingHour = 20;
            }
            else if(Place == "Bar")
            {
                Name = "Bar";
                OpeningHour = 10;
                ClosingHour = 4;
            }

        }
        public bool action(Person visitor)
        {
            if(Place == "Library")
            {
                float moodGain = 2;
                visitor.modifyMood(moodGain);
                return true;
            }
            else if(Place == "Bar")
            {
                float thirstGain = 1;
                float moodGain = 1;
                float moneyGain = -2;
                bool afford = visitor.modifyMoney(moneyGain);

                if (afford)
                {
                    visitor.modifyThirst(thirstGain);
                    visitor.modifyMood(moodGain);
                }
                return true;
            }
            else
            {
                return false;
            }

            
        }
    }
}
