using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrolGrab : MonoBehaviour
{
   // Utilizaremos apenas uma LayerMask para verificar a colisão
   [Header("Sensor")]
   public LayerMask whatIsObstacle0;
   public LayerMask whatIsObstacle1;
   public LayerMask whatIsObstacle2;
   public GameObject wallCheck;
   public GameObject groundCheck;
   public float rangeGroundCheck;
   public float rangeWallCheck;

   [Header("Movimentação")]
   public float speed;

   // Utilizaremos uma estrutura de controle para gerenciar a rotação do personagem
   [Header("DEBUG")]
   public Vector2 holdPosition;
   public float ZAxis;

   [Header("Componentes")]
   private Rigidbody2D rb;

   // Utilizaremos constantes para facilitar a manutenção do código
   private const float ROTACAO_PADRAO = 90f;
   private const float ROTACAO_MINIMA = -360f;
   private const float ROTACAO_MAXIMA = 360f;

   private void Start() { rb = GetComponent<Rigidbody2D>();}

   private void Update() 
   {
        /*Chamar funções*/
        Mover(); // Responsável pela movimentação
        Rotacionar(); // Responsável pela rotação
   }

     private void Rotacionar()
   {
        // Utilizaremos uma estrutura de controle para gerenciar a rotação do personagem
        if (!VerificarColisaoChao())
        {   
            // Rotaciona o personagem em -90 graus
            ZAxis -= ROTACAO_PADRAO;
            transform.eulerAngles = new Vector3(0, 0, ZAxis);
        } 
        else if (VerificarColisaoParede())
        {   
            // Rotaciona o personagem em +90 graus
            ZAxis += ROTACAO_PADRAO;
            transform.eulerAngles = new Vector3(0, 0, ZAxis);

            // Atualiza a posição do personagem de acordo com a rotação
            AtualizarPosicaoAposRotacao();
        }

        // Controlador de direccion
        // Utilizaremos uma estrutura de controle para gerenciar o limite de rotação do personagem
        if (ZAxis == ROTACAO_MINIMA 
        ||  ZAxis == ROTACAO_MAXIMA) {ZAxis = 0;}
        
        if (ZAxis <= ROTACAO_MINIMA)  {ZAxis = ROTACAO_MINIMA;}
        if (ZAxis >= ROTACAO_MAXIMA)  {ZAxis = ROTACAO_MAXIMA;}
   }

   private void Mover()
   {
        // Utilizamos o método "velocity" para mover o personagem na direção correta
        rb.velocity = transform.right * speed;
   }
   private bool VerificarColisaoChao()
   {
        // Identificar colisão com o chao
        return Physics2D.OverlapCircle(groundCheck.transform.position, rangeGroundCheck, whatIsObstacle0 | whatIsObstacle1 | whatIsObstacle2);
   }

   private bool VerificarColisaoParede()
   {
        // Identificar colisão com a parede
        return Physics2D.OverlapCircle(wallCheck.transform.position, rangeWallCheck, whatIsObstacle0 | whatIsObstacle1 | whatIsObstacle2);
   }

    private void AtualizarPosicaoAposRotacao()
   {
        if(transform.position.y >= 0)
        {
            if (ZAxis == 0   || ZAxis == 180 || ZAxis == -180 || ZAxis == 360)  
            {
                transform.position = new Vector2 (transform.position.x , transform.position.y + 1);
            }
        }
        else 
        {
            if (ZAxis == 0   || ZAxis == 180 || ZAxis == -180 || ZAxis == 360)  
            {
                transform.position = new Vector2 (transform.position.x , transform.position.y - 1);
            }
        }

        if(transform.position.x >= 0)
        {
            if (ZAxis == 270 || ZAxis == 90  || ZAxis == -90  || ZAxis == -270) 
            {
                transform.position = new Vector2 (transform.position.x + 1, transform.position.y);
            }
        }
        else 
        {
            if (ZAxis == 270 || ZAxis == 90  || ZAxis == -90  || ZAxis == -270) 
            {
                transform.position = new Vector2 (transform.position.x - 1, transform.position.y);
            }
        }
   }

   private void OnDrawGizmosSelected() 
   {
        // Desenhar range do sensor do chao
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, rangeGroundCheck);

        // Desenhar range do sensor da parede
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(wallCheck.transform.position, rangeWallCheck);
   }
}