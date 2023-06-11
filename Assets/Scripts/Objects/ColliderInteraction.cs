using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interaction;
    [SerializeField] private ParticleSystem particle;
    private bool IsTouching;

    private Collider2D col;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.IsTouchingLayers(col, interaction))
        {
            if(Player_Carried.Throwablefinished)
            {
                // Desligar componentes (SpriteRenderer, Collider2D, Rigidbody2D)


                if (spriteRenderer != null)
                    spriteRenderer.enabled = false;

                if (col != null)
                    col.enabled = false;

                if (rb != null)
                    rb.gravityScale = 0f;

                // Iniciar a partícula
                particle?.Play();

                // Destruir o GameObject após a duração da partícula
                float particleDuration = particle.main.duration;
                Destroy(gameObject, particleDuration);

                Player_Carried.Throwablefinished = false;
            }
        }
    }

}
