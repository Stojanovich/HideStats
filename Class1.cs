using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace StatsManagerPlugin
{
    public class StatsManager : RocketPlugin<StatsManagerConfiguration>
    {
        private Dictionary<ulong, PlayerStats> playerStats = new Dictionary<ulong, PlayerStats>();

        protected override void Load()
        {
            U.Events.OnPlayerConnected += OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerUpdateStamina += OnPlayerUpdateStamina;
            UnturnedPlayerEvents.OnPlayerUpdateFood += OnPlayerUpdateFood;
            UnturnedPlayerEvents.OnPlayerUpdateWater += OnPlayerUpdateWater;
            UnturnedPlayerEvents.OnPlayerUpdateVirus += OnPlayerUpdateVirus;
            Logger.Log($"{Name} v{Assembly.GetName().Version} has been loaded!");
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerUpdateStamina -= OnPlayerUpdateStamina;
            UnturnedPlayerEvents.OnPlayerUpdateFood -= OnPlayerUpdateFood;
            UnturnedPlayerEvents.OnPlayerUpdateWater -= OnPlayerUpdateWater;
            UnturnedPlayerEvents.OnPlayerUpdateVirus -= OnPlayerUpdateVirus;

            // Restore UI for all players
            foreach (var player in Provider.clients)
            {
                if (playerStats.ContainsKey(player.playerID.steamID.m_SteamID))
                {
                    EnableAllStats(player.player);
                }
            }

            Logger.Log($"{Name} has been unloaded!");
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            playerStats[player.CSteamID.m_SteamID] = new PlayerStats();
            ApplyStatVisibility(player.Player);
        }

        private void OnPlayerUpdateStamina(UnturnedPlayer player, byte stamina)
        {
            if (Configuration.Instance.InfiniteStats.Stamina && stamina < 100)
            {
                player.Player.life.serverModifyStamina(100);
            }
        }

        private void OnPlayerUpdateFood(UnturnedPlayer player, byte food)
        {
            if (Configuration.Instance.InfiniteStats.Food && food < 100)
            {
                player.Player.life.serverModifyFood(100);
            }
        }

        private void OnPlayerUpdateWater(UnturnedPlayer player, byte water)
        {
            if (Configuration.Instance.InfiniteStats.Water && water < 100)
            {
                player.Player.life.serverModifyWater(100);
            }
        }

        private void OnPlayerUpdateVirus(UnturnedPlayer player, byte virus)
        {
            if (Configuration.Instance.InfiniteStats.Virus && virus > 0)
            {
                player.Player.life.serverModifyVirus(0);
            }
        }

        private void ApplyStatVisibility(Player player)
        {
            if (Configuration.Instance.HideStats.Health)
                player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowHealth);
            if (Configuration.Instance.HideStats.Food)
                player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowFood);
            if (Configuration.Instance.HideStats.Water)
                player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowWater);
            if (Configuration.Instance.HideStats.Virus)
                player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowVirus);
            if (Configuration.Instance.HideStats.Stamina)
                player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowStamina);
            if (Configuration.Instance.HideStats.Oxygen)
                player.disablePluginWidgetFlag(EPluginWidgetFlags.ShowOxygen);
        }

        private void EnableAllStats(Player player)
        {
            player.enablePluginWidgetFlag(EPluginWidgetFlags.ShowHealth);
            player.enablePluginWidgetFlag(EPluginWidgetFlags.ShowFood);
            player.enablePluginWidgetFlag(EPluginWidgetFlags.ShowWater);
            player.enablePluginWidgetFlag(EPluginWidgetFlags.ShowVirus);
            player.enablePluginWidgetFlag(EPluginWidgetFlags.ShowStamina);
            player.enablePluginWidgetFlag(EPluginWidgetFlags.ShowOxygen);
        }
    }

    public class PlayerStats
    {
        public bool HasInfiniteFood { get; set; }
        public bool HasInfiniteWater { get; set; }
        public bool HasInfiniteVirus { get; set; }
        public bool HasInfiniteStamina { get; set; }
        public bool HasInfiniteOxygen { get; set; }
    }

    public class StatsManagerConfiguration : IRocketPluginConfiguration
    {
        public StatSettings InfiniteStats { get; set; }
        public StatSettings HideStats { get; set; }

        public void LoadDefaults()
        {
            InfiniteStats = new StatSettings
            {
                Health = false,
                Food = false,
                Water = false,
                Virus = false,
                Stamina = false,
                Oxygen = false
            };

            HideStats = new StatSettings
            {
                Health = false,
                Food = false,
                Water = false,
                Virus = false,
                Stamina = false,
                Oxygen = false
            };
        }
    }

    public class StatSettings
    {
        public bool Health { get; set; }
        public bool Food { get; set; }
        public bool Water { get; set; }
        public bool Virus { get; set; }
        public bool Stamina { get; set; }
        public bool Oxygen { get; set; }
    }
}
