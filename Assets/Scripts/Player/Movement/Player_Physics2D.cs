using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Physics2D : MonoBehaviour
{
    public static Rigidbody2D corpoDoPersonagem;
    public static BoxCollider2D boxCol;
    public static GameObject playerGameObject;
    [SerializeField] private GameObject player;
    
    [Header("Move")]
    public static Vector2 Direction;

    private void Update()
    {
        #region Direction
        //ForceX parado 🧍
        if (Player_Input.InputRight == false && Player_Input.InputLeft == false
        && Player_Input.InputDown == false && Player_Input.InputUp == false)
        { if (Player_Input.OlhandoDireita) { Direction.x = 1; } else { Direction.x = -1; } }

        //ForceX em movimento 🏃
        if (Player_Input.InputRight == true) { Direction.x = 1; }
        else if (Player_Input.InputLeft == true) { Direction.x = -1; }
        else if (Player_Input.InputUp == true || Player_Input.InputDown == true) { Direction.x = 0; }

        //ForceY 🪂
        if (Player_Input.InputUp == true) { Direction.y = 1; }
        else if (Player_Input.InputDown == true) { Direction.y = -1; } else { Direction.y = 0; }

        #endregion

    }
    private void Start()
    {
        corpoDoPersonagem = GetComponent<Rigidbody2D>();
        playerGameObject = player;
        boxCol = GetComponent<BoxCollider2D>();
    }

    public static void ResetVelocity()
    {
        corpoDoPersonagem.velocity = Vector2.zero;
        corpoDoPersonagem.AddForce(Vector2.zero);
    }
}
