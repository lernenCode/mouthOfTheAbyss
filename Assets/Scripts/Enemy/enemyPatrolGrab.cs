using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrolGrab : MonoBehaviour
{
   [Header("Sensor")]
   public LayerMask whatIsObstacle1;
   public LayerMask whatIsObstacle2;
   public LayerMask whatIsObstacle3;
   public GameObject wallCheck;
   public GameObject gorundCheck;
   public float rangeGroundCheck;
   public float rangeWallCheck;

   [Header("Movimentanção")]
   public float speed;

   [Header("DEBUG")]
   public bool isGroud;
   public bool isWall;
   public bool inRotation;
   public Vector2 holdPosition;
   public float ZAxis;

   [Header("Componenetes")]
   private Rigidbody2D rb;

   private void Start() { rb = GetComponent<Rigidbody2D>();}

   private void Update() 
   {
        /*Chamar funções*/
        checkCol(); // Responsavel por verificar a colisão
        move(); // Responsavel pela movimentação
        rotate(); // Responsavel pela rotação
   }

   private void rotate()
   {
        // Chao rotaciona - 90 
        if(isGroud == false)
        {   
            if(inRotation == false)
            {
                ZAxis -= 90;
                transform.eulerAngles = new Vector3(0, 0, ZAxis);
                inRotation = true;
            } 
        } else {inRotation = false;}

        // Parede rotaciona + 90 
        if(isWall == true)
        {   
            if(inRotation == false)
            {
               ZAxis += 90;
               transform.eulerAngles = new Vector3(0, 0, ZAxis);
               
               if(transform.position.y >= 0){if (ZAxis == 0   || ZAxis == 180 || ZAxis == -180 ||ZAxis == 360)  {transform.position = new Vector2 (transform.position.x , transform.position.y +1);}}
               else { if (ZAxis == 0   || ZAxis == 180 || ZAxis == -180 ||ZAxis == 360)  {transform.position = new Vector2 (transform.position.x , transform.position.y -1);}}
               
               if(transform.position.x >= 0) { if (ZAxis == 270 || ZAxis == 90  || ZAxis == -90  ||ZAxis == -270) {transform.position = new Vector2 (transform.position.x +1, transform.position.y);}}
               else { if (ZAxis == 270 || ZAxis == 90  || ZAxis == -90  ||ZAxis == -270) {transform.position = new Vector2 (transform.position.x -1, transform.position.y);}}
              
               inRotation = true;
            } 
        } else {inRotation = false;}

        //Controlador de direccion
        if (ZAxis <= -360)  {ZAxis = 0;}
        if (ZAxis >=  360)  {ZAxis = 0;}

   }

   private void move()
   {
        rb.velocity = transform.right * speed;
   }

   private void checkCol()
   {
        // Identificar colisão com o chao
        isGroud = Physics2D.OverlapCircle(gorundCheck.transform.position, rangeGroundCheck, whatIsObstacle1 | whatIsObstacle2 | whatIsObstacle3);
        
        // Identificar colisão com a parede
        isWall = Physics2D.OverlapCircle(wallCheck.transform.position, rangeWallCheck, whatIsObstacle1 | whatIsObstacle2 | whatIsObstacle3);
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
