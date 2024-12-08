using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using Rocket.Unturned.Chat;
using Rocket.Unturned;
using System;
using UnityEngine;
using System.Collections;

namespace RiotGadgets
{
    public class EACMain : RocketPlugin<EACRiotconfiguration>
    {
        protected override void Load()
        {
            Rocket.Core.Logging.Logger.Log("Riot Gadgets made by Ekin");
            Rocket.Core.Logging.Logger.Log("Riot Gadgets loaded");
            DamageTool.damagePlayerRequested += mydamagetool;
            U.Events.OnPlayerConnected += onplayerjoined;

        }

        private void mydamagetool(ref DamagePlayerParameters parameters, ref bool shouldAllow)
        {
            bool isplayer = true;
            Player victim = (Player)parameters.player;
            UnturnedPlayer killer;
            try
            {
                killer = UnturnedPlayer.FromCSteamID((CSteamID)parameters.killer.m_SteamID);
                string zattirizort = killer.CharacterName; //Does nothing, just for gives error
            }
            catch (Exception ex)
            {
                isplayer = false;
            }
            finally
            {
                if (isplayer)
                {
                    getsdamage(victim, parameters.killer, parameters.damage);
                }
            }
            
        }
        public void getsdamage(Player player, CSteamID killer, float damage)
        {
  
            UnturnedPlayer player1 = UnturnedPlayer.FromPlayer(player);
            UnturnedPlayer val = UnturnedPlayer.FromCSteamID(killer);
            if (Configuration.Instance.TaserWeapons.Contains(val.Player.equipment.itemID))
            {
                if (player1.IsInVehicle)
                {
                    VehicleManager.forceRemovePlayer(player1.CSteamID);
                }
                
                player1.Player.equipment.dequip();
                player1.Player.stance.channel.send("tellStance", (ESteamCall)1, (ESteamPacket)15, new object[1] { 5 });
                player1.Player.animator.sendGesture((EPlayerGesture)6, true);
                player1.Player.life.askHeal((byte)(player1.Player.life.health + damage), true, true);
                StartCoroutine(zaman(player1));
            }
            else
            {
                return;
            }
        }
        public IEnumerator zaman(UnturnedPlayer player1) //Checking Time
        {
            int count = 1;
            while (count <= Configuration.Instance.Neutralized_Time / 0.2f)
            {
                yield return new WaitForSeconds(0.2f);
                player1.Player.equipment.dequip();
                player1.Player.stance.channel.send("tellStance", (ESteamCall)1, (ESteamPacket)15, new object[1] { 5 });
                player1.Player.animator.sendGesture((EPlayerGesture)6, true);
                count++;
            }
            yield break;
        }
        void onplayerjoined(UnturnedPlayer player) //Its about Gas
        {
            if (Configuration.Instance.Tear_Gas)
            {
                player.Player.life.onVirusUpdated += checkgas;
                void checkgas(byte virus)
                {
                    if (player.IsInVehicle)
                    {
                        VehicleManager.forceRemovePlayer(player.CSteamID);
                    }
                    player.Player.equipment.dequip();
                    player.Player.stance.channel.send("tellStance", (ESteamCall)1, (ESteamPacket)15, new object[1] { 5 });
                    player.Player.animator.sendGesture((EPlayerGesture)6, true);
                    player.Player.life.serverModifyVirus(100);
                }
            }

        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= onplayerjoined;
            Rocket.Core.Logging.Logger.Log("Riot Gadgets Unloaded");

        }
    }
    
}
