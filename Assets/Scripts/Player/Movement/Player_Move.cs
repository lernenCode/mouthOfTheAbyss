using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField] private float deccelaration;
    [SerializeField] private float acceleration;
    [SerializeField] private float debuffAcceleration;
    [SerializeField] private float debuffSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float velPower;
    [SerializeField] private float accelInAir;
    [SerializeField] private float deccelInAir;


    private float startSpeed;
    private float startAcelerations;

    [Header("inspecionar")]
    public float runMaxSpeed;
    public float runAcceleration;
    public float runDecceleration;
    public float runAccelAmount;
    public float runDeccelAmount;
    public float targetSpeed;
    public float speedDif;
    public float accelRate;
    public float movement;

    private void Start()
    {
        startSpeed = moveSpeed;
        startAcelerations = acceleration;
        // run tbm precisa colocar aqui dps
    }
    private void Update()
    {
        if (Player_Carried.HolderItem != null)
        {
            moveSpeed = debuffSpeed;
            acceleration = debuffAcceleration;
        }

        else
        {
            moveSpeed = startSpeed;
            acceleration = startAcelerations;
        }
    }
    private void FixedUpdate() // Uma vez que cada segundo | Operações relacionadas à física | Consistente
    {
        calculate();

        #region MoveHorizontal
        // Fazer movimento
        if (Player_Input.InputRight && Player_Input.canMove == true || Player_Input.InputLeft && Player_Input.canMove == true)
        {
            if (Player_Dash.runningDash == false && Player_Rope.drawingRope == false && Player_WallMove.isJumpWall == false && player_status.isDie == false)
            {
                Player_Physics2D.corpoDoPersonagem.AddForce(movement * Vector2.right, ForceMode2D.Force);
            }
        }
        #endregion
    }

    public void calculate()
    {
        // Conservar movimento
        if (Mathf.Abs(Player_Physics2D.corpoDoPersonagem.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(Player_Physics2D.corpoDoPersonagem.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && Player_Input.coyoteTimeCounter < 0)
        { accelRate = 0; }

        //Calcular as forças de desaceleração 
        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
        runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);

        //Calcular as forças de aceleração 
        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
        runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;

        // Calcular movimento
        targetSpeed = Player_Physics2D.Direction.x * runMaxSpeed;
        speedDif = targetSpeed - Player_Physics2D.corpoDoPersonagem.velocity.x;
        accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deccelaration;
        movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        // Parar movimento
        if (Player_Input.coyoteTimeCounter > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
        else accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount * accelInAir : runDeccelAmount * deccelInAir;
    }
}
