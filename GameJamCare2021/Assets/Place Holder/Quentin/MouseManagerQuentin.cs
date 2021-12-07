using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MouseManagerQuentin : MonoBehaviour{
    private static MouseManagerQuentin _instance;
    public static MouseManagerQuentin Instance { get { return _instance; } }
    [System.NonSerialized] public GameObject selection;
    [HideInInspector] public bool isCar;
    [HideInInspector] public CarBehaviour actualCar;
    void Awake(){
        _instance = this;
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
            else if (hit.transform.gameObject.name.Contains("Cell") && !hit.transform.gameObject.GetComponent<CellQuentin>().house)
            {
                selection = hit.transform.gameObject;
            }
        }
    }
}
