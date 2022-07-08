using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float force;
    private void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>() == null) return;
        other.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position) * force, ForceMode.Impulse);
    }
}
