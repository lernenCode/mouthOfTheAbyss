using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Anim : MonoBehaviour
{
    public Animator anim;
    private AnimationState currentState; 
    public enum AnimationState {Attack, Dash, Death, Death_Carry, Fall, Fall_Carry, Hook, Hook_Engage, Hook_Initial, Hook_Up, Hook_Up_Grab, Hook_Up_Initial, Hurt, Idle, Idle_Carry, Jump, Jump_Carry, Lift, Lift_Down, Lower, Slide, Throw, Walk, Walk_Carry}

    void Start()
    {
        anim = GetComponent<Animator>(); 
        currentState = AnimationState.Idle; 
    }

    void Update() 
    {
        #region ParameterForAnimation
            //Pega a velocidade do objeto no eixo Y
            float velY = Player_Physics2D.corpoDoPersonagem.velocity.y;
            if (Mathf.Approximately(velY, 0)) {velY = 0;} //Ajusta o valor para 0 se estiver próximo o suficiente

            //Pega a velocidade do objeto no eixo X e pega o valor absoluto
            float velX = Mathf.Abs(Player_Physics2D.corpoDoPersonagem.velocity.x);
            if (Mathf.Approximately(velX, 0)) {velX = 0;} //Ajusta o valor para 0 se estiver próximo o suficiente

            //Referenciar se está no chao
            bool isGround = Player_CheckColision.isGround || Player_CheckColision.isPlatformGrounded;

            //Referenciar se está na parede
            bool isWall = Player_CheckColision.isWall || Player_CheckColision.isPlatformLeft ||  Player_CheckColision.isPlatformRight;
        #endregion

        #region AnimationOffGround 
            if(isGround == false)
            {
                if(Player_Dash.isDashing == false && playerDamage.isDamage == false && Player_Carried.CrouchToPickUp == false)
                {
                    #region Jump
                        if(velY > 0.1)
                        {
                            if(Player_Carried.HolderItem != null) {ChangeAnimationState(AnimationState.Jump_Carry);}
                            else ChangeAnimationState(AnimationState.Jump);
                        }
                    #endregion

                    #region Fall
                        if(velY < -0.1 && Player_CheckColision.isWall == false)
                        {
                            if(Player_Carried.HolderItem != null) {ChangeAnimationState(AnimationState.Fall_Carry);}
                            else ChangeAnimationState(AnimationState.Fall);
                        }
                    #endregion

                    #region Slide
                        if(velY < -0.1  && isWall == true )
                        {ChangeAnimationState(AnimationState.Slide);}
                    #endregion
                }
            }
        #endregion

        #region AnimationOnGround
            if(isGround == true)
            {   
                if(Player_Dash.isDashing == false && playerDamage.isDamage == false && Player_Carried.CrouchToPickUp == false)
                {
                    #region Walk e Iddle

                        if (velX > 0.1)
                        {
                            if(Player_Carried.HolderItem != null) {ChangeAnimationState(AnimationState.Walk_Carry);}
                            else ChangeAnimationState(AnimationState.Walk);
                        }

                        if (velX < 0.1)
                        {
                            if(Player_Carried.HolderItem != null) {ChangeAnimationState(AnimationState.Idle_Carry);}
                            else ChangeAnimationState(AnimationState.Idle);
                        }
                    #endregion
                }
            }
        #endregion

        #region AnimationNoMatterWhere

            #region PickUp
                if(Player_Carried.CrouchToPickUp == true && playerDamage.isDamage == false)
                {ChangeAnimationState(AnimationState.Lift);}
            #endregion

            #region Dash
                if(Player_Dash.isDashing == true && playerDamage.isDamage == false)
                {ChangeAnimationState(AnimationState.Dash);}
            #endregion

            #region Hurt
                if(playerDamage.isDamage == true) 
                {ChangeAnimationState(AnimationState.Hurt);}
            #endregion

        #endregion

        if(Input.GetKeyDown("k"))
        {
            Debug.Log(velX);
            Debug.Log(velY);
            Debug.Log(isGround);
            Debug.Log(isWall);
        }
    }

    void ChangeAnimationState( AnimationState newState)
    {
         //Se o novo estado for o mesmo que o estado atual, não faz nada
         if (currentState == newState)
        {
            return;
        }

        //Inicia a animação do novo estado e atualiza o estado atual
        anim.Play(newState.ToString().ToLower());
        currentState = newState;
    }
}
