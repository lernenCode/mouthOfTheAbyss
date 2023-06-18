using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Anim : MonoBehaviour
{

    [Header("StateAnimations")]
    private Animator anim;
    private AnimationState currentState;
    public enum AnimationState { Attack, Dash, Death, Death_Carry, Fall, Fall_Carry, Hook, Hook_Engage, Hook_Initial, Hook_Up, Hook_Up_Grab, Hook_Up_Initial, Hurt, Idle, Idle_Carry, Jump, Jump_Carry, Lift, Lift_Down, Lower, Slide, Throw, Walk, Walk_Carry }

    [Header("ParameterForAnimations")]
    private float velY;
    private float velX;
    private bool isWall;
    private bool isGround;
    void Start()
    {
        anim = GetComponent<Animator>();
        currentState = AnimationState.Idle;
    }

    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            string debugMessage =
                "-=DEBUG LOG ALL=-" + "\n"

                + "Esta no chao: " + isGround + "\n"
                + "Esta na parede: " + isWall + "\n"
                + "Esta morto: " + player_status.isDie
                + "Quantidade de vida: " + player_status.life + "\n"
                + "Velocidade X: " + velX
                + "Velocidade Y: " + velY + "\n"
                + "Dash: " + Player_Dash.isDashing + "\n"
                + "Damage: " + playerDamage.isDamage + "\n"
                + "Knocback: " + playerDamage.inKnocback + "\n"
                + "drawingRope: " + Player_Rope.drawingRope + "\n"
                + "ropeAdvancing: " + Player_Rope.ropeAdvancing + "\n"
                + "ropeReturning: " + Player_Rope.ropeReturning + "\n"
                + "collidingRope: " + Player_Rope.collidingRope + "\n"
                + "HolderItem: " + Player_Carried.HolderItem + "\n"
                + "CrouchToPickUp: " + Player_Carried.CrouchToPickUp + "\n"
                + "Throwable: " + Player_Carried.Throwable + "\n"
                + "Throwablefinished: " + Player_Carried.Throwablefinished + "\n"

                + "";
            Debug.Log(debugMessage);
        }

        #region CallFunctions
        AnimationOffGround();
        AnimationOnGround();
        AnimationNoMatterWhere();
        #endregion

        #region ParameterForAnimation
        // Referenciar Valores

        velY = Player_Physics2D.corpoDoPersonagem.velocity.y;
        velX = Mathf.Abs(Player_Physics2D.corpoDoPersonagem.velocity.x);

        isGround = Player_CheckColision.isGround || Player_CheckColision.isPlatformGrounded;
        isWall = Player_CheckColision.isWall || Player_CheckColision.isPlatformLeft || Player_CheckColision.isPlatformRight;

        // Ajusta o valor para 0 se estiver próximo o suficiente
        if (Mathf.Approximately(velY, 0)) { velY = 0; }
        if (Mathf.Approximately(velX, 0)) { velX = 0; }
        #endregion
    }

    public void AnimationOffGround()
    {
        if (isGround == false)
        {
            if (Player_Dash.isDashing == false && playerDamage.inKnocback == false && Player_Carried.CrouchToPickUp == false
            && Player_Carried.Throwable == false && Player_Rope.drawingRope == false && player_status.isDie == false)
            {
                #region Jump
                if (velY > 0.1)
                {
                    if (Player_Carried.HolderItem != null) { ChangeAnimationState(AnimationState.Jump_Carry); }
                    else ChangeAnimationState(AnimationState.Jump);
                }
                #endregion

                #region Fall
                if (velY < -0.1 && Player_CheckColision.isWall == false)
                {
                    if (Player_Carried.HolderItem != null) { ChangeAnimationState(AnimationState.Fall_Carry); }
                    else ChangeAnimationState(AnimationState.Fall);
                }
                #endregion

                #region Slide
                if (Player_Carried.HolderItem == null)
                {
                    if (velY < -0.1 && isWall == true && Player_WallMove.isSliding == true)
                    { ChangeAnimationState(AnimationState.Slide); }
                }
                #endregion
            }
        }
    }
    public void AnimationOnGround()
    {
        if (isGround == true)
        {
            if (Player_Dash.isDashing == false && playerDamage.inKnocback == false && Player_Carried.CrouchToPickUp == false
            && Player_Carried.Throwable == false && Player_Rope.drawingRope == false && player_status.isDie == false)
            {
                #region Walk e Iddle

                if (velX > 0.1)
                {
                    if (Player_Carried.HolderItem != null) { ChangeAnimationState(AnimationState.Walk_Carry); }
                    else ChangeAnimationState(AnimationState.Walk);
                }

                if (velX < 0.1)
                {
                    if (Player_Carried.HolderItem != null) { ChangeAnimationState(AnimationState.Idle_Carry); }
                    else ChangeAnimationState(AnimationState.Idle);
                }
                #endregion
            }
        }
    }
    public void AnimationNoMatterWhere()
    {
        #region Death
        if (player_status.isDie == true)
        {
            if (Player_Carried.HolderItem != null)
            { ChangeAnimationState(AnimationState.Death_Carry); }
            else { ChangeAnimationState(AnimationState.Death); }
        }
        #endregion

        #region HookRope
        if (Player_Rope.drawingRope == true && player_status.isDie == false 
        && Player_Carried.CrouchToPickUp == false && Player_Carried.HolderItem == null)
        {
            // Initial position
            if (Player_Rope.finishInitialPose == false)
            {
                if (Player_Rope.ropeUp == true)
                { ChangeAnimationState(AnimationState.Hook_Up_Initial); }
                else { ChangeAnimationState(AnimationState.Hook_Initial); }
            }

            if (Player_Rope.finishInitialPose == true)
            {
                // Winding
                if (Player_Rope.collidingRope == false)
                {
                    if (Player_Rope.ropeUp == true)
                    { ChangeAnimationState(AnimationState.Hook_Up); }
                    else ChangeAnimationState(AnimationState.Hook);
                }

                // Pulling
                if (Player_Rope.collidingRope == true)
                {
                    if (Player_Rope.ropeUp == true)
                    { ChangeAnimationState(AnimationState.Hook_Up_Grab); }
                    else ChangeAnimationState(AnimationState.Hook_Engage);
                }
            }

        }
        #endregion

        #region PickUp
        if (Player_Carried.CrouchToPickUp == true && playerDamage.inKnocback == false 
        && player_status.isDie == false && Player_Rope.drawingRope == false)
        {
            if (Player_Input.InputDown == true)
            { ChangeAnimationState(AnimationState.Lift_Down); }
            else { ChangeAnimationState(AnimationState.Lift); }
        }
        #endregion

        #region Dash
        if (Player_Dash.isDashing == true && playerDamage.inKnocback == false && player_status.isDie == false)
        { ChangeAnimationState(AnimationState.Dash); }
        #endregion

        #region Hurt
        if (playerDamage.inKnocback == true && player_status.isDie == false)
        { ChangeAnimationState(AnimationState.Hurt); }
        #endregion

        #region Throwable
        if (Player_Carried.Throwable == true && playerDamage.inKnocback == false && player_status.isDie == false)
        { ChangeAnimationState(AnimationState.Throw); }
        #endregion
    }

    void ChangeAnimationState(AnimationState newState)
    {
        //Se o novo estado for o mesmo que o estado atual, não faz nada
        if (currentState == newState)
        { return; }

        //Inicia a animação do novo estado e atualiza o estado atual
        anim.Play(newState.ToString().ToLower());
        currentState = newState;
    }
}
