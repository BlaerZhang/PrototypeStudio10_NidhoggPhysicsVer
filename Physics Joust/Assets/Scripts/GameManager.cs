using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState
    {
        IsPreparing, IsInGame, IsGameOver
    }

    public GameState gameState;

    public TextMeshProUGUI timerText;

    public string winner;
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameState = GameState.IsPreparing;

        Sequence startCountDown = DOTween.Sequence();
        startCountDown
            .Append(timerText.DOText("3", 0))
            .AppendInterval(1)
            .Append(timerText.DOText("2", 0))
            .AppendInterval(1)
            .Append(timerText.DOText("1", 0))
            .AppendInterval(1)
            .Append(timerText.DOText("", 0))
            .OnComplete((() => { gameState = GameState.IsInGame; }));
        
        startCountDown.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.IsGameOver)
        {
            timerText.text = winner + " Wins";
        }
    }
}
