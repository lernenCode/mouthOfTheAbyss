using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public static bool canAttack = true;
    public static bool lockAttack = false;
    public GameObject rangeAttack;
    public int damageThatDo;
    void Update()
    {
        if (Player_Dash.isDashing == false && playerDamage.inKnocback == false && Player_Carried.CrouchToPickUp == false
            && Player_Carried.Throwable == false && Player_Rope.drawingRope == false && player_status.isDie == false && Player_Carried.HolderItem == null)
        {
            if (Player_Input.InputAttack == true && canAttack == true)
            { canAttack = false; Debug.Log("Input"); }
        }

        else { FinishAttack(); }
    }

    public void StartAttack()
    {
        lockAttack = true;
        Player_Input.canMove = false;
        Debug.Log("Start");
    }

    public void Attack()
    {
        rangeAttack.SetActive(true);     
    }

    public void FinishAttack()
    {
        Debug.Log("Finish");
        rangeAttack.SetActive(false);    
        canAttack = true;
        lockAttack = false;
        Player_Input.canMove = true;
    }
}
