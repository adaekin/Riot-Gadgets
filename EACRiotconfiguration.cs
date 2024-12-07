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
        public List<ushort> tazerSilahları = new List<ushort>();
        public bool gason;
        public int tasedtime;
        public void LoadDefaults()
        {
            tazerSilahları = new List<ushort> { 1023, 1337 };
            tasedtime = 3;
            gason = false;

        }
    }
}
