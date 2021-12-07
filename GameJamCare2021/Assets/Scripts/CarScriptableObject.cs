using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car", menuName = "Car")]
public class CarScriptableObject : ScriptableObject
{
    public new string name;

    public int stockMax;
    public float speed;

    public Material mat;
    public Sprite carImage;
}
