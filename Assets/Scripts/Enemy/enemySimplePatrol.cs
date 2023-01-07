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
    private GameObject groundCheck;
    private bool isGround,
        isWall;

    [Header("Layers")]
    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private LayerMask whatIsCatchable;

    [SerializeField]
    private LayerMask whatIsPlatform;

    [Header("Componentes")]
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    [Header("Movimentanção")]
    [SerializeField]
    private float speed;

    [SerializeField]
    private bool lookingRight;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        #region Check
        // Identificar colisão com o chão
        isGround = Physics2D.OverlapCircle(
            groundCheck.transform.position,
            rangeGroundCheck,
            whatIsCatchable | whatIsGround | whatIsPlatform
        );

        // Identificar colisão com a parede
        isWall = Physics2D.OverlapCircle(
            wallCheck.transform.position,
            rangeWallCheck,
            whatIsCatchable | whatIsGround | whatIsPlatform
        );
        #endregion

        #region Iniciar Função
        move();
        #endregion

        #region Flip
        if (isGround == false || isWall == true)
        {
            if (lookingRight)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                lookingRight = false;
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                lookingRight = true;
            }
        }
        #endregion
    }

    private void move() // Responsável pela movimentação
    {
        Vector2 movement = transform.right * speed;

        rb.MovePosition(rb.position + movement * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Desenhar range do sensor do chão
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, rangeGroundCheck);

        // Desenhar range do sensor da parede
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.transform.position, rangeWallCheck);
    }
}