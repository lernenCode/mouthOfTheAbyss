using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] private int faseParaCarregar;
    [SerializeField] private GameObject PersistentScene;
    void OnTriggerEnter2D(Collider2D hit) 
    {
        if(hit.tag == "Player")
        {
            SceneManager.LoadScene(faseParaCarregar);
            DontDestroyOnLoad(PersistentScene);
        }
    }
}
