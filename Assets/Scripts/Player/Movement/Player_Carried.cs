using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Carried : MonoBehaviour
{
    [Header("Carry Object")]
    [SerializeField] private LayerMask whatIsCatchable;
    [SerializeField] private Transform HolderPosition;
    [SerializeField] private float rayDistance;
    public static GameObject HolderItem;

    [Header("Throwable Object")]
    [SerializeField] private float throwableForce;
    [SerializeField] private float maxPressButton;
    public float pressButton;
    public static bool CrouchToPickUp;
    public static bool carryLock;
    public static bool Throwable;
    public static bool startAnimThrowable;
    public static bool Throwablefinished;
    private bool inThrowableObject;
    private GameObject Object;
    private RaycastHit2D CollisionObject;

    private void Update()
    {
        #region Verificar Colision com Objeto
        // Verificar se tem item pra pegar colidindo
        if (Player_Input.InputDown == true)
        { CollisionObject = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), rayDistance, whatIsCatchable); }
        else { CollisionObject = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), rayDistance, whatIsCatchable); }
        #endregion

        #region Colocar || Arrmesar Item
        // carregar jogar item
        if (Player_Input.InputGrab && carryLock == false && HolderItem != null)
        {
            if (pressButton < maxPressButton)
            { pressButton += 0.07f; }
        }

        //Arremesar item
        if (Player_Input.InputCarryUP && carryLock == false && HolderItem != null)
        {
            Throwable = true;
            Player_Input.canMove = false;
        }

        // Arremesar mesmo se machucado
        if (Throwable == true && playerDamage.isDamage == true)
        {
            ThrowableHolderItem();
        }
        #endregion

        #region Pegar o item
        if (Player_Input.InputCarry)
        {
            if (CollisionObject.collider != null && HolderItem == null)
            {
                CrouchToPickUp = true;
                Player_Input.canMove = false;
                Player_Physics2D.corpoDoPersonagem.gravityScale = 0;
            }
        }
        #endregion
    }

    public void PickUpItem()
    {
        // parar animação de pegar
        CrouchToPickUp = false;
        Player_Input.canMove = true;

        if (CollisionObject.collider != null && HolderItem == null)
        {
            // Passar quem é meu item
            HolderItem = CollisionObject.collider.gameObject;

            // Transformar meu item em filhoss
            HolderItem.transform.SetParent(transform);

            // Dar uma nova posicao para meu item
            HolderItem.transform.position = HolderPosition.position;

            // Se ele tiver um rb2D desligar controle de física
            if (HolderItem.GetComponent<Rigidbody2D>())
            {
                HolderItem.GetComponent<Rigidbody2D>().simulated = false;

                // Alterar de static para dynamic
                HolderItem.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }

            // Tempo para poder soltar o item depois que pegou
            StartCoroutine(Player_IEnumerator.carryLock());
        }
    }
    public void ThrowableHolderItem()
    {
        #region Arremesar
        Player_Input.canMove = true;
        Throwablefinished = true;
        HolderItem.layer = LayerMask.NameToLayer("Throwable");

        // Se ele tiver um rb2D ligar controle de física
        if (HolderItem.GetComponent<Rigidbody2D>())
        { HolderItem.GetComponent<Rigidbody2D>().simulated = true; }

        // Desertar filho
        HolderItem.transform.SetParent(null);
        if (pressButton > 1)
        { HolderItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(throwableForce * Player_Physics2D.Direction.x * pressButton, throwableForce), ForceMode2D.Impulse); }
        else { HolderItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(throwableForce * Player_Physics2D.Direction.x, throwableForce), ForceMode2D.Impulse); }
        pressButton = 0;

        HolderItem = null;
        Throwable = false;
        #endregion
    }
    void OnDrawGizmosSelected()
    {
        // Desenhar distancia pra pegar o item
        Gizmos.color = Color.red;
        Vector3 direction1 = (transform.TransformDirection(Vector2.right) * rayDistance);
        Gizmos.DrawRay(transform.position, direction1);
    }
}
