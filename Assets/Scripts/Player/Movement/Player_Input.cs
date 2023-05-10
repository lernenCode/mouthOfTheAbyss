using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Input : MonoBehaviour
{
    #region Variaveis

    [Header("Inputs")]
    public static bool InputRight;
    public static bool InputLeft;
    public static bool InputDown;
    public static bool InputUp;
    public static bool InputJump;
    public static bool InputWallJump;
    public static bool InputDash;
    public static bool InputGrab;
    public static bool InputCarry;
    public static bool InputRope;
    public static bool InputCarryUP;

    [Header("Manager")]
    public static bool canMove = true;
    public static bool OlhandoDireita = true;

    [Header("coyoteTimer")]
    [SerializeField] private float coyoteTime = 0.2f;
    public static float coyoteTimeCounter;
    #endregion

    void Update() // Uma vez por quadro | Operações logicas | Instavel 
    {
        #region Comentario 𝙶𝚎𝚝𝙱𝚞𝚝𝚝𝚘𝚗 | 𝙶𝚎𝚝𝙱𝚞𝚝𝚝𝚘𝚗Down | 𝙶𝚎𝚝𝙱𝚞𝚝𝚝𝚘𝚗Up
        /*
        𝙶𝚎𝚝𝙱𝚞𝚝𝚝𝚘𝚗	    Os retornos são verdadeiros enquanto o botão virtual identificado pelo botãoNo nome é mantido para baixo.
        𝙶𝚎𝚝𝙱𝚞𝚝𝚝𝚘𝚗𝙳𝚘𝚠𝚗	Retorna verdadeiro durante o quadro o usuário pressionou para baixo o botão virtual identificado pelo botãoNo.
        𝙶𝚎𝚝𝙱𝚞𝚝𝚝𝚘𝚗𝚄𝚙	  Retorna verdadeiro o primeiro quadro que o usuário libera o botão virtual identificado pelo botãoNo.
        */
        #endregion

        #region InputsMove
        if (canMove == true)
        {
            if (player_status.isDie == false)
            {
                // InputRope
                if (Input.GetButtonDown("Rope") && Player_Carried.HolderItem == null)
                {
                    InputRope = true;
                }
                else { InputRope = false; }

                // InputGrab
                if (Input.GetButton("Grab"))
                {
                    InputGrab = true;
                }
                else { InputGrab = false; }

                // InputCarry
                if (Input.GetButtonDown("Grab"))
                {
                    InputCarry = true;
                }
                else { InputCarry = false; }

                // InputCarry
                if (Input.GetButtonUp("Grab"))
                {
                    InputCarryUP = true;
                }
                else { InputCarryUP = false; }

                // InputJump
                if (Input.GetButtonDown("Jump") && coyoteTimeCounter > 0f)
                {
                    InputJump = true;
                }
                else { InputJump = false; }

                //CoyoteJump
                if (Player_CheckColision.isGround || Player_CheckColision.isPlatform)
                { coyoteTimeCounter = coyoteTime; }
                else { coyoteTimeCounter -= Time.deltaTime; }
                if (Input.GetButtonUp("Jump")) { coyoteTimeCounter = 0f; }

                // InputWallJump
                if (Input.GetButtonDown("Jump") && Player_CheckColision.isWall == true && Player_CheckColision.isGround == false 
                || Input.GetButtonDown("Jump") && Player_CheckColision.isPlatformLeft == true && Player_CheckColision.isPlatformGrounded == false
                || Input.GetButtonDown("Jump") && Player_CheckColision.isPlatformRight == true && Player_CheckColision.isPlatformGrounded == false)
                {
                    InputWallJump = true;
                }
                else { InputWallJump = false; }

                // InputDash
                if (Input.GetButtonDown("Dash") && Player_Carried.HolderItem == null)
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
                if (Player_Rope.drawingRope == false)
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
        #endregion
    }
}
