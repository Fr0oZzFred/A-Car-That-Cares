using System.Collections;
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
    private bool demande = false;
    private string selection;

    RaycastHit hit;

    private void Start()
    {
        MaisonManager.Instance.AddMaison(this);
    }

    void Update()
    {
        if (MouseManager.Instance.selection == null)
        {
            car = null;
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

    private void OnMouseDown()
    {
        if (MouseManager.Instance.isCar == true)
        {
            car = CarBehaviour.globalCar;
        }
    }

    private void Collide()
    {
        if (collide == true)
        {
            selection = hit.transform.gameObject.name;

            if (car != null && selection == car.name && demande == true)
            {
                Debug.Log("give");
                demande = false;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
            }
        }
    }

    public void Demande()
    {
        if (demande == false)
        {
            demande = true;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }
    }
}
