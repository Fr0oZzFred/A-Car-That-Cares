﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cell : MonoBehaviour{
    public GameObject trail;
    public bool house;
    [HideInInspector] public bool car;
    [System.NonSerialized] public List<Cell> neighbors;
    [System.NonSerialized] public Cell parent;
    [System.NonSerialized] public bool visited;
    void Update(){
        if (car) trail.SetActive(false);
        if (trail.activeInHierarchy == false) car = false;
    }
    public void SetTrail(){trail.SetActive(true);}
    public void OnMouseDown(){if(MouseManager.Instance.isCar){MouseManager.Instance.actualCar.GoTo(this);}}
}
