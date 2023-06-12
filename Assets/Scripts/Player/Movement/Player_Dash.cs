using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dash : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float forceDash;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float takeEnergy;
    public static bool isDashing;
    public static bool dashInCooldown = false;
    public static bool runningDash = false;
    void Update()
    {
        // fazer o teste do dash
        if (Player_Input.InputDash == true && dashInCooldown == false && runningDash == false && player_status.energy >= takeEnergy)
        {
            // Tirar energia
            player_status.reduceEnergy(takeEnergy);
            
            // Executar dash
            isDashing = true;
        }

        // devolver o dash
        if (dashInCooldown == true)
        {
            // Iniciar contador⌚
            StartCoroutine(Player_IEnumerator.cooldownDash(dashCooldown));
        }
    }

    private void FixedUpdate()
    {
        // executar o Dash 
        if (isDashing == true)
        {
            // Iniciar contador⌚
            StartCoroutine(Player_IEnumerator.durationDash(dashDuration));
            
            // Falar que estou fazendo o Dash 0️⃣|1️⃣
            runningDash = true;

            // Executar Dash 🏃
            Player_Physics2D.corpoDoPersonagem.AddForce
            (new Vector2(forceDash * Player_Physics2D.Direction.x, forceDash * Player_Physics2D.Direction.y), ForceMode2D.Impulse);
        }
    }
}
