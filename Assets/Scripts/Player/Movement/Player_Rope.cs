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

    void Update()
    {
        // Condição para iniciar 
        if (Player_Input.InputRope == true && drawingRope == false && player_status.energy >= takeEnergy)
        { drawingRope = true; player_status.reduceEnergy(takeEnergy);} 
        //! ver uma forma melhor de tirar a energia (pensei em tirar 1ms antes de  chamar drawLine atraves da animação)

        //! Isso aqui depois pode ser chamado por uma animação 
        if (drawingRope == true)
        { 
            drawLine(); 
        }

        // Movimento
        if (collidingRope == true)
        { ropeMove(); }

        // Reiniciar a corda se o ciclo estiver completo
        if (ropeReturning == true && ropePositionPercentage <= 0)
        { drawingRope = false; }

        // Cancelar gravidade e movimento
        if (drawingRope == true && collidingRope == false)
        {
            Player_Physics2D.ResetVelocity();
            Player_Physics2D.corpoDoPersonagem.gravityScale = 0;
            Player_Physics2D.corpoDoPersonagem.AddForce(Vector2.up * 0, ForceMode2D.Impulse);
        }
    }

    void drawLine()
    {
        // Definir pontos
        pointA = startingPoint.position; // Posição do ponto A da corda
        pointB = endPoint.position; // Posição do ponto B da corda

        // Avançar
        if (ropeReturning == false && collidingRope == false)
        { ropeAdvancing = true; ropePositionPercentage += .1f / ropeSpeed; } // Aumenta a porcentagem de avanço da corda
        else { ropeAdvancing = false; }

        // Trocar estados | Avançar > Recuar 
        if (ropePositionPercentage >= 1)
        { ropeAdvancing = false; ropeReturning = true; }

        if (ropePositionPercentage <= 0)
        { ropeAdvancing = true; ropeReturning = false; }

        // Recuar
        if (ropeReturning && collidingRope == false)
        { ropePositionPercentage -= .1f / ropeSpeed; } // Diminui a porcentagem de avanço da corda 

        // Calcular
        distancePercent = Mathf.Lerp(0, maxDistance, ropePositionPercentage); // Calcula a posição da corda na distância máxima baseado na porcentagem de avanço
        Vector3 pointAlongLine = distancePercent * Vector3.Normalize(pointB - pointA) + pointA; // Calcula a posição atual da corda ao longo da linha entre o ponto A e B

        // Desenhar corda
        if (collidingRope == false)
        {
            lineRenderer.SetPosition(0, pointA); // Define a posição inicial da linha (ponto A)
            lineRenderer.SetPosition(1, pointAlongLine); // Define a posição final da linha (ponto atual da corda)
        }

        // Desenhar (ponto A) enquanto colidir
        else { lineRenderer.SetPosition(0, pointA); }

        // Verificar colisão
        Vector2 direction = (endPoint.position - startingPoint.position).normalized;
        hit = Physics2D.Raycast(startingPoint.position, direction, distancePercent * ropePositionPercentage, whatIsColision | whatIsCatchable | whatIsPlatform);
        if (hit) { collidingRope = true; }

        // Debug
        string debugMessage = "Advancing: " + ropeAdvancing + "\n" + "Return: " + ropeReturning + "\n" + "colliding: " + collidingRope + "\n" + "pulling: " + pullingRope + "\n" + "draw: " + drawingRope + "\n" + "";
        Debug.Log(debugMessage);
    }

    void ropeMove()
    {
        // Calcular o vetor direção entre o jogador e o ponto final
        Vector2 direction = (pointB - transform.position).normalized;

        // Aplicar a força na direção do vetor direção
        Player_Physics2D.corpoDoPersonagem.AddForce(direction * moveSpeed, ForceMode2D.Impulse);

        // Impacto da parede
        if (Player_CheckColision.isWall || Player_CheckColision.isPlatformLeft || Player_CheckColision.isPlatformRight || Player_CheckColision.isRoof)
        {
            restart();
            if(Player_CheckColision.isRoof == false)
            { Player_WallMove.isJumpRope = true; }
        }
    }

    void restart()
    {
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        ropePositionPercentage = 0; distancePercent = 0;
        ropeAdvancing = false; ropeReturning = false; drawingRope = false; collidingRope = false;
    }

}