using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    #region Declaration
    public float timerDay;
    int GameDuration = 5;
    public int dayCount = 0;
    public int deliver { get; private set; }
    public int[] dayTimeOne = new int[5];
    public int[] dayTimeTwo = new int[5];
    public int[] objectiveArray = new int[5];
    public int[] rndTimerMin = new int[5];
    public int[] rndTimerMax = new int[5];
    public int objective { get; private set; }
    public bool victory = false;
    bool firstTime = true;
    public GameObject tuto;
    [Header("Camera")]
    public CinemachineVirtualCamera camMainMenu;
    public CinemachineVirtualCamera camInGame;
    [Header("Debug")]
    public bool enabledDebug;
    #endregion
    public static GameManager Instance { get; private set; }

    public enum GameState {
        MainMenu = 0,
        InGame = 1,
        Pause = 2,
        GameOver = 3,
        Credit = 4,
    }
    public static GameState GameStates { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        ResetGameManager();
    }


    private void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) Pause();
        if (GameStates == GameState.InGame) {
            if (timerDay < 0) {
                if (deliver >= objective) {
                        dayCount++;
                        victory = true;
                        ChangeGameState(GameState.GameOver);
                        victory = false;
                } else {
                    ChangeGameState(GameState.GameOver);
                }
            }
            timerDay -= Time.deltaTime;
            UIManager.Instance.TimerUpdate(timerDay);
        }
        

        if (enabledDebug && Input.GetKeyDown(KeyCode.G)) ChangeGameState(GameState.GameOver);
    }
    void SetTimer() {

        timerDay = Random.Range(dayTimeOne[dayCount], dayTimeTwo[dayCount]);
        objective = objectiveArray[dayCount];
    }
    void ChangeGameState(GameState gameState) {
        GameState oldGameState = GameStates;
        GameStates = gameState;
        UIManager.Instance.ChangeState(oldGameState);
        switch (GameStates) {
            case GameState.MainMenu:
                if(!firstTime) VehicleCenterManager.Instance.ActivateCarMenu();
                Restart();
                Time.timeScale = 1;
                camMainMenu.Priority = camInGame.Priority + 1;
                break;
            case GameState.InGame:
                if (firstTime) {
                    Time.timeScale = 0;
                    tuto.SetActive(true);
                    VehicleCenterManager.Instance.InstanceCar(dayCount + 1);
                    VehicleCenterManager.Instance.DectivateCarMenu();
                    Debug.Log("dea");
                } else {
                    if (dayCount > GameDuration) dayCount = GameDuration;
                    VehicleCenterManager.Instance.DectivateCarMenu();
                    Debug.Log("dea");
                    if (oldGameState != GameState.Pause) {
                        Restart();
                        VehicleCenterManager.Instance.InstanceCar(dayCount + 1);
                    }
                    Time.timeScale = 1;
                }
                
                camInGame.Priority = camMainMenu.Priority + 1;
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
                Time.timeScale = 0;
                break;
            case GameState.Credit:
                break;
        }
        if (enabledDebug) DebugGameState();
    }

    public void AddScore(int score) {
        deliver += score;
        UIManager.Instance.UpdateScore();
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
    public void Restart() {
        VehicleCenterManager.Instance.ClearVehicle();
        ResetGameManager();
        UIManager.Instance.ResetUI();
    }
    public void SetFirstTimeFalse() {
        firstTime = false;
        Time.timeScale = 1;
        tuto.SetActive(false);
    }
    public void ResetGameManager() {
        SetTimer();
        deliver = 0;
        AddScore(-deliver);
    }

    public void QuitGame() {
        Application.Quit();
    }
    #region Debug
    void DebugGameState() {
        Debug.Log(GameStates);
    }
    #endregion
}
