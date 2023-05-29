using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_status : MonoBehaviour
{
    [Header("Manager points")]
    public static int life = 3;
    public static float energy = 100;
    public static float stamina = 100;

    [Header("Manager Death")]
    public static bool isDie;

    [Header("Manage Damage")]
    public static bool recovery = true;

    private void Update() 
    {
        #region recuperar stamina
        if(stamina < 100 && Player_CheckColision.isGround == true 
        || stamina < 100 && Player_CheckColision.isPlatformGrounded)
        { player_status.addStamina(100); player_UI.barStamina.fillAmount = stamina / 100; }
        #endregion

        #region canDamageTimer
        if(recovery == true) { StartCoroutine(Player_IEnumerator.canDamageTime());}
        #endregion
    }

    #region Manager Ui LifeBar

    #region addLife
        public static void addLife(int value)
        {

            life = value;
            player_UI.barLife.fillAmount = life / 100;
        
            // Evitar extrapolar
            if (life > 100)
            {
                life = 100;
            }
        }
    #endregion

    #region reduceLife
        public static void reduceLife(int value)
        {
            if (life > 0)
            {
                //Evitar dano consecutivo 
                if(recovery == false)
                {
                    life -= value;
                    player_UI.barLife.fillAmount = life / 100;
                    recovery = true;
                }
            }

            if(life <= 0)
            {
                isDie = true;
            }
        }
    #endregion

    #endregion

    #region Manager Ui EnergyBar

        #region addEnergy
            public static void addEnergy(float value)
            {
                energy += value;
                player_UI.barEnergy.fillAmount = energy / 100;
                
                // Evitar extrapolar
                if (energy > 100)
                {
                    energy = 100;
                }
            }
        #endregion

        #region reduceEnergy
            public static void reduceEnergy(float value)
            {
                if (energy > 0)
                {
                    energy -= value;
                    player_UI.barEnergy.fillAmount = energy / 100;
                }

                // Evitar extrapolar
                if (energy < 0)
                {
                    energy = 0;
                }
            }
        #endregion

    #endregion

    #region Manager Ui StaminBar

        #region addStamina
            public static void addStamina(float value)
            {
                stamina += value;
                player_UI.barStamina.fillAmount = stamina / 100;

                // Evitar extrapolar
                if (stamina > 100)
                {
                    stamina = 100;
                }
            }
        #endregion

        #region reduceStamina
            public static void reduceStamina(float value)
            {
                if (stamina > 0)
                {
                    stamina -= value;
                    player_UI.barStamina.fillAmount = stamina / 100;
                }
            }
        #endregion

    #endregion
}
