using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneSwap : MonoBehaviour
{
    [SerializeField] private string sceneName = "Maze Scene";
    public void OnButtonPressed()
    {
        SceneManager.LoadScene("Maze Scene");
    }
}
