using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //public GameObject wallObject;
    public bool IsWall;
    public GameObject choosenGO;
    [System.NonSerialized] public bool visited;
    [System.NonSerialized] public List<Cell> neighbors;
    [System.NonSerialized] public Cell parent;
    [System.NonSerialized] public Node<Cell> node;

    private void Start() {
    }
    public void SetWall(bool wall)
    {
        IsWall = wall;
        //wallObject.SetActive(wall);
    }

    public void SetMaterial(Material mat, bool b)
    {
        if(!IsWall)
        choosenGO.SetActive(b);
        //choosenGO.GetComponent<MeshRenderer>().material = mat;
    }

    private void OnMouseDown()
    {
        if (MouseManager.Instance.selected != null)
        {
            MouseManager.Instance.selected.GoTo(this);
        }
    }
}
