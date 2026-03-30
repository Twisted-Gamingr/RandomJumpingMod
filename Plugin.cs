using BepInEx;
using BepInEx.Logging;
using System;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Utilla;
using Utilla.Attributes;

namespace MyFirstPlugin;

[BepInPlugin("com.TwistedGaming.RandomJumpMod", "RandomJumpMod", "0.0.2")]
[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")] // Make sure to add Utilla 1.5.0 as a dependency!
[ModdedGamemode] // Enable callbacks in default modded gamemodes
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    private Rigidbody Player;

    bool inAllowedRoom = false;

    private float Timer;

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
                Timer += Time.deltaTime;
                if (Timer >= 10)
                {
                    Player.linearVelocity = new UnityEngine.Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(1, 10), UnityEngine.Random.Range(-10, 10));
                    Timer = 0;
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
