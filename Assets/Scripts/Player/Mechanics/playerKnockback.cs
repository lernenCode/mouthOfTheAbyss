using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerKnockback : MonoBehaviour
{
    [SerializeField] public int layerPlayer;
    [SerializeField] public int layerEnemy;
    [SerializeField] public float recoveryTime;
    [SerializeField] public float knocbackForce;
    private SpriteRenderer spriteRenderer;
    public static float _knocbackForce;
    private bool IsTouching;
    private void Start()
    {
        _knocbackForce = knocbackForce;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerDamage.isDamage)
        {
            StartCoroutine(flickerSprite());
            Physics2D.IgnoreLayerCollision(layerPlayer, layerEnemy, true);
        }

        else
        { Physics2D.IgnoreLayerCollision(layerPlayer, layerEnemy, false); }
    }

    public static void knocback(int damage, Vector2 enemyPos)
    {
        Vector2 knockbackDirection = ((Vector2)Player_Physics2D.corpoDoPersonagem.transform.position - enemyPos).normalized;
        Player_Physics2D.corpoDoPersonagem.AddForce(knockbackDirection * _knocbackForce, ForceMode2D.Impulse);
        player_status.reduceLife(damage);
    }

    IEnumerator flickerSprite()
    {
        while (playerDamage.isDamage)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator recovery() // Chamado pela animação
    {
        Player_Input.canMove = true;
        playerDamage.inKnocback = false;

        yield return new WaitForSeconds(recoveryTime);
        playerDamage.isDamage = false;
    }
}

