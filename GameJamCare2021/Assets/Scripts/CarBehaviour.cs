using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarBehaviour : MonoBehaviour{
    public MapPathFinding map;
    public static GameObject globalCar;
    public Vector2Int startPos;
    Cell currentCell;
    List<Cell> goToList;
    void Start(){
        globalCar = this.gameObject;
        currentCell = map.map[startPos.x, startPos.y];
        currentCell.car = true;
        goToList = new List<Cell>();
    }
    void Update(){
        if (goToList.Count > 0){
            transform.position = Vector3.MoveTowards(transform.position, goToList[0].transform.position, Time.deltaTime * 2);
            if (transform.position == goToList[0].transform.position){
                currentCell.car = false;
                currentCell = goToList[0];
                currentCell.car = true;
                goToList.RemoveAt(0);
            }
        }
    }
    public void GoTo(Cell target){
        Cell start = currentCell;
        if (goToList.Count > 0) start = goToList[goToList.Count - 1];
        goToList.AddRange(map.PathFind(start, target));
    }
}