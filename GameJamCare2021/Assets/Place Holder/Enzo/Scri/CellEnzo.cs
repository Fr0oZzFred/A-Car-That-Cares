using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellEnzo : MonoBehaviour
{
    public GameObject wallObject;
    public bool IsWall { get; set; }
    [System.NonSerialized] public bool visited;
    [System.NonSerialized] public List<CellEnzo> neighbors;
    [System.NonSerialized] public CellEnzo parent;
    [System.NonSerialized] public NodeEnzo<CellEnzo> node;

    public void SetWall(bool wall)
    {
        IsWall = wall;
        wallObject.SetActive(wall);
    }

    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
    }

    private void OnMouseDown()
    {
        CharacterEnzo.globalCharacter.GoTo(this);
    }

}
