using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MouseManager : MonoBehaviour{
    private static MouseManager _instance;
    public static MouseManager Instance { get { return _instance; } }
    [System.NonSerialized] public GameObject selection;
    [HideInInspector] public bool isCar;
    [HideInInspector] public CarBehaviour actualCar;
    public EventRoad eventRoad;
    float t = 0;
    int action;
    void Awake(){
        _instance = this;
        action = Random.Range(10, 15);
    }
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            if (selection == hit.transform.gameObject){
                selection = null;
                isCar = false;
            }
            else if (hit.transform.gameObject.name.Contains("Car")){
                selection = hit.transform.gameObject;
                isCar = true;
                actualCar = selection.GetComponent<CarBehaviour>();
            }
            else if (hit.transform.gameObject.name.Contains("Cell") && !hit.transform.gameObject.GetComponent<Cell>().house)
            {
                selection = hit.transform.gameObject;
            }
        }
        t += Time.deltaTime;
        if (t > action){
            eventRoad.BlockingRoad();
            t = 0;
            action = Random.Range(10, 15);
        }
    }
}
