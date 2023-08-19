using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JHelper.JStructures
{
    public static class StructureManagerExtensions
    {
        public static Transform GetStructureTransform(this StructureManager manager, Player player, out StructureData structureData)
        {
            structureData = null;
            RaycastHit hit;
            Ray ray = new Ray(player.look.aim.position, player.look.aim.forward);
            if (Physics.Raycast(ray, out hit, 3, RayMasks.STRUCTURE_INTERACT))
            {
                StructureDrop drop = StructureManager.FindStructureByRootTransform(hit.transform);
                if (drop != null)
                {
                    structureData = drop.GetServersideData();
                    return drop.model;
                }
            }

            return null;
        }
    }
}
