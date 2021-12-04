using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof (Animator))]
public class GestionnaireScript : MonoBehaviour {
    Animator anim;
    private void Start() {
        anim = gameObject.GetComponent<Animator>();
    }

    public void ChangeState() {
        anim.SetBool("Opened",!anim.GetBool("Opened"));
    }
}
