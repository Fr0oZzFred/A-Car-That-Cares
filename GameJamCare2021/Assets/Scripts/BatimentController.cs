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
    private bool charging;
    public Vector3 direction;
    public int stock = 0;

    RaycastHit hit;

    private void Start()
    {
        MaisonManager.Instance.AddMaison(this);
        StartCoroutine(Charging());
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

        if (charging == true && stock < 10 && gameObject.tag == "Stockage")
        {
            charging = false;
            stock++;
            StartCoroutine(Charging());
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
                car.ChangeStock(-1);
                demande = false;
                houseSelected = false;
            }
            
            if (car != null && demande == true && houseSelected == true && gameObject.tag == "Church")
            {
                car.ChangeStock(-2);
                demande = false;
                houseSelected = false;
            }

            if (car != null && demande == true && houseSelected == true && gameObject.tag == "School")
            {
                car.ChangeStock(-4);
                demande = false;
                houseSelected = false;
            }

            if (gameObject.tag == "Stockage")
            {
                car = MouseManager.Instance.selected;
                car.ChangeStock(stock);
                stock = 0;
            }
        }
    }

    public void Demande()
    {
        if (demande == false)
        {
            demande = true;
        }
        Debug.Log(transform.position);
    }

    private IEnumerator Charging()
    {
        charging = false;
        yield return new WaitForSeconds(3);
        charging = true;
    }
}
