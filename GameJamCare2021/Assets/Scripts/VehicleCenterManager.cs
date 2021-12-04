using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCenterManager : MonoBehaviour
{
    [SerializeField]GameObject CarPrefab;
    [SerializeField] List<GameObject> vehicleList; //mettre en script

    void Start()
    {
        vehicleList = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
            InstanceCar();
    }

    void InstanceCar()
    {
        GameObject car = Instantiate(CarPrefab, transform.position, Quaternion.identity);
        car.gameObject.name = "voiture" + vehicleList.Count;
        // Script script = car.GetComponenet<Script>();
        vehicleList.Add(car);
    }
}
