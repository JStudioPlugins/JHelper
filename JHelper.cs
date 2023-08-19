using Rocket.Core;
using Rocket.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDG.Unturned;
using SDG.Framework;
using System.Reflection;
using System.IO;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using JHelper.Components;
using Steamworks;
using Action = System.Action;
using CSharpDonateBot;
using HarmonyLib;

namespace JHelper
{
    public class JHelper : RocketPlugin
    {
        public static JHelper Instance { get; private set; }

        public static string CommitId = "79f81686b6ba9199897a9b75965cbf93ea03fa0";

        public static event Action OnLoad;

        public static event Action OnUnload;

        public static bool AdvancedLogging = true;

        public static void AdvancedLog(string log)
        {
            if (AdvancedLogging)
            {
                Task.Run(() =>
                {
                    List<string> lines = File.ReadAllLines("JStudioDebug.log").ToList();
                    lines.Add($"[{DateTime.Now:G}] {Assembly.GetCallingAssembly().GetName().Name} >> {log}");
                    File.WriteAllLines(Instance.Directory + "/JStudioDebug.log", lines);
                });
                
            }
        }

        public static void LogPluginStartup(string message = "")
        {
            Assembly assem = Assembly.GetCallingAssembly();

            Logger.Log($"Loaded {assem.GetName().Name} on version {assem.GetName().Version}");
            Logger.Log($"-- This plugin is property of JStudio. Support at: https://discord.gg/GdFDduWrWC --");
            if (AdvancedLogging) Logger.Log($"This plugin comes with advanced logging at {Instance.Directory + "/JStudioDebug.log"}");
            Logger.Log(message);
        }

        public DonationBotClient Client { get; private set; }

        protected override void Load()
        {
            Instance = this;
            InitPatches();
            OnLoad?.Invoke();
            U.Events.OnPlayerConnected += ManagePlayerConnected;
        }

        private void ManagePlayerConnected(UnturnedPlayer player)
        {
            OnUnload?.Invoke();
            player.Player.gameObject.AddComponent<JPlayer>();
        }

        protected override void Unload()
        {
            CleanupPatches();
        }

        public static Harmony Harmony;
        public const string HarmonyId = "Jdance.JHelper";

        public static void InitPatches()
        {
            try
            {
                Harmony = new Harmony(HarmonyId);
                Harmony.PatchAll();
            }
            catch (Exception e)
            {
                Logger.LogException(e);
            }
        }
        public static void CleanupPatches()
        {
            try
            {
                Harmony.UnpatchAll(HarmonyId);

            }
            catch (Exception e)
            {
                Logger.LogException(e);
            }
        }
    }
}
