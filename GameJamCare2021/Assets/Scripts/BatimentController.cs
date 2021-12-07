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
    private bool demande;
    private bool houseSelected;
    private string selection;

    RaycastHit hit;

    private void Start()
    {
        MaisonManager.Instance.AddMaison(this);
    }

    void Update()
    {
        if (MouseManagerQuentin.Instance.selection == null)
        {
            car = null;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * rayDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, rayDistance, layers))
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
        if (MouseManagerQuentin.Instance.isCar == true)
        {
            car = CarBehaviour.globalCar;
        }
        if (demande == true)
        {
            houseSelected = true;
        }
    }

    private void Collide()
    {
        if (collide == true)
        {
            selection = hit.transform.gameObject.name;

            if (car != null && selection == car.name && demande == true && houseSelected == true)
            {
                Debug.Log("give");
                demande = false;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                houseSelected = false;
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
