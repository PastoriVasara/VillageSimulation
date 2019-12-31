using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Village_Simulation
{
    class Program
    {
        static void Main(string[] args)
        {
            MainThread gameLoop = new MainThread();
            gameLoop.startVillage();
        }
    }
}
