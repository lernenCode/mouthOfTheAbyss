using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveManager : MonoBehaviour
{   
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject support;
    public void SavePlayer ()
    {
        saveSystem.Save(this);
    }

    public void LoadPlayer ()
    {
        // Arquivo
        Data data = saveSystem.Load();

        // Scena
        SceneManager.LoadScene(data.scene);

        // Status
        player_status.life = data.life;
        player_status.energy = data.energy;
        player_status.stamina = data.stamina;

        // Atualizar UI
        player_UI.barLife.fillAmount = player_status.life / 100;
        player_UI.barEnergy.fillAmount = player_status.energy / 100;
        player_UI.barStamina.fillAmount = player_status.stamina / 100;

        // Bools
        player_status.isDie = data.isDie;

        // Position player
        Vector2 positionPlayer;
        positionPlayer.x = data.playerPosition[0];
        positionPlayer.y = data.playerPosition[1];
        player.transform.position = positionPlayer;

        // Position Support
        Vector2 positionSupport;
        positionSupport.x = data.supportPosition[0];
        positionSupport.y = data.supportPosition[1];
        support.transform.position = positionSupport;

        if(player_status.isDie == false)
        {
            Support_Physics2D.boxCol.isTrigger = true;
            Support_Physics2D.ResetVelocity();
        }
    }
}
