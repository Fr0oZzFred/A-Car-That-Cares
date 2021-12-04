using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaisonEnzoTest : MonoBehaviour
{
    void Start()
    {
        MaisonManager.Instance.AddMaison(this);
    }

    public void Demande()
    {
        Debug.Log("Il est l'heure du eubidelice");
    }
}
