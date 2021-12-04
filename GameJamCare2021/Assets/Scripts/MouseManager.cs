using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MouseManager : MonoBehaviour{
    private static MouseManager _instance;
    public static MouseManager Instance { get { return _instance; } }
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
                Debug.Log(hit.transform.gameObject + "delete");
                isCar = false;
            }
            /*else if (isCar && hit.transform.gameObject == Cell.cell)
            {
                isCar = false;
                actualCar.GoTo(hit.transform.gameObject.GetComponent<Cell>());
                Debug.Log(hit.transform.gameObject);
            }*/
            else {
                selection = hit.transform.gameObject;
                if (selection == CarBehaviour.globalCar){
                    isCar = true;
                    actualCar = selection.GetComponent<CarBehaviour>();
                    Debug.Log(hit.transform.gameObject);
                }
            }
        }
    }
}
