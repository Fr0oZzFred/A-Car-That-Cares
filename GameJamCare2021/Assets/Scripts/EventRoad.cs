using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventRoad : MonoBehaviour{
    public MapPathFinding map;
    List<CellEnzo> road = new List<CellEnzo>();
    float t = 0;
    int action;
    void Start(){
        for (int i = 0; i < 20; i++){
            for(int j = 0; j < 25; j++)
            if (!GetGrid.grid[i, j].IsWall) road.Add(GetGrid.grid[i, j]);
        }
        action = Random.Range((int)GameManager.Instance.timerDay + 10, (int)GameManager.Instance.timerDay + 15);
    }
    private void Update(){
        t += Time.deltaTime;
        if (t > action)
        {
            BlockingRoad();
            t = 0;
            action = Random.Range((int)GameManager.Instance.timerDay + 10, (int)GameManager.Instance.timerDay + 15);
        }
    }
    public void BlockingRoad(){
        CellEnzo eventCell = road[Random.Range(0, road.Count)];
        int end = Random.Range(8, 10);
        float t = 0;
        while(t < end)
        {
            t += Time.deltaTime;
            eventCell.IsWall= true;
        }
        eventCell.IsWall = false;
    }
}
