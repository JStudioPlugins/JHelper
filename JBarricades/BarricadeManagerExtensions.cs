using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JHelper;
using SDG.Unturned;
using Steamworks;
using UnityEngine;

namespace JHelper.JBarricades
{
    public static class BarricadeManagerExtensions
    {
        public static void UpdateStorageOwner(this BarricadeManager manager, SDG.Unturned.Barricade storage, Transform storageTransform, CSteamID owner, CSteamID group)
        {
            byte[] state = storage.state;

            BitConverter.GetBytes(owner.m_SteamID).CopyTo(state, 0);
            BitConverter.GetBytes(group.m_SteamID).CopyTo(state, 8);

            BarricadeManager.updateReplicatedState(storageTransform, state, state.Count());
            BarricadeManager.changeOwnerAndGroup(storageTransform, owner.m_SteamID, group.m_SteamID);
        }

        public static void UpdateDoorOwner(this BarricadeManager manager, SDG.Unturned.Barricade door, Transform doorTransform, CSteamID owner, CSteamID group)
        {
            byte[] state = door.state;

            BitConverter.GetBytes(owner.m_SteamID).CopyTo(state, 0);
            BitConverter.GetBytes(group.m_SteamID).CopyTo(state, 8);
            BitConverter.GetBytes(false).CopyTo(state, 16);

            BarricadeManager.updateReplicatedState(doorTransform, state, state.Count());
            BarricadeManager.changeOwnerAndGroup(doorTransform, owner.m_SteamID, group.m_SteamID);
        }

        public static Transform GetBarricadeTransform(this BarricadeManager manager, Player player, out BarricadeData barricadeData, out BarricadeDrop drop)
        {
            barricadeData = null;
            drop = null;
            RaycastHit hit;
            Ray ray = new Ray(player.look.aim.position, player.look.aim.forward);
            if (Physics.Raycast(ray, out hit, 3, RayMasks.BARRICADE_INTERACT))
            {
                Transform transform = hit.transform;
                InteractableDoorHinge doorHinge = hit.transform.GetComponent<InteractableDoorHinge>();
                if (doorHinge != null)
                {
                    transform = doorHinge.door.transform;
                }

                drop = BarricadeManager.FindBarricadeByRootTransform(transform);
                if (drop != null)
                {
                    barricadeData = drop.GetServersideData();
                    return drop.model;
                }
            }

            return null;
        }
    }
}
