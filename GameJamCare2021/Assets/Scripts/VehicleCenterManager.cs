using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCenterManager : MonoBehaviour
{
    [SerializeField]GameObject CarPrefab;
    List<GameObject> vehicleList;

    void Start()
    {
        vehicleList = new List<GameObject>();
    }

    void Update()
    {
        
    }

    void InstanceCar()
    {
        GameObject car = Instantiate(CarPrefab, transform.position, Quaternion.identity);

    }
}
