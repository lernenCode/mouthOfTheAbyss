using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFlyRandow : MonoBehaviour
{
    [Header("Sensores")]
    [SerializeField] private GameObject wallCheck;
    [SerializeField] private GameObject ceilingCheck;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float wallCheckRange;
    [SerializeField] private float ceilingCheckRange;
    [SerializeField] private float groundCheckRange;
    [SerializeField] private LayerMask whatIsGround;
    private bool isWall, isCeil, isGroud;

    [Header("Movimentação")]
    [SerializeField] private float speed;
    [SerializeField] private float pos;
    [SerializeField] private float neg;
    private float speedY;

    private bool olhandoDireita = true;

    [Header("Componentes")]
    private Rigidbody2D corpoDoPersonagem;
    
    void Start()
    {
        speedY = speed;
        corpoDoPersonagem = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*      Iniciar funções       */
        CheckCol(); // verificar colisão
        Move();     // Faz movimentação
        Rotation(); // Faz a rotação
    }

    private void Rotation()
    {
        // Rotação
        if(isWall)
        {
           if(olhandoDireita)
           {olhandoDireita = false;  transform.localRotation = Quaternion.Euler(0, 180, 0);}
           else {olhandoDireita = true;  transform.localRotation = Quaternion.Euler(0, 0, 0);}
        }

        if(isCeil) {speedY = speed * - neg;}
        if(isGroud) {speedY = speed *   pos;}
    }

    private void Move()
    {
        Vector2 movement = transform.right * speed;

        corpoDoPersonagem.velocity = new Vector2(movement.x, speedY);
    }

    private void CheckCol()
    {
        isGroud = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRange, whatIsGround);  // Verifica colisão do chao   
        isWall = Physics2D.OverlapCircle(wallCheck.transform.position, wallCheckRange, whatIsGround);       // Verifica colisão da parede  
        isCeil = Physics2D.OverlapCircle(ceilingCheck.transform.position, ceilingCheckRange, whatIsGround); // Verifica colisão do teto    
    }

    private void OnDrawGizmosSelected() 
    {
        // Desenhar range do sensor do chao
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRange);

        // Desenhar range do sensor da parede
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.transform.position, wallCheckRange);

        // Desenhar range do sensor do teto
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(ceilingCheck.transform.position, ceilingCheckRange);
    }
}
