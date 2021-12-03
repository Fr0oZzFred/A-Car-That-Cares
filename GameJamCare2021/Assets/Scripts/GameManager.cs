using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }
    public enum GameState {
        MainMenu,
        InGame,
        Pause,
        GameOver,
    }
    public static GameState GameStates { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {

    }


    private void Update() {

    }

    void ChangeGameState(GameState gameState) {
        GameStates = gameState;
        UIManager.Instance.ChangeState();
        switch (GameStates) {
            case GameState.MainMenu:
                break;
            case GameState.InGame:
                break;
            case GameState.Pause:
                break;
            case GameState.GameOver:
                break;
        }
    }
}
