using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    [Header("Load")]
    [SerializeField] private int faseParaCarregar;
    void OnTriggerEnter2D(Collider2D hit) 
    {
        if(hit.tag == "Player")
        {
            #region load
                SceneManager.LoadScene(faseParaCarregar);
            #endregion
        }
    }
}
