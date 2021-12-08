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

        rndTimer = Random.Range(1, 4); //bouger var avec temps
    }

    void Update()
    {
        if(GameManager.GameStates == GameManager.GameState.InGame) {
            if (rndTimer < 0) {
                rndChoose = Random.Range(0, maisonList.Count);
                if (maisonList[rndChoose].tag == "Maison") {
                    maisonList[rndChoose].Demande();
                }
                rndTimer = Random.Range(5, 20); //bouger var avec temps
            }
            rndTimer -= Time.deltaTime;
        }
    }

    public void AddMaison(BatimentController maison)
    {
        maisonList.Add(maison);
    }
}
