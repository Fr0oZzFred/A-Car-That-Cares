using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarBehaviour : MonoBehaviour
{
    public MapPathFinding map;
    public static CarBehaviour globalCar;
    public Vector2Int currentCell;
    void Awake(){
        globalCar = this;
    }
    void Update()
    {
        map.map[currentCell.x, currentCell.y].car = true;
    }
    public void GoTo(Cell target){
    }
}
