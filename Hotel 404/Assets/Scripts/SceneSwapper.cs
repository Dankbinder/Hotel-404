using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwapper : MonoBehaviour
{
    public static void LoadScene(string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    public static void LoadScene(ref string scene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }

    // public static void Initialize()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    // private static void OnSceneLoaded(Scene scene, LoadedSceneMode mode)
    //{
    //    
    // }
}