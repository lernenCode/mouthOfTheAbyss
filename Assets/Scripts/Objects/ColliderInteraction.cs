using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask interaction;
    private bool IsTouching;
    private Collider2D col;
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        IsTouching = Physics2D.IsTouchingLayers(col, interaction);
        

        if(IsTouching)
        {
            if(Player_Carried.Throwablefinished)
            {
                //destruir
                Destroy(gameObject);
                Player_Carried.Throwablefinished = false;
            }
        }
    }

}
