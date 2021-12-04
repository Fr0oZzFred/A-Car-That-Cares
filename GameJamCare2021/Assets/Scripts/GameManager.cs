using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {
    #region Declaration
    [Header("Debug")]
    public bool enabledDebug;
    #endregion
    public static GameManager Instance { get; private set; }
    public enum GameState {
        MainMenu = 0,
        InGame = 1,
        Pause = 2,
        GameOver = 3,
    }
    public static GameState GameStates { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {

    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
        if (enabledDebug && Input.GetKeyDown(KeyCode.G)) ChangeGameState(GameState.GameOver);
    }

    void ChangeGameState(GameState gameState) {
        GameState oldGameState = GameStates;
        GameStates = gameState;
        UIManager.Instance.ChangeState(oldGameState);
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
        if (enabledDebug) DebugGameState();
    }


    public void ChangeGameState(int gamestate) {
        ChangeGameState((GameState)gamestate);
    }

    public void Pause() {
        if (GameStates == GameState.InGame)
            ChangeGameState(GameState.Pause);
        else if (GameStates == GameState.Pause)
            ChangeGameState(GameState.InGame);
    }
    #region Debug
    void DebugGameState() {
        Debug.Log(GameStates);
    }
    #endregion
}
