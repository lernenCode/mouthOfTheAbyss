using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private LayerMask Checkpoint;
    private BoxCollider2D boxCol2D;
    private bool checkPoint;
    private void Start() 
    {
        boxCol2D = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        // Verficar colisao
        checkPoint = Physics2D.IsTouchingLayers(boxCol2D, Checkpoint);

        if(checkPoint){save();}
    }

    void save()
    {gameObject.GetComponent<saveManager>().SavePlayer();}
}
