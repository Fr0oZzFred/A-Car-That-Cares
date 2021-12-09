using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCenterManager : MonoBehaviour
{
    [SerializeField]List<GameObject> CarPrefab;
    public List<Character> vehicleList;

    bool isActive = false;

    [SerializeField] List<GameObject> carsMenu;

    public static VehicleCenterManager Instance { get; private set; }

    void Start()
    {
        Instance = this;

        vehicleList = new List<Character>();

    }

    void Update()
    {
        if(GetGrid.grid != null && !isActive) { 
            ActivateCarMenu();
        }
    }
    public void ClearVehicle() {
        for(int i = 0; i < vehicleList.Count; i++) {
            Destroy(vehicleList[i].gameObject);
        }
        vehicleList.Clear();
    }
    public void InstanceCar(int nbOfCar)
    {
        for(int i = 0; i < nbOfCar; i++) {
            GameObject car = Instantiate(CarPrefab[i], transform.position, Quaternion.identity);
            car.gameObject.name = "voiture" + vehicleList.Count;
            Character script = car.GetComponent<Character>();
            vehicleList.Add(script);
        }
        UIManager.Instance.UpdateCarList();
    }

    public void ActivateCarMenu()
    {
        for(int i = 0; i < carsMenu.Count; i++)
        {
            carsMenu[i].SetActive(true);
        }
        isActive = true;
    }

    public void DectivateCarMenu()
    {
        for (int i = 0; i < carsMenu.Count; i++)
        {
            carsMenu[i].SetActive(false);
        }
    }
}
