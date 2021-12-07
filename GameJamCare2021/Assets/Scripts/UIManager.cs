using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    #region Declaration
    public List<GameObject> uiElement;
    public GameObject stockPanel;

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

    public void TimerUpdate(float timer) {
        timer = timer / 60;
        int timerInt = (int)timer;
        textTimer.text = $"{timerInt} : {(int)(Math.Round(timer - timerInt,2)*60)}"; // Timer
    }
    public void DisplayStock() {
        foreach(Transform t in stockPanel.transform) {
            if(t.GetComponent<Image>() != null) t.gameObject.SetActive(false);
        }
    }

    public void ChangeState(GameManager.GameState oldGameState) {
        uiElement[(int)oldGameState].SetActive(false);
        uiElement[(int)GameManager.GameStates].SetActive(true);
    }

    #region Debug
    private void Update() {
        if(enabledDebug && Input.GetKeyDown(KeyCode.S)){
            DisplayStock();
        }
    }
    #endregion
}
