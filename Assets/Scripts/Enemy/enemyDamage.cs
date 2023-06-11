using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private LayerMask whatHurtsMe;
    [SerializeField] private readonly float knockbackForce;
    [SerializeField] private readonly float invulnerabilityDuration;
    private BoxCollider2D boxCol2D;
    private Rigidbody2D rb;
    private bool iHurt, canTakeDamage = true;
    private Vector2 lastImpactPoint;
    private float invulnerabilityTimer;
    private   RaycastHit2D touchedHurts;

    void Start()
    { 
        boxCol2D = GetComponent<BoxCollider2D>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        iHurt = TouchedWhatHurtsMe();

        if(iHurt && canTakeDamage){Damage();}

        if(life <= 0)
        {/*anim, particle, time*/ Destroy(gameObject);}

         if (!canTakeDamage)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0f)
            {
                canTakeDamage = true;
            }
        }
    }

    private bool TouchedWhatHurtsMe()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        foreach (Vector2 direction in directions)
        {
            touchedHurts =  Physics2D.BoxCast(boxCol2D.bounds.center, boxCol2D.bounds.size, 0f, direction, 0.1f, whatHurtsMe);
            if (touchedHurts.collider != null)
            {
                return true;
            }
        }

        return false;
    }

    public void StartInvulnerabilityTimer()
    {
        invulnerabilityTimer = invulnerabilityDuration;
        canTakeDamage = false;
    }

    private void Damage()
    {
        // criar particula
        life--;
        lastImpactPoint = transform.position;
        Knockback();
        StartInvulnerabilityTimer();
    }

    private void Knockback()
    {
        // não funciona ver porque
        Vector2 knockbackDirection = rb.position - (Vector2)lastImpactPoint;
        knockbackDirection.Normalize();
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        Debug.Log(knockbackDirection);
    }
}
