using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDamage : MonoBehaviour
{
    [Header ("Player")]
    [SerializeField] public LayerMask Player;
    [SerializeField] public float recoveryTime;
    public static float _recoveryTime;
    [SerializeField] public float damage;
    private bool IsTouching;
    public static bool isDamage;
    private BoxCollider2D boxCol;

    // Start is called before the first frame update
    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        _recoveryTime = recoveryTime;
    }

    // Update is called once per frame
    void Update()
    {
        IsTouching = Physics2D.IsTouchingLayers(boxCol, Player);
        if (IsTouching)
        {
            if(isDamage == false )
            {
                StartCoroutine(recovery());
            }
        }
    }

    public IEnumerator recovery()
    {
        isDamage = true;
        Player_Input.canMove =  false;
        playerKnockback.knocback(damage, transform.position);
        yield return new WaitForSeconds(_recoveryTime);
        Player_Input.canMove =  true;
        yield return new WaitForSeconds(_recoveryTime);
        isDamage = false;
    }
}
