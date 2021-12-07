using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public Character selected = null;
    public string selectedName = "";

    public static MouseManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        //if (selected != null && Input.GetMouseButtonDown(0))
            //selected = null;
    }
}
