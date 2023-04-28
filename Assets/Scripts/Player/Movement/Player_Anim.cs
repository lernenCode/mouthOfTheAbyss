using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Anim : MonoBehaviour
{
    public Animator anim;
    private AnimationState currentState;
    public enum AnimationState { Idle, Walk, Jump, Fall }

    void Start()
    {
        anim = GetComponent<Animator>();
          currentState = AnimationState.Idle;
    }

    void Update() 
    {
        // Walk
        float velX = Mathf.Abs(Player_Physics2D.corpoDoPersonagem.velocity.x);
        if (Mathf.Approximately(velX, 0)) {velX = 0;}
        if(velX > 0.1 && Player_CheckColision.isGround == true){ChangeAnimationState(AnimationState.Walk);}

        // Jump fall
        float velY = Player_Physics2D.corpoDoPersonagem.velocity.y;
        if (Mathf.Approximately(velY, 0)) {velY = 0;}
        if(Player_CheckColision.isGround == false)
        {
            if(velY > 0.1){ChangeAnimationState(AnimationState.Jump);}
            if(velY < -0.1){ChangeAnimationState(AnimationState.Fall);}
        }

        // Idle
        if(Mathf.Approximately(velY, 0) && Mathf.Approximately(velX, 0) && Player_CheckColision.isGround == true)
        {ChangeAnimationState(AnimationState.Idle);}



    }

    void ChangeAnimationState( AnimationState newState)
    {
         if (currentState == newState)
        {
            return;
        }


        anim.Play(newState.ToString().ToLower());
        currentState = newState;
    }


}