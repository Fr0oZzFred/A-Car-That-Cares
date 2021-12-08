using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleCenterManager : MonoBehaviour
{
    [SerializeField]GameObject CarPrefab;
    public List<Character> vehicleList;

    public static VehicleCenterManager Instance { get; private set; }

    void Start()
    {
        Instance = this;

        vehicleList = new List<Character>();

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
        Character script = car.GetComponent<Character>();
        vehicleList.Add(script);
        UIManager.Instance.UpdateCarList();
    }
}
