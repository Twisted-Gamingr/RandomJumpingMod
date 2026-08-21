using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using System;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using Utilla;
using Utilla.Attributes;

namespace MyFirstPlugin;

[BepInPlugin("com.TwistedGaming.RandomJumpMod", "RandomJumpMod", "0.0.3")]
[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")] // Make sure to add Utilla 1.5.0 as a dependency!
[ModdedGamemode] // Enable callbacks in default modded gamemodes
public class Plugin : BaseUnityPlugin
{
    internal static ManualLogSource Logger;

    private Rigidbody Player;

    private bool inAllowedRoom;

    private float Timer = 0f;
    private float NextTime = 0f;

    // READ ME!
    // So i deleted the proj on my pc a long time ago and i pasted this code to something else and i dont have the original.
    // So this isnt really the code and this is just an example of what it looks like. I pasted the code from RandomJumpingModV2.

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
                NextTime = UnityEngine.Random.Range(1,15);
                if (Timer >= NextTime)
                {
                    Player.linearVelocity = new UnityEngine.Vector3(UnityEngine.Random.Range(-10, 10), UnityEngine.Random.Range(1, 10), UnityEngine.Random.Range(-10, 10));
                    Logger.LogInfo("Jumped! Prob didnt work.");
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
