using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energy : MonoBehaviour
{
    [SerializeField] private float energyRecovery;
    [SerializeField] private LayerMask interaction;
    private bool IsTouching;
    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {

        IsTouching = Physics2D.IsTouchingLayers(col, interaction);


        if (IsTouching && player_status.energy < 100)
        {
            Destroy(gameObject);
            player_status.addEnergy(energyRecovery);
        }
    }
}
