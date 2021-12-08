using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatimentController : MonoBehaviour
{
    public Character car;
    [SerializeField]
    private float rayDistance = 0;
    [SerializeField]
    private LayerMask layers = default;
    private bool collide;
    private bool demande;
    private bool houseSelected;
    public Vector3 direction;

    RaycastHit hit;

    private void Start()
    {
        MaisonManager.Instance.AddMaison(this);
    }

    void Update()
    {
        if (MouseManager.Instance.selected == null)
        {
            car = null;
        }

        Debug.DrawRay(transform.position, transform.TransformDirection(direction) * rayDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, rayDistance, layers))
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
            car = MouseManager.Instance.selected;
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
            if (car != null && demande == true && houseSelected == true && gameObject.tag == "Maison")
            {
                Debug.Log("give");
                demande = false;
                houseSelected = false;
            }
            
            if (car != null && demande == true && houseSelected == true && gameObject.tag == "Church")
            {
                Debug.Log("give");
                demande = false;
                houseSelected = false;
            }

            if (car != null && demande == true && houseSelected == true && gameObject.tag == "School")
            {
                Debug.Log("give");
                demande = false;
                houseSelected = false;
            }

            if (gameObject.tag == "Stockage")
            {

            }
        }
    }

    public void Demande()
    {
        if (demande == false)
        {
            Debug.Log(transform.position);
            demande = true;
        }
    }
}
