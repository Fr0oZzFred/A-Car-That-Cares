using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EventRoad : MonoBehaviour{
    public MapPathFinding map;
    List<Cell> road = new List<Cell>();
    void Start(){
        for (int i = 0; i < map.map.Length; i++)
        {
            if (!map.cells[i].house) road.Add(map.cells[i]);
        }
    }
    public void BlockingRoad(){
        Cell eventCell = road[Random.Range(0, road.Count - 1)];
        Debug.Log(eventCell.name);
        int end = Random.Range(8, 10);
        float t = 0;
        while(t < end)
        {
            t += Time.deltaTime;
            eventCell.eventRoad = true;
        }
        Debug.Log(eventCell.eventRoad);
        eventCell.eventRoad = false;

    }
}
