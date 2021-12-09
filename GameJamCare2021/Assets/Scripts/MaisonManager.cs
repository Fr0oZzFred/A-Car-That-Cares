using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaisonManager : MonoBehaviour
{
    [SerializeField]List<BatimentController> maisonList;
    [SerializeField] float rndTimer;
    [SerializeField] int rndChoose;


    public static MaisonManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        maisonList = new List<BatimentController>();

        rndTimer = Random.Range(GameManager.Instance.rndTimerMin[GameManager.Instance.dayCount], GameManager.Instance.rndTimerMax[GameManager.Instance.dayCount]); //bouger var avec temps
    }

    void Update()
    {
        if(GameManager.GameStates == GameManager.GameState.InGame) {
            if (rndTimer < 0) {
                rndChoose = Random.Range(0, maisonList.Count);
                maisonList[rndChoose].Demande();
                /*if (maisonList[rndChoose].tag == "Maison"|| maisonList[rndChoose].tag == "Church" || maisonList[rndChoose].tag == "School") {
                    maisonList[rndChoose].Demande();
                }*/
                rndTimer = Random.Range(GameManager.Instance.rndTimerMin[GameManager.Instance.dayCount], GameManager.Instance.rndTimerMax[GameManager.Instance.dayCount]); //bouger var avec temps 
            }
            rndTimer -= Time.deltaTime;
        }
    }

    public void AddMaison(BatimentController maison)
    {
        maisonList.Add(maison);
    }

    public void SetPopUpFalse() {
        if(maisonList.Count>0)
        foreach(BatimentController m in maisonList) {
            m.askPopUp.SetActive(false);
        }
    }
}
