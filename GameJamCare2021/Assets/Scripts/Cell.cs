using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    //public GameObject wallObject;
    public bool IsWall;
    public GameObject choosenGO;
    public GameObject pingGO;
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

    public void SetMaterial(Material mat, bool chosen, bool ping)
    {
        if (ping && !IsWall) {
            choosenGO.SetActive(false);
            pingGO.SetActive(chosen);
            pingGO.GetComponent<MeshRenderer>().material = mat;
        }
        else if (!IsWall) {
            choosenGO.SetActive(chosen);
            choosenGO.GetComponent<MeshRenderer>().material = mat;
        }
    }

    private void OnMouseDown()
    {
        if (MouseManager.Instance.selected != null)
        {
            MouseManager.Instance.selected.GoTo(this);
        }
    }
}
