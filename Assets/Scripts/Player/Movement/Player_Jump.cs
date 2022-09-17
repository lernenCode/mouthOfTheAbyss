using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Jump : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float debuffJump;
    private bool jumpRequest;
    private float JumpStart;

    private void Start() { JumpStart = jumpVelocity; }
    void Update()
    {
        if (Player_Input.InputJump)
        {
            jumpRequest = true;
        }

        if (Player_Carried.HolderItem != null)
        {
            jumpVelocity = debuffJump;
        }
        else { jumpVelocity = JumpStart; }

    }
    void FixedUpdate()
    {
        if (jumpRequest == true)
        {
            Player_Physics2D.ResetVelocity();
            Player_Physics2D.corpoDoPersonagem.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
    }
}
