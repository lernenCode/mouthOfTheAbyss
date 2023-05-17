using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] private GameObject PersistentScene;
    private void Start() 
    {
        DontDestroyOnLoad(PersistentScene);
    }
}
