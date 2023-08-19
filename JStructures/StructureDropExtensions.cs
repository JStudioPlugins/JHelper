using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JHelper;

namespace JHelper.JStructures
{
    public static class StructureDropExtensions
    {
        public static HousingConnections GetHousingConnections(this StructureDrop drop)
        {
            try
            {
                var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                var field = typeof(StructureDrop).GetField("housingConnectionData", bindingFlags);
                return (HousingConnections)field.GetValue(drop);
            }
            catch (Exception ex)
            {
                JHelper.AdvancedLog(ex.Message);
                return null;
            }
        }
    }
}
