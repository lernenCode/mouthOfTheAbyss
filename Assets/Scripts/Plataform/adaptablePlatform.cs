using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adaptablePlatform : MonoBehaviour
{
    public Transform[] target;

    public float moveSpeed;
    public int atualTarget = 0;
    public int maxTarget = 0;
    public bool advancing = true;
    private void Start() 
    {
        maxTarget = target.Length - 1;
        #region Garantir posição inicial        
            if(transform.position != target[0].position)
            {transform.position = target[0].position;}
        #endregion
    }
    void Update()
    {
        if(advancing == true){ advancingMovePlataform(atualTarget);} else {retreatingMovePlataform(atualTarget);}
    }

    void advancingMovePlataform(int targ)
    {
        if(transform.position != target[targ].position) // só vou me mover enquanto minha posição for diferente do alvo
        {transform.position = Vector2.MoveTowards(transform.position, target[targ].position, moveSpeed * Time.deltaTime);}
        
        if(transform.position == target[targ].position){atualTarget++;} // incrementar

        if(atualTarget == maxTarget){advancing = false;}
    }

    void retreatingMovePlataform(int targ)
    {
        if(transform.position != target[targ].position) // só vou me mover enquanto minha posição for diferente do alvo
        {transform.position = Vector2.MoveTowards(transform.position, target[targ].position, moveSpeed * Time.deltaTime);}
        
        if(transform.position == target[targ].position){atualTarget--;} // decrementar 

        if(atualTarget == 0){advancing = true;}
    }
}
