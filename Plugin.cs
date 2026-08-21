using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using System;
using UnityEngine;
using Utilla;
using Utilla.Attributes;
using GorillaLocomotion;

namespace RandomJumpingModV2
{
    [BepInPlugin("com.TwistedGaming.RandomJumpModV2", "RandomJumpModV2", "0.0.2")]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")] // Make sure to add Utilla 1.5.0 as a dependency!
    [ModdedGamemode] // Enable callbacks in default modded gamemodes
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private Rigidbody Player;

        private bool inAllowedRoom = false;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;
            Logger.LogInfo($"Plugin {"com.TwistedGaming.RandomJumpingMod"} is loaded!");
        }

        private void Update()
        {
            if (inAllowedRoom)
            {
                Player = GameObject.Find("GorillaPlayer").GetComponent<Rigidbody>();
                if (Player)
                {
                    bool v = UnityInput.Current.GetKeyDown(KeyCode.Space);
                    if (v)
                    {
                        GTPlayer.Instance.playerRigidBody.linearVelocity = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(1f, 15f), UnityEngine.Random.Range(-10f, 10f));
                        Logger.LogInfo("Jumped! Applied velocity.");
                    }
                }
                else
                {
                    Logger.LogError("GorillaPlayer isnt found. :(");
                }
            }
        }

        [ModdedGamemodeJoin]
        private void RoomJoined(string gamemode)
        {
            // The room is modded. Enable mod stuff.
            inAllowedRoom = true;
        }

        [ModdedGamemodeLeave]
        private void RoomLeft(string gamemode)
        {
            // The room was left. Disable mod stuff.
            inAllowedRoom = false;
        }
    }
}
