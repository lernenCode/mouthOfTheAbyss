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
        // Verficar colisao
        colider = Physics2D.IsTouchingLayers(boxCol2D, checkPoint);
        if(colider){save();}
    }

    public void save()
    {gameObject.GetComponent<saveManager>().SavePlayer();}
}
