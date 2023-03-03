using Rocket.Core.Logging;
using Rocket.Core.Utils;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;

namespace JHelper.PlayerExtensions
{
    public static class FreezePlayerExtensions
    {
        public static void FreezePlayer(this Player player)
        {
            TaskDispatcher.QueueOnMainThread(() =>
            {
                player.movement.sendPluginJumpMultiplier(0);
                player.movement.sendPluginSpeedMultiplier(0);
                player.movement.sendPluginGravityMultiplier(0);
            });
            JHelper.DebugLog($"{player.channel.owner.playerID.characterName} was frozen!");
        }

        public static void UnfreezePlayer(this Player player, float gravity = 1f, float speed = 1f, float jump = 1f)
        {
            TaskDispatcher.QueueOnMainThread(() =>
            {
                player.movement.sendPluginJumpMultiplier(jump);
                player.movement.sendPluginSpeedMultiplier(speed);
                player.movement.sendPluginGravityMultiplier(gravity);
            });
            JHelper.DebugLog($"{player.channel.owner.playerID.characterName} was unfrozen!");
        }
    }
}
