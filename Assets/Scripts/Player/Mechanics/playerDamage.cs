using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDamage : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] public LayerMask Player;
    [SerializeField] public int damage;
    private bool IsTouching;
    public static bool isDamage;
    public static bool inKnocback;
    private BoxCollider2D boxCol;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsTouching = Physics2D.IsTouchingLayers(boxCol, Player);
        if (IsTouching == true && isDamage == false) { knocback(); }
    }

    void knocback()
    {
        Player_Input.canMove = false;
        inKnocback = true;
        isDamage = true;
        playerKnockback.knocback(damage, transform.position);
    }
}
