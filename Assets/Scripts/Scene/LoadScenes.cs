using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{


    [SerializeField] private int phaseToLoad;
    [SerializeField] private List<int> indexAsync;
    public static List<int> scenesAlreadyLoaded;

    private void Start()
    {
        if (scenesAlreadyLoaded == null)
        {
            scenesAlreadyLoaded = new List<int>();
        }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            LoadScene(phaseToLoad);

            foreach (int i in indexAsync)
            {
                LoadScene(i);
            }

            RemoveNonCurrentScenes();
        }
    }

    private void LoadScene(int index)
    {
        if (!scenesAlreadyLoaded.Contains(index))
        {
            SceneManager.LoadScene(index, LoadSceneMode.Additive);
            scenesAlreadyLoaded.Add(index);
        }
    }

    private void RemoveFromList(int index)
    {
        scenesAlreadyLoaded.Remove(index);
    }

    private void UnloadScene(int index)
    {
        SceneManager.UnloadSceneAsync(index);
        RemoveFromList(index);
    }

    private void RemoveNonCurrentScenes()
    {
        List<int> scenesToRemove = new List<int>();
        foreach (int sceneIndex in scenesAlreadyLoaded)
        {
            if (sceneIndex != phaseToLoad && !indexAsync.Contains(sceneIndex))
            {
                scenesToRemove.Add(sceneIndex);
            }
        }

        foreach (int sceneIndex in scenesToRemove)
        {
            UnloadScene(sceneIndex);
        }
    }
}