using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support_Inputs : MonoBehaviour
{
    [Header("Inputs")]
    public static bool InputRight;
    public static bool InputLeft;
    public static bool InputDown;
    public static bool InputUp;
    public static bool InputDash;

    [Header("Manager")]
    public static bool canMove = true;
    public static bool OlhandoDireita = true;

    void Update()
    {
        if (canMove && player_status.isDie == true)
        {
            // InputDash
            if (Input.GetButtonDown("Dash"))
            {
                InputDash = true;
            }
            else { InputDash = false; }

            // InputLeft
            if (Input.GetButton("InputLeft") && InputRight == false)
            {
                InputLeft = true;
            }
            else { InputLeft = false; }

            // InputRight
            if (Input.GetButton("InputRight") && InputLeft == false)
            {
                InputRight = true;
            }
            else { InputRight = false; }

            // InputUp
            if (Input.GetButton("InputUp") && InputDown == false)
            {
                InputUp = true;
            }
            else { InputUp = false; }

            // InputDown
            if (Input.GetButton("InputDown") && InputUp == false)
            {
                InputDown = true;
            }
            else { InputDown = false; }

            #region Flip
            // Só pode fazer isso se
            if(player_status.energy > 0)
            {
                if (InputRight)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                    OlhandoDireita = true;
                }
                if (InputLeft)
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                    OlhandoDireita = false;
                }
            }
            #endregion
        }
    }
}
