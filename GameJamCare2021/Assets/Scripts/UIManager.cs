using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    #region Declaration
    public List<GameObject> uiElement;

    [Header("Text")]
    public TextMeshProUGUI textStock;
    public TextMeshProUGUI textTimer;
    #endregion
    public static UIManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    void UpdateScore() {
        textStock.text = 0.ToString(); // Stock
    }

    void TimerUpdate() {
        textTimer.text = 0.ToString(); // Timer
    }

    public void ChangeState() {
        switch (GameManager.GameStates) {
            case GameManager.GameState.MainMenu:
        break;
            case GameManager.GameState.InGame:
                break;
            case GameManager.GameState.Pause:
                break;
            case GameManager.GameState.GameOver:
                break;
    }
    }
}
