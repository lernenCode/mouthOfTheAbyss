using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private LayerMask whatHurtsMe;
    [SerializeField] private float knockbackForce;
    [SerializeField] private float invulnerabilityDuration;

    private bool canTakeDamage = true;

    private BoxCollider2D col;
    private Rigidbody2D rb;

    private void Start() { col = GetComponent<BoxCollider2D>(); rb = GetComponent<Rigidbody2D>(); }

    private void Update()
    {

        // Dano
        if (canTakeDamage && Physics2D.IsTouchingLayers(col, whatHurtsMe))
        { Damage(); }

        // Morte
        if (life <= 0)
        {  Destroy(gameObject); }
    }

    private void Damage()
    {
        life--;
        Vector2 lastImpactPoint = transform.position;
        Knockback(lastImpactPoint);
        StartCoroutine(InvulnerabilityTimer());
    }

    private void Knockback(Vector2 lastImpactPoint)
    {
        Vector2 knockbackDirection = (Vector2)transform.position - lastImpactPoint;
        knockbackDirection.Normalize();
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        Debug.Log(knockbackDirection);
    }

    private IEnumerator InvulnerabilityTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invulnerabilityDuration);
        canTakeDamage = true;
    }
}
