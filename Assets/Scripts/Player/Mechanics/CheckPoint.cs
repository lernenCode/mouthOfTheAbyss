using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    [SerializeField] private LayerMask checkPoint;
    private BoxCollider2D boxCol2D;
    private bool colider;
    private void Start() 
    {
        boxCol2D = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        // Verficar colisao e Salvar
        if(Physics2D.IsTouchingLayers(boxCol2D, checkPoint) && player_status.isDie == false)
        {gameObject.GetComponent<saveManager>().SavePlayer();}
    }
}
