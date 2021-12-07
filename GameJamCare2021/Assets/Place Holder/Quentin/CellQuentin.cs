using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CellQuentin : MonoBehaviour{
    public GameObject trail;
    public bool house;
    [HideInInspector] public bool eventRoad;
    [HideInInspector] public bool car;
    [System.NonSerialized] public List<CellQuentin> neighbors;
    [System.NonSerialized] public CellQuentin parent;
    [System.NonSerialized] public bool visited;
    [System.NonSerialized] public NodeQuentin<CellQuentin> node;
    public void SetTrail(){trail.SetActive(true);}
    public void RemoveTrail(){trail.SetActive(false);}
    public void OnMouseDown(){if(MouseManagerQuentin.Instance.isCar){MouseManagerQuentin.Instance.actualCar.GoTo(this);}}
}
