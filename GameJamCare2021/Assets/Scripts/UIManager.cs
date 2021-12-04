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

    [Header("Debug")]
    public bool enabledDebug;
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

    public void ChangeState(GameManager.GameState oldGameState) {
        uiElement[(int)oldGameState].SetActive(false);
        uiElement[(int)GameManager.GameStates].SetActive(true);
    }

    #region Debug
    #endregion
}
