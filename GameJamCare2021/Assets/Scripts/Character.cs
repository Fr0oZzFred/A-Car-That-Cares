using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GridPathFinding grid;
    //public GetGrid gridGet;
    //public static CharacterEnzo globalCharacter; //pas faire ca marche mais c po bien
    Cell currentCell;
    List<Cell> goToList;

    Vector3 posA = new Vector3();
    Vector3 posB = new Vector3();
    public GameObject pingCar;

    [SerializeField] CarScriptableObject vehicle;
    public int stockMax { get; private set; }
    public int actualStock { get; private set; }
    float speed;
    public Sprite carImage { get; private set; }
    Material material;

    private void Awake()
    {
        //globalCharacter = this;
        currentCell = GetGrid.grid[13, 10];
        goToList = new List<Cell>();

        stockMax = vehicle.stockMax;
        speed = vehicle.speed;

        carImage = vehicle.carImage;
        material = vehicle.mat;
        actualStock = stockMax;
    }

    public void GoTo(Cell target)
    {
        Cell start = currentCell;
        goToList.Clear();
        goToList.AddRange(grid.PathFind(start, target));
    }

    void Update()
    {
        if (goToList.Count > 0) {
            pingCar.GetComponent<Animator>().SetBool("Moving", true);
            transform.position = Vector3.MoveTowards(
                transform.position,
                goToList[0].transform.position,
                Time.deltaTime * 5);
            if (transform.position == goToList[0].transform.position) {
                currentCell.SetMaterial(material, false,false);
                currentCell = goToList[0];
                goToList.RemoveAt(0);
            }
        } else if (goToList.Count == 0) currentCell.SetMaterial(material, false,true);

        //script direction
        if (posA != null && posA != transform.position)
            posB = posA;
        posA = transform.position;

        Vector3 dir = (posB - posA).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
    }
    private void OnMouseDown()
    {
        if(MouseManager.Instance.selected != null) {
            MouseManager.Instance.selected.pingCar.GetComponent<Animator>().SetBool("Moving", false);
            MouseManager.Instance.selected.pingCar.SetActive(false);
        }
        MouseManager.Instance.selected = this;
        MouseManager.Instance.selectedName = this.gameObject.name;
        MouseManager.Instance.isCar = true;
        pingCar.SetActive(true);
        pingCar.GetComponent<Animator>().SetBool("Moving",false); // à changer pour avoir 1 seul marqueur à la fois
        UIManager.Instance.DisplayStock(carImage,stockMax,actualStock); //Envoyer infos au UIManager
    }
}
