using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotGadgets
{
    public class EACRiotconfiguration : IRocketPluginConfiguration
    {
        public List<ushort> TaserWeapons = new List<ushort>();
        public bool Tear_Gas;
        public int Neutralized_Time;
        public void LoadDefaults()
        {
            TaserWeapons = new List<ushort> { 1023, 1337 };
            Neutralized_Time = 3;
            Tear_Gas = false;

        }
    }
}
