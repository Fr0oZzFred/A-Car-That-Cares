using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCenterManager : MonoBehaviour
{
    [SerializeField]GameObject CarPrefab;
    [SerializeField] List<CarBehaviour> vehicleList;

    public static VehicleCenterManager Instance { get; private set; }

    void Start()
    {
        Instance = this;

        vehicleList = new List<CarBehaviour>();

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
        CarBehaviour script = car.GetComponent<CarBehaviour>();
        vehicleList.Add(script);
    }
}
