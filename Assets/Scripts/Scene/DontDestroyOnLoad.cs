using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] private int faseParaCarregar;
    [SerializeField] private GameObject PersistentScene;
    private void Start() 
    {
        SceneManager.LoadScene(faseParaCarregar);
        DontDestroyOnLoad(PersistentScene);
    }
}
