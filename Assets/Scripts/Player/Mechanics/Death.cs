using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    void Update()
    {
        if(player_status.energy <= 0 && player_status.isDie == true)
        {/*Animação*/reload();}
    }

    void reload()
    {
        gameObject.GetComponent<saveManager>().LoadPlayer();
    }
}
