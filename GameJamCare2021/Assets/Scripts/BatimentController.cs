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
    public GameObject askPopUp;
    public GameObject thanksPopUp;

    private void Start()
    {
        MaisonManager.Instance.AddMaison(this);
        StartCoroutine(Charging());
    }

    void Update()
    {
        if(GameManager.GameStates == GameManager.GameState.InGame) {
            /*if (MouseManager.Instance.selected == null) {
                car = null;
            }*/

            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * rayDistance, Color.red);
            if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, rayDistance, layers)) {
                car = hit.collider.GetComponent<Character>();
                if(car != null)
                collide = true;
            } else {
                collide = false;
            }

            if (charging == true && stock < 10 && gameObject.tag == "Stockage") {
                charging = false;
                stock++;
                StartCoroutine(Charging());
                if(!thanksPopUp.isStatic)
                askPopUp.SetActive(true);
            }

            Collide();
        }
    }

    private void OnMouseDown()
    {
        /*if (MouseManager.Instance.isCar == true)
        {
            car = MouseManager.Instance.selected;
        }*/
        if (demande == true)
        {
            houseSelected = true;
        }
    }

    private void Collide()
    {
        if (collide == true) {
            if (car != null && demande == true && gameObject.tag == "Maison" && car.actualStock > 0)
            {
                GameManager.Instance.AddScore(1);
                car.ChangeStock(-1);
                demande = false;
                houseSelected = false;
                StartCoroutine(ThanksPopUp());
            }
            
            if (car != null && demande == true && gameObject.tag == "Church" && car.actualStock > 1)
            {
                GameManager.Instance.AddScore(2);
                car.ChangeStock(-2);
                demande = false;
                houseSelected = false;
                StartCoroutine(ThanksPopUp());
            }

            if (car != null && demande == true && gameObject.tag == "School" && car.actualStock > 3) {
                GameManager.Instance.AddScore(4);
                car.ChangeStock(-4);
                demande = false;
                houseSelected = false;
                StartCoroutine(ThanksPopUp());
            }

            if (gameObject.tag == "Stockage")
            {
                car.ChangeStock(Mathf.Clamp(stock, 0 ,car.stockMax - car.actualStock));
                stock = 0;
                StartCoroutine(ThanksPopUp());
            }
        }
    }

    public void Demande()
    {
        if (demande == false)
        {
            thanksPopUp.SetActive(false);
            demande = true;
            askPopUp.SetActive(true);
        }
    }

    private IEnumerator Charging()
    {
        thanksPopUp.SetActive(false);
        charging = false;
        yield return new WaitForSeconds(3);
        charging = true; 
    }

    private IEnumerator ThanksPopUp() {
        askPopUp.SetActive(false);
        thanksPopUp.SetActive(true);
        yield return new WaitForSeconds(3);
        thanksPopUp.SetActive(false);

    }
}
