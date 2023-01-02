using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySimplePatrol : MonoBehaviour
{
    [Header("Check")]
    [SerializeField]
    private float rangeGroundCheck;

    [SerializeField]
    private float rangeWallCheck;

    [SerializeField]
    private GameObject wallCheck;

    [SerializeField]
    private GameObject gorundCheck;
    private bool isGround,
        isWall;

    [Header("Layers")]
    [SerializeField]
    private LayerMask whatisGround;

    [SerializeField]
    private LayerMask whatIsCatchable;

    [SerializeField]
    private LayerMask whatIsPlatform;

    [Header("Componentes")]
    private BoxCollider2D boxCol2D;
    private Rigidbody2D rb;

    [Header("Movimentanção")]
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool OlhandoDireita;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCol2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        #region Check
        // Identificar colisão com o chao
        isGround = Physics2D.OverlapCircle(
            gorundCheck.transform.position,
            rangeGroundCheck,
            whatIsCatchable | whatisGround | whatIsPlatform
        );

        // Identificar colisão com a parede
        isWall = Physics2D.OverlapCircle(
            wallCheck.transform.position,
            rangeWallCheck,
            whatIsCatchable | whatisGround | whatIsPlatform
        );
        #endregion

        #region Iniciar Funcao
        move();
        #endregion

        #region Flip
        if (isGround == false || isWall == true)
        {
            if (OlhandoDireita)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                OlhandoDireita = false;
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                OlhandoDireita = true;
            }
        }
        #endregion
    }

    private void move() // Responsavel pela movimentação
    {
        rb.velocity = transform.right * speed;
    }

    private void OnDrawGizmosSelected()
    {
        // Desenhar range do sensor do chao
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gorundCheck.transform.position, rangeGroundCheck);

        // Desenhar range do sensor da parede
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.transform.position, rangeWallCheck);
    }
}
