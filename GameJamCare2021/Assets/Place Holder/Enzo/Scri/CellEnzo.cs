using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellEnzo : MonoBehaviour
{
    //public GameObject wallObject;
    public bool IsWall;
    public GameObject choosenGO;
    [System.NonSerialized] public bool visited;
    [System.NonSerialized] public List<CellEnzo> neighbors;
    [System.NonSerialized] public CellEnzo parent;
    [System.NonSerialized] public NodeEnzo<CellEnzo> node;

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
        if (MouseManagerEnzo.Instance.selected != null)
        {
            MouseManagerEnzo.Instance.selected.GoTo(this);
        }
    }
}
