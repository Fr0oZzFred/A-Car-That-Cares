using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventRoad : MonoBehaviour{
    List<Cell> road = new List<Cell>();
    float t = 0;
    int action = 15;
    void Start(){
        for (int i = 0; i < GetGrid.grid.GetLength(0); i++){
            for(int j = 0; j < GetGrid.grid.GetLength(1); j++){
                if (!GetGrid.grid[i, j].IsWall){
                    road.Add(GetGrid.grid[i, j]); 
                }
            }
        }
        //action = Random.Range(10, 15/*(int)GameManager.Instance.timerDay + 10, (int)GameManager.Instance.timerDay + 15*/);
    }
    private void Update(){
        t += Time.deltaTime;
        if (t > action)
        {
            BlockingRoad();
            t = 0;
            action = Random.Range(10, 15);
        }
    }
    public void BlockingRoad(){
        Cell eventCell = road[Random.Range(0, road.Count)];
        int end = Random.Range(8, 10);
        float tAction = 0;
        eventCell.IsBlock = true;
        eventCell.barrier.SetActive(true);
        while (tAction < end)
        {
            tAction += Time.deltaTime;
        }
        eventCell.IsBlock = false;
        eventCell.barrier.SetActive(false);
    }
}
