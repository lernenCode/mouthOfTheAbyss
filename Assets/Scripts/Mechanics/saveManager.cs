using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private bool loading = false;
    private Vector3 positionPlayer;

    private void Update()
    {
        if (loading == true)
        {
            // garantir que ta tudo ok 
            if (player.transform.position != positionPlayer) { player.transform.position = positionPlayer; } else { loading = false; }
        }
    }

    public void SavePlayer()
    {
        saveSystem.Save(this);
    }

    public void LoadPlayer()
    {
        // Arquivo
        Data data = saveSystem.Load();

        loading = true;

        // Load Scenes
        foreach (int index in data.scenesAlreadyLoaded)
        {
            if (!LoadScenes.scenesAlreadyLoaded.Contains(index))
            {
                SceneManager.LoadScene(index, LoadSceneMode.Additive);
                LoadScenes.scenesAlreadyLoaded.Add(index);
            }
        }

        // Status
        player_status.life = data.life;
        player_status.energy = data.energy;
        player_status.stamina = data.stamina;

        // Atualizar UI
        player_UI.barLife.fillAmount = player_status.life / 100;
        player_UI.barEnergy.fillAmount = player_status.energy / 100;
        player_UI.barStamina.fillAmount = player_status.stamina / 100;

        // Bools
        player_status.isDie = false;

        // Position player
        positionPlayer.x = data.playerPosition[0];
        positionPlayer.y = data.playerPosition[1];
        positionPlayer.z = data.playerPosition[2];
        player.transform.position = positionPlayer;

    }
}
