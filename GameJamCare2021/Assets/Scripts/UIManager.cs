using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    #region Declaration
    public List<GameObject> uiElement;
    [Header("CarElement")]
    public GameObject gestionnaire;
    List<Image> gestionnaireImage;
    List<Image> gestionnaireColis;
    List<TextMeshProUGUI> gestionnaireText;
    public GameObject stockPanel;
    public Image selectedCarImage;

    public GameObject goodGameOver;
    public GameObject badGameOver;

    [Header("Text")]
    public TextMeshProUGUI textStock;
    public TextMeshProUGUI textTimer;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textJauge;

    [Header("Debug")]
    public bool enabledDebug;
    #endregion
    public static UIManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
    private void Start() {
        gestionnaireImage = new List<Image>();
        gestionnaireColis = new List<Image>();
        gestionnaireText = new List<TextMeshProUGUI>();
        int i = 0;
        int indexImage = 0;
        int indexColis = 1;
        int indexText = 2;
        foreach (Transform t in gestionnaire.transform) {
            if(i == indexImage) {
                gestionnaireImage.Add(t.GetComponent<Image>());
                indexImage += 3;
            } if (i == indexColis) {
                gestionnaireColis.Add(t.GetComponent<Image>());
                indexColis += 3;
            } else if (i == indexText) {
                gestionnaireText.Add(t.GetComponent<TextMeshProUGUI>());
                indexText += 3;
            }
            i++;
        }
        ResetGestionnaire();
    }
    public void UpdateScore() {
        textScore.text = GameManager.Instance.deliver + "/" + GameManager.Instance.objectiveArray[GameManager.Instance.dayCount];
        textJauge.SetText("tutu");
    }

    public void TimerUpdate(float timer) {
        timer = timer / 60;
        int timerInt = (int)timer;
        textTimer.text = $"{timerInt} : {(int)(Math.Round(timer - timerInt, 2) * 60)}";
    }
    public void DisplayStock(Sprite carImage, int stockMax, int stock) {
        selectedCarImage.sprite = carImage;
        int stockCount = stock;
        foreach(Transform t in stockPanel.transform) {
            if (t.GetComponent<Image>() != null) { 
                if(stockCount-- > 0) t.gameObject.SetActive(true); 
                else t.gameObject.SetActive(false);
            }
            else t.GetComponent<TextMeshProUGUI>().SetText($"{stock}/{stockMax}");
        }
        stockPanel.SetActive(true);
        selectedCarImage.gameObject.SetActive(true);
    }
    public void DisplayStock(Sprite carImage, int stockMax, int stock, int nb) {
        DisplayStock(carImage, stockMax, stock);
        textJauge.GetComponent<TextMeshProUGUI>().SetText(nb.ToString());
        if(nb > 0) textJauge.GetComponent<Animator>().SetBool("Positif", false);
        else       textJauge.GetComponent<Animator>().SetBool("Positif",true);
        textJauge.gameObject.SetActive(true);
    }
    public void UpdateCarList() {
        for(int i = 0; i < VehicleCenterManager.Instance.vehicleList.Count; i++) {
            gestionnaireImage[i].gameObject.SetActive(true);
            gestionnaireColis[i].gameObject.SetActive(true);
            gestionnaireText[i].gameObject.SetActive(true);
            gestionnaireImage[i].sprite = VehicleCenterManager.Instance.vehicleList[i].carImage;
            gestionnaireText[i].SetText(
                $"{VehicleCenterManager.Instance.vehicleList[i].actualStock}/" +
                $"{VehicleCenterManager.Instance.vehicleList[i].stockMax}");
        }
    }

    public void ChangeState(GameManager.GameState oldGameState) {
        uiElement[(int)oldGameState].SetActive(false);
        uiElement[(int)GameManager.GameStates].SetActive(true);
        if (GameManager.GameStates == GameManager.GameState.GameOver) {
            if (GameManager.Instance.victory) {
                badGameOver.SetActive(false);
                goodGameOver.SetActive(true);
                goodGameOver.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(
                    $"You helped {GameManager.Instance.deliver} and your objective was {GameManager.Instance.objective}. \n Awesome! You get a new Car!");
            } else {
                goodGameOver.SetActive(false);
                badGameOver.SetActive(true);
                badGameOver.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(
                    $"You helped {GameManager.Instance.deliver} and your objective was {GameManager.Instance.objective}. \n Try again! To get a new Car!");
            }
        }
    }
    public void ResetUI() {
        stockPanel.SetActive(false);
        selectedCarImage.gameObject.SetActive(false);
        ResetGestionnaire();
    }
    public void ResetGestionnaire() {
        foreach(Transform t in gestionnaire.transform) {
            t.gameObject.SetActive(false);
        }
    }
    #region Debug
    #endregion
}
