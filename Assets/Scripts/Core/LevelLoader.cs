using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private static LevelLoader _instance;
    public static int ActiveSceneBuildIndex => SceneManager.GetActiveScene().buildIndex;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        if (ActiveSceneBuildIndex == 0)
            LoadLevel(SaveSystem.Level % 3 + 1);
    }

    public static void LoadLevel(int index)
    {
        GC.Collect();
        SceneManager.LoadScene(index);
    }

    public static void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
