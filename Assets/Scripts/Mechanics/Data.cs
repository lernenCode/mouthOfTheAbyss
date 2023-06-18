using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Data
{
    public int life;
    public float energy;
    public float stamina;
    public float[] playerPosition;
    public float[] supportPosition;
    public List<int> scenesAlreadyLoaded;

    public Data (saveManager player)
    {
        scenesAlreadyLoaded = LoadScenes.scenesAlreadyLoaded;

        life = player_status.life;
        energy = player_status.energy;
        stamina = player_status.stamina;

        playerPosition = new float[3];
        playerPosition[0] = player_lastPosition.lastPosition.x;
        playerPosition[1] = player_lastPosition.lastPosition.y;
        playerPosition[2] = player_lastPosition.lastPosition.z;

        supportPosition = new float[3];
        supportPosition[0] = player_lastPosition.lastPositionSupport.x;
        supportPosition[1] = player_lastPosition.lastPositionSupport.y;
        supportPosition[2] = player_lastPosition.lastPositionSupport.z;
    }
}
