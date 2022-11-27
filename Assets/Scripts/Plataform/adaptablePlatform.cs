using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adaptablePlatform : MonoBehaviour
{
    #region Variaveis
    [Header("Platform")]
        public float moveSpeed;
        
    [Header("Points")]
        public Transform[] target;
        private int atualTarget = 0;
        private int maxTarget = 0;
        private bool advancing = true;
    #endregion
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
        // avançando ou voltando
        if(advancing == true){ advancingMovePlataform(atualTarget);} else {retreatingMovePlataform(atualTarget);}
    }

    void advancingMovePlataform(int targ) // Avançar
    {
        if(transform.position != target[targ].position) // só vou me mover enquanto minha posição for diferente do alvo
        {transform.position = Vector2.MoveTowards(transform.position, target[targ].position, moveSpeed * Time.deltaTime);}
        
        if(transform.position == target[targ].position){atualTarget++;} // incrementar

        if(atualTarget == maxTarget){advancing = false;}
    }

    void retreatingMovePlataform(int targ) // Retornar
    {
        if(transform.position != target[targ].position) // só vou me mover enquanto minha posição for diferente do alvo
        {transform.position = Vector2.MoveTowards(transform.position, target[targ].position, moveSpeed * Time.deltaTime);}
        
        if(transform.position == target[targ].position){atualTarget--;} // decrementar 

        if(atualTarget == 0){advancing = true;}
    }
}
