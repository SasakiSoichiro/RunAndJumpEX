using System;
using UnityEngine;

internal class RestartManagerEX
{
    private GameObject player;
    private GameObject text;

    public RestartManagerEX(GameObject player, GameObject text)
    {
        this.player = player;
        this.text = text;
    }

    internal bool IsGameOver()
    {
        throw new NotImplementedException();
    }

    internal void Restart()
    {
        throw new NotImplementedException();
    }
}