using Rocket.API;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHelper.JPlayers
{
    public static class PlayerToolExtended
    {
        public static List<Tuple<CSteamID, byte>> GetPreviousPlayersFromSavedata()
        {
            List<Tuple<CSteamID, byte>> steamIDs = new List<Tuple<CSteamID, byte>>();
            string[] dir = Directory.GetDirectories(ServerSavedata.directory + "/Players", "", SearchOption.TopDirectoryOnly);
            
            foreach (string str in dir)
            {
                string[] split = str.Split('_');
                steamIDs.Add(new Tuple<CSteamID, byte>(new CSteamID(ulong.Parse(split[0])), byte.Parse(split[1])) );
            }

            return steamIDs;
        }

        public static bool GetCharacter(this CSteamID steamId, out byte characterId)
        {
            characterId = 0;
            string[] dir = Directory.GetDirectories(ServerSavedata.directory + "/Players", $"{steamId.m_SteamID}*", SearchOption.TopDirectoryOnly);

            foreach (string str in dir)
            {
                string[] split = str.Split('_');
                characterId = byte.Parse(split[1]);
                return true;
            }

            return false;
        }

        public static bool GetGroupData(this CSteamID steamId, byte characterId, out CSteamID groupId, out EPlayerGroupRank groupRank)
        {
            SteamPlayerID steamPlayerId = new SteamPlayerID(steamId, characterId, "", "", "", CSteamID.Nil);
            groupId = CSteamID.Nil;
            groupRank = EPlayerGroupRank.MEMBER;

            if (PlayerSavedata.fileExists(steamPlayerId, "/Player/Quests.dat") && Level.info.type == ELevelType.SURVIVAL)
            {
                River river = PlayerSavedata.openRiver(steamPlayerId, "/Player/Quests.dat", isReading: true);
                byte b = river.readByte();
                if (b > 0)
                {

                    if (b > 2)
                    {
                        groupId = river.readSteamID();
                    }
                    else
                    {
                        river.closeRiver();
                        return false;
                    }
                    if (b > 3)
                    {
                        groupRank = (EPlayerGroupRank)river.readByte();
                    }
                }
                else
                {
                    river.closeRiver();
                    return false;
                }
                river.closeRiver();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool GetReputationData(this CSteamID steamId, byte characterId, out int reputation)
        {
            SteamPlayerID steamPlayerId = new SteamPlayerID(steamId, characterId, "", "", "", CSteamID.Nil);
            reputation = 0;

            if (PlayerSavedata.fileExists(steamPlayerId, "/Player/Skills.dat") && Level.info.type == ELevelType.SURVIVAL)
            {
                Block block = PlayerSavedata.readBlock(steamPlayerId, "/Player/Skills.dat", 0);
                byte b = block.readByte();
                if (b >= 7)
                {
                    reputation = block.readInt32();
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public static bool IsPlayer(this IRocketPlayer caller)
        {
            if (ulong.TryParse(caller.Id, out ulong s64) && new CSteamID(s64).GetEAccountType() == EAccountType.k_EAccountTypeIndividual)
                return true;
            else
                return false;
        }
    }
}
