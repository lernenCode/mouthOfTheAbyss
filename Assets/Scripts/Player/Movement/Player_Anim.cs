using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Anim : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update() 
    {
        // VelX
        float velX = Player_Physics2D.corpoDoPersonagem.velocity.x;
        if (velX < 0.1 && velX > -0.1f) 
        {velX = 0;}
        anim.SetFloat("vel.X", Mathf.Abs(velX));

        // VelY
        float velY = Player_Physics2D.corpoDoPersonagem.velocity.y;
        if (velY < 0.1 && velY > -0.1f) 
        {velY = 0;}
        anim.SetFloat("vel.Y", velY);

        // isGroud
        if(Player_CheckColision.isGround || Player_CheckColision.isPlatformGrounded)
        {anim.SetBool("isGround", true);} else {anim.SetBool("isGround", false);}

        // isWall
        if(Player_CheckColision.isWall || Player_CheckColision.isPlatformLeft || Player_CheckColision.isPlatformRight)
        {anim.SetBool("isWall", true);} else {anim.SetBool("isWall", false);}

        // isCharge
        if(Player_Carried.HolderItem != null)
        {anim.SetBool("charge", true);} else {anim.SetBool("charge", false);}
    }
}