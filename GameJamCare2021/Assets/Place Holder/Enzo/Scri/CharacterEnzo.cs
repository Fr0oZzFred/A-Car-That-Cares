using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnzo : MonoBehaviour
{
    public GridPathFindingEnzo grid;
    //public static CharacterEnzo globalCharacter; //pas faire ca marche mais c po bien
    CellEnzo currentCell;
    List<CellEnzo> goToList;

    [SerializeField] CarScriptableObject vehicle;

    int stock;
    float speed;

    Material material;

    void Start()
    {
        //globalCharacter = this;
        currentCell = grid.grid[0, 0];
        goToList = new List<CellEnzo>();

        stock = vehicle.stock;
        speed = vehicle.speed;

        material = vehicle.mat;
    }

    public void GoTo(CellEnzo target)
    {
        CellEnzo start = currentCell;
        goToList.Clear();
        goToList.AddRange(grid.PathFind(start, target));
    }

    void Update()
    {
        if (goToList.Count > 0)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                goToList[0].transform.position,
                Time.deltaTime * 5);
            if (transform.position == goToList[0].transform.position)
            {
                currentCell = goToList[0];
                goToList.RemoveAt(0);
            }
        }
    }
    private void OnMouseDown()
    {
        MouseManagerEnzo.Instance.selected = this;
        MouseManagerEnzo.Instance.selectedName = this.gameObject.name;
        Debug.Log("Touché");
    }
}
