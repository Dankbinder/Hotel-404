using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private fpsUI _scoreUI;
    [SerializeField] private fpsUI _timerUI;
    [SerializeField] private float levelStartLives = 3f;
    [SerializeField] private string EndSceneName = "End";
    [SerializeField] private string WinSceneName = "Win";
    [SerializeField] private AudioClip fogHorn;
    private AudioSource _hornSource;
    public static GameManager Instance { get; private set; }


    private int _score = 0;
    private float _lives = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

        }
    }

    private void Update()
    {
        
    }

    private void Start()
    {
        StartGame();
    }

    public void ResetGame()
    {

        _lives = levelStartLives;

        /*_timerUI.UpdateUI(_lives);*/
    }

    public void StartGame()
    {
        ResetGame();
    }

    public void EndGame()
    {
        SceneSwapper.LoadScene(EndSceneName);
    }

    public void GameWon()
    {
        PlayerPrefs.SetInt("HighScore", _score);
        SceneSwapper.LoadScene(WinSceneName);
    }

    public void UpdateScore(int Value)
    {
        _score += Value;
        _scoreUI.UpdateUI(_score);
    }

    public void UpdateLives(float Value)
    {
        _lives += Value;
        _timerUI.UpdateUI(_lives);
        if (_lives < 1)
        {
            EndGame();
        }
    }
}
    