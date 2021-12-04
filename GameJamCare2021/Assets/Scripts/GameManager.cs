using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    #region Declaration
    float timerDay;
    int GameDuration = 10;
    int dayCount = 0;
    int deliver;
    public int[] dayTimeOne = new int[10];
    public int[] dayTimeTwo = new int[10];
    public int[] objectiveArray = new int[10];
    int objective;
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
        timerDay = Random.Range(dayTimeOne[dayCount], dayTimeTwo[dayCount]);
        objective = objectiveArray[dayCount];
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
        if (enabledDebug && Input.GetKeyDown(KeyCode.G)) ChangeGameState(GameState.GameOver);

        if (timerDay < 0)
        {
            if (deliver >= objective)
            {
                dayCount++;
                if (dayCount < GameDuration)
                {
                    timerDay = Random.Range(dayTimeOne[dayCount], dayTimeTwo[dayCount]);
                    objective = objectiveArray[dayCount];

                }
                else
                {
                    //Victory
                }
            }
            else
            {
                //defaite
            }
        }
        timerDay -= Time.deltaTime;
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
