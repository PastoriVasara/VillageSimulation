using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Village_Simulation
{
    public class MainThread
    {
        void jsonit(City city)
        {
            string json = JsonConvert.SerializeObject(city, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            System.IO.File.WriteAllText(@"D:\Ohjelmointi\VillageSimulation\data\people.json", json);
        }

        public void startVillage()
        {
            RollGeneration initializedRoller = new RollGeneration();
            City flint = new City("Flint",1000);




            waitForCommand();
        }
        public void waitForCommand()
        {
            Console.Read();
        }
    }
}
