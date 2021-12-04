using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarBehaviour : MonoBehaviour{
    public MapPathFinding map;
    public static CarBehaviour globalCar;
    public int startPosX, startPosY;
    Cell currentCell;
    List<Cell> goToList = new List<Cell>();
    void Awake(){
        globalCar = this;
        currentCell = map.map[startPosX, startPosY];
        currentCell.car = true;
    }
    void Update(){
        
    }
    public void GoTo(Cell target){
        Cell start = currentCell;
        if (goToList.Count > 0) start = goToList[goToList.Count - 1];

    }
}