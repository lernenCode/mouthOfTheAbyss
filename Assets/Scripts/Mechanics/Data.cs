using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public float life;
    public float energy;
    public float stamina;
    public float[] playerPosition;
    public float[] supportPosition;
    public bool isAlive;

    public Data (player_status player)
    {
        life = player_status.life;
        energy = player_status.energy;
        stamina = player_status.stamina;

        playerPosition = new float[2];
        playerPosition[0] = player_lastPosition.lastPosition.x;
        playerPosition[1] = player_lastPosition.lastPosition.y;
    }
}
