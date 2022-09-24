using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyBody : MonoBehaviour
{
    [SerializeField]
    private LayerMask carcass;

    [SerializeField]
    private GameObject abandonedCarcass;
    private bool isEmpty;

    [Header("Carcaças")]
    [SerializeField]
    private bool fred,cherry;

    void Update()
    {
        isEmpty = Physics2D.IsTouchingLayers(Support_Physics2D.boxCol, carcass);

        if (isEmpty == true && player_status.isDie == true)
        {
            StartCoroutine("bodyEmpty");
        }
    }

    public IEnumerator bodyEmpty()
    {
        // devolver vida / energia 100
        player_status.addStamina(100);
        player_status.addEnergy(100);
        player_status.addLife(100);


        // teleportar o jogador para esta posição aqui
        Player_Physics2D.playerGameObject.transform.position = new Vector2
        (abandonedCarcass.transform.position.x,abandonedCarcass.transform.position.y);

        // dar novos atributos se tiver conforme o corpo que estiver aqui utilizar de bools e ifs
        if (cherry)
        {
            Debug.Log("Jogando com cherry");
        }
        if (fred)
        {
            Debug.Log("Jogando com fred");
        }

        // deletar o corpo caido
        abandonedCarcass.SetActive(false);

        // Voltar a vida
        player_status.isDie = false;
        yield return 0;
    }
}
