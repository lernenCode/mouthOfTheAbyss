using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rope : MonoBehaviour
{
    [Header("Components")]
    private LineRenderer lineRenderer;

    [Header("Controlers")]
    public static bool ropeAdvancing;
    public static bool ropeReturning;
    public static bool collidingRope;
    public static bool pullingRope;
    public static bool drawingRope;
    public static bool ropeUp;
    public static bool finishInitialPose;

    [Header("Layers")]
    [SerializeField] private LayerMask whatIsColision;
    [SerializeField] private LayerMask whatIsCatchable;
    [SerializeField] private LayerMask whatIsPlatform;

    [Header("Variables Drawline")]
    [SerializeField] private Transform startingPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float ropeSpeed = 1f;

    [Header("Variables PlayerMove")]
    [SerializeField] private float moveSpeed;

    [Header("Variables Privadas")]
    private Vector3 pointA;
    private Vector3 pointB;
    private RaycastHit2D hit;
    private float ropePositionPercentage;
    private float distancePercent;

    [Header("Energy system")]
    [SerializeField] private float takeEnergy;


    void Start()
    { lineRenderer = GetComponent<LineRenderer>(); }

    private void Update()
    {
        // Condição para iniciar 
        if (Player_Input.InputRope == true && drawingRope == false && player_status.energy >= takeEnergy && Player_CheckColision.isWall == false && playerDamage.isDamage == false && Player_Carried.CrouchToPickUp == false && Player_Carried.HolderItem == null)
        { drawingRope = true; if (Player_Input.InputUp == true) { ropeUp = true; } else { ropeUp = false; } }
    }
    
    void FixedUpdate()
    {
        // Desenhar
        if (finishInitialPose == true)
        { drawLine(); }

        // Movimento
        if (collidingRope == true)
        { ropeMove(); }

        // Reiniciar a corda se o ciclo estiver completo
        if (ropeReturning == true && ropePositionPercentage <= 0 || playerDamage.isDamage == true)
        { restart(); }

        // Cancelar gravidade e movimento
        if (drawingRope == true && collidingRope == false)
        {
            Player_Physics2D.ResetVelocity();
            Player_Physics2D.corpoDoPersonagem.gravityScale = 0;
            Player_Physics2D.corpoDoPersonagem.AddForce(Vector2.up * 0, ForceMode2D.Impulse);
        }
    }

    void FinishInitialPose()
    {
        finishInitialPose = true;
        player_status.reduceEnergy(takeEnergy); 
    }

    void drawLine()
    {
        // Definir pontos
        pointA = startingPoint.position; // Posição do ponto A da corda
        pointB = endPoint.position; // Posição do ponto B da corda

        // Calcular
        distancePercent = Mathf.Lerp(0, maxDistance, ropePositionPercentage); // Calcula a posição da corda na distância máxima baseado na porcentagem de avanço
        Vector3 pointAlongLine = distancePercent * Vector3.Normalize(pointB - pointA) + pointA; // Calcula a posição atual da corda ao longo da linha entre o ponto A e B

        // Verificar colisão
        Vector3 direction = (endPoint.position - startingPoint.position).normalized;
        hit = Physics2D.Linecast(startingPoint.position, startingPoint.position + direction * (distancePercent * ropePositionPercentage + 0.2f), whatIsColision | whatIsCatchable | whatIsPlatform);
        if (hit) { collidingRope = true; }

        // Trocar estados | Avançar > Recuar 
        if (ropePositionPercentage >= 1)
        { ropeAdvancing = false; ropeReturning = true; }

        if (ropePositionPercentage <= 0)
        { ropeAdvancing = true; ropeReturning = false; }

        // Avançar
        if (ropeReturning == false && collidingRope == false)
        { ropeAdvancing = true; ropePositionPercentage += .1f / ropeSpeed; } // Aumenta a porcentagem de avanço da corda
        else { ropeAdvancing = false; }

        // Recuar
        if (ropeReturning && collidingRope == false)
        { ropePositionPercentage -= .1f / ropeSpeed; } // Diminui a porcentagem de avanço da corda 

        // Desenhar corda
        if (collidingRope == false)
        {
            lineRenderer.SetPosition(0, pointA); // Define a posição inicial da linha (ponto A)
            lineRenderer.SetPosition(1, pointAlongLine); // Define a posição final da linha (ponto atual da corda)
        }

        // Desenhar (ponto A) enquanto colidir
        else { lineRenderer.SetPosition(0, pointA); }
    }

    void ropeMove()
    {
        //  Trocar estado controlador
        pullingRope = true;

        // Calcular o vetor direção entre o jogador e o ponto final
        Vector2 direction = (pointB - transform.position).normalized;

        // Aplicar a força na direção do vetor direção
        Player_Physics2D.corpoDoPersonagem.AddForce(direction * moveSpeed, ForceMode2D.Impulse);

        // Impacto da parede
        if (Player_CheckColision.isWall || Player_CheckColision.isPlatformLeft || Player_CheckColision.isPlatformRight || Player_CheckColision.isRoof)
        {
            restart();
            if (Player_CheckColision.isRoof == false)
            { Player_WallMove.isJumpRope = true; }
        }
    }

    void restart()
    {
        drawingRope = false; finishInitialPose = false; collidingRope = false;
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        ropePositionPercentage = -0.1f; distancePercent = -0.1f;
        ropeAdvancing = false; ropeReturning = false; drawingRope = false; pullingRope = false;
    }

}