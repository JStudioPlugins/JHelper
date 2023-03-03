using Rocket.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHelper
{
    public static class JHelper
    {
        public static bool Debug = false;

        public static void DebugLog(string log)
        {
            if (Debug)
            {
                Logger.Log(log);
            }
        }
    }
}
