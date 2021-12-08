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
    List<TextMeshProUGUI> gestionnaireText;
    public GameObject stockPanel;
    public Image selectedCar;

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
    private void Start() {
        gestionnaireImage = new List<Image>();
        gestionnaireText = new List<TextMeshProUGUI>();
        int i = 0;
        int indexImage = 0;
        int indexText = 2;
        foreach (Transform t in gestionnaire.transform) {
            if(i == indexImage) {
                gestionnaireImage.Add(t.GetComponent<Image>());
                Debug.Log("image" + i);
                indexImage += 3;
            }else if(i == indexText) {
                gestionnaireText.Add(t.GetComponent<TextMeshProUGUI>());
                indexText += 3;
                Debug.Log("Text" + i);
            }
            i++;
        }
        ResetGestionnaire();
    }
    void UpdateScore() {
        textStock.text = 0.ToString(); // Stock
    }

    public void TimerUpdate(float timer) {
        timer = timer / 60;
        int timerInt = (int)timer;
        textTimer.text = $"{timerInt} : {(int)(Math.Round(timer - timerInt,2)*60)}"; // Timer fix quand 0.01
    }
    public void DisplayStock(Sprite carImage, int stockMax, int stock) {
        selectedCar.sprite = carImage;
        int stockCount = stock;
        foreach(Transform t in stockPanel.transform) {
            if (t.GetComponent<Image>() != null) { 
                if(stockCount-- > 0) t.gameObject.SetActive(true); 
                else t.gameObject.SetActive(false);
            }
            else t.GetComponent<TextMeshProUGUI>().SetText($"{stock}/{stockMax}");
        }
        stockPanel.SetActive(true);
        selectedCar.gameObject.SetActive(true);
    }
    public void UpdateCarList() {
        for(int i = 0; i < VehicleCenterManager.Instance.vehicleList.Count; i++) {
            gestionnaireImage[i].gameObject.SetActive(true);
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
    }

    public void ResetGestionnaire() {
        foreach(Transform t in gestionnaire.transform) {
            t.gameObject.SetActive(false);
        }
    }
    #region Debug
    #endregion
}
