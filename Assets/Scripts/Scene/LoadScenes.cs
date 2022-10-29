using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    [SerializeField] private int faseParaCarregar;
    void OnTriggerEnter2D(Collider2D hit) 
    {
        if(hit.tag == "Player")
        {
            SceneManager.LoadScene(faseParaCarregar);
        }
    }
}
