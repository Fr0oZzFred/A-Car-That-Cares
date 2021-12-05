using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManagerEnzo : MonoBehaviour
{
    public CharacterEnzo selected = null;
    public string selectedName = "";

    public static MouseManagerEnzo Instance { get; private set; }
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
