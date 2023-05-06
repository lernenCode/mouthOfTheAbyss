using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDraw : MonoBehaviour
{
    #region Variaveis
    [Header("DrawLine")]
    [SerializeField] private LayerMask whatIsColision;
    [SerializeField] private LayerMask whatIsCatchable;
    [SerializeField] private LayerMask whatIsPlatform;
    [SerializeField] private float takeEnergy;
    [SerializeField] private float ropeDuration;
    public Transform firePoint, Destination, DestinationUp;
    private LineRenderer lineRenderer;
    private float distance, Counter;
    public float lineDrawSpeed;
    public static bool returningRope;
    public static bool advanceRope;
    public static bool hookRope;
    public static bool ropeUp;
    public static bool RopeInColision, InputRope;
    public static bool RopeInCooldown;
    public static bool devolverRope;
    #endregion
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        #region  cooldown
        if (devolverRope == true)
        {
            if (Player_CheckColision.isGround || Player_CheckColision.isWall || Player_CheckColision.isPlatform || Player_CheckColision.isRoof)
            {
                RopeInCooldown = false;
                devolverRope = false;
            }
        }
        #endregion

        #region DrawRope
        if (InputRope)
        {
            if (Player_CheckColision.isWall || Player_CheckColision.isPlatform || Player_CheckColision.isRoof)
            {
                returningRope = false;
                InputRope = false;
                RopeInColision = false;
                Counter = -0;
            }

            if (RopeInColision == false)
            {
                // Para avançar>>
                if (returningRope == false)
                {
                    advancing();
                }

                // para Recuar <<
                if (Counter >= 1 || returningRope == true)
                {
                    Returning();
                }

                // para Reiniciar
                if (Counter <= 0)
                {
                    returningRope = false;
                    advanceRope = false;
                    InputRope = false;
                }
            }
            else { hook(); }
        }

        if (Player_CheckColision.isWall || Player_CheckColision.isPlatform || Player_CheckColision.isRoof)
        {
            lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
            lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        }
        #endregion

        #region DetectColision   
        DetectColision();
        #endregion

        #region DetectInput
        // fazer o teste 
        if (Player_Input.InputRope && player_status.energy >= takeEnergy && RopeInCooldown == false)
        {
            // Definir direcao
            if (Player_Input.InputUp == true)
            { ropeUp = true; } else { ropeUp = false; }

            // Tirar energia
            player_status.reduceEnergy(takeEnergy);

            // Executar 
            InputRope = true;

            // Iniciar contador⌚
            StartCoroutine(Player_IEnumerator.cooldownRope(ropeDuration));
            RopeInCooldown = true;
        }
        #endregion
    }
    public void advancing()
    {
        advanceRope = true;
        returningRope = false;
        hookRope = false;

        // ponto A = firepoint
        lineRenderer.SetPosition(0, firePoint.position);

        // Retorna a distancia Float do começo e fim
        if (ropeUp == true)
        { distance = Vector3.Distance(firePoint.position, DestinationUp.position); }
        else { distance = Vector3.Distance(firePoint.position, Destination.position); }

        // A = inicial | B = final 
        Vector3 pointA = firePoint.position;

        Vector3 pointB;
        if (ropeUp == true)
        { pointB = DestinationUp.position; }
        else { pointB = Destination.position; }


        // Para descobrirmos quantos frames no update temos do ponto B ao A 
        Counter += .1f / lineDrawSpeed;
        float x = Mathf.Lerp(0, distance, Counter);
        Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

        // Desenhar
        lineRenderer.SetPosition(1, pointAlongLine);
    }

    public void hook()
    {
        hookRope = true;
        advanceRope = false;
        returningRope = false;

        // ponto A = firepoint
        lineRenderer.SetPosition(0, firePoint.position);
    }
    public void Returning()
    {
        returningRope = true;
        advanceRope = false;
        hookRope = false;

        // ponto A = firepoint
        lineRenderer.SetPosition(0, firePoint.position);

        // Retorna a distancia Float do começo e fim
        if (ropeUp == true)
        { distance = Vector3.Distance(firePoint.position, DestinationUp.position); }
        else { distance = Vector3.Distance(firePoint.position, Destination.position); }

        // A = inicial | B = final 
        Vector3 pointA = firePoint.position;
        Vector3 pointB;
        if (ropeUp == true)
        { pointB = DestinationUp.position; }
        else { pointB = Destination.position; }

        // Para descobrirmos quantos frames no update temos do ponto B ao A 
        Counter -= .1f / lineDrawSpeed;
        float x = Mathf.Lerp(0, distance, Counter);
        Vector3 pointAlongLine = x * Vector3.Normalize(pointB - pointA) + pointA;

        // Desenhar
        lineRenderer.SetPosition(1, pointAlongLine);
    }
    void DetectColision()
    {
        if (returningRope == false)
        {
            if (ropeUp == true)
            {
                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.TransformDirection(Vector2.up), distance * Counter, whatIsColision | whatIsCatchable | whatIsPlatform);
                if (hit) { RopeInColision = true;  advanceRope = false;} else { RopeInColision = false; }
            }

            else
            {
                RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.TransformDirection(Vector2.right), distance * Counter, whatIsColision | whatIsCatchable | whatIsPlatform);
                if (hit) { RopeInColision = true;  advanceRope = false;} else { RopeInColision = false; }
            }
        }
    }
}
