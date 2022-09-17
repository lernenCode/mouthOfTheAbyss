using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support_Physics2D : MonoBehaviour
{
    public static Rigidbody2D corpoDoPersonagem;
    [Header("Move")]
    public static Vector2 Direction;
    public static BoxCollider2D boxCol;
  private void Start()
    {
        corpoDoPersonagem = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
   private void Update()
    {
        #region Direction
        //ForceX 🏃 parado 🧍
        if (Support_Inputs.InputRight == false && Support_Inputs.InputLeft == false)
        { if (Support_Inputs.OlhandoDireita) { Direction.x = 1; } else { Direction.x = -1; } }

        //ForceX em movimento 🏃
        if (Support_Inputs.InputRight == true) { Direction.x = 1; }
        else if (Support_Inputs.InputLeft == true) { Direction.x = -1; }

        //ForceY 🪂 parado 🧍
        if (Support_Inputs.InputDown == false && Support_Inputs.InputUp == false)
        { Direction.y = 0; }

        //ForceY 🪂
        if (Support_Inputs.InputUp == true) { Direction.y = 1; }
        else if (Support_Inputs.InputDown == true) { Direction.y = -1; }

        #endregion

    }
    public static void ResetVelocity()
    {
        corpoDoPersonagem.velocity = Vector2.zero;
        corpoDoPersonagem.AddForce(Vector2.zero);
    }
}
