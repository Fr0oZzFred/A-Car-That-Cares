﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatimentController : MonoBehaviour
{
    public GameObject car;
    [SerializeField]
    private float rayDistance = 0;
    [SerializeField]
    private LayerMask layers = default;
    private bool collide;

    RaycastHit hit;

    void Update()
    {
        if (MouseManager.Instance.isCar == true)
        {
            car = CarBehaviour.globalCar;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance, layers))
        {
            collide = true;
        }
        else
        {
            collide = false;
        }

        Collide();
    }

    private void Collide()
    {
        if (collide == true)
        {
            string selection = hit.transform.gameObject.name;

            if (selection == car.name)
            {
                Debug.Log("give");
            }
        }
    }
}
